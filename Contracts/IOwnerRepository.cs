using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
  public interface IOwnerRepository:IRepositoryBase<Owner>
    {
        public List<Owner> PDF();
    }
}
