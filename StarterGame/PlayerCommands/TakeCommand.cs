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
            Player pl = (Player)player;
            if (!this.HasSecondWord() || this.SecondWord.Equals("all"))
            {
                if (player.CurrentRoom.items.Count > 0)
                {
                    foreach (Item item in player.CurrentRoom.items)
                    {
                        if (pl.heldWeight + item.Weight <= pl.WeightLimit && pl.heldVolume + item.Volume <= pl.VolumeLimit)
                        {
                            Notification notification = new Notification("ItemObtained", this);
                            NotificationCenter.Instance.PostNotification(notification);
                            player.InfoMessage("You picked up " + item.Name);
                            if (item.Found != true)
                            {
                                pl.XpUp(2);
                                item.Found = true;
                            }
                            player.Inventory.Add(item);
                            pl.heldWeight += item.Weight;
                            pl.heldVolume += item.Volume;
                        }
                        else
                        {
                            player.WarningMessage("Cannot fit " + item.Name + " into your inventory.");
                        }
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
                if (pl.heldWeight + item.Weight <= pl.WeightLimit && pl.heldVolume + item.Volume <= pl.VolumeLimit) {
                player.InfoMessage("You picked up " + item.Name);
                if (item.Found != true)
                {
                    pl.XpUp(2);
                    item.Found = true;
                }
                player.Inventory.Add(item);
                player.CurrentRoom.items.Remove(item);
                pl.heldWeight += item.Weight;
                pl.heldVolume += item.Volume;
            }
            else
            {
                player.WarningMessage("Cannot fit " + item.Name + " into your inventory.");
            }
        }
            else
            {
                player.WarningMessage("Cannot pick up " + this.SecondWord);
            }
            return false;
        }
    }
}
