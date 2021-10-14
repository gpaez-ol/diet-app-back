using AlgoFit.BaseEntities;
using Microsoft.AspNetCore.Http;

namespace AlgoFit.Security.Utils
{
    public static class Authorization
    {
        public static TokenOptions CreateTokenOptions(TokenOptions options)
        {
            return new TokenOptions(options.Issuer, options.Key, options.Expiration);
        }

        public static CookieOptions CreateCookieOptions(CookieOptions options)
        {
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Domain = options.Domain;
            cookieOptions.SameSite = options.SameSite;
            cookieOptions.HttpOnly = options.HttpOnly;
            cookieOptions.Secure = options.Secure;
            return cookieOptions;
        }
    }
}
