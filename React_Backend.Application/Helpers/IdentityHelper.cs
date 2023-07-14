using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace React_Backend.Application.Helpers
{
    public class IdentityHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IdentityHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        

        public string UserId
        {
            get
            {
                var identity = _httpContextAccessor.HttpContext.User.Claims;
                var userId = identity.FirstOrDefault(x => x.Type == "Id").Value;
                if (userId != null)
                {
                    return userId;
                }
                return string.Empty;
            }
        }

        public string UserEmail
        {
            get
            {
                var identity = _httpContextAccessor.HttpContext.User.Claims;
                var userId = identity.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
                if (userId != null)
                {
                    return userId;
                }
                return string.Empty;
            }
        }
        public string Role
        {
            get
            {
                var identity = _httpContextAccessor.HttpContext.User.Claims;
                var Role = identity.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
                return Role;
            }
        }

    }
}
