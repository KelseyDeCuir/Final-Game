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
            Player pl = (Player)player;
            if (this.HasSecondWord())
            {
                if (this.SecondWord.Equals("armor"))
                {
                    if (player.EquippedArmor != null)
                    {
                        if (pl.heldWeight + player.EquippedArmor.Weight <= pl.WeightLimit && pl.heldVolume + player.EquippedArmor.Volume <= pl.VolumeLimit)
                        {
                            player.Inventory.Add(player.EquippedArmor);
                            player.InfoMessage("Unequipped " + player.EquippedArmor.Name);
                            pl.heldWeight += player.EquippedArmor.Weight;
                            pl.heldVolume += player.EquippedArmor.Volume;
                            player.EquippedArmor = null;
                        }
                        else
                        {
                            player.WarningMessage("Cannot fit " + player.EquippedArmor.Name + " into your inventory.");
                        }
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

                        if (pl.heldWeight + player.EquippedWeapon.Weight <= pl.WeightLimit && pl.heldVolume + player.EquippedWeapon.Volume <= pl.VolumeLimit)
                        {
                            player.Inventory.Add(player.EquippedWeapon);
                            player.InfoMessage("Unequipped " + player.EquippedWeapon.Name);
                            pl.heldWeight += player.EquippedWeapon.Weight;
                            pl.heldVolume += player.EquippedWeapon.Volume;
                            player.EquippedWeapon = null;
                        }
                        else
                        {
                            player.WarningMessage("Cannot fit " + player.EquippedWeapon.Name + " into your inventory.");
                        }
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
                player.WarningMessage("Cannot Unequip the emptyness you feel...");
            }
            return false;
        }
    }
}
