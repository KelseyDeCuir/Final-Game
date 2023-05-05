using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using Ascension.PlayerCommands;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ascension
{
    public class PlayerOptions
    {
        public Player player;
        public Character character;
        public List<string> BodyTxt;
        public int TXTnum = 0;
        public string Endtxt { get; set; }
        public string? NewResponseTxtFile { get; set; } //when you end dialouge but want a new response after ending dialouge
        //is nullable 

        //TODO: FREE TIME
        //public List<OptionandEvent> optionsAndEvents = new List<OptionandEvent>();
        //could be state?

        public PlayerOptions(List<string> BodyTxt)
        {
            this.TXTnum = 0;
            this.BodyTxt = BodyTxt;
            NotificationCenter.Instance.AddObserver("PlayerEndedDialouge", PlayerEndedDialouge);
            // NotificationCenter.Instance.AddObserver("PlayerContinuesDialouge", PlayerContinuesDialouge);
        }
        //use observer based on option to notify to switch

        public void ReadBodyTxt()
        {
            if (TXTnum >= 0 && TXTnum < BodyTxt.Count)
            {
                Console.WriteLine(BodyTxt[TXTnum]);
                TXTnum = TXTnum + 1;
            }
            else { Console.WriteLine("Cannot read anything"); } 
        }

        public void ContinueDialouge()
        {
            //TXTnum + 1
            if (TXTnum >= 0 && TXTnum < BodyTxt.Count)
            {
                ReadBodyTxt();
            }
            else { Console.WriteLine("I have finished my dialouge"); //insert events here :) 
            
            }
        }

        public void EndDialouge()
        {

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
                else
                {
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


        // public void ReadOptions() {
        //   if (optionsAndEvents != null)
        // {
        //   clearoptions();
        // foreach (OptionandEvent e in optionsAndEvents) //TODO: check if optionsandevents exist if it does then clear out Options and Events :)
        //{
        //  Console.WriteLine("\n" + e.PlayerOp);//need to add usuable options to list

        //                    CommandWords._commandArrayInDialogue.Append(e.DialougeEvent);
        //              }
        //        }
        //      else {
        //        Console.WriteLine("no options sadge");
        //          }


        // }

    }
}
