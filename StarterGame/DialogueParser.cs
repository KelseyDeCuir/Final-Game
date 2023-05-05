using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using System.Reflection;

namespace Ascension
{
    public class DialogueParser

       //TODO:CONVER TO SINGLETON :((((
    {

        //string text = File.ReadAllText(@"./person.json");

        //public String NPCStarter { get; set; }

        //public string PlayerOp { get; set; }
        private string txtfile;
        private Player player;
        private Character npc;
        private CombatDialogue cdialogue;
        private PlayerOptions pdialogue;

        //convert DP to singleton

        //private static DialogueParser _instance = null;
        //public static DialogueParser Instance
        //{
            //get
            //{
                //if (_instance == null)
                //{
              //      _instance = new DialogueParser("The Elevator", "Elevator Description");
            //    }
          //      return _instance;
        //    }
      //  }


        public DialogueParser(Player player, Character npc)
        {
            this.player = player;
            string resourceName = "MyLibrary.Resources."+npc.Name+"_Dialogue.json";
            var assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream(resourceName);
            this.npc = npc;
            this.txtfile =   npc.generalDialouge + ".json";
            NotificationCenter.Instance.AddObserver("ContinueDialouge", ContinueDialouge);
            NotificationCenter.Instance.AddObserver("EndDialouge", EndDialouge);
       

        }
        public DialogueParser(string txtfile) {
            this.txtfile = "@\"./" + txtfile + ".json\"";
            NotificationCenter.Instance.AddObserver("ContinueDialouge", ContinueDialouge);
            NotificationCenter.Instance.AddObserver("EndDialouge", EndDialouge);
        

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
            if (player.State == States.DIALOGUE) {
                pdialogue = JsonConvert.DeserializeObject<PlayerOptions>(json);
                pdialogue.player = player;
            }
            else if (player.State == States.COMBAT) {
                CombatDialogue cdialogue = JsonConvert.DeserializeObject<CombatDialogue>(json);
                //Console.Write("MY STATE CHANGED >:)");
                //observer based on which command is called 
                //example player types in continue dialouge
                //dp is notified by player and does continue dialouge

            }
            //Console.WriteLine(); //for testing purposes
            Console.WriteLine("I closed my txt file");
            reader.Close();


        }
        public void StartDialouge() {
            if (pdialogue != null)
            {
                pdialogue.ReadBodyTxt();
            }
            else { Console.WriteLine("Don't talk to me"); }
        }

        public void ContinueDialouge(Notification notification)
        {
            pdialogue.ContinueDialouge();

        }
        public void EndDialouge(Notification notification) { 
            pdialogue.EndDialouge();    

            
        }

        


    }
  
}
