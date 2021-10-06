using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using AlgoFit.Data.BaseEntities;

namespace AlgoFit.Data.Models
{
    public class UserDiet
    {
        /// <summary>
        /// Id of the associated diet
        /// </summary>
        public Guid? DietId { get; set; }
        public Diet Diet { get; set; }

        /// <summary>
        /// Id of the associated User
        /// </summary>
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
