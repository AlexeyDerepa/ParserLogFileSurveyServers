using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class LoadedFile
    {
        [Key]
        [ForeignKey("Log")]
        public Int64 LogID { get; set; }

        public string PathToFile { get; set; }
        public Int64 SizeOfFile { get; set; }
        public string TitlePage { get; set; }



        public virtual Log Log { get; set; }
        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}",
                LogID, TitlePage, SizeOfFile, PathToFile);
        }

    }
}
