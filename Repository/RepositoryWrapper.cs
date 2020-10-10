using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;

        private IArticuloRepository _articulo;
        private ICategoriaRepository _categoria;
        private IPersonaRepository _persona;
        private IVentaRepository _Venta;
        private IDetalleVentaRepository _DetalleVenta;
        public IArticuloRepository Articulo
        {
            get
            {
                if (_articulo == null)
                {
                    _articulo = new ArticuloRepository(_repoContext);
                }
                return _articulo;
            }
        }
        public ICategoriaRepository Categoria
        {
            get
            {
                if (_categoria == null)
                {
                    _categoria = new CategoriaRepository(_repoContext);
                }
                return _categoria;
            }
        }
        public IPersonaRepository Persona
        {
            get
            {
                if (_persona == null)
                {
                    _persona = new PersonaRepository(_repoContext);
                }
                return _persona;
            }
        }
        public IVentaRepository Venta
        {
            get
            {
                if (_Venta == null)
                {
                    _Venta = new VentaRepository(_repoContext);
                }
                return _Venta;
            }
        }
        public IDetalleVentaRepository DetalleVenta
        {
            get
            {
                if (_DetalleVenta == null)
                {
                    _DetalleVenta = new DetalleVentaRepository(_repoContext);
                }
                return _DetalleVenta;
            }
        }

        public RepositoryWrapper(RepositoryContext repoContext)=> _repoContext = repoContext;       
        public async Task SaveAsync()=> await _repoContext.SaveChangesAsync();        

    }
}
