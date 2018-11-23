using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RightBlog.Core.Models
{
    public class TestModel
    {
        [Key]
        public Guid Id { get; set; }
        public int MyProperty { get; set; }

        public string MyProperty2 { get; set; }
    }
}
