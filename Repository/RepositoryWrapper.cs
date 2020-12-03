using Contracts;
using Entities;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repositoryContext;

        private IArticuloRepository _articuloRepository;
        private ICategoriaRepository _categoriaRepository;
        private IPersonaRepository _personaRepository;
        private IVentaRepository _ventaRepository;
        private IDetalleVentaRepository _detalleVentaRepository;
        public IArticuloRepository Articulo
        {
            get
            {
                if (_articuloRepository == null)
                {
                    _articuloRepository = new ArticuloRepository(_repositoryContext);
                }
                return _articuloRepository;
            }
        }
        public ICategoriaRepository Categoria
        {
            get
            {
                if (_categoriaRepository == null)
                {
                    _categoriaRepository = new CategoriaRepository(_repositoryContext);
                }
                return _categoriaRepository;
            }
        }
        public IPersonaRepository Persona
        {
            get
            {
                if (_personaRepository == null)
                {
                    _personaRepository = new PersonaRepository(_repositoryContext);
                }
                return _personaRepository;
            }
        }
        public IVentaRepository Venta
        {
            get
            {
                if (_ventaRepository == null)
                {
                    _ventaRepository = new VentaRepository(_repositoryContext);
                }
                return _ventaRepository;
            }
        }
        public IDetalleVentaRepository DetalleVenta
        {
            get
            {
                if (_detalleVentaRepository == null)
                {
                    _detalleVentaRepository = new DetalleVentaRepository(_repositoryContext);
                }
                return _detalleVentaRepository;
            }
        }

        public RepositoryWrapper(RepositoryContext repoContext)=> _repositoryContext = repoContext;       
        public async Task SaveAsync()=> await _repositoryContext.SaveChangesAsync();        

    }
}
