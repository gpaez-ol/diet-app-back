using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using AlgoFit.Data.BaseEntities;

namespace AlgoFit.Data.Models
{
    public class Meal : Entity
    {

        /// <summary> 
        ///Name of the Meal
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary> 
        /// Preparation Description of the Meal
        /// </summary>
        [MaxLength(500)]
        public string Preparation { get; set; }

        /// <summary> 
        /// Kilocalories of the Meal
        /// </summary>
        public double Kilocalories { get; set; }


        /// <summary>
        /// AWS Bucket reference to the Meal Image
        /// </summary>
        [MaxLength(500)]
        public string ImageRef { get; set; }

        /// <summary>
        /// The Ingredients for the given Meal
        /// </summary>
        public ICollection<MealIngredient> Ingredients { get; set; }
    }
}
