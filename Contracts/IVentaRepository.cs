using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IVentaRepository : IRepositoryBase<Venta>
    {
        Task<IEnumerable<Venta>> GetAllVentaAsync(bool trackChanges);
        Task<Venta> GetByVentaIDAsync(int ventaId, bool trackChanges);
        void CreateVenta(Venta order );
        void DeleteVenta(Venta order);
    }
}
