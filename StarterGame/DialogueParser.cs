using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using System.Reflection;

namespace Ascension
{
    public class DialogueParser
    {

        //string text = File.ReadAllText(@"./person.json");

        //public String NPCStarter { get; set; }

        //public string PlayerOp { get; set; }
        private string txtfile;
        private Player player;
        private Character npc;
        private CombatDialogue cdialogue;
        private PlayerOptions pdialogue;



        public DialogueParser(Character npc)
        {
            string resourceName = "MyLibrary.Resources."+npc.Name+"_Dialogue.json";
            var assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream(resourceName);
            this.npc = npc;
            this.txtfile = "@\"./" + npc.generalDialouge + ".json\"";
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
            if (player.State == States.COMBAT) {
                CombatDialogue cdialogue = JsonConvert.DeserializeObject<CombatDialogue>(json);
            }
            else if (player.State == States.DIALOGUE) {
                PlayerOptions pdialogue = JsonConvert.DeserializeObject<PlayerOptions>(json);
                pdialogue.starter();
                //observer based on which command is called 
                //example player types in continue dialouge
                //dp is notified by player and does continue dialouge

            }
            //Console.WriteLine(); //for testing purposes
            reader.Close();


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
