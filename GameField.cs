using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Transactions;
public enum Type
{
    Eel,
    Escalator,
    Field,
    Wormhole,
}
class GameField
{
    internal class FieldNode
    {
        public Type Type { get; set; }
        public bool PlayerOnNode { get; set; }
        public FieldNode Next { get; set; }
        public FieldNode Previous { get; set; }

        public FieldNode(FieldNode next, FieldNode previous, Type type = Type.Field, bool playerOnNode = false)
        {
            Type = type;
            Next = next;
            Previous = previous;
            PlayerOnNode = playerOnNode;
        }
    }
    internal class Player
    {
        public string Name;
        public int Throws;
        public FieldNode Position;
        public FieldNode LastPosition;
        public bool LandedOnWormhole;

        public Player(string name)
        {
            Name = name;
        }
    }

    public Player Player1 { get; private set; }
    public Player Player2 { get; private set; }

    public GameField(int size, string player1Name, string player2Name)
    {
        for (int i = 0; i < size; i++)
        {
            FieldNode newFieldNode = new FieldNode(null, null);

            if (i == 0)
            {
                first = last = newFieldNode;
            }
            else
            {
                last.Next = newFieldNode;
                newFieldNode.Previous = last;
                last = newFieldNode;
            }
        }
        Player1 = new Player(player1Name);
        Player2 = new Player(player2Name);
        Player1.Position = first;
        Player2.Position = first;
    }
    FieldNode first = null;
    FieldNode last = null;
    public FieldNode GetLast
    {
        get { return last; }
    }

    public FieldNode First
    {
        get { return first; }
    }

    public void FieldNodeAdd()  // adds 3 fieldnodes at the end
    {
        for (int i = 0; i < 3; i++)
        {
            FieldNode newFieldNode = new FieldNode(null, null);

            last.Next = newFieldNode;
            newFieldNode.Previous = last;
            last = newFieldNode;
        }
    }

    public bool FieldNodeAddCurrentPosition(Player player) // adding 5 fieldnodes behind the current player position
    {
        if (player.Position.Previous != null)
        {
            for (int i = 0; i < 5; i++)
            {
                FieldNode newFieldNode = new FieldNode(null, null);

                newFieldNode.Next = player.Position;
                newFieldNode.Previous = player.Position.Previous;
                player.Position.Previous.Next = newFieldNode;
                player.Position.Previous = newFieldNode;
            }
            return true;
        }
        else
        {
            return false;
        }
    }
    public void EelOrEscalate(int fieldSize)
    {
        Random rnd = new Random();
        int rowSize = (int)Math.Sqrt(fieldSize);

        int amount_E_E = Math.Max(1, fieldSize / 4);
        int amount_Eal = rnd.Next(1, amount_E_E);
        int amount_Escalator = amount_E_E - amount_Eal;

        int indexW = rnd.Next(rowSize, fieldSize - 1);   // add one wormhole at a random location
        GetNodeAt(indexW).Type = Type.Wormhole;

        int attempts = 0;
        while (amount_Eal > 0 && attempts < fieldSize * 5)
        {
            int index = rnd.Next(rowSize, fieldSize - 1); // vermeide erste Reihe und letztes Feld
            FieldNode candidate = GetNodeAt(index);

            if (candidate.Type == Type.Field)
            {
                candidate.Type = Type.Eel;
                amount_Eal--;
            }
            attempts++;
        }

        attempts = 0;
        while (amount_Escalator > 0 && attempts < fieldSize * 5)
        {
            int index = rnd.Next(1, fieldSize - rowSize); // vermeide letztes Reihe + Startfeld
            FieldNode candidate = GetNodeAt(index);

            if (candidate.Type == Type.Field)
            {
                candidate.Type = Type.Escalator;
                amount_Escalator--;
            }
            attempts++;
        }
    }

    public int GetEntireLength(GameField gameField)
    {
        int entireLength = 0;
        FieldNode currentPos = first;
        while (currentPos != null)
        {
            entireLength++;
            currentPos = currentPos.Next;
        }
        return entireLength;
    }
    public FieldNode SearchRandomUnusedNode(int size)
    {
        Random rnd = new Random();

        while (true)
        {
            int index = rnd.Next(1, size - 1);
            FieldNode currentNode = GetNodeAt(index);

            if (currentNode != null && currentNode.Type == Type.Field)
            {
                return currentNode;
            }
        }
    }
    private FieldNode GetNodeAt(int index, int startSearch = 0)
    {
        FieldNode current = first;
        int currentIndex = 0;
        while (current != null && currentIndex < startSearch)
        {
            current = current.Next;
            currentIndex++;
        }

        while (current != null && currentIndex < index)
        {
            current = current.Next;
            currentIndex++;
        }
        return current;
    }
}