using System.Numerics;
using BKTree.Interfaces;

namespace BKTree.Algorithms;

public class LevenstheinDistance<T> : IDistanceCalculator<T> where T : INumber<T>
{
    public int CalculateDistance(T[] source, T[] pattern)
    {
        var dp = new int[source.Length + 1, pattern.Length + 1];

        for (var i = 0; i <= source.Length; i++)
        {
            dp[i, 0] = i;
        }

        for (var j = 0; j <= pattern.Length; j++)
        {
            dp[0, j] = j;
        }

        for (var i = 1; i <= source.Length; i++)
        {
            for (var j = 1; j <= pattern.Length; j++)
            {
                var cost = (source[i - 1] == pattern[j - 1]) ? 0 : 1;
                dp[i, j] = Math.Min(Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1), dp[i - 1, j - 1] + cost);
            }
        }

        return dp[source.Length, pattern.Length];
    }
}