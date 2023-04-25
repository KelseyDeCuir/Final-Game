using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    class EnchantCommand : Command
    {
        public EnchantCommand() : base()
        {
            this.Name = "enchant";
        }
        public override bool Execute(Character player)
        {
            if (this.HasSecondWord())
            {
                if (this.SecondWord.Equals("weapon"))
                {
                    if (player.EquippedWeapon != null)
                    {
                        player.InfoMessage("You enchanted " + player.EquippedWeapon.Name);
                        player.EquippedWeapon.enchanted = true;
                    }
                    else
                    {
                        player.WarningMessage("No Weapon to Enchant");
                    }
                }
                else if (this.SecondWord.Equals("armor"))
                {
                    if (player.EquippedArmor != null)
                    {
                        player.InfoMessage("You enchanted " + player.EquippedArmor.Name);
                        player.EquippedArmor.enchanted = true;
                    }
                    else
                    {
                        player.WarningMessage("No Armor to Enchant");
                    }
                }
                else
                {
                    player.WarningMessage("Cannot enchant that.");
                }
            }
            return false;
        }
    }
}
