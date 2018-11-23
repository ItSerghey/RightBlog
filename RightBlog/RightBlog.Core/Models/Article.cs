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
        public DateTime DatePublish { get; set; }

        public string ImageId { get; set; }

        [Display(Name = "Тип статьи")]
        public TypeArticle TypeArticle { get; set; }

        public bool HasImage()
        {
            return !String.IsNullOrWhiteSpace(ImageId);
        }
    }


    public class ArticleFilter
    {
        public string Title { get; set; }
        public int? Year { get; set; }
    }

    public class ArticleList
    {
        public List<Article> Articles { get; set; }
        public ArticleFilter Filter { get; set; }
    }

    public enum TypeArticle
    {
        TechnoCrunch = 1,
        Life = 2,
        GamblingAddiction = 3
    }
}
