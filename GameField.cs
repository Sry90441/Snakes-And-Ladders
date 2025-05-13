using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Transactions;
public enum Type 
{ 
    Eel,
    Escalator,
    Field,
}
class GameField
{
    internal class FieldNode
    {
        public Type Type {get; set; }
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
    public GameField.FieldNode GetLast
    {
        get { return last; }
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

    public void FieldNodeAddCurrentPosition(Player player) // adding 5 fieldnodes behind the current player position
    {
        for (int i = 0; i < 5; i++)
        {
            FieldNode newFieldNode = new FieldNode(null, null);
           
           player.Position.Previous = newFieldNode;
           newFieldNode.Previous = player.Position.Previous;
           player.Position.Previous.Next = newFieldNode;
           newFieldNode.Next = player.Position;
        }
    }
    public void EelOrEscalate(int fieldSize)
    {
        Random rnd = new Random();
        int rowSize = (int)Math.Sqrt(fieldSize);

        int amount_E_E = Math.Max(1, fieldSize / 4);
        int amount_Eal = rnd.Next(1, amount_E_E);
        int amount_Escalator = amount_E_E - amount_Eal;

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
        GameField.FieldNode currentPos = first;
        while(currentPos != null)
        {
            entireLength++;
            currentPos = currentPos.Next;
        }
        return entireLength;
    }   
    public FieldNode SearchRandomUnusedNode(int size)
    {
        FieldNode currentNode = first;
        Random rnd = new Random();
        int count = 0;
        while(currentNode.Type != Type.Field && count != 0)
        {
            int rand = rnd.Next(size);
            int rand2 = rnd.Next(size);
            if(rand < rand2)
            {
                currentNode = GetNodeAt(rand2, rand);
            }
            else
            {
            currentNode = GetNodeAt(rand, rand2);
            }
            if (currentNode.Type == Type.Field)
            {
                count++;
            }
        }
        return currentNode;
    }
    public FieldNode First
    {
        get {return first; }
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