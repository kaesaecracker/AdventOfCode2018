using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FabricFinder
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

            var ids = new List<string>();
            using (var tr = new StreamReader(args[0]))
                while (!tr.EndOfStream)
                {
                    var line = tr.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    ids.Add(line);
                }

            for (int j = 0; j < ids.Count; j++)
            {
                var idJ = ids[j];
                for (int k = j + 1; k < ids.Count; k++)
                {
                    var idK = ids[k];
                    int differences = idJ.Where((t, index) => t != idK[index]).Count();
                    if (differences == 1)
                    {
                        Console.WriteLine($"{j}\t{idJ}\t{k}\t{idK}");
                        Console.WriteLine(new String(
                            idJ.Where((t, index) => t == idK[index]).ToArray()
                        ));
                    }
                }
            }
        }
    }
}