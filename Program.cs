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
    }
}
