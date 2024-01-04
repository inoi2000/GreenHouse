using GreenHouse.Domain.Entities;

namespace GreenHouse.Domain.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Admin admin);
    }
}
