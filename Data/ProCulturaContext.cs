using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Data
{
    public class ProCulturaContext : DbContext
    {
        public IDbSet<User> Users { get; set; }
        public ProCulturaContext() : base("local")
        {

        }
    }
}
