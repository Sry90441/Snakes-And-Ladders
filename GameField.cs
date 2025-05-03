using System.Linq.Expressions;
using System.Transactions;

class GameField
{
    internal class FieldNode
    {
        public bool Eel { get; set; }
        public bool Escalator { get; set; }
        public bool PlayerOnNode { get; set; }
        public FieldNode Next { get; set; }
        public FieldNode Previous { get; set; }

        public FieldNode(bool eel, bool escalator, FieldNode next, FieldNode previous)
        {
            Eel = eel;
            Escalator = escalator;
            Next = next;
            Previous = previous;
        }

    }

    internal class Player
    {
        public string Name;
        public int Throws;
        public FieldNode Position;
    }

    public GameField(int size)
    {
        for (int i = 0; i < size; i++)
        {
            FieldNode newFieldNode = new FieldNode(false, false, null, null);

            if (i == 0)
            {
                first = last = newFieldNode;
            }
            else
            {
                last.Next = newFieldNode;
                newFieldNode.Previous = last;
                last = newFieldNode;

                if (i % 5 == 0)
                {
                    newFieldNode.Eel = true;
                }
                if (i % 8 == 0)
                {
                    newFieldNode.Escalator = true;
                }
            }
        }
    }

    FieldNode first = null;
    FieldNode last = null;

    public bool PlayerTurn(Player player, int eyes)
    {
        FieldNode current = player.Position;

        for (int i = 0; i < eyes; i++)
        {
            current = current.Next;
        }

        player.Position = current;

        if (player.Position == last)    // Wincondition
        {
            return true;    
        }

        return false;
    }

}