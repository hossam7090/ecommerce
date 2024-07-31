using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string ISBN { get; set; } = string.Empty;
        [Required]
        [Range(0,1000)]
        [Display(Name ="List price")]
        public double ListPrice { get; set; }

        [Required]
        [Range(0, 1000)]
        [Display(Name = "price for 1-50")]
        public double Price { get; set; }




        [Required]
        [Range(0, 1000)]
        [Display(Name = "price For +50")]
        public double Price50 { get; set; }


        [Required]
        [Range(0, 1000)]
        [Display(Name = "price For +100")]
        public double Price100 { get; set; }

    }
}
