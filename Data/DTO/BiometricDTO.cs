using System;
using AlgoFit.Data.Models;

namespace AlgoFit.Data.DTO
{
    public class BiometricItemDTO
    {
        public Guid Id {get;set;}
        public string Date { get; set; }

        public double BodyMassIndex { get; set;}
    }
    public class BiometricCreateDTO 
    {
        public double Weight { get; set; }
        public double Height { get; set; }
    }
    public class BiometricDTO
    {
        public double Weight { get; set; }
        public double Height { get; set; }
        public DateTime Date { get; set; }
         public User User { get; set; }
    }
}