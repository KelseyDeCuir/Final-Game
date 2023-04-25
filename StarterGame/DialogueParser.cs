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
  
        public String NPCStarter { get; set; }
        

        private string textFileName;
   
        public string PlayerOp { get; set; }

        public DialogueParser(string textFileName)
        {
            this.textFileName = "@\"./" + textFileName + ".json\"";
        }

        public void readfile()
        {

            //opens up files and reads out Dialouge and player optiopns
            //kinda of too over complicated try to find a way
            //to remove dialogue class and just have
            //dailougeparser
            StreamReader reader = new StreamReader(textFileName);
            String json = reader.ReadToEnd();
            PlayerOptions dialogue = JsonConvert.DeserializeObject<PlayerOptions>(json); //puts everything in playeroptions //need to specfiiy lsit in jso file
            Console.WriteLine();
            reader.Close();


            //try to write something that goes in a for loop
            //and recognizes 



        }


    }
  
}
