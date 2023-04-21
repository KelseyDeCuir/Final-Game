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
                if (player.aptPoints > 0)
                {
                    Skills skills = player.aptitudeLvl;
                    if (this.SecondWord.Equals("health"))
                    {
                        skills.health += (int)Math.Ceiling(skills.health * .12);
                        player.aptPoints -= 1;
                        player.InfoMessage("Your health: " + skills.health + "\nAptitude Points Remaining: " + player.aptPoints);
                    }
                    else if (this.SecondWord.Equals("strength"))
                    {
                        skills.strength += 2;
                        player.aptPoints -= 1;
                        player.InfoMessage("Your strength: " + skills.strength + "\nAptitude Points Remaining: " + player.aptPoints);
                    }
                    else if (this.SecondWord.Equals("intelligence"))
                    {
                        skills.intelligence += 2;
                        player.aptPoints -= 1;
                        player.InfoMessage("Your intelligence: " + skills.intelligence + "\nAptitude Points Remaining: " + player.aptPoints);
                    }
                    else if (this.SecondWord.Equals("magic"))
                    {
                        skills.magic += 2;
                        player.aptPoints -= 1;
                        player.InfoMessage("Your magic: " + skills.magic + "\nAptitude Points Remaining: " + player.aptPoints);
                    }
                    else if (this.SecondWord.Equals("speed"))
                    {
                        skills.speed += 2;
                        player.aptPoints -= 1;
                        player.InfoMessage("Your speed: " + skills.speed + "\nAptitude Points Remaining: " + player.aptPoints);
                    }
                    else
                    {
                        player.WarningMessage("Cannot level " + this.SecondWord);
                    }
                    player.aptitudeLvl = skills;
                }
                else
                {
                    player.WarningMessage("No Apt Points to level with.");
                }
            }
            else
            {
                player.WarningMessage("Cannot level nothing.");
            }
            return false;
        }
    }
}