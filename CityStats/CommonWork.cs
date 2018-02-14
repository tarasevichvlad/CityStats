using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Configuration;


namespace CityStats
{
    public abstract class CommonWork
    {
        protected CityAmount cityAmount;
        protected SemaphoreSlim semaphoreSlim;
        protected CountdownEvent threadSignal;

        protected CommonWork()
        {
            semaphoreSlim = new SemaphoreSlim(Convert.ToInt32(ConfigurationManager.AppSettings["initialCount"]),Convert.ToInt32(ConfigurationManager.AppSettings["maxCount"]));
            cityAmount = new CityAmount();
        }

        protected Dictionary<string, int> ConvertOfDictionary(string[] parseStrings)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            for (int i = 0; i < parseStrings.Length; i += 2)
            {
                try
                {
                    if (dictionary.ContainsKey(parseStrings[i].Substring(0, 1).ToUpper() + parseStrings[i].Substring(1)))
                        dictionary[parseStrings[i].Substring(0, 1).ToUpper() + parseStrings[i].Substring(1)] += Convert.ToInt32(parseStrings[i + 1]);
                    else dictionary.Add(parseStrings[i].Substring(0, 1).ToUpper() + parseStrings[i].Substring(1), Convert.ToInt32(parseStrings[i + 1]));
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    Environment.Exit(1);
                }
            }
            return dictionary;
        }

        protected string[] GetParsedLine(string parsingString)
        {
            return parsingString.Split(new string[] { ",", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        protected bool IsFileUtf8(string addressFile)
        {
            using (BinaryReader reader = new BinaryReader(File.OpenRead(addressFile)))
            {
                byte[] buffer = reader.ReadBytes((int)reader.BaseStream.Length);
                return ((((buffer.Length > 2) && (buffer[0] == 0xef)) && (buffer[1] == 0xbb)) && (buffer[2] == 0xbf));
            }
        }

        public static void ProcessData(string inputMode, string inputAddress)
        {
            switch (inputMode)
            {
                case "filesystem":new FileModeOfOperation().Start(inputAddress); break;
                case "http":new HttpModeOfOperation().Start(inputAddress);break;
                default:Console.WriteLine("Нет такого режима работы!");Environment.Exit(1);break;
            }
        }
    }

}