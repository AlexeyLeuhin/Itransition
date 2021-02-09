using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    class Game
    {
        private string[] possibleSteps { get; }
        private Computer computer;
        private User user;
        private bool userMadeStep = false;
        public Game(string[] possibleSteps)
        {
            if(possibleSteps.Length % 2 != 1 
                || possibleSteps.Length < 3 
                || new SortedSet<string>(possibleSteps).Count() != possibleSteps.Length)
            {
                throw new Exception("Wrong steps. Emount of steps must be odd and at least 3.\n " +
                    "For example: rock paper scissors lizard Spock");
            }
            computer = new Computer();
            user = new User();
            this.possibleSteps = possibleSteps;
        }


        private bool UserWon()
        {         
            if(computer.step > user.step - 1 + possibleSteps.Length / 2)
            {
                return true;
            } else if(computer.step < user.step - 1)
            {
                if(user.step + possibleSteps.Length / 2 > possibleSteps.Length)
                {
                    return computer.step > (user.step - 1 + possibleSteps.Length / 2) % possibleSteps.Length;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public void Start()
        {
            while (true)
            {
                computer.MakeTurn(possibleSteps);
                GUI.ShowComputerMoveHmac(computer.hmac);
                GUI.ShowMenu(possibleSteps);
                GUI.RequireUsersTurn();
                userMadeStep = false;
                while (!userMadeStep)
                {
                    try
                    {
                        user.MakeTurn(possibleSteps);
                        userMadeStep = true;
                    }
                    catch
                    {
                        GUI.ShowMenu(possibleSteps);
                    }
                }
                if (user.step == 0)
                {
                    GUI.Exit();
                    break;
                }
                GUI.ShowUserMove(possibleSteps[user.step - 1]);
                GUI.ShowComputerMove(possibleSteps[computer.step]);
                if(computer.step == user.step - 1)
                {
                    GUI.ShowDraw();
                }
                else{
                    if (UserWon())
                    {
                        GUI.ShowUserWon();
                    }
                    else
                    {
                        GUI.ShowComputerWon();
                    }
                }
                
                GUI.ShowComputerMoveHMACKey(computer.secretKey);   
            }
        }
      
    }
}
