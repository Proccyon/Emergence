//-----Usage-----//
//Defines the Room class. Rooms contain a 2D array which holds tile objects. 
//These tile objects can contain characters, items and blocks. 
//The room object is basically the map of the game. Rooms are shaped like a rectangle.


//-----UnityImports-----//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----ScriptImports-----//
using TileSpace;
using GenericMethods;
using StructureSpace;

namespace RoomSpace
{

    //Heigt: The vertical length of the room in tiles. Height = 4 implies the room is 4 tiles high.
    //Width: The horizontal length of the room in tiles. Width = 4 implies the room is 4 tiles wide.
    //TileArray: A 2D array that holds the tiles in the room.  This array will either be generated or read from a custom made scene. 
    public class Room
    {
        public int Height;
        public int Width;
        public Tile[,] TileArray;

        //Main constructor, Makes a room based on a (readonly) structure instance
        public Room (Structure Structure)
        {
            this.Height = Structure.Height;
            this.Width = Structure.Width;
            this.TileArray = Methods.CopyTileArray(Structure.TileArray, this);
        }


        //Creates an empty room of size (Width,Height) and fills it with grass tiles
        public Room(int Height, int Width)
        {
            this.Height = Height;
            this.Width = Width;

            TileArray = new Tile[Width, Height];

            //Goes through each index of the array and fills it with grass tiles
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    //Adds grass tiles with no block or actor.
                    TileArray[x, y] = new GrassTile(this, x, y);
                }

            }

        }

        //Constructor that sets everything to null
        public Room()
        {
            this.Height = 0;
            this.Width = 0;
            this.TileArray = null;
        }

        

        //Returns a new room instance that is identical to this one.
        public Room Copy()
        {
            //Creates a new empty room

            Room NewRoom = new Room();

            //Sets the variables of the new room
            NewRoom.Height = this.Height;
            NewRoom.Width = this.Width;
            NewRoom.TileArray = Methods.CopyTileArray(this.TileArray, NewRoom);

            return NewRoom;

        }


        //Method that renders the room in the active scene. Will be deleted later.
        public List<GameObject> RenderRoom()
        {

            List<GameObject> SpriteObjectList = new List<GameObject>();

            foreach (Tile Tile in this.TileArray)
            {
                SpriteObjectList.Add(Methods.CreateSpriteObject(Tile.FloorOfTile.Sprite, Tile.X+0.5f, Tile.Y+0.5f));
            }
            return SpriteObjectList;

        }

    }

}


