using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StockSystem.Models
{
    public class SubCategory
    {
        [Key]
        public int SubCategoryId { get; set; }
        [Display(Name ="Sub Category")]
        public string Description { get; set; }
        public Category Category { get; set; }
        [Display(Name= "Category")]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
    }
}