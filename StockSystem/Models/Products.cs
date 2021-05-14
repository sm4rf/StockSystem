using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StockSystem.Models
{
    public class Products
    {
        [Key]
        [Display(Name = "Product ID")]
        public int ProductId { get; set; }
        [Display(Name = "Added By")]
        public string AddedBy { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [ForeignKey("SubCategory")]
        public int SubCategoryId { get; set; }
        [Display(Name = "Cost (£)")]
        public float Cost { get; set; }
        [Display(Name = "Resale Price (£)")]
        public float Price { get; set; }
        public string UnitSize { get; set; }
        public int Quantity { get; set; }
        
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }

        public Category Category { get; set; }
        public SubCategory SubCategory { get; set; }
        public Supplier Supplier { get; set; }
    }
}