using System;
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
   
        public SaveSystem(Character player) {
            PlayerSave = JsonConvert.SerializeObject(player, Formatting.Indented);
        }
        public void SavePlayerinfo()
        {
            String path = "saveGame.json"; //needs to save in a specfic path
            //also eventually have it so player can name file
            //and check if player is overwriting file
            StreamWriter writer = new StreamWriter(path, false); //ture appends onto file
            writer.Write(PlayerSave);
            writer.Close(); 
        }

        public void LoadPlayerinfo()
        {


        }

        //TODO: load gameworld

    }

}
