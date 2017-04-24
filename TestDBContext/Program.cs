using ProcessinglogsAndDB;
using ProcessinglogsAndDB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestDBContext
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the name DB");
            string nameDB = Console.ReadLine();

            //LogContextDB db = new LogContextDB(nameDB);

            //string index = 1.ToString();

            //switch (index)
            //{
            //    case "1":
            //        foreach (var item in db.Logs.ToList<Log>())
            //        {
            //            Console.WriteLine(item.Address);
            //        }
            //        break;
            //    case "2":
            //        foreach (var item in db.LoadedFiles.ToList<LoadedFile>())
            //        {
            //            Console.WriteLine(item.Log);
            //        }
            //        break;
            //    case "3":
            //        foreach (var item in db.Addresses.ToList<Address>())
            //        {
            //            Console.WriteLine(item);
            //        }
            //        break;
            //    default:
            //        break;
            //}




            //string path = @"C:\Users\Alexey\Documents\Visual Studio 2017\Projects\regex_from_file\regex_from_file\data\tariscope.com.access.log.0";
            string path = @"tariscope.com.access.log.0";
            ProcessingData pd = new ProcessingData(nameDB);
            pd.StartProcessFromFile(path);
            Console.WriteLine("All OK");
            Console.Read();
        }
        static string someMethod(Log index)
        {
           string targetPage = "http://www.tariscope.com"  + index.PathAndFileName.Trim();
           Uri uri = new Uri(targetPage);

           HttpWebRequest proxy_request = (HttpWebRequest)WebRequest.Create(uri);
           proxy_request.Method = index.TypeOfRequest.Trim();
            proxy_request.ContentType = "application/x-www-form-urlencoded";
            proxy_request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/532.5 (KHTML, like Gecko) Chrome/4.0.249.89 Safari/532.5";
            proxy_request.KeepAlive = true;
            HttpWebResponse resp = proxy_request.GetResponse() as HttpWebResponse;
            string html = "";
            using (StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.GetEncoding(1251)))
                html = sr.ReadToEnd();
            html = html.Trim();
            return ParserLogFile.ParseJustOnewItem(html, @"<title>(.*?)</title>");
        }

       static string GetHostName(Byte[] ipAddress)
        {
            try
            {
                return System.Net.Dns.GetHostEntry(System.Text.Encoding.UTF8.GetString(ipAddress)).HostName;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

       static string GetTitlePage(Log index)
       {

           string targetPage = "http://www.tariscope.com"  + index.PathAndFileName.Trim();
               string source = null;
           try
           {
               //System.Net.WebRequest req = System.Net.HttpWebRequest.Create(targetPage);
               //req.Method = index.TypeOfRequest;// GET | POST | 

               //using (System.IO.StreamReader reader = new System.IO.StreamReader(req.GetResponse().GetResponseStream()))
               //{
               //    source = reader.ReadToEnd();
               //}
               source = new System.Net.WebClient().DownloadString(targetPage);
               return ParserLogFile.ParseJustOnewItem(source, @"<title>.*?</title>");

           }
           catch (Exception ex)
           {
               return null;
           }
       }
    }
}
