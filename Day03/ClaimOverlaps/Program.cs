using System;
using System.IO;
using System.Linq;
using static ClaimOverlaps.Helper;

namespace ClaimOverlaps
{
    static class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1 || !File.Exists(args[0]))
            {
                Console.WriteLine("first argument must be input file");
                return;
            }

            var claims = ReadClaims(args[0]);
            // actually waaaay faster with LINQ than with for loops
            var overlapCount = (
                from x in Enumerable.Range(0, 1000)
                from y in Enumerable.Range(0, 1000)
                let matches =
                    from c in claims
                    where c.x <= x
                    where x <= c.x2
                    where c.y <= y
                    where y <= c.y2
                    select c
                where matches.Count() > 1
                select y
            ).Count();
            Console.WriteLine(overlapCount);
        }
    }
}