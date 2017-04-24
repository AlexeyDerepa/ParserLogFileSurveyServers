using ProcessinglogsAndDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication.Controllers
{
    public class LogsController : ApiController
    {

        ProcessinglogsAndDB.MyDB db = new ProcessinglogsAndDB.MyDB("LogContext");

        public IEnumerable<CombinedData> Get()
        {
            return db.GetCombinedData();
        }

    }
}
