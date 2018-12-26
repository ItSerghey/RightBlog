using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RightBlog.Core.Models
{
    public class Article
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Display(Name = "Название статьи")]
        public string Title { get; set; }

        [Display(Name = "Описание статьи")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Статья")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        [Display(Name = "Год выпуска")]
        [DataType(DataType.DateTime)]
        public DateTime DatePublish { get; set; }

        public string ImageId { get; set; }

        public string CategoryId { get; set; }

        public bool HasImage()
        {
            return !String.IsNullOrWhiteSpace(ImageId);
        }

        [Display(Name = "Автор")]
        public string Author { get; set; }

        [Display(Name = "СЕО ссылка")]
        public string UrlSeo { get; set; }

        [Display(Name = "Метатег название")]
        public string MetaTitle { get; set; }

        [Display(Name = "Метатег описание")]
        [DataType(DataType.MultilineText)]
        public string MetaDescription { get; set; }

        [Display(Name = "Опубликовано")]
        public bool IsPublished { get; set; }

        public Category Badge { get; set; }
    }


    public class ArticleFilter
    {

        public string Title { get; set; }
        public bool? IsPublished { get; set; }
        public int? Year { get; set; }
    }

    public class ArticleList
    {
        public List<Article> Articles { get; set; }
        public ArticleFilter Filter { get; set; }
    }

}
