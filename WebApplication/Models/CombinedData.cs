using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class CombinedData
    {
        public Int64 LogId { get; set; }
        public Byte[] IPAddress { get; set; }
        public string IPAddressString { get { return System.Text.Encoding.UTF8.GetString(IPAddress); } }
        public string CompaniName { get; set; }
        public DateTimeOffset LogTime { get; set; }
        public string TypeOfRequest { get; set; }
        public int ResultOfRequest { get; set; }
        public string PathToFile { get; set; }
        public Int64 SizeOfFile { get; set; }
        public string TitlePage { get; set; }
    }
}
