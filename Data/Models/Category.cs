using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using AlgoFit.Data.BaseEntities;

namespace AlgoFit.Data.Models
{
    public class Category : Entity
    {

        /// <summary> 
        ///Name of the Category
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary> 
        /// Description of the Category
        /// </summary>
        [MaxLength(500)]
        public string Description { get; set; }
    }
}
