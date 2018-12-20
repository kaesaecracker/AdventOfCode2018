using System;
using System.IO;

namespace Calibration
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

            long sum = 0;
            using (var tr = new StreamReader(args[0]))
            {
                while (!tr.EndOfStream)
                {
                    var line = tr.ReadLine();
                    sum +=  int.Parse(line);
                }
            }
            
            Console.WriteLine($"Sum: {sum}");
        }
    }
}