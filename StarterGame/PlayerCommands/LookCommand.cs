using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    class LookCommand : Command
    {
        /*
         * Shows the player the room they are in so they don't have to scroll up 
         */
        public LookCommand() : base()
        {
            this.Name = "look";
        }
        public override bool Execute(Character player)
        {
            player.CurrentRoom.MonsterAttack((Player)player);
            if (this.HasSecondWord())
            {
                if (this.SecondWord.Equals("room"))
                {
                    if (this.HasThirdWord())
                    {
                        if (player.CurrentRoom.items.Exists(item => item.Name.ToLower().Equals(this.Thirdword)))
                        {
                            Item item = player.CurrentRoom.items.Find(item => item.Name.ToLower().Equals(this.Thirdword));
                            player.NormalMessage("\nItem in Room:\n" + item.Name + ": " + item.Description);
                        }
                        else
                        {
                            player.WarningMessage("Could not find " + this.Thirdword);
                        }
                    }
                    else
                    {
                        player.NormalMessage("\n" + player.CurrentRoom.Description());
                    }

                }
                else if (this.SecondWord.Equals("inventory"))
                {
                    if (this.HasThirdWord())
                    {
                        if (player.Inventory.Exists(item => item.Name.ToLower().Equals(this.Thirdword)))
                        {
                            Item item = player.Inventory.Find(item => item.Name.ToLower().Equals(this.Thirdword));
                            player.NormalMessage("\nItem in Inventory:\n" + item.Name + ": " + item.Description);
                        }
                        else
                        {
                            player.WarningMessage("Could not find " + this.Thirdword);
                        }
                    }
                    else
                    {
                        player.NormalMessage("Inventory:\n" + player.GetInventory());
                    }
                }
                else
                {
                    if (player.Inventory.Exists(item => item.Name.ToLower().Equals(this.SecondWord)))
                    {
                        Item item = player.Inventory.Find(item => item.Name.ToLower().Equals(this.SecondWord));
                        player.NormalMessage("\nItem in Inventory:\n" + item.Name + ": " + item.Description);
                    }
                    else if (player.CurrentRoom.items.Exists(item => item.Name.ToLower().Equals(this.SecondWord)))
                    {
                        Item item = player.CurrentRoom.items.Find(item => item.Name.ToLower().Equals(this.SecondWord));
                        player.NormalMessage("\nItem in Room:\n" + item.Name + ": " + item.Description);
                    }
                    else
                    {
                        player.WarningMessage("Could not find " + this.SecondWord);
                    }
                }
            }
            else
            {
                player.NormalMessage("\n" + player.CurrentRoom.Description());
            }

            return false;
        }
    }
}
