using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Strategy1
{
    public static class Helper
    {
        public const string DtFormat = "yyyy-MM-dd HH:mm";

        public static Dictionary<int, GuardInfo> ParseGuards(List<Line> lines)
        {
            if (lines == null) throw new ArgumentNullException(nameof(lines));
            if (lines.Count == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(lines));

            var guards = new Dictionary<int, GuardInfo>();
            GuardInfo currentGuard = null;
            foreach (var line in lines)
            {
                if (line._kind == Line.Kind.BeginShift)
                {
                    if (!guards.ContainsKey(line._guardId))
                    {
                        guards.Add(line._guardId,
                            new GuardInfo {GuardId = line._guardId, Sleeps = new List<GuardInfo.SleepTime>()});
                    }

                    currentGuard = guards[line._guardId];
                }
                else if (line._kind == Line.Kind.FallAsleep)
                    currentGuard.Sleeps.Add(new GuardInfo.SleepTime {Start = line._timestamp.Minute});
                else if (line._kind == Line.Kind.WakeUp)
                    currentGuard.Sleeps.Last().End = line._timestamp.Minute;
            }

            return guards;
        }

        public static List<Line> ReadInput(string path)
        {
            var list = new List<Line>();

            using (var sr = new StreamReader(path))
                while (!sr.EndOfStream)
                {
                    var lineStr = sr.ReadLine()?.Trim();
                    if (string.IsNullOrWhiteSpace(lineStr)) continue;

                    var rest = lineStr.Substring(lineStr.IndexOf(']') + 2);
                    var timeStr = Regex.Match(lineStr, "\\[(.+)\\]")
                        .Value
                        .Replace("[", "")
                        .Replace("]", "");

                    var line = new Line
                    {
                        _timestamp = DateTime.ParseExact(timeStr, DtFormat, null)
                    };

                    switch (rest)
                    {
                        case "falls asleep":
                            line._kind = Line.Kind.FallAsleep;
                            break;
                        case "wakes up":
                            line._kind = Line.Kind.WakeUp;
                            break;

                        default:
                            var guardIdStr = Regex.Match(rest, "[0-9]+").Value;
                            line._guardId = int.Parse(guardIdStr);
                            line._kind = Line.Kind.BeginShift;
                            break;
                    }

                    list.Add(line);
                }

            return list;
        }
        
        public static (int Minute, int SleepCount) FindMinuteWithMaxSleep(this GuardInfo guard)
        {
            var sleepCountPerMinute = new int[60];
            for (var i = 0; i < guard.Sleeps.Count; i++)
            {
                var s = guard.Sleeps[i];
                for (var minute = s.Start; minute < s.End; minute++)
                {
                    sleepCountPerMinute[minute]++;
                }
            }

            var biggestIndex = 0;
            for (var i = 0; i < 60; i++)
            {
                if (sleepCountPerMinute[i] > sleepCountPerMinute[biggestIndex])
                    biggestIndex = i;
            }

            return (biggestIndex, sleepCountPerMinute[biggestIndex]);
        }
    }


    public class GuardInfo
    {
        public int GuardId;
        public List<SleepTime> Sleeps;

        public int SumSleepMinutes => Sleeps.Count == 0 ? 0 : Sleeps.Sum(st => st.Length);

        public override string ToString() => $"[#{GuardId}, slept {Sleeps.Count} times or {SumSleepMinutes}min]";

        public class SleepTime
        {
            public int Start;
            public int End;

            public int Length => End - Start;

            public override string ToString() => $"[From: {Start}, To: {End}, Length: {Length}]";
        }
    }

    public struct Line
    {
        public enum Kind
        {
            BeginShift,
            FallAsleep,
            WakeUp
        }

        public DateTime _timestamp;
        public Kind _kind;
        public int _guardId;

        public override string ToString()
        {
            var output = "[" + this._timestamp.ToString(Helper.DtFormat) + "] ";
            switch (this._kind)
            {
                case Kind.WakeUp:
                    output += "wakes up";
                    break;
                case Kind.BeginShift:
                    output += $"Guard #{this._guardId} begins shift";
                    break;
                case Kind.FallAsleep:
                    output += "falls asleep";
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return output;
        }
    }
}