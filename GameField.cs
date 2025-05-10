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
    public void EelOrEscalate(int FieldSize)
    {
        Random rnd = new Random();
        int random_minuss = rnd.Next(5);
        int random_add_min = rnd.Next(4);
        int decider = rnd.Next(2);
        int amount_E_E = rnd.Next(FieldSize/4-random_minuss);
        int amount_Eal;
        int amount_Escalator;
        if(decider == 1)
        {
            amount_Eal = amount_E_E / 2 + random_add_min;
            amount_Escalator = amount_E_E / 2 - random_add_min;
        }
        else
        {
            amount_Eal = amount_E_E / 2 - random_add_min;
            amount_Escalator = amount_E_E / 2 + random_add_min;
        }
    int gridSize = (int)Math.Sqrt(FieldSize);

    for (int i = 0; i < gridSize; i++)
    {
        for (int j = 0; j < gridSize; j++)
        {
            int index = i * gridSize + j;
            FieldNode currentNode = GetNodeAt(index);

            if (currentNode.Type == Type.Field)
            {
                if (i != 0 && amount_Eal > 0)
                {
                    currentNode.Type = Type.Eel;
                    amount_Eal--;
                }
                else if (i != gridSize - 1 && amount_Escalator > 0)
                {
                    currentNode.Type = Type.Escalator;
                    amount_Escalator--;
                }
            }

            if (amount_Eal <= 0 && amount_Escalator <= 0)
                break;
        }

        if (amount_Eal <= 0 && amount_Escalator <= 0)
            break;
    }
}

    private FieldNode GetNodeAt(int index)
    {
        FieldNode current = first;
        int currentIndex = 0;

        while (current != null && currentIndex < index)
        {
            current = current.Next;
            currentIndex++;
        }
        return current;
    }
}