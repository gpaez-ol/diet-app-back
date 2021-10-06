using AlgoFit.Data.BaseEntities;

namespace AlgoFit.Data.Models
{
    public class UserCredential : Entity
    {
        //TODO: Hash Password before saving
        /// <summary> 
        /// Password for the user
        /// </summary>
        public string Password { get; set; } 
    }
}