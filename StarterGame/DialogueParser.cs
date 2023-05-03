using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.IO;

namespace Ascension
{
    public class DialogueParser
    {

        //string text = File.ReadAllText(@"./person.json");

        //public String NPCStarter { get; set; }

        //public string PlayerOp { get; set; }
        private string txtfile;
        private Player player;
  

        public DialogueParser(string txtfile, Player player)
        {
            this.txtfile = "@\"./" + txtfile + ".json\"";
            this.player = player;
        }

        public void readfile()
        {

            //opens up files and reads out Dialouge and player optiopns
            //kinda of too over complicated try to find a way
            //to remove dialogue class and just have
            //dailougeparser
            StreamReader reader = new StreamReader(txtfile);
            String json = reader.ReadToEnd();
            //puts everything in playeroptions //need to specfiiy lsit in jso file

            //check if player state is in dialouge or combat
            if (player.State == States.COMBAT) {
                CombatDialogue dialogue = JsonConvert.DeserializeObject<CombatDialogue>(json);
                CombatDialogue CD = dialogue;
            }
            else if (player.State == States.DIALOGUE) {
                //PlayerOptions dialogue = JsonConvert.DeserializeObject<PlayerOptions>(json);
                //PlayerOptions PlyOp = dialogue;
            }


            //Console.WriteLine(); //for testing purposes
            reader.Close();



        }


    }
  
}
