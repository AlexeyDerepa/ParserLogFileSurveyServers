using ProcessinglogsAndDB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessinglogsAndDB
{
    public class ProcessingData
    {
        private ParserLogFile _ParserLogFile;
        private LogContextDB _LogContextDB;
        private object _threadLock;
        private int _count;
        public ProcessingData(string nameDB)
        {
            this._LogContextDB = new LogContextDB(nameDB);
            this._ParserLogFile = new ParserLogFile();
            this._threadLock = new object();
            this._count = 0;
        }

        public ProcessingData (): this("LogContext")
	    {

	    }
        public void StartProcessFromFile(string pathToFile)
        {
            if (!File.Exists(pathToFile))
            {
                return;
            }
            string data = System.IO.File.ReadAllText(
                pathToFile,
                Encoding.GetEncoding(1251)
                );


            ParseDataFromLgoFileAndSaveToDB(data);
            FillAddressesTable();


        }
        public void StartProcessFromString(string data)
        {
            if (data == null)
            {
                return;
            }


            ParseDataFromLgoFileAndSaveToDB(data);
            FillAddressesTable();


        }



        private void FillAddressesTable()
        {
            Console.WriteLine("it is filling the tables\nWait pleas");
            Parallel.For(0, this._ParserLogFile.GetListLogs.Count, FillAddressesAndLoadedFilesTable);
            Console.WriteLine("Wait just a few time, please");
            this._LogContextDB.SaveChanges();
        }

        private void FillAddressesAndLoadedFilesTable(int index)
        {

            string hostName = GetHostName(this._ParserLogFile.GetListLogs[index].IPAddress);
            string titleName = GetTitlePage(index);

            lock (this._threadLock)
            {
                this._count++;
                Console.WriteLine("{0} / {1}", this._count, this._ParserLogFile.GetListLogs.Count);
                this._LogContextDB.Addresses.Add(new Address { CompaniName = hostName, IPAddress = this._ParserLogFile.GetListLogs[index].IPAddress, LogID = this._ParserLogFile.GetListLogs[index].LogId });
                this._LogContextDB.LoadedFiles.Add(new LoadedFile {  PathToFile =this._ParserLogFile.GetListLogs[index].PathAndFileName,SizeOfFile = this._ParserLogFile.GetListLogs[index].SizeOfData, TitlePage = titleName, LogID = this._ParserLogFile.GetListLogs[index].LogId });
            }
        }

        private string GetHostName(Byte[] ipAddress)
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
        private string GetTitlePage(int index)
        {

            string targetPage = "http://www.tariscope.com" + this._ParserLogFile.GetListLogs[index].PathAndFileName.Trim();
            Uri uri = new Uri(targetPage);

            try
            {
                System.Net.HttpWebRequest proxy_request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uri);
                proxy_request.Method = this._ParserLogFile.GetListLogs[index].TypeOfRequest.Trim();
                proxy_request.ContentType = "application/x-www-form-urlencoded";
                proxy_request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/532.5 (KHTML, like Gecko) Chrome/4.0.249.89 Safari/532.5";
                proxy_request.KeepAlive = true;
                System.Net.HttpWebResponse resp = proxy_request.GetResponse() as System.Net.HttpWebResponse;
                string html = "";
                using (StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.GetEncoding(1251)))
                {
                    html = sr.ReadToEnd();
                }
                html = html.Trim();
                return ParserLogFile.ParseJustOnewItem(html, @"<title>(.*?)</title>");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void ParseDataFromLgoFileAndSaveToDB(string data)
        {

            this._ParserLogFile.ParseLogJustHTML(data);
            Console.WriteLine("the data has been read");
            this._LogContextDB.Logs.AddRange(this._ParserLogFile.GetListLogs);
            Console.WriteLine("the data has been parse");
            this._LogContextDB.SaveChanges();
            Console.WriteLine("the data has been save");
        }

    }
}
