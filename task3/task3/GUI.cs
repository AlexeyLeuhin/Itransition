using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    class GUI
    {

        public static void ShowMenu(string[] possibleSteps)
        {
            for(int i = 0; i < possibleSteps.Length; i++)
            {
                Console.WriteLine((i+1) + " - " + possibleSteps[i]);
            }
            Console.WriteLine(0 + " - exit");
        }

        public static void RequireUsersTurn()
        {
            Console.WriteLine("Enter your move: ");
        }

        public static void ShowUserMove(string move)
        {
            Console.WriteLine("Your move: " + move);
        }

        public static void ShowComputerMove(string move)
        {
            Console.WriteLine("Computer move: " + move);
        }

        public static void ShowComputerMoveHmac(byte[] hmac)
        {
            Console.WriteLine("HMAC: " + Convert.ToBase64String(hmac));
            
        }

        public static void ShowComputerMoveHMACKey(byte[] key)
        {
            Console.WriteLine("HMAC key: " + Convert.ToBase64String(key) + '\n');
        }

        public static void ShowUserWon()
        {
            Console.WriteLine("You win!!!");
        }
        public static void ShowComputerWon()
        {
            Console.WriteLine("Computer win(");
        }


        public static void ShowDraw()
        {
            Console.WriteLine("Draw, you have chosen the same move as computer!");
        }

        public static void Exit()
        {
            Console.WriteLine("GoodBye!");
        }
    }
}
