using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using AlgoFit.Data.BaseEntities;

namespace AlgoFit.Data.Models
{
    public class Ingredient : Entity
    {

        /// <summary> 
        ///Name of the Category
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
