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

        public List<string> BodyTxt;
        public int TXTnum = 0;
        public string? Endtxt { get; set; }
        public string? NewResponseTxtFile { get; set; }

        //could be state?

        public PlayerOptions(List<string> BodyTxt) {
          
            this.BodyTxt = BodyTxt;
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
            if (TXTnum >= 0 && TXTnum < BodyTxt.Count)
            {
                ReadBodyTxt();
            }
            else
            {
                Console.WriteLine("I have finished my dialouge"); //insert events here :) 

            }
        }
        public void End()
        {
            Console.WriteLine(Endtxt);
            //todo make observer for end dailouge 
            Console.WriteLine("\n" + "Player ended dialouge");
          

        }

    }

}

