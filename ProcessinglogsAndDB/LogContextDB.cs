using ProcessinglogsAndDB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessinglogsAndDB
{
    public class LogContextDB : DbContext
    {
        public LogContextDB(string db ) : base(db) { }
        public LogContextDB( ) { }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<LoadedFile> LoadedFiles { get; set; }
    }
}
