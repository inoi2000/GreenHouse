using GreenHouse.Domain.Entities;
using GreenHouse.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GreenHouse.Data.EntityFramework.Reposirories
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly AppDbContext _dbContext;
        public EfRepository(AppDbContext dbContext)
        {
            if (dbContext is null) { throw new ArgumentNullException(nameof(dbContext)); }
            _dbContext = dbContext;
        }

        protected DbSet<TEntity> Entities => _dbContext.Set<TEntity>();

        public virtual Task<TEntity> GetById(Guid Id, CancellationToken cancellationToken)
            => Entities.FirstAsync(it => it.Id == Id, cancellationToken);

        public virtual async Task<IReadOnlyList<TEntity>> GetAll(CancellationToken cancellationToken)
            => await Entities.ToListAsync(cancellationToken);

        public virtual async Task Add(TEntity entity, CancellationToken cancellationToken)
        {
            if (entity is null) { throw new ArgumentNullException(nameof(entity)); }

            await Entities.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task AddUnsafe(TEntity entity, CancellationToken cancellationToken)
        {
            if (entity is null) { throw new ArgumentNullException(nameof(entity)); }

            await Entities.AddAsync(entity, cancellationToken);
        }

        public virtual async Task Update(TEntity entity, CancellationToken cancellationToken)
        {
            if (entity is null) { throw new ArgumentNullException(nameof(entity)); }

            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task UpdateUnsafe(TEntity entity, CancellationToken cancellationToken)
        {
            if (entity is null) { throw new ArgumentNullException(nameof(entity)); }

            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            Entities.Remove(await GetById(id, cancellationToken));
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task DeleteUnsafe(Guid id, CancellationToken cancellationToken)
        {
            Entities.Remove(await GetById(id, cancellationToken));
        }
    }
}
