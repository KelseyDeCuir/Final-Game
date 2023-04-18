using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    class EquipCommand : Command
    {
        public EquipCommand () : base()
        {
            this.Name = "equip";
        }
        public override bool Execute(Character player)
        {
            if (this.HasSecondWord()) {
                if (player.Inventory.Exists(item => item.Name.ToLower().Equals(this.SecondWord)))
                {
                    Item item = player.Inventory.Find(item => item.Name.ToLower().Equals(this.SecondWord));
                    var weapon = item as Weapon;
                    var armor = item as Armor;
                    if (weapon != null)
                    {
                        player.Inventory.Add(player.EquippedWeapon);
                        player.EquippedWeapon = weapon;
                        player.Inventory.Remove(item);
                        player.InfoMessage("You Equipped the weapon " + weapon.Name);
                    }
                    else if (armor != null)
                    {
                        player.Inventory.Add(player.EquippedArmor);
                        player.EquippedArmor = armor;
                        player.Inventory.Remove(item);
                        player.InfoMessage("You Equipped the weapon " + armor.Name);
                    }
                    else
                    {
                        player.WarningMessage("Cannot equip " + this.SecondWord + " it is neither weapon not armor.");
                    }
                }
                else
                {
                    player.WarningMessage("Could not find " + this.SecondWord);
                }
            }
            else
            {
                player.WarningMessage("Cannot equip the concept of nothing.");
            }
            return false;
        }
    }
}
