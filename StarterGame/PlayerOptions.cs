using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Ascension.PlayerCommands;
using Newtonsoft.Json;

namespace Ascension
{
    public class PlayerOptions
    {
        public Player player;
        public Character character;
        public string BodyTxt { get; set; }
        public string Endtxt { get; set; }

        public string NextTxtFile { get; set; } //when continueing dialouge
        public string? NewResponseTxtFile { get; set; } //when you end dialouge but want a new response after ending dialouge
        //is nullable 
        public List<OptionandEvent> optionsAndEvents = new List<OptionandEvent>();
        //could be state?

        public PlayerOptions(string BodyTxt, List<OptionandEvent> optionsAndEvents) {
          
            this.BodyTxt = BodyTxt;
            this.optionsAndEvents = optionsAndEvents;
            NotificationCenter.Instance.AddObserver("PlayerEndedDialouge", PlayerEndedDialouge);
           // NotificationCenter.Instance.AddObserver("PlayerContinuesDialouge", PlayerContinuesDialouge);
        }

        //use observer based on option to notify to switch

        public void ReadBodyTxt() {
            Console.WriteLine(BodyTxt);
        }

        public void ReadOptions() {
            if (optionsAndEvents != null)
            {
                clearoptions();
                foreach (OptionandEvent e in optionsAndEvents) //TODO: check if optionsandevents exist if it does then clear out Options and Events :)
                {
                    Console.WriteLine("\n" + e.PlayerOp);//need to add usuable options to list

                    CommandWords._commandArrayInDialogue.Append(e.DialougeEvent);
                }
            }
            else {
                Console.WriteLine("no options sadge");
                    }
  

        }

        public void clearoptions() {
          Array.Clear(CommandWords._commandArrayInDialogue,0, CommandWords._commandArrayInDialogue.Length);
          CommandWords._commandArrayInDialogue.Append(new EndDialougeCommand());

        }

        public void starter() {
            clearoptions();
            ReadBodyTxt();
            ReadOptions();
        
        }
       

        public void ContinueDialouge() {
            if (NextTxtFile != null)
            {
                clearoptions();
                DialogueParser dialogueParser = new DialogueParser(NextTxtFile);
                dialogueParser.readfile();

            }
            else { Console.WriteLine("Can't continue dialouge"); }
        }

        public void EndDialouge() {
            clearoptions();

            Console.WriteLine(Endtxt);
            //use observer
            Notification notification = new Notification("PlayerEndedDialouge", this);
            NotificationCenter.Instance.PostNotification(notification);
            player.State = States.GAME; //player is null have it not be null 
            //TODO: npc should only move in game state 
            //Switch State to gameplay but not here
        }


            //obserever on Dialouge end switchs npc currenttxtfile to nexttxtfile
          
            //should be called if you want to go to the next file without having to
            //have pc manually type in ContinueDialouge
            //how it goes:
            //uses an observer to check if dialouge has ended, recvies message that it has ended
            //AND no option to continue txt is present
            //sets char file so when you talk to npc again something new comes out
        
        public void PlayerEndedDialouge(Notification notification)
        {
            PlayerOptions playeroptions = (PlayerOptions)notification.Object;
            if (playeroptions != null)
            {
                Console.WriteLine("\n" + "Player ended dialouge");
                if (NewResponseTxtFile != null)
                {
                    character.generalDialouge = NewResponseTxtFile;
                    
                }
                else {
                    Console.WriteLine("Cannot not switch dialouge");
                }

            }
            else
            {
                Console.WriteLine("\n" + "Player ended dialouge without NPC file switch");
            }
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
