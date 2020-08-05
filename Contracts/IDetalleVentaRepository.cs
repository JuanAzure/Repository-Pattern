using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IDetalleVentaRepository : IRepositoryBase<DetalleVenta>
    {
        Task<IEnumerable<DetalleVenta>> GetVentaDetallesAsync(int VentId,bool trackChanges);

        Task<IEnumerable<DetalleVenta>> GetExistsArticuloDetallesAsync(int ArticuloId, bool trackChanges);

    }
}
