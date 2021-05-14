using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StockSystem.Models
{
    public class Employees
    {
        [Key]
        public int EmployeeId { get; set; }
        [Display(Name = "Employee ID")]
        public string UniqueId { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Surname")]
        public string SurName { get; set; }
        public position Position { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string Hours { get; set; }
        [Display(Name = "Wage P/H")]
        public string WagePH { get; set; }
        public string Pay { get; set; }
        [Display(Name = "Tax Code")]
        public string TaxCode { get; set; }
        public string TaxC { get; set; }


        public string FilePath { get; set; }

    }
}