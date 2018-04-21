using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CarInventorySystem.Models
{
    public class CarsModel
    {
        public int Id { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public decimal Price { get; set; }
        public bool New { get; set; }
        public string UName { get; set; }
    }
}