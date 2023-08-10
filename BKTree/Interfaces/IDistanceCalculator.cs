using System.Numerics;

namespace BKTree.Interfaces;

public interface IDistanceCalculator<in T> where T : INumber<T>
{
    int CalculateDistance(T[] source, T[] pattern);
}