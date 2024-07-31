using bulky.Models.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bullky.Models.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]

        public string Description {  get; set; } = string.Empty;
        [Required]
        public string Author {  get; set; } = string.Empty;
        [Required]
        [Range(0,1000)]
        [Display(Name ="List price")]
        public double ListPrice { get; set; }

        [Required]
        [Range(0, 1000)]
        [Display(Name = "price for 1-50")]
        public double Price { get; set; }


 
        public int CategoryId {  get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
        [ValidateNever]
        public String ImageUrl { get; set; } = string.Empty;

    }
}
