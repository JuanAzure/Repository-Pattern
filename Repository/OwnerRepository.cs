using Contracts;
using Entities;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class OwnerRepository:RepositoryBase<Owner>,IOwnerRepository
    {
        public OwnerRepository(RepositoryContext repositoryContext):base(repositoryContext)
        {
           
        }

        public  List<Owner> PDF()
        {
            return FindAll().ToList();
        }
    }
}
