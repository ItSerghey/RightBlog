using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using RightBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RightBlog.Core.Data
{
    public class ArticleContext : MongoContext
    {
        public ArticleContext(IConfiguration configuration) : base(configuration)
        {

        }

        // appeal to the collection of Articles
        public IMongoCollection<Article> Articles
        {
            get { return database.GetCollection<Article>("Articles"); }
        }


        public async Task<List<Article>> GetArticles(int? datePublish, string title)
        {
            // filter builder
            var builder = new FilterDefinitionBuilder<Article>();
            var filter = builder.Empty; // filter for all articles
            // filter by name
            if (!String.IsNullOrWhiteSpace(title))
            {
                filter = filter & builder.Regex("Title", new BsonRegularExpression(title));
            }
            if (datePublish.HasValue)  // filter by date publish
            {
                filter = filter & builder.Eq("DatePublish", datePublish.Value);
            }

            return await Articles.Find(filter).ToListAsync();
        }



        // get article by id
        public async Task<Article> GetArticle(string id)
        {
            return await Articles.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }

        public async Task<string> Create(Article article)
        {
            await Articles.InsertOneAsync(article);
            return article.Id;
        }

        public async Task Update(Article c)
        {
            await Articles.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(c.Id)), c);
        }

        public async Task Remove(string id)
        {
            await Articles.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }

        public async Task<byte[]> GetImage(string id)
        {
            return await gridFS.DownloadAsBytesAsync(new ObjectId(id));
        }

        public async Task StoreImage(string id, byte[] image, string imageName)
        {
            Article article = await GetArticle(id);
            if (article.HasImage())
            {
                // if the picture has already been attached, delete it
                await gridFS.DeleteAsync(new ObjectId(article.ImageId));
            }

            ObjectId imageId = await gridFS.UploadFromBytesAsync(imageName, image);

            // update document data
            article.ImageId = imageId.ToString();
            var filter = Builders<Article>.Filter.Eq("_id", new ObjectId(article.Id));
            var update = Builders<Article>.Update.Set("ImageId", article.ImageId);
            await Articles.UpdateOneAsync(filter, update);
        }
    }
}
