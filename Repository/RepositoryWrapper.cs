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

        private IOwnerRepository _owner;
        private IAccountRepository _account;
        private IArticuloRepository _articulo;
        private ICategoriaRepository _categoria;
        private IPersonaRepository _persona;

        private IItemRepository _item;

        private ICustomerRepository _customer;
        private IOrderRepository _Order;
        private IOrderItemsRepository _orderItems;

        private IVentaRepository _Venta;



        public IOwnerRepository Owner
        {
            get
            {
                if (_owner ==null)
                {
                    _owner = new OwnerRepository(_repoContext);
                }
                return _owner;
            }
        }

        public IAccountRepository Account
        {
            get
            {
                if (_account == null)
                {
                    _account = new AccountRepository(_repoContext);
                }
                return _account;
            }
        }

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

        public IItemRepository Item
        {
            get
            {
                if (_item == null)
                {
                    _item = new ItemRepository(_repoContext);
                }
                return _item;
            }
        }

        public ICustomerRepository Customer
        {
            get
            {
                if (_customer == null)
                {
                    _customer = new CustomerRepository(_repoContext);
                }
                return _customer;
            }
        }

        public IOrderRepository Order
        {
            get
            {
                if (_Order == null)
                {
                    _Order = new OrderRepository(_repoContext);
                }
                return _Order;
            }
        }


        public IOrderItemsRepository OrderItems
        {
            get
            {
                if (_orderItems == null)
                {
                    _orderItems = new OrderItemsRepository(_repoContext);
                }
                return _orderItems;
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



        public RepositoryWrapper(RepositoryContext repoContext)=> _repoContext = repoContext;        
        public async Task SaveAsync()=> await _repoContext.SaveChangesAsync();        

    }
}
