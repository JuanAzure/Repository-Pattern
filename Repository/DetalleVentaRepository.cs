using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class DetalleVentaRepository : RepositoryBase<DetalleVenta>, IDetalleVentaRepository
    {

        public DetalleVentaRepository(RepositoryContext repositoryContext) : base(repositoryContext) { 

        }

        public async Task<IEnumerable<DetalleVenta>> GetVentaDetallesAsync(int VentId,bool trackChanges)
        {
           return await FindByCondition(v=> v.VentaId.Equals(VentId), trackChanges).ToListAsync();
        }

        public async Task<IEnumerable<DetalleVenta>> GetExistsArticuloDetallesAsync(int ArticuloId, bool trackChanges) =>

            await FindByCondition(art => art.ArticuloId.Equals(ArticuloId),trackChanges).ToListAsync();
    }
}
