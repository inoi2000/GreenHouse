using GreenHouse.Domain.Entities;
using GreenHouse.Domain.Interfaces;

namespace GreenHouse.Data.EntityFramework.Reposirories
{
    public class AppartmentRepositoryEf : EfRepository<Appartment>, IAppartmentRepository
    {
        public AppartmentRepositoryEf(AppDbContext dbContext) : base(dbContext) { }
    }
}
