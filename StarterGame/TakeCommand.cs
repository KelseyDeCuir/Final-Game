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
                    }
                    player.CurrentRoom.items.Clear();
                }
            }
            return false;
        }
    }
}
