using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace OpenWebTech.Contacts.Web.Security
{
    public class JwtAuthorizeAttribute : Microsoft.AspNetCore.Authorization.AuthorizeAttribute
    {
        public JwtAuthorizeAttribute()
        {
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}
