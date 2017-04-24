using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WebApplication.ForDB
{
    public class LogContextInitializer : DropCreateDatabaseIfModelChanges<LogContextDB>
    {
        protected override void Seed(LogContextDB context)
        {
            base.Seed(context);
        }
    }
}
