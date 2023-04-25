using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    class DescendCommand : Command
    {
        public DescendCommand() : base()
        {
            this.Name = "descend";
        }
        public override bool Execute(Character player)
        {
            int floorNum = Elevator.Instance.floorLvl - 2;
            if (floorNum <= 0)
            {
                floorNum = GameWorld.Instance.floors.Count + floorNum;
            }
            if (GameWorld.Instance.floors[floorNum].Unlocked)
            {
                player.CurrentFloor = GameWorld.Instance.floors[floorNum];
                Elevator.Instance.floorLvl = floorNum+1;
                var pl = player as Player;
                if (pl != null)
                {
                    if (Elevator.Instance.floorLvl.Equals(4))
                    {
                        pl.WhenYouWin();
                        Notification notification = new Notification("YouWinGenocide", this);
                        NotificationCenter.Instance.PostNotification(notification);
                    }
                }
            }
            else
            {
                player.ErrorMessage(GameWorld.Instance.floors[floorNum] + " is still locked, you must beat any bosses left on this floor.");
            }
            return false;
        }
    }
}
