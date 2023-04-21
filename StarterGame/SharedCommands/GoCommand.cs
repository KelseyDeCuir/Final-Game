using System.Collections;
using System.Collections.Generic;

namespace Ascension
{
    /*
     * Spring 2023
     */
    public class GoCommand : Command
    {

        public GoCommand() : base()
        {
            this.Name = "go";
        }

        override
        public bool Execute(Character player)
        {
            if (this.HasSecondWord())
            {
                player.WalkTo(this.SecondWord);
            }
            else
            {
                try
                {
                    Player pl = (Player)player;
                    if (pl != null)
                    {
                        pl.WarningMessage("\nGo Where?");
                    }
                }
                catch (System.InvalidCastException ex) {

                }
            }
            return false;
        }
    }
}
