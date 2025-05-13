using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.Swift;
using System.Security;

namespace Aale_und_Rolltreppen;
class GamePlay
{
    GameField _gameField;
    public int Round { get; set; }
    bool _dice_system_self;

    public GamePlay(GameField gameField)
    {
        Round = 0;
        _gameField = gameField;
    }
    public int DiceThrow(GameField.Player player)
    {
        int dice = 0;

        if (Round == 0)
        {
            System.Console.WriteLine("Würfelst du selbst oder entscheidet das System?");
            string decision = Console.ReadLine();
            if ((decision == "selbst" || decision == "ich" || decision == "wir" || decision == "würfeln") && decision != "System")
            {
                _dice_system_self = true;
            }
            else
            {
                _dice_system_self = false;
            }
        }
        if (_dice_system_self == false)
        {
            Random rnd = new Random();
            dice = rnd.Next(1, 7);
            System.Console.Write($"{player.Name} - You've rolled a: {dice}.");
            return dice;
        }
        else
        {
            Console.Write($"{player.Name}: Gib die geworfene Zahl an: ");
            dice = Convert.ToInt32(Console.ReadLine());
        }
        return dice;
    }
    public GameField.Player MoveForward(GameField.Player player, int dicethrow, GameField gamefield)
    {
        player.LastPosition = player.Position;
        if (dicethrow == 1) // Gamefield will be bigger, by 5 nodes at the end
        {
            System.Console.Write(" - adding 5 nodes at the end of the field!");
            gamefield.FieldNodeAdd();

        }

        if (dicethrow == 6) // Gamefield will be bigger behind you
        {
            if (gamefield.FieldNodeAddCurrentPosition(player))
            {
                System.Console.Write(" - adding 5 fieldnodes behind your position");
            }
            else
            {
                System.Console.Write("Can't add fieldnodes if Player is on the starting-field");
            }
        }

        for (int i = 0; i < dicethrow; i++)
        {
            if (player.Position.Next != null)   // so that you cant move out of the field at the end
            {
                player.Position.PlayerOnNode = false;
                player.Position = player.Position.Next;

            }
        }

        if (player.Position.PlayerOnNode == true)   // check if another player is on the end position of the throw
        {
            System.Console.Write(" - There is another player on this node, you will be moved back on field");
            player.Position = player.Position.Previous;
        }
        else
        {
            player.Position.PlayerOnNode = true;
        }

        player.Throws++;
        System.Console.WriteLine("\n");
        return player;
    }
    public GameField.Player Eal_orLadder(GameField.Player player, GameField gamefield) // or Wormhole
    {
        if (player.Position.Type == Type.Escalator)
        {
            System.Console.WriteLine("You've landed on an Escalator, moving forward 3 nodes");
            player.Position.PlayerOnNode = false;

            for (int i = 0; i < 3; i++) // move 3 nodes forward
            {
                player.Position = player.Position.Next;
            }
        }
        else if (player.Position.Type == Type.Eel)
        {
            System.Console.WriteLine("You've landed on an Eel, falling back 3 nodes");
            player.Position.PlayerOnNode = false;

            for (int i = 0; i < 3; i++) //move 3 nodes back
            {
                player.Position = player.Position.Previous;
            }
        }
        else if (player.Position.Type == Type.Wormhole) // wormhole teleports the player back or forwards and a limited amount of fields (random for each encounter)
        {
            Random rnd = new Random();
            int teleport = rnd.Next(1, gamefield.GetEntireLength(gamefield) / 3);
            int backOrForwad = rnd.Next(0, 2);

            System.Console.Write($"Teleporting {teleport} fieldnodes ");
            player.Position.PlayerOnNode = false;

            for (int i = 0; i < teleport; i++)
            {
                if (backOrForwad == 0)
                {
                    if (i == 0)
                    {
                        System.Console.WriteLine("back...");
                    }
                    if (player.Position.Previous != null)
                    {
                        player.Position = player.Position.Previous;
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        System.Console.WriteLine("forward...");
                    }
                    if (player.Position.Next != null)
                    {
                        player.Position = player.Position.Next;
                    }
                }
            }
        }
        return player;
    }

    public int MissingEyes(GameField.Player player, GameField gamefield) // calculates how much eyes are missing for reaching the last field and winning
    {
        int missingEyes = 0;
        GameField.FieldNode current = _gameField.GetLast;

        while (current != player.Position)
        {
            current = current.Previous;
            missingEyes++;
        }

        return missingEyes;
    }
    public bool You_Win_Questionmark(GameField.Player player) // win condition
    {
        if (player.Position == _gameField.GetLast || player.Position == null)
        {
            System.Console.WriteLine($"-!- {player.Name}, you've won! -!-");
            return true;
        }
        return false;
    }

    public GameField.FieldNode EalAndEscelateMover(GameField.Player player, GameField gameField)
    {
        GameField.FieldNode currentNode = player.LastPosition;
        GameField.FieldNode newNode;
        Type tempType;
        if (currentNode.Type == Type.Eel || Type.Escalator == currentNode.Type)
        {
            tempType = currentNode.Type;
            currentNode.Type = Type.Field;
            newNode = gameField.SearchRandomUnusedNode(gameField.GetEntireLength(gameField));
            newNode.Type = tempType;
        }
        else
        {
            return currentNode;
        }
        return newNode;
    }
}

/*
    Für Player: 
    → Last Poition if ladder / snake
*/