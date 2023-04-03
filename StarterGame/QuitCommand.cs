using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    /*
     * Spring 2023
     */
    public class QuitCommand : Command
    {

        public QuitCommand() : base()
        {
            this.Name = "quit";
        }

        override
        public bool Execute(Character player)
        {
            bool answer = true;
            if (this.HasSecondWord())
            {
                player.WarningMessage("\nI cannot quit " + this.SecondWord);
                answer = false;
            }
            return answer;
        }
    }
}
