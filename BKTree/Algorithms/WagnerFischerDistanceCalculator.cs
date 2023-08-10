using System.Numerics;
using BKTree.Interfaces;

namespace BKTree.Algorithms;

public class WagnerFischerDistanceCalculator<T> : IDistanceCalculator<T> where T : INumber<T>
{
    public int CalculateDistance(T[] s1, T[] s2)
    {
        while (true)
        {
            var len1 = s1.Length;
            var len2 = s2.Length;
            var buf = new int[Math.Min(len1, len2) + 1];

            if (len1 < len2)
            {
                (s1, s2) = (s2, s1);
                continue;
            }

            switch (len2)
            {
                case 0:
                    return len1;
                case 1:
                    return len1 - (Array.IndexOf(s1, s2[0]) != -1 ? 1 : 0);
                case 2:
                {
                    var index = Array.IndexOf(s1, s2[0]);
                    if (index >= 0)
                    {
                        return len1 - (Array.IndexOf(s1, s2[1], index + 1) != -1 ? 1 : 0) - 1;
                    }

                    return len1 - (Array.IndexOf(s1, s2[1], 1) != -1 ? 1 : 0);
                }
            }

            var i1 = (len2 - 1) / 2;
            var mx = 1 - i1 - (len1 - len2);

            for (var j = 0; j <= i1; j++) buf[j] = j;

            for (var i = 1; i <= len1; i++)
            {
                buf[0] = i - 1;

                var m = mx > 1 ? mx : 1;
                var M = i1 > len2 ? len2 : i1;
                mx++;
                i1++;

                int dia = buf[m - 1];
                int top = buf[m];

                if (!s1[i - 1].Equals(s2[m - 1])) dia = Math.Min(dia, top) + 1;

                buf[m] = dia;
                int left = dia;
                dia = top;

                for (int j = m + 1; j <= M; j++)
                {
                    top = buf[j];

                    if (!s1[i - 1].Equals(s2[j - 1])) dia = Math.Min(Math.Min(dia, top), left) + 1;

                    buf[j] = dia;
                    left = dia;
                    dia = top;
                }

                if (len2 == M) continue;

                if (!s1[i - 1].Equals(s2[M])) dia = Math.Min(dia, left) + 1;

                buf[M + 1] = dia;
            }

            return buf[len2];
        }
    }
}