using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StockSystem.Models
{
    public class Supplier
    {
        [Key]
        [Display(Name = "Supplier ID")]
        public int SupplierId { get; set; }
        [Display(Name = "Supplier Name")]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Town { get; set; }
        [Display(Name = "Postcode")]
        public string PostCode { get; set; }
        [Display(Name = "Contact Number")]
        public string ContactNo { get; set; }

    }
}