using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StockSystem.Models
{
    public class Category
    {
        [Key]
        [Display(Name = "Category ID")]
        public int CategoryId { get; set; }
        [Display(Name = "Category")]
        public string Description { get; set; }


        public List<SubCategory> SubCategories { get; set; }
    }
}