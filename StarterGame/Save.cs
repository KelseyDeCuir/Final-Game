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
        String GameW;
        /* NOTE:
         * Because gameworld contains floors and floors store rooms
         * we just need to write gameworld onto a file in order to access a players game session
         * 
         * 
         * **/
   
        public SaveSystem(GameWorld game) {
            GameW = JsonConvert.SerializeObject(game, Formatting.Indented);
        }

        public void SaveGameWorld()
        {
            String path = "saveGame.Json"; // its possible for the player to create mulitple save files but lets keep it like this for now
            StreamWriter writer = new StreamWriter(path, false);
            writer.Write(GameW);
            writer.Close();
        }

        //TODO: load gameworld

    }

}
