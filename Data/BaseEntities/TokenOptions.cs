
using Microsoft.Extensions.Configuration;

namespace AlgoFit.BaseEntities

{
    public class TokenOptions
    {
        private readonly string _Issuer;
        private readonly string _Key;
        private readonly double _Expiration;
        public TokenOptions(string Issuer, string Key, double Expiration)
        {
            _Issuer = Issuer;
            _Key = Key;
            _Expiration = Expiration;
        }

        public string Issuer
        {
            get { return _Issuer; }
        }

        public string Key
        {
            get { return _Key; }
        }

        public double Expiration
        {
            get { return _Expiration; }
        }
    }
}
