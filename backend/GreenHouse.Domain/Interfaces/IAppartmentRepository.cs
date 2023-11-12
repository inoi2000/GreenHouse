using GreenHouse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHouse.Domain.Interfaces
{
    public interface IAppartmentRepository : IRepository<Appartment>
    {
        Task<IReadOnlyList<Appartment>> GetByCityId(Guid cityId, CancellationToken cancellationToken);
    }
}
