using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Ascension
{
    public class PlayerOptions
    {
        public string BodyTxt { get; set; }
        public string Endtxt { get; set; }
        public string NextTxtFile { get; set; }
        public List<OptionandEvent> optionsAndEvents = new List<OptionandEvent>();
        //could be state?

        public PlayerOptions(string BodyTxt, List<OptionandEvent> optionsAndEvents) {
            this.BodyTxt = BodyTxt;
            this.optionsAndEvents = optionsAndEvents;
        }

        //use observer based on option to notify to switch

        public void ReadBodyTxt() {
            Console.WriteLine(BodyTxt);
        }

        public void ReadOptions() {
            foreach (OptionandEvent e in optionsAndEvents)
            {
                Console.WriteLine("\n" + e.PlayerOp);
            }
        }

        public void ContinueDialouge() {
            if (NextTxtFile != null)
            {
                DialogueParser dialogueParser = new DialogueParser(NextTxtFile);
                dialogueParser.readfile();
            }
            else { Console.WriteLine("No more txt!"); }
        }

        public void EndDialouge() {
            
            Console.WriteLine(Endtxt);
            //Switch State to gameplay
        }

        public void setNewCharFile() { 
         //obserever on Dialouge end switchs npc currenttxtfile to nexttxtfile
        



        //should be called if you want to go to the next file without having to
        //have pc manually type in ContinueDialouge
        //how it goes:
        //uses an observer to check if dialouge has ended, recvies message that it has ended
        //AND no option to continue txt is present
        //sets char file so when you talk to npc again something new comes out

        
        }
        

        //TODO: AUTOMICALLY ADD A COMMAND TO A STATE IF IT CORRESPONDS WIth OPTION ADN EVENTS
        //if option+ event = true (options would be continue,fight)
        //state.command.add (option+event)

        //need something to play events based on option choosen.
        //we need to check if player can do a specfic event to npc

        //boss
        //enter fight play dialouge
        //on death play dialouge
        //in fight play dialouge
        //on kill(player death)

        //npc
        //depeends on enum personality
        //eneter fight(can fight)
        //on run(coward)
        //on npc death(all)
        
        //elevator attendeeeeeeeeeeee
        //USE OBSEREVERS
        //comments on player equipment,items,npcs killed,etc 
        //keep tracks of deaths
        //keeps track of floor
        //objectively most dialouge heavy 


    }
}
