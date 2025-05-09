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
    public int DiceThrow()
    {
        int dice = 0;

        if(Round == 0)
        {
            System.Console.WriteLine("Würfelst du selbst oder entscheidet das System?");
            string decision = Console.ReadLine();
            if((decision == "selbst" || decision == "ich" || decision == "wir" || decision == "würfeln") && decision != "System")
            {
                _dice_system_self = true;
            }
            else 
            { 
                _dice_system_self = false; 
            }
        }
        if(_dice_system_self == false)
        {
            Random rnd = new Random();
            dice = rnd.Next(1,7);
            System.Console.WriteLine($"You've rolled a: {dice}.");
            return dice;
        }
        else
        {   
            Console.Write("Gib die geworfene Zahl an: ");
            dice = Convert.ToInt32(Console.ReadLine());
        }
        return dice; 
    }
    public GameField.Player MoveForward(GameField.Player player, int dicethrow )
    {
        System.Console.Write($"You've moved from positon {player.Position} ");
        for(int i = 0; i < dicethrow; i++)
        {
            player.Position = player.Position.Next;
            player.Throws++;
        }
        System.Console.WriteLine($"to position {player.Position}.");
        return player;
    }
    public GameField.Player Eal_orLadder(GameField.Player player)
    { 
        if(player.Position.Type == Type.Escalator)
        {
            System.Console.WriteLine("You've landed on an Escalator.");
            System.Console.Write($"You've moved from positon {player.Position} ");
            for(int i = 0; i < 3; i++)
            {
                player.Position = player.Position.Next;
            }
            System.Console.WriteLine($"to position {player.Position}.");
        }
        else if(player.Position.Type == Type.Eel)
        {
            System.Console.WriteLine("You've landed on an Eel.");
            System.Console.Write($"You've moved from positon {player.Position} ");
            for(int i = 0; i < 3; i++)
            {
                player.Position = player.Position.Previous;
            }
            System.Console.WriteLine($"to position {player.Position}.");
        }
        return player;
    }
    public bool You_Win_Questionmark(GameField.Player player)
    {
        if (player.Position == _gameField.GetLast)
        {
            System.Console.WriteLine("You've won!");
            return true; 
        }
        return false;
    }
    
}