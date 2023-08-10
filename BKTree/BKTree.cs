using System.Numerics;
using BKTree.Interfaces;

namespace BKTree
{
    public class BkTree<TW, TD> where TW : INumber<TW>
    {
        private readonly Node<TW, TD> _root;
        private readonly IDistanceCalculator<TW> _distanceCalculator;

        public BkTree(TW[] rootWord, TD data, IDistanceCalculator<TW> distanceCalculator)
        {
            _root = new Node<TW, TD>(rootWord, data);
            _distanceCalculator = distanceCalculator;
        }

        public void Add(TW[] word, TD data)
        {
            AddNode(_root, word, data, _distanceCalculator.CalculateDistance(_root.Word, word));
        }

        private static void AddNode(Node<TW, TD> node, TW[] word, TD data, int distance)
        {
            while (true)
            {
                if (!node.Children.ContainsKey(distance))
                {
                    node.Children[distance] = new Node<TW, TD>(word, data);
                }
                else
                {
                    node = node.Children[distance];
                    continue;
                }

                break;
            }
        }

        public List<(TW[] Word, TD Value, int distance)> Search(TW[] queryWord, int tolerance)
        {
            var result = SearchNode(_root, queryWord, tolerance);
            return result;
        }

        private List<(TW[] Word, TD Value, int distance)> SearchNode(Node<TW, TD> node, TW[] queryWord, int tolerance)
        {
            var result = new List<(TW[] Word, TD Value, int distance)>();
            var nodeQueue = new Queue<Node<TW, TD>>();
            nodeQueue.Enqueue(node);

            while (nodeQueue.Count > 0)
            {
                var currentNode = nodeQueue.Dequeue();
                var distance = _distanceCalculator.CalculateDistance(currentNode.Word, queryWord);

                if (distance <= tolerance)
                {
                    result.Add((currentNode.Word, currentNode.Data, distance));
                }

                foreach (var childDistance in currentNode.Children.Keys)
                {
                    if (Math.Abs(childDistance - distance) <= tolerance)
                    {
                        nodeQueue.Enqueue(currentNode.Children[childDistance]);
                    }
                }
            }

            return result;
        }
    }
}
