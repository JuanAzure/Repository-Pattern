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
                .Include(p=>p.Persona)
                .ToListAsync();


        public async Task<Venta> GetByVentaDetailsAsync(int ventaId,bool trackChanges) =>
             await FindByCondition(v=>v.VentaId.Equals(ventaId),trackChanges)            
            .Include(det=>det.DetalleVentas).ThenInclude(art=>art.Articulo)
            .Include(per=>per.Persona)
            .FirstOrDefaultAsync();

        public async Task<Venta> GetVentaByIdAsync(int ventaId, bool trackChanges) =>
            await FindByCondition(v => v.VentaId.Equals(ventaId), trackChanges)
            .SingleOrDefaultAsync();


        public void CreateVenta(Venta venta) => Create(venta);

        public void DeleteVenta(Venta venta) => Delete(venta);
    }
}
