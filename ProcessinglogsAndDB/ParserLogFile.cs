using ProcessinglogsAndDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessinglogsAndDB
{
    public class ParserLogFile
    {
        private List<Log> _listLogs;
        private string _paternGetAllEntry;
        private string _paternGetOnlyHtml;

        public List<Log> GetListLogs { get { return this._listLogs; } }

        public ParserLogFile()
        {
            this._paternGetAllEntry = @"([\d\.]+).+?\[(.+?)\]\s\""(GET|POST|HEAD|OPTIONS)(.+?)\sHTTP.+?\""\s((\d{3}))\s(\d+)\s";
            this._paternGetOnlyHtml = @"([\d\.]+).+?\[(.+?)\]\s\""(GET|POST|HEAD)(.+?\.html(.+?)?)\sHTTP.+?\""\s(\d{3})\s(\d+).+";
        }


        public void ParseLogAllEntry(string source)
        {
            Parse(source, this._paternGetAllEntry);
        }
        public void ParseLogJustHTML(string source)
        {
            Parse(source, this._paternGetOnlyHtml);
        }
        public static string ParseJustOnewItem(string source, string patern)
        {
            if (source == null) return null;

            System.Text.RegularExpressions.Regex regEx = new System.Text.RegularExpressions.Regex(patern,System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            System.Text.RegularExpressions.MatchCollection matches = regEx.Matches(source);
            //Console.WriteLine(source);
            foreach (System.Text.RegularExpressions.Match m in matches)
                return m.Groups[1].Value;

            return null;
        }
        private void Parse(string source, string patern)
        {
            System.Text.RegularExpressions.Regex regEx = new System.Text.RegularExpressions.Regex(patern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.MatchCollection matches = regEx.Matches(source);
            this._listLogs = new List<Log>();
            foreach (System.Text.RegularExpressions.Match m in matches)
            {
                this._listLogs.Add(new Log
                {
                    Entry = m.Groups[0].Value,
                    IPAddress = Encoding.ASCII.GetBytes(m.Groups[1].Value),
                    LogTime = GetDateTimeFromString(m.Groups[2].Value),
                    TypeOfRequest = m.Groups[3].Value,
                    PathAndFileName = m.Groups[4].Value,
                    ResultOfRequest = int.Parse(m.Groups[6].Value),
                    SizeOfData = Int64.Parse(m.Groups[7].Value)
                });
            }
        }

        private DateTimeOffset GetDateTimeFromString(string time)
        {
            return DateTimeOffset.ParseExact(time, "dd/MMM/yyyy:HH:mm:ss zzz", System.Globalization.CultureInfo.InvariantCulture);
        }


    }
}
