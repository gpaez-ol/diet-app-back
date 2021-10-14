using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlgoFit.Data.DTO
{
    public class MealItemDTO
    {
        public Guid Id {get;set;}
        public string Name { get; set; }
        public double Kilocalories { get; set; }
    }
    public class MealCreateDTO 
    {
        public string Name { get; set; }
        public double Kilocalories { get; set; }
        [MaxLength(500)]
        public string Preparation { get; set; }
    }
    public class MealDTO
    {
        public string Name { get; set; }
        public double Kilocalories { get; set; }
        [MaxLength(500)]
        public string Preparation { get; set; }
        public string ImageRef { get; set; }
        public ICollection<Guid> Ingredients { get; set; }
    }
}