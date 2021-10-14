using System;

namespace AlgoFit.Data.DTO
{
    public class IngredientDTO
    {
        public Guid Id {get;set;}
        public string Name { get; set; }
    }
    public class IngredientCreateDTO
    {
        public string Name { get; set; }
    }
}