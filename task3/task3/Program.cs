using System;

namespace task3
{
    class Program
    {
        static void Main(string[] args)
        {
            try{
                Game game = new Game(args);
                game.Start();
            } 
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);    
            }
            
        }
    }
}
