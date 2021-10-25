using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AlgoFit.Data.Models;

namespace AlgoFit.Data.DTO
{
    public class DietItemDTO
    {
        public Guid Id {get;set;}
        public string Name { get; set; } // TODO; Add category
        public string ImageRef{ get; set; }
        public List<Guid> CategoryIds {get;set;}
    }
    public class DietMealDTO 
    {
        public Guid Id { get; set;}
        public int MealNumber {get; set;}
    }
    public class DietCreateDTO 
    {
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public DietType Type { get; set; }
        public List<Guid> CategoryIds {get; set;}
        public List<DietMealDTO > DietMeals {get; set;}
    }
    public class DietDTO
    {
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public ICollection<Guid> Categories { get; set; }
        public ICollection<MealItemDTO> Meals { get; set; }
        public DietType Type { get; set; }
        public string ImageRef{ get; set; }
    }
}