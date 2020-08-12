using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IVentaRepository : IRepositoryBase<Venta>
    {
        Task<IEnumerable<Venta>> GetAllVentaAsync(bool trackChanges);
        Task<Venta> GetByVentaDetailsAsync(int ventaId, bool trackChanges);

        Task<Venta> GetVentaByIdAsync(int ventaId, bool trackChanges);

        void CreateVenta(Venta order );
        void DeleteVenta(Venta order);
    }
}
