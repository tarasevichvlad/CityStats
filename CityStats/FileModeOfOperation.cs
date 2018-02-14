using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace CityStats
{
    public class FileModeOfOperation : CommonWork
    {
        private void CreateOutputFile(ICollection<string> listFile)
        {
            threadSignal = new CountdownEvent(listFile.Count);
            foreach (var item in listFile)
            {
                new Thread(()=>{
                    semaphoreSlim.Wait();
                    cityAmount.AddCityAmount(ConvertOfDictionary(GetParsedLine(OpenFileAndDownloadContent(item))));
                    threadSignal.Signal();
                    semaphoreSlim.Release();
                }).Start();
            }
            threadSignal.Wait();
            cityAmount.SaveAllItemСity();
        }

        protected List<string> GetAllFileUtf8(string way)
        {
            try
            {
                return Directory.GetFiles(way, "*.txt", SearchOption.AllDirectories).Where(IsFileUtf8).ToList();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Environment.Exit(1);
            }
            return new List<string>();
        }

        private string OpenFileAndDownloadContent(string fileAddress)
        {
            return File.ReadAllText(fileAddress);
        }

        public void Start(string inputAddress)
        {
            try
            {
                CreateOutputFile(GetAllFileUtf8(inputAddress));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Environment.Exit(1);
            }
        }
    }


}
