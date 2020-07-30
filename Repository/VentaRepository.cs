using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class VentaRepository : RepositoryBase<Venta>, IVentaRepository
    {

        public VentaRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<Venta>> GetAllVentaAsync(bool trackChanges) =>

        await FindAll(trackChanges)
            .Include(c => c.Persona)
            .Include(o => o.DetalleVenta)
            .OrderByDescending(o => o.Total)
            .ToListAsync();

        public async Task<Venta> GetByVentaIDAsync(int ventaId, bool trackChanges) =>
              await FindByCondition(or => or.VentaId.Equals(ventaId), trackChanges)
                  .Include(c => c.Persona)
                 .Include(o => o.DetalleVenta).ThenInclude(x => x.Articulo)
                .FirstOrDefaultAsync();

        public void CreateVenta(Venta venta) => Create(venta);

        public void DeleteVenta(Venta venta) => Delete(venta);
    }
}
