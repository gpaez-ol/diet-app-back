using System.Collections.Generic;

namespace AlgoFit.Data.DTO
{

    public class DashboardDTO
    {
        public ICollection<SimpleBiometricDTO> BiometricHistory { get; set;}
        public BiometricDashboardDTO LatestBiometrics { get; set; }
    }
}