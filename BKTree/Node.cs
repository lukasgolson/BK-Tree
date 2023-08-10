namespace BKTree;

internal class Node<TW,TD>
{
    public TW[] Word { get; }

    public TD Data { get; }
    
    
    public Dictionary<int, Node<TW,TD>> Children { get; } = new();

    public Node(TW[] word, TD data)
    {
        Word = word;
        Data = data;
    }
}