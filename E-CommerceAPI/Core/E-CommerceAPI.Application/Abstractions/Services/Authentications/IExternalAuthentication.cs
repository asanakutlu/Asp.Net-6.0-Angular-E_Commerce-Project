using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Abstractions.Services.Authentications
{
    public interface IExternalAuthentication
    {
        Task<DTOs.Token> FacebookLoginAsync(string authToken, int accesTokenLifeTime);
        Task<DTOs.Token> GoogleLoginAsync(string idToken, int accesTokenLifeTime);
        Task TwitterLoginAsync();
    }
}
