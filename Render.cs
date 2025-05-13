using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Security;

namespace Aale_und_Rolltreppen;

class Render
{
    GameField _gameField;
    public Render(GameField gameField )
    {
        _gameField = gameField;
    }
    /*
    - [] Spielfeld mit Leiter
    - [] Spielgeld mit SChlange
    - [] Spielfeld mit spieler
    - [] Spielfeld mit Leiter und Spieler
    - [] Spielfeld mit Schlange und Spieler
    - [] Start 
    - [] Ende 
    - [] Loop 
    */
    public void Checkfornode(GameField.FieldNode currentnode, GameField.Player player1, GameField.Player player2 )
    {   
        switch(currentnode.Type)
        {
            case Type.Eel:
                if(currentnode == player1.Position)
                {
                    Console.Write("[S P1 ]");
                }
                else if(currentnode == player2.Position)
                {
                    Console.Write("[S P2 ]");
                }
                else
                {
                    Console.Write("[S    ]");
                }
            break;
            case Type.Escalator:
                if(currentnode == player1.Position)
                {
                    Console.Write("[H P1 ]");
                }
                else if(currentnode == player2.Position)
                {
                    Console.Write("[H P2 ]");
                }
                else
                {
                    Console.Write("[H    ]");
                }
            break;
            case Type.Field:
                if(currentnode == player1.Position)
                {
                    Console.Write("[  P1 ]");
                }
                else if(currentnode == player2.Position)
                {
                    Console.Write("[  P2 ]");
                }
                else
                {
                    Console.Write("[     ]");
                }
            break;
        }
    }

    public void PrintTheField(GameField gamefield, GameField.Player player1, GameField.Player player2 )
    {
        int size = gamefield.GetEntireLength(gamefield);
        GameField.FieldNode curNod = gamefield.First;
    
        for(int i = 0; i < size; i++)
        {
            if(i == 0)
            {
                Console.Write("[Start]");
            }
            else
            {
                Checkfornode(curNod,player1, player2);
                if(i%10 == 0)
                {
                    Console.WriteLine();
                }
            }
            curNod = curNod.Next;
        }
        Console.WriteLine();
        Console.WriteLine();
    }
}