using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace GreenBite.Domain.Entities
{
    public class Salad
    {
        [HiddenInput(DisplayValue = false)]
        public int SaladID { get; set; }

        [Required(ErrorMessage = "Please enter a salad name")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage =
        "Please enter a positive price")]
        public decimal Price { get; set; }

        public byte[] ImageData { get; set; }

        public string ImageMimeType { get; set; }

    }
}
