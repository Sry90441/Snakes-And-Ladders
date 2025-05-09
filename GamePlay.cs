using System.Security;

namespace Aale_und_Rolltreppen;
class GamePlay
{
    GameField _gameField;
    int _round; 
    bool _dice_system_self;  

    public GamePlay(GameField gameField)
    {
        _round = 0;
        _gameField = gameField;
    }
    public int DiceThrow()
    {
        int dice = 0;

        if(_round == 0)
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
        for(int i = 0; i < dicethrow; i++)
        {
            player.Position = player.Position.Next;
        }
        return player;
    }
    public GameField.Player Eal_orLadder(GameField.Player player)
    { 
        if(player.Position.Type == Type.Escalator) //<~ fuck your mom
        {
            for(int i = 0; i < 3; i++)
            {
                player.Position = player.Position.Next;
            }
            
        }
        else if(player.Position.Type == Type.Eel)
        {
            for(int i = 0; i < 3; i++)
            {
                player.Position = player.Position.Previous;
            }
        }
        return player;
    }
    public bool You_Win_Questionmark(GameField.Player player)
    {
        
        if(player.Position == GameField.FieldNode.)
        {
            return true; 
        }
        return false;
    }
    
}