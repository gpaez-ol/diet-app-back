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
        public double FatIndex { get; set; }
    }
    public class BiometricDTO
    {
        public double Weight { get; set; }
        public double Height { get; set; }
        public DateTime Date { get; set; }
        public double FatIndex { get; set; }
    }
    public class BiometricDashboardDTO
    {
        public double Weight { get; set; }
        public double Height { get; set; }
        public double CaloriesConsumed { get; set; }
        public double FatIndex { get; set; }
    }
    public class SimpleBiometricDTO
    {
        public double Weight { get; set; }
        public double Height { get; set; }
        public DateTime Date { get; set; }
    }
}