using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using AlgoFit.Data.BaseEntities;

namespace AlgoFit.Data.Models
{
    public class DietMeal : Entity
    {
        /// <summary> 
        /// Number of the Meal in the Diet
        /// </summary>
        [Required]
        public int MealNumber { get; set; }
        /// <summary> 
        /// Was the meal eaten
        /// </summary>
        public bool Eaten { get; set; }

        /// <summary>
        /// Id of the associated diet
        /// </summary>
        public Guid DietId { get; set; }
        public Diet Diet { get; set; }

        /// <summary>
        /// Id of the associated meal
        /// </summary>
        public Guid? MealId { get; set; }
        public Meal Meal { get; set; }
    }
}
