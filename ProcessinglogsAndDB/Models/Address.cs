using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessinglogsAndDB.Models
{
    public class Address
    {
        [Key]
        [ForeignKey("Log")]
        public Int64 LogID { get; set; }
        public Byte[] IPAddress { get; set; }
        public string CompaniName { get; set; }



        public virtual Log Log { get; set; }


        public string GetIpToString()
        {
            return System.Text.Encoding.UTF8.GetString(IPAddress);
        }

        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}", LogID, System.Text.Encoding.UTF8.GetString(IPAddress), CompaniName);
        }

    }
}
