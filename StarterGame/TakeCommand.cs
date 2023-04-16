using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    
    class TakeCommand : Command
    {
        public TakeCommand() : base()
        {
            this.Name = "take";
        }
        public override bool Execute(Character player)
        {
            if (!this.HasSecondWord() || this.SecondWord.Equals("all"))
            {
                if (player.CurrentRoom.items.Count > 0)
                {
                    foreach (Item item in player.CurrentRoom.items)
                    {
                        player.Inventory.Add(item);
                        player.InfoMessage("You picked up " + item.Name);
                    }
                    player.CurrentRoom.items.Clear();
                }
                else
                {
                    player.WarningMessage("Nothing to pick up");
                }
            }
            else if (player.CurrentRoom.items.Exists(item => item.Name.ToLower().Equals(this.SecondWord)))
            {
                Item item = player.CurrentRoom.items.Find(item => item.Name.ToLower().Equals(this.SecondWord));
                player.Inventory.Add(item);
                player.InfoMessage("You picked up " + item.Name);
                player.CurrentRoom.items.Remove(item);
            }
            else
            {
                player.WarningMessage("Cannot pick up " + this.SecondWord);
            }
            return false;
        }
    }
}
