using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using AlgoFit.Data.BaseEntities;

namespace AlgoFit.Data.Models
{
    public class Diet : Entity
    {

        /// <summary> 
        ///Name of the Diet
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary> 
        /// Description of the Diet
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

         /// <summary>
        /// Type of the Diet
        /// </summary>
        public DietType Type { get; set; }

        /// <summary>
        /// The Categories for the Diet
        /// </summary>
        public ICollection<DietCategory> Categories { get; set; }

        /// <summary>
        /// The Categories for the Diet
        /// </summary>
        public ICollection<DietMeal> Meals { get; set; }
    }

    public enum DietType
    {
        [EnumMember(Value = "Customer")]
        Customer,
        [EnumMember(Value = "Template")]
        Template
    }
}
