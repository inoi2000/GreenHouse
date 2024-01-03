using GreenHouse.Domain.Entities;
using GreenHouse.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GreenHouse.Data.EntityFramework.Reposirories
{
    public class AppartmentRepositoryEf : EfRepository<Appartment>, IAppartmentRepository
    {
        public AppartmentRepositoryEf(AppDbContext dbContext) : base(dbContext) { }

        public override async Task<Appartment> GetById(Guid Id, CancellationToken cancellationToken)
        {
            return await _dbContext.Appartments
                .Include(a => a.Rules)
                .Include(a => a.Orders)
                .FirstAsync(a => a.Id == Id, cancellationToken);
        }
        public virtual async Task<IReadOnlyList<Appartment>> GetByCityId(Guid cityId, CancellationToken cancellationToken)
        {
            var city = await _dbContext.Cities
                .Include(c => c.Appartments)
                .FirstAsync(c => c.Id == cityId, cancellationToken);
            return city.Appartments;
        }

        public async override Task Add(Appartment entity, CancellationToken cancellationToken)
        {
            //var city = await _dbContext.Cities.Include(c => c.Appartments).FirstAsync(c => c.Id == entity.CityId, cancellationToken);
            //city.Appartments.Add(entity);
            _dbContext.Appartments.Add(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async override Task Update(Appartment entity, CancellationToken cancellationToken)
        {
            var appartment = await GetById(entity.Id, cancellationToken);

            if (!appartment.CityId.Equals(entity.CityId)) { appartment.CityId = entity.CityId; }
            if (!appartment.Location.Equals(entity.Location)) { appartment.Location = entity.Location; }
            if (!appartment.NumberOfGuests.Equals(entity.NumberOfGuests)) { appartment.NumberOfGuests = entity.NumberOfGuests; }
            if (!appartment.NumberOfRooms.Equals(entity.NumberOfRooms)) { appartment.NumberOfRooms = entity.NumberOfRooms; }
            if (!appartment.NumberOfSlippingPlaces.Equals(entity.NumberOfSlippingPlaces)) { appartment.NumberOfSlippingPlaces = entity.NumberOfSlippingPlaces; }
            if (!appartment.Square.Equals(entity.Square)) { appartment.Square = entity.Square; }
            if (!appartment.Bail.Equals(entity.Bail)) { appartment.Bail = entity.Bail; }
            if (!appartment.Price.Equals(entity.Price)) { appartment.Price = entity.Price; }
            if (!appartment.Rules.Equals(entity.Rules))
            {
                appartment.Rules.IsChildrenAllowed = entity.Rules.IsChildrenAllowed;
                appartment.Rules.IsPetsAllowed = entity.Rules.IsPetsAllowed;
                appartment.Rules.IsSmokingAllowed = entity.Rules.IsSmokingAllowed;
                appartment.Rules.IsPartyAllowed = entity.Rules.IsPartyAllowed;
            }

            if(entity.Photos is not null) appartment.Photos = entity.Photos;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
