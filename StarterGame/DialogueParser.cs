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


        private string txtfile;
       
        private Player player;
        private CombatDialogue cdialogue;


        public Character character;
        public List<string> BodyTxt;
        public int TXTnum = 0;
        public string? Endtxt { get; set; }
        public string? NewResponseTxtFile { get; set; }

        private static DialogueParser _instance = null;
        public static DialogueParser Instance
        {
        get
        {
        if (_instance == null)
        {
              _instance = new DialogueParser();
            }
              return _instance;
            }
        }


        public void setCurrentItems(Player player, Character character) {
            this.player = player;
            this.character = character;
            string resourceName = "MyLibrary.Resources." + character.Name + "_Dialogue.json";
            var assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream(resourceName);
            this.txtfile = character.generalDialouge + ".json";

        }

        public void setnewtxtFile(string txtfile) {
            this.txtfile = "@\"./" + txtfile + ".json\"";

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
                _instance = JsonConvert.DeserializeObject<DialogueParser>(json);
              
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
            if (txtfile != null)
            {
                TXTnum = 0;
                ReadBodyTxt();
            }
            else { Console.WriteLine("Don't talk to me"); }
        }

        public void ReadBodyTxt() {
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
        public void EndDialouge() {
            Console.WriteLine(Endtxt);
          
                Console.WriteLine("\n" + "Player ended dialouge");
                if (NewResponseTxtFile != null)
                {
                    setnewtxtFile(NewResponseTxtFile);
                    character.generalDialouge = NewResponseTxtFile;

                }
                else
                {
                    Console.WriteLine("Player ended dialouge without file switch");
                }
          
        }

    }

    }
  
