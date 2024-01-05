using GreenHouse.Domain.Entities;
using GreenHouse.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GreenHouse.Data.EntityFramework.Reposirories
{
    public class AdminRepositoryEf : EfRepository<Admin>, IAdminRepository
    {
        public AdminRepositoryEf(AppDbContext dbContext) : base(dbContext) { }

        public async Task<Admin> GetAdminByLogin(string login, CancellationToken token)
        {
            if (string.IsNullOrEmpty(login)) { throw new ArgumentException($"\"{nameof(login)}\" не может быть неопределенным или пустым.", nameof(login)); }

            return await Entities.SingleAsync(e => e.Login == login, token);
        }

        public async Task<Admin?> FindAdminByLogin(string login, CancellationToken token)
        {
            if (string.IsNullOrEmpty(login)) { throw new ArgumentException($"\"{nameof(login)}\" не может быть неопределенным или пустым.", nameof(login)); }

            return await Entities.SingleOrDefaultAsync(e => e.Login == login, token);
        }

        public override async Task Update(Admin entity, CancellationToken token)
        {
            await base.Update(entity, token);
        }
    }
}
