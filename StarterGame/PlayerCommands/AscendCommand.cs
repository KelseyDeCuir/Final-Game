using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    class AscendCommand : Command
    {
        public AscendCommand() : base()
        {
            this.Name = "ascend";
        }
        public override bool Execute(Character player)
        {
            if (GameWorld.Instance.floors[Elevator.Instance.floorLvl].Unlocked)
            {
                player.CurrentFloor = GameWorld.Instance.floors[Elevator.Instance.floorLvl];
                Elevator.Instance.floorLvl += 1;
                var pl = player as Player;
                if(pl != null)
                {
                    if (Elevator.Instance.floorLvl.Equals(4))
                    {
                        pl.WhenYouWin();
                        Notification notification = new Notification("YouWinTrue", this);
                        NotificationCenter.Instance.PostNotification(notification);
                    }
                }
            }
            else
            {
                player.ErrorMessage("That floor is still locked, you must beat any bosses left on this floor.");
            }
            return false;
        }
    }
}
