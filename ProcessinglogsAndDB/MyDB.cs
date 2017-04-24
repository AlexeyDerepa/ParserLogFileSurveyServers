using ProcessinglogsAndDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ProcessinglogsAndDB
{
    public class MyDB
    {
        private LogContextDB db;
        public MyDB(string nameDB)
        {
            db = new LogContextDB(nameDB);
        }
        public MyDB()
        {
            db = new LogContextDB();
        }
        public List<Log> GetLogsTable()
        {
            return db.Logs.ToList<Log>();
        }

        public List<Address> GetAddressesTable()
        {
            return db.Addresses.ToList<Address>();
        }

        public List<LoadedFile> GetLoadedFileTable()
        {
            return db.LoadedFiles.ToList<LoadedFile>();
        }
        public IEnumerable<CombinedData> GetCombinedData()
        {

            return db.Logs.Select(x => new CombinedData { 
                CompaniName = x.Address.CompaniName,
                IPAddress = x.IPAddress,
                //IPAddress = System.Text.Encoding.UTF8.GetString(x.IPAddress).ToString(),
                LogId = x.LogId, 
                LogTime = x.LogTime, 
                PathToFile = x.PathAndFileName, 
                ResultOfRequest = x.ResultOfRequest, 
                SizeOfFile = x.SizeOfData , 
                TitlePage = x.LoadedFile.TitlePage, 
                TypeOfRequest = x.TypeOfRequest}).ToList<CombinedData>();
        }
    }
}
