using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    //Possibly allow first and last name
    public class CharNameCommand : Command
    {
        public CharNameCommand() : base()
        {
            this.Name = "name";
        }
        public override bool Execute(Character player)
        {
            if (this.HasSecondWord())
            {
                if (this.SecondWord.Equals(" ") || this.SecondWord.Equals(""))
                {
                    player.WarningMessage("Cannot enter a blank name");
                }
                else
                {
                    player.SetName(SecondWord);
                    player.State = States.ELEVATOR;
                    Command reflect = new ReflectCommand(); // Check with Prof Obando
                    reflect.Execute(player);
                }
            }
            else
            {
                player.WarningMessage("Cannot enter a blank name");
            }
            return false;
        }
    }
}
