using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace CityStats
{
    public class HttpModeOfOperation : CommonWork
    {
        private void CreateOutputFile(ICollection<string> listUrl)
        {
            threadSignal = new CountdownEvent(listUrl.Count);
            foreach (var item in listUrl)
            {
                new Thread(() =>
                {
                    semaphoreSlim.Wait();
                    cityAmount.AddCityAmount(ConvertOfDictionary(GetParsedLine(DownloadContentPage(item))));
                    threadSignal.Signal();
                    semaphoreSlim.Release();
                }).Start();
            }
            threadSignal.Wait();
            cityAmount.SaveAllItemСity();
        }

        public string DownloadContentPage(string url)
        {
            return new WebClient().DownloadString(url);
        }

        protected List<string> GetListUrl(string addressFile)
        {
            List<string> list = new List<string>();
            using (StreamReader reader = new StreamReader(File.OpenRead(addressFile)))
            {
                while (!reader.EndOfStream)
                {
                    try
                    {
                        UriBuilder builder = new UriBuilder(reader.ReadLine() ?? "");
                        list.Add(builder.Uri.OriginalString);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                        Environment.Exit(1);
                    }
                }
            }
            return list;
        }

        public void Start(string inputAddress)
        {
            if (!File.Exists(inputAddress))
            {
                Console.WriteLine("Нет такого файла на диске");
                Environment.Exit(1);
            }
            if (!IsFileUtf8(inputAddress))
            {
                Console.WriteLine("Данный файл не является UTF8");
                Environment.Exit(1);
            }
            CreateOutputFile(GetListUrl(inputAddress));
        }
    }
}
