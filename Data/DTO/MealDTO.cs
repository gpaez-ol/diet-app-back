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
        public string Preparation { get; set; }
        public List<string> MealIngredients { get; set; }
    }
    public class MealIngredientDTO 
    {
        public Guid IngredientId {get;set;}
        public int Amount {get;set;}
        public string Notes { get; set; }
    }
    public class MealCreateDTO 
    {
        public string Name { get; set; }
        public double Kilocalories { get; set; }
        [MaxLength(500)]
        public string Preparation { get; set; }
        public List<MealIngredientDTO > MealIngredients {get; set;}
    }
    public class MealIngredientDetailDTO
    {
        public Guid IngredientId {get;set;}
        public string Name {get;set;}
        public int Amount {get;set;}
        public string Notes { get; set; }
    }
    public class MealDTO
    {
        public string Name { get; set; }
        public double Kilocalories { get; set; } 
        [MaxLength(500)]
        public string Preparation { get; set; }
        public string ImageRef { get; set; }
        public ICollection<MealIngredientDetailDTO> MealIngredients { get; set; }
    }
}