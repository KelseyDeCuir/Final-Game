﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Ascension
{
   public class SaveSystem
        //WILL BECOME STATIC POSSIBLY
    {
        String PlayerSave;
        //Simpleton for now

        public SaveSystem(Character player)
        {
            PlayerSave = JsonConvert.SerializeObject(player);
            //NOTE : the problem is with character and how chara current room is done
            //if i try to call character.currentRoom it returns a null point exception
        }

        //TODO: read save files names
        public void readPlayerSavesNames()
        { //stores inside bin which is stored inside another file


        }
        public void CheckIfOverwrite(String newSave) { 
            String path = newSave;
            if (!File.Exists(path))
            {

            }
            else { 
            //ask if want to over write
            //thrid word commands 
            //that file already exisr if  you wish to ovverwrite it 
            //do "save (filename) ovverwrite" command 
            //kicks you out if you dont type properly


            }
        
        }

        public void OverwriteSave(String newSave) //overwrite saves
        {
            String path = newSave + ".json"; 
            StreamWriter writer = new StreamWriter(path, false); //ture appends onto file
            writer.Write(PlayerSave); //if gameworld in player is set public it returns a null eorror in currernt room inside charat=cter>>>
            writer.Close();
        }


        //change to take txt file
        public void SavePlayerinfo()
        {
            //TODO: REVERT back FOR TESting
            //*String path = "SaveGame\\saveGame.json"; //needs to save in a specfic path
            //also eventually have it so player can name file
            //and check if player is overwriting file
            //*Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveGame"));
            //*StreamWriter writer = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path), false); //true appends onto file
            String path = "saveGame.json";
            StreamWriter writer = new StreamWriter(path,false);
            writer.Write(PlayerSave,Formatting.Indented);
            writer.Close(); 
        }

        public Player LoadPlayerinfo()
        {
            String path = "saveGame.json";
            StreamReader reader = new StreamReader(path);
            String json = reader.ReadToEnd();
            //NOTE: reader is reading object but deserliaing is problem
            //detecing empty list in item and no priting
            return JsonConvert.DeserializeObject<Player>(json);
          
            //turns words from json file into a list of stuff to be shoved into player
        }
  
        //TODO: load gameworld

    }

}