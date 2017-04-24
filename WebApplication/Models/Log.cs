using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Log
    {
        public Int64 LogId { get; set; }
        public string Entry { get; set; }
        public Byte[] IPAddress { get; set; }
        public DateTimeOffset LogTime { get; set; }
        public string TypeOfRequest { get; set; }
        public int ResultOfRequest { get; set; }
        public string PathAndFileName { get; set; }
        public Int64 SizeOfData { get; set; }



        public virtual LoadedFile LoadedFile { get; set; }
        public virtual Address Address { get; set; }

        public string GetIpAddressToString()
        {
            return System.Text.Encoding.UTF8.GetString(IPAddress);
        }
        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}",
                LogId,
                System.Text.Encoding.UTF8.GetString(IPAddress), LogTime, TypeOfRequest, ResultOfRequest, SizeOfData, PathAndFileName);
        }
    }
}
