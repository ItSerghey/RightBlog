using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using RightBlog.Core.Models;
namespace RightBlog.Core.Data
{
    public class CategoryContext : MongoContext
    {
        public CategoryContext(IConfiguration configuration) : base(configuration)
        {

        }


        // appeal to the collection of Categories
        public IMongoCollection<Category> Categories
        {
            get { return database.GetCollection<Category>("Categories"); }
        }

        public async Task<List<Category>> GetCategories(bool? display)
        {
            // filter builder
            var builder = new FilterDefinitionBuilder<Category>();
            var filter = builder.Empty; // filter for all articles

            if (display.HasValue)
            {
                filter = filter & builder.Eq("Display", display.Value);
            }

           
            return await Categories.Find(filter).ToListAsync();
        }

        // get article by id
        public async Task<Category> GetCategory(string id)
        {
            return await Categories.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }


        public async Task Create(Category category)
        {
            await Categories.InsertOneAsync(category);
        }

        public async Task Update(Category category)
        {
            await Categories.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(category.Id)), category);
        }

        public async Task Remove(string id)
        {
            await Categories.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }


    }
}
