using System;
using System.IO;
using System.Linq;
using static Strategy1.Helper;

namespace Strategy1
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 1 || !File.Exists(args[0]))
            {
                Console.WriteLine("first argument must be input file");
                return;
            }

            var lines = ReadInput(args[0]);
            lines.Sort((l1, l2) => l1._timestamp.CompareTo(l2._timestamp));

            var guards = ParseGuards(lines);
            var guardWithMaxSleepMins = guards.OrderBy(kv => kv.Value.SumSleepMinutes).Last().Value;

            var minuteWithMaxSleep = guardWithMaxSleepMins.FindMinuteWithMaxSleep().Minute;
            Console.WriteLine(
                $"{guardWithMaxSleepMins}, Minute {minuteWithMaxSleep}. Solution: {guardWithMaxSleepMins.GuardId * minuteWithMaxSleep}");
        }
    }
}