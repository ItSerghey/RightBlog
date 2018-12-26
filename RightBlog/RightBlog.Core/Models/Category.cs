using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RightBlog.Core.Models
{
    public class Category
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Лейбл")]
        public string Label { get; set; }

        [Display(Name = "SEO Url")]
        public string Url { get; set; }

        [Display(Name = "Порядок в меню")]
        public int Sort { get; set; }

        [Display(Name = "Отображать")]
        public bool Display { get; set; }

        public bool Selected { get; set; }

    }
}
