using Framework.Web.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Framework.Web.Security.Authentication
{
    /// <summary>
    /// Encapsulates authentication and authorization behavior.
    /// </summary>
    public class FWAuthProvider : IFWAuthProvider
    {
        // For cookie bases auth, install Microsoft.AspNetCore.Authentication.Cookies pakage.

        /// <inheritdoc />
        public string AuthenticationType { get; set; } = "Cookie";

        /// <summary>
        /// Creates a new instance of the <see cref="FWAuthProvider"/>.
        /// </summary>
        /// <param name="memoryCache">The memory cache object.</param>
        /// <param name="httpContextAccessor">The httpcontext factory.</param>
        public FWAuthProvider(IMemoryCache memoryCache, IHttpContextAccessor httpContextAccessor)
        {
            _memoryCache = memoryCache;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc />
        public ClaimsIdentity AuthenticateUser(IFWUser user, string inputPassword)
        {   
            if (user == null)
                throw new FWAuthenticationException();

            var hashedPassword = FWHashProvider.HashString(inputPassword, user.Salt).Result;
            if (hashedPassword != user.Password)
            {   
                throw new FWAuthenticationException();
            }
            else
            {
                // Clears the recaptcha cache.
                var attemptsCacheKey = $"LoginAttempt_{_httpContextAccessor.HttpContext.Connection.RemoteIpAddress}";
                _memoryCache.Remove(attemptsCacheKey);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var nameParts = GetNameParts(user.FullName);
            if (nameParts.FirstName != null)
            {
                claims.Add(new Claim(ClaimTypes.GivenName, nameParts.FirstName));
                claims.Add(new Claim(ClaimTypes.Surname, nameParts.LastName));
            }

            foreach (var token in user.Tokens)
            {
                claims.Add(new Claim(token.Key, token.Value));
            }

            return new ClaimsIdentity(claims, AuthenticationType);
        }

        private (string FirstName, string LastName) GetNameParts(string fullName)
        {
            if (fullName != null)
            {
                var nameParts = fullName.Split(' ');
                var firstName = nameParts.First();
                var lastName = (nameParts.Length > 1) ? nameParts.Last() : string.Empty;
                return (firstName, lastName);
            }
            return (null, null);
        }

        private IMemoryCache _memoryCache;
        private IHttpContextAccessor _httpContextAccessor;
    }
}
