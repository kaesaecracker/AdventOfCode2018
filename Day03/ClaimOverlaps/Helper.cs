using System.Collections.Generic;
using System.IO;

namespace ClaimOverlaps
{
    public static class Helper
    {
        public static List<Claim> ReadClaims(string path)
        {
            var claims = new List<Claim>();
            using (var tr = new StreamReader(path))
                while (!tr.EndOfStream)
                {
                    var line = tr.ReadLine();
                    if ((line = line.Trim()) == string.Empty) continue;

                    line = line.Substring(1); // remove #
                    var idStr = line.Substring(0, line.IndexOf(' '));

                    line = line.Substring(line.IndexOf("@ ") + 2); // remove '<id> @ '
                    var xStr = line.Substring(0, line.IndexOf(','));

                    line = line.Substring(line.IndexOf(',') + 1);
                    var yStr = line.Substring(0, line.IndexOf(':'));

                    line = line.Substring(line.IndexOf(' ') + 1);
                    var wh = line.Split('x');
                    var wStr = wh[0];
                    var hStr = wh[1];

                    Claim c;
                    c.id = int.Parse(idStr);
                    c.x = int.Parse(xStr);
                    c.y = int.Parse(yStr);
                    c.width = int.Parse(wStr);
                    c.height = int.Parse(hStr);
                    claims.Add(c);
                }

            return claims;
        }
    }
}