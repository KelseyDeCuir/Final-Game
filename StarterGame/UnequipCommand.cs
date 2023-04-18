using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    class UnequipCommand : Command
    {
        public UnequipCommand() : base()
        {
            this.Name = "unequip";
        }
        public override bool Execute(Character player)
        {
            if (this.HasSecondWord())
            {
                if (this.SecondWord.Equals("armor"))
                {
                    if (player.EquippedArmor != null)
                    {
                        player.Inventory.Add(player.EquippedArmor);
                        player.InfoMessage("Unequipped " + player.EquippedArmor.Name);
                        player.EquippedArmor = null;
                    }
                    else
                    {
                        player.InfoMessage("Nothing equipped there.");
                    }
                }
                else if (this.SecondWord.Equals("weapon"))
                {
                    if (player.EquippedWeapon != null)
                    {
                        player.Inventory.Add(player.EquippedWeapon);
                        player.InfoMessage("Unequipped " + player.EquippedWeapon.Name);
                        player.EquippedWeapon = null;
                    }
                    else
                    {
                        player.InfoMessage("Nothing equipped there.");
                    }
                }
                else
                {
                    player.WarningMessage("Cannot unequip " + this.SecondWord);
                }
            }
            else
            {
                player.WarningMessage("Cannot Unequip emptyness...");
            }
            return false;
        }
    }
}
