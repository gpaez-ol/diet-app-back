using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using AlgoFit.Data.BaseEntities;

namespace AlgoFit.Data.Models
{
    public class Biometric : Entity
    {

        /// <summary> 
        /// Biometric Weight
        /// </summary>
        [Required]
        public double Weight { get; set; }
        /// <summary> 
        /// Biometric Height
        /// </summary>
        [Required]
        public double Height { get; set; }
        /// <summary> 
        /// Fat Percentage
        /// </summary>
        public double FatIndex { get; set; }

        /// <summary> 
        /// Date the Measurement was Taken
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary> 
        /// Id of the user who owns this biometric reading
        /// </summary>
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
