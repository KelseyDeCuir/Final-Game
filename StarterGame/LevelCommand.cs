using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    class LevelCommand : Command
    {
        public LevelCommand() : base()
        {
            this.Name = "level";
        }
        public override bool Execute(Character player)
        {
            if (this.HasSecondWord())
            {
                Skills skills = player.aptitudeLvl;
                if (this.SecondWord.Equals("health"))
                {
                    skills.health += (int)Math.Ceiling(skills.health*.12);
                }
                else if (this.SecondWord.Equals("strength"))
                {
                    skills.strength += 2;
                }
                else if (this.SecondWord.Equals("intelligence"))
                {
                    skills.intelligence += 2;
                }
                else if (this.SecondWord.Equals("magic"))
                {
                    skills.magic += 2;
                }
                else if (this.SecondWord.Equals("speed"))
                {
                    skills.speed += 2;
                }
                else
                {
                    player.WarningMessage("Cannot level " + this.SecondWord);
                }
                player.aptitudeLvl = skills;
            }
            else
            {
                player.WarningMessage("Cannot level nothing.");
            }
            return false;
        }
    }
}