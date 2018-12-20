using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Checksum
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.Error.WriteLine("First parameter should be input file");
                return;
            }

            if (!File.Exists(args[0]))
            {
                Console.Error.WriteLine($"File '{args[0]}' does not exist");
                return;
            }

            int twoTimes = 0;
            int threeTimes = 0;
            using (var tr = new StreamReader(args[0]))
                while (!tr.EndOfStream)
                {
                    var line = tr.ReadLine();
                    if (line == null) continue;

                    var charCounts = new Dictionary<char, int>();
                    foreach (var c in line)
                        charCounts[c] = charCounts.ContainsKey(c) ? charCounts[c] + 1 : 1;

                    if (charCounts.Any(pair => pair.Value == 2))
                        twoTimes++;
                    if (charCounts.Any(pair => pair.Value == 3))
                        threeTimes++;
                }

            Console.WriteLine($"The checksum is {twoTimes * threeTimes}");
        }
    }
}