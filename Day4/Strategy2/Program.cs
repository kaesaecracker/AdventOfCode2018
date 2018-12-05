using System;
using System.IO;
using System.Linq;
using System.Net;
using Strategy1;
using static Strategy1.Helper;

namespace Strategy2
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
            var match = guards.Values.First();
            var matchBest = match.FindMinuteWithMaxSleep();
            foreach (var guard in guards.Values)
            {
                var guardBest = guard.FindMinuteWithMaxSleep();
                if (guardBest.SleepCount > matchBest.SleepCount)
                {
                    match = guard;
                    matchBest = guardBest;
                }
            }
            
            Console.WriteLine($"{match}: {matchBest.SleepCount}x in Minute {matchBest.Minute}, Solution is {match.GuardId * matchBest.Minute}");
        }
    }
}