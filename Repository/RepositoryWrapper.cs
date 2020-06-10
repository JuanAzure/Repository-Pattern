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

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public async Task Save()
        {
          await  _repoContext.SaveChangesAsync();
        }

    }
}
