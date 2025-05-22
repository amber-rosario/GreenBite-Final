using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using GreenBite.Domain.Entities;

namespace GreenBite.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Salad> Salads { get; set; }
    }
}
