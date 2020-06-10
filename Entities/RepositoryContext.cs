using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class RepositoryContext: DbContext
    {
        public RepositoryContext(DbContextOptions options): base (options)
        {
        }

        public DbSet<Owner> Owners{ get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Articulo> Articulos { get; set; }

        public DbSet<Categoria> Categorias { get; set; }


    }
}
