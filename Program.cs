using System.Security;

namespace Aale_und_Rolltreppen;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Eels and Escalators");
        System.Console.WriteLine("How big should the GameField be?");
        int size = Convert.ToInt16(Console.ReadLine());
        System.Console.Write("Player 1 name: ");
        System.Console.Write("Player 2 name:");
        string player1Name = "Patrik";
        string player2Name = "Spongebob";
        GameField gamefield = new GameField(size, player1Name, player2Name);
        bool gameEnd = false;
        GamePlay gamePlay = new GamePlay(gamefield);
        while(gameEnd == false)
        {
            GameField.Player currentPlayer;
            if (gamePlay.Round % 2 == 0)
            {
                currentPlayer = gamefield.Player2;
            }
            else
            {
                currentPlayer = gamefield.Player1;
            }
            int dice = gamePlay.DiceThrow();
            gamePlay.MoveForward(currentPlayer, dice);
            gamePlay.Eal_orLadder(currentPlayer);
            gameEnd = gamePlay.You_Win_Questionmark(currentPlayer);
            gamePlay.Round++;
        }

    }
}
