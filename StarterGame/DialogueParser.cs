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
        private Character character;
  

        public DialogueParser(string txtfile, Character character)
        {
            this.txtfile = "@\"./" + txtfile + ".json\"";
            this.character = character;
        }

        public void readfile()
        {

            //opens up files and reads out Dialouge and player optiopns
            //kinda of too over complicated try to find a way
            //to remove dialogue class and just have
            //dailougeparser
            StreamReader reader = new StreamReader(txtfile);
            String json = reader.ReadToEnd();
            PlayerOptions dialogue = JsonConvert.DeserializeObject<PlayerOptions>(json); //puts everything in playeroptions //need to specfiiy lsit in jso file
            //check if player state is in dialouge or combat

            //Console.WriteLine(); //for testing purposes
            reader.Close();



        }


    }
  
}
