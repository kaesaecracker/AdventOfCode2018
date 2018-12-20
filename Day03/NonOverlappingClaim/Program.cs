using System;
using System.Reflection.Metadata.Ecma335;
using ClaimOverlaps;
using static ClaimOverlaps.Helper;

namespace NonOverlappingClaim
{
    class Program
    {
        static void Main(string[] args)
        {
            var claims = ReadClaims(args[0]);
            claims.Sort((c1, c2) => c1.y - c2.y);
            claims.Sort((c1, c2) => c1.x - c2.x); // sorted from leftmost to rightmost
            for (var outerIndex = 0; outerIndex < claims.Count; outerIndex++)
            {
                var outer = claims[outerIndex];
                bool foundOverlap = false;
                for (var innerIndex = 0; innerIndex < claims.Count; innerIndex++)
                {
                    if (innerIndex == outerIndex) continue;
                    var inner = claims[innerIndex];

                    var overlapsX = (outer.x <= inner.x && outer.x2 >= inner.x)
                                    || (inner.x <= outer.x && inner.x2 >= outer.x);
                    var overlapsY = (outer.y <= inner.y && outer.y2 >= inner.y)
                                    || (inner.y <= outer.y && inner.y2 >= outer.y);
                    if (overlapsX && overlapsY)
                    {
                        foundOverlap = true;
                        break;
                    }
                }

                if (!foundOverlap)
                {
                    Console.WriteLine(outer);
                    return;
                }
            }
        }
    }
}