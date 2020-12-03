 using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IArticuloRepository Articulo { get; }
        ICategoriaRepository Categoria { get; }
        IPersonaRepository Persona { get; }
        IVentaRepository Venta { get; }
        IDetalleVentaRepository DetalleVenta { get; }
        Task SaveAsync();
    }
}
