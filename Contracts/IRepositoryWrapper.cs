using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IOwnerRepository Owner { get; }
        IAccountRepository Account { get; }
        IArticuloRepository Articulo { get; }
        ICategoriaRepository Categoria { get; }
        Task Save();
    }
}
