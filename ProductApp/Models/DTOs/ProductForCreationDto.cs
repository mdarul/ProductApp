using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApp.Models.DTOs
{
    public class ProductForCreationDto
    {
        [Required]
        public string Name { get; set; }

        public float Cost { get; set; }

        [Required]
        public string Category { get; set; }
    }
}
