using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CityStats
{
    public class CityAmount
    {
        private readonly Dictionary<string, int> cityAmounts = new Dictionary<string, int>();

        public void AddCityAmount(Dictionary<string, int> cityAmount)
        {
            lock (cityAmounts)
            {
                foreach (var pair in cityAmount)
                {
                    if (cityAmounts.ContainsKey(pair.Key))
                        cityAmounts[pair.Key] += pair.Value;
                    else cityAmounts.Add(pair.Key, pair.Value);
                }
            }
        }

        public void SaveAllItemСity()
        {
            lock (cityAmounts)
            {
                File.WriteAllLines(@".\output.txt", cityAmounts.Select(n => $"{n.Key}, {n.Value}"), Encoding.UTF8);
            }
        }
    }
}


