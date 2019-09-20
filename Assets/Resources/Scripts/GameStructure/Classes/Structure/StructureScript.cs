//-----Usage-----//
//Defines the Structure class. Structures are read only versions of rooms. The objects in certain scenes will be read and turned into an instance of a scene.
//Rooms can be initialized using a structure. Structures are meant to be copied and placed inside a room. For example
//a Tree structure which consists of multiple blocks and tiles can be placed into a room multiple times.
//Variables of a structure should be readonly but the way scenes work dont allow it.

//-----UnityImports-----//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----GameImports-----//
using TileSpace;
using RoomSpace;

namespace StructureSpace
{
    //Heigt: The vertical length of the structure in tiles. Height = 4 implies the room is 4 tiles high.
    //Width: The horizontal length of the structure in tiles. Width = 4 implies the room is 4 tiles wide.
    //TileArray: A 2D array that holds the tiles in the structure. Same as in the room class.  
    public class Structure
    {
        public int Height;
        public int Width;
        public Tile[,] TileArray; //Readonly arrays dont work. TileArray[0,0] = x is still posible. We need it to work this way.

        //Constructor that fills the structure with empty grass tiles
        public Structure(int Height, int Width)
        {
            this.Height = Height;
            this.Width = Width;

            this.TileArray = new Tile[Width, Height];

            //Goes through each index of the array and fills it with grass tiles
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    //Adds grass tiles with no block or actor.
                    this.TileArray[x, y] = new GrassTile(null, x, y);
                }

            }
        }

        //Empty constructor
        public Structure()
        {
            this.Height = 0;
            this.Width = 0;
            this.TileArray = null;
        }



    }

}