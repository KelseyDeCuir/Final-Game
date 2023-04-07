﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Ascension
{
    class Floor
    {
        // 2-D array to simulate rooms in floor
        // Create a matrix to simulate a map of rooms
        public Room[,] FloorMap = new Room[2,3];
        private Room elevator;

        /*Constructor takes an array of names for the rooms and changes that into a list
         * which is used to randomly name each of the generated rooms that are added to the matrix */
        public Floor(String[] roomNames )
        {
            List<String> roomNamesList = roomNames.ToList();
            FloorMap[0, 0] = elevator;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Random rnd = new Random();
                    int index = rnd.Next(0, roomNamesList.Count);
                    FloorMap[i,j] = new Room(roomNamesList[index]);
                    roomNamesList.RemoveAt(index);
                }
            }
        }





    }
}
