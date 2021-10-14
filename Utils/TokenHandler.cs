using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using AlgoFit.BaseEntities;

namespace AlgoFit.Security.Utils
{
    public static class TokenHandler
    {
        private static TokenOptions _tokenOptions;
        private static CookieOptions _cookieOptions;

        public static void SetTokenOptions(TokenOptions tokenOptions)
        {
            _tokenOptions = tokenOptions;
        }

        public static void SetCookieOptions(CookieOptions cookieOptions)
        {
            _cookieOptions = cookieOptions;
        }

        public static void SetJWTCookie(HttpResponse response, List<Claim> claims = null)
        {
            string token = GenerateJWT(claims);
            AppendTokenToCookie(token, response);
        }

        private static string GenerateJWT(List<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_tokenOptions.Issuer,
              _tokenOptions.Issuer,
              claims,
              expires: DateTime.Now.AddMinutes(_tokenOptions.Expiration),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static void AppendTokenToCookie(string token, HttpResponse response)
        {
            response.Cookies.Append("Token", token, _cookieOptions);
        }

        public static void RemoveTokenToCookie(HttpResponse response)
        {
            response.Cookies.Delete("Token", _cookieOptions);
        }

        public static string GetClaimValue(System.Collections.Generic.IEnumerable<System.Security.Claims.Claim> claims, string claimType)
        {
            return claims.Where(claim => claim.Type == claimType).FirstOrDefault().Value;
        }
    }
}
