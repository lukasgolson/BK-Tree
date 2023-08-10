using BKTree;
using BKTree.Algorithms;

var rootSeed = -1;
var rootSequence = new byte[32];


var tree = new BkTree<byte,int>(rootSequence, rootSeed,new MyersByteDistanceCalculator());




for (var i = 0; i < 10000; i++)
{
    tree.Add(GenerateRandomByteArray(i, 32), i);
}


var x =tree.Search(GenerateRandomByteArray(500, 32), 29);

foreach (var tuple in x)
{
    
    var word = tuple.Word.Aggregate("", (current, b) => current + b.ToString("X2"));

    Console.WriteLine($"Word: {word}, Value: {tuple.Value}, Distance: {tuple.distance},");
}


static byte[] GenerateRandomByteArray(int seed, int arrayLength)
{
    var random = new Random(seed);
    var randomByteArray = new byte[arrayLength];

    for (var i = 0; i < arrayLength; i++)
    {
        randomByteArray[i] =
            (byte)random.Next(0, 101); // Generates a random value between 0 (inclusive) and 101 (exclusive)
    }

    return randomByteArray;
}