using Orion.Core.Entities.Users;

namespace Orion.Application.Services
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user);
    }
}
