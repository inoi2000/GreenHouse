using GreenHouse.Domain.Entities;

namespace GreenHouse.Domain.Interfaces
{
    public interface IAdminRepository : IRepository<Admin>
    {
        Task<Admin> GetAdminByLogin(string login, CancellationToken token);
        Task<Admin?> FindAdminByLogin(string login, CancellationToken token);
    }
}
