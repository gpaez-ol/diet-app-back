using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using AlgoFit.Data.BaseEntities;

namespace AlgoFit.Data.Models
{
    public class User : Entity
    {

        /// <summary>
        /// AWS Bucket reference to Avatar image
        /// </summary>
        [MaxLength(500)]
        public string Avatar { get; set; }

        /// <summary> 
        /// The first name of the user
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        /// <summary> 
        /// The last name of the user
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        /// <summary>
        /// Email address of the user
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        /// <summary>
        /// Phone number of user
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Type of user
        /// </summary>
        [Required]
        public UserType Type { get; set; }
        /// <summary>
        /// Id of the associated credential
        /// </summary>
        public Guid UserCredentialId { get; set; }
        public UserCredential UserCredential { get; set; }
        /// <summary>
        /// The Diets for the given user
        /// </summary>
        public Guid? DietId { get; set;}
        public Diet Diet { get; set;}
    }

    public enum UserType
    {
        [EnumMember(Value = "Customer")]
        Customer,
        [EnumMember(Value = "Admin")]
        Admin
    }
}
