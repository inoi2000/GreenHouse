using GreenHouse.Domain.Entities;
using GreenHouse.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GreenHouse.Data.EntityFramework.Reposirories
{
    public class CityRepositoryEf : EfRepository<City>, ICityRepository
    {
        public CityRepositoryEf(AppDbContext dbContext) : base(dbContext) { }

        public override async Task Add(City entity, CancellationToken token)
        {
            var temp = await _dbContext.Cities.FirstOrDefaultAsync(c => c.Name == entity.Name, token);
            if (temp == null)
            {
                await base.Add(entity, token);
            }
            else
            {
                throw new ArgumentException("The city with this name has already exists", nameof(entity));
            }
            
        }
        public override async Task Update(City entity, CancellationToken token)
        {
            var city = await GetById(entity.Id, token);
            city.Name = entity.Name;
            await _dbContext.SaveChangesAsync(token);
        }
    }
}
