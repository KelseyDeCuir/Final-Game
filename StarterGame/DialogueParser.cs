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
        private Character npc;
        private CombatDialogue cdialogue;
        private PlayerOptions pdialogue;



        public DialogueParser(Player player, Character npc)
        {
            this.player = player;
            this.npc = npc;
            this.txtfile =   npc.generalDialouge + ".json";
            NotificationCenter.Instance.AddObserver("ContinueDialouge", ContinueDialouge);
            NotificationCenter.Instance.AddObserver("EndDialouge", EndDialouge);
            NotificationCenter.Instance.AddObserver("StartDialouge", StartDialouge);

        }
        public DialogueParser(string txtfile) {
            this.txtfile = "@\"./" + txtfile + ".json\"";
            NotificationCenter.Instance.AddObserver("ContinueDialouge", ContinueDialouge);
            NotificationCenter.Instance.AddObserver("EndDialouge", EndDialouge);
            NotificationCenter.Instance.AddObserver("StartDialouge", StartDialouge);

        }

        public void readfile()
        {
            //opens up files and reads out Dialouge and player optiopns
            //kinda of too over complicated try to find a way
            //to remove dialogue class and just have
            //dailougeparser
            Console.WriteLine(txtfile);
            StreamReader reader = new StreamReader(txtfile);
            String json = reader.ReadToEnd();
            //puts everything in playeroptions //need to specfiiy lsit in jso file

            //check if player state is in dialouge or combat
            if (player.State == States.DIALOGUE) {
                Console.WriteLine("Deserializing objects");
                 pdialogue = JsonConvert.DeserializeObject<PlayerOptions>(json);
                Console.WriteLine(pdialogue.BodyTxt);
                
            }
            else if (player.State == States.COMBAT) {
                CombatDialogue cdialogue = JsonConvert.DeserializeObject<CombatDialogue>(json);
                //Console.Write("MY STATE CHANGED >:)");
                //observer based on which command is called 
                //example player types in continue dialouge
                //dp is notified by player and does continue dialouge

            }
            //Console.WriteLine(); //for testing purposes
            reader.Close();


        }
        public void StartDialouge(Notification notification) {
            Console.WriteLine(txtfile);
            if (pdialogue != null)
            {
                pdialogue.starter();
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
