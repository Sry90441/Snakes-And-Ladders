using System.Security;

namespace Aale_und_Rolltreppen;
class GamePlay
{
    GameField _gameField;
    GameField.Player _player1;
    //GameField.Player _player2;
    int _round; 
    bool _dice_system_self;  


    public GamePlay(GameField gameField, GameField.Player player1 /*, gameField.Player player2*/ )
    {
        _round = 0;
        _gameField = gameField;
        _player1 = player1;     
        //_player2 = player2;
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
    public void MoveForward(GameField.Player player, int dicethrow )
    {
        GameField.Player.Throws
    }

}