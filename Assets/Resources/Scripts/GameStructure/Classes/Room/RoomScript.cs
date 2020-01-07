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
using ActorSpace;

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
        public Room(Structure Structure)
        {
            this.Height = Structure.Height;
            this.Width = Structure.Width;
            this.TileArray = new Tile[Width, Height];

            for (int x = 0; x < Structure.Width; x++)
            {
                for (int y = 0; y < Structure.Height; y++)
                {
                    Tile NewTile = Structure.TileSpawnerArray[x, y].SpawnTile(this, x, y);
                    if (Structure.TileSpawnerArray[x, y].ActorSpawner != null)
                    {
                        Structure.TileSpawnerArray[x, y].ActorSpawner.SpawnActor(NewTile);
                    }
                    if(Structure.TileSpawnerArray[x, y].BlockSpawner != null)
                    {
                        Structure.TileSpawnerArray[x, y].BlockSpawner.SpawnBlock(NewTile);
                    }
                }
            }
        }
        //Creates an empty room of size (Width,Height) and fills it with grass tiles
        public Room(int Height=0, int Width=0)
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

        //Returns a list of all actors in the room
        public List<Actor> GetActorList()
        {

            List<Actor> ActorList = new List<Actor>();
            foreach(Tile Tile in this.TileArray)
            {
                if(Tile.ActorOfTile != null)
                {
                    ActorList.Add(Tile.ActorOfTile);
                }
            }
            return ActorList;
        }

        //Method that renders the room in the active scene. Will be deleted later.
        public List<GameObject> RenderRoom()
        {

            List<GameObject> SpriteObjectList = new List<GameObject>();

            foreach (Tile Tile in this.TileArray)
            {
                SpriteObjectList.Add(Methods.CreateSpriteObject(Tile.Sprite, Tile.X+0.5f, Tile.Y+0.5f,Tile.Name));
                if(Tile.ActorOfTile != null)
                {
                    SpriteObjectList.Add(Methods.CreateSpriteObject(Tile.ActorOfTile.Sprite, Tile.X + 0.5f, Tile.Y + 0.5f,Tile.ActorOfTile.Name,2));
                }
                if (Tile.BlockOfTile != null)
                {
                    SpriteObjectList.Add(Methods.CreateSpriteObject(Tile.BlockOfTile.Sprite, Tile.X + 0.5f, Tile.Y + 0.5f, Tile.BlockOfTile.Name, 1));
                }

            }
            return SpriteObjectList;

        }

    }

}


