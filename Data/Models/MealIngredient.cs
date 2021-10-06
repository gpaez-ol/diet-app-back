using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using AlgoFit.Data.BaseEntities;

namespace AlgoFit.Data.Models
{
    public class MealIngredient
    {
        /// <summary> 
        // Amount of the Ingredients
        /// </summary>
        [Required]
        public int Amount { get; set; }
        /// <summary> 
        /// Notes on the Meal Ingredients
        /// </summary>
        [MaxLength(50)]
        public string Notes { get; set; }

        /// <summary>
        /// Id of the associated meal
        /// </summary>
        public Guid? MealId { get; set; }
        public Meal Meal { get; set; }

        /// <summary>
        /// Id of the associated ingredient
        /// </summary>
        public Guid IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
