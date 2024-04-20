using Microsoft.AspNetCore.Identity;

namespace Usermanagement_API.Repositories.Interface
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
