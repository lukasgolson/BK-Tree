using BKTree.Interfaces;

namespace BKTree.Algorithms;

public class MyersByteDistanceCalculator: IDistanceCalculator<byte>
{
    public int CalculateDistance(byte[] source, byte[] pattern)
    {
        var score = pattern.Length;

        uint mv = 0;
        var pv = uint.MaxValue;
        var last = (uint)1 << (pattern.Length - 1);

        var peq = new uint[256];

        for (var i = 0; i < pattern.Length; i++)
        {
            var value = pattern[i];
            peq[value] |= (uint)1 << i;
        }

        foreach (var t in source)
        {
            var eq = peq[t];

            var xv = eq | mv;
            var xh = (((eq & pv) + pv) ^ pv) | eq;

            var ph = mv | ~ (xh | pv);
            var mh = pv & xh;

            if ((ph & last) != 0) score++;
            if ((mh & last) != 0) score--;

            ph = (ph << 1) | 1;
            mh = (mh << 1);

            pv = mh | ~ (xv | ph);
            mv = ph & xv;
        }

        return score;
    }
}