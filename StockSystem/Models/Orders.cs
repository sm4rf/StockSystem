using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StockSystem.Models
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("Products")]
        [Display(Name = "Product ID")]
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public Products Products { get; set; }
    }
}