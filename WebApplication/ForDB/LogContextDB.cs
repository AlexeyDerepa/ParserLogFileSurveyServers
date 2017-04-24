
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.ForDB
{
    public class LogContextDB : DbContext
    {
        public LogContextDB( ) { }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<LoadedFile> LoadedFiles { get; set; }
    }
}
