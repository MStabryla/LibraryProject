using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace LibraryProject.Models
{
    public class _Models : DbContext
    {
        public _Models() : base("MainCon")
        {

        }
        public DbSet<Essay> Essays { get; set; }
        public DbSet<Argument> Arguments { get; set; }
        public DbSet<Author> Autors{ get; set; }
    }
}