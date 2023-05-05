using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    public class CombatDialogue
    {

        //every fightable npc shares this combat dialouge
        //if you want a specific reaction for players death or something similar
        //set to whats needed or else keep it null;
        public Character character;
        public string FightEnterenceDialouge;
        public string onDeathDialouge;
        public string onPlayerDeathDialouge;
        public List<string> onPlayerHitDialouge; //when npc hits player
        public List<string> onPlayerMissDialouge; //when npc fails to hit player
        public List<string> onEnemyHitDialouge;  //when player hits npc
        public List<string> onEnemyMissDialouge; //when player fails to hit npc
        public List<string> onRunDialouge;

        Random rnd = new Random();

        //enter fight play dialouge
        //on death play dialouge
        //in fight play dialouge
        // on player hit
        // on enemy hit

        //on kill(player death)

        public CombatDialogue(Character character) //takes desrielized lines and puts inside list and strings
        {
            this.character = character;


        }


        /*TODO:
         * have default responses based on personality (fearfull,agressive,brace,etc)
         * allow these to be overwritten if chosen
         * how i want it to go:
         * character has a feaful personality so they use the fearful dialouge when hit however
         * ondeath rather than they say the standard feaful dialouge they would say something specific to them
         * 
         * **/


        //if null take characters personality dialouge

        public void onFightEnterence() {
            //notification code for when fight starts
            character.NormalMessage(FightEnterenceDialouge);    
        }
        public void onDeath()
        {
            character.NormalMessage(onDeathDialouge);
        }
        public void onPlayerDeath() {
            character.NormalMessage(onPlayerDeathDialouge);
        }
        public void onPlayerHit() {
            int index = rnd.Next(onPlayerHitDialouge.Count);
            character.NormalMessage(onPlayerHitDialouge[index]);
        }
        public void onPlayerMiss() {
            int index = rnd.Next(onPlayerMissDialouge.Count);
            character.NormalMessage(onPlayerMissDialouge[index]);
        }
        public void onEnemyHit() {
            int index = rnd.Next(onEnemyHitDialouge.Count);
            character.NormalMessage(onEnemyHitDialouge[index]);
        }
        public void onEnemyMiss()
        {
            int index = rnd.Next(onEnemyMissDialouge.Count);
            character.NormalMessage(onEnemyMissDialouge[index]);
        }

        public void onRun() {
            int index = rnd.Next(onRunDialouge.Count);
            character.NormalMessage(onRunDialouge[index]);
        }
  

    }
}
