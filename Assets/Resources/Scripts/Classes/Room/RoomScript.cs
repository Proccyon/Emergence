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
using WallSpace;

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

        //Main constructor, Makes a room based on a structure instance
        public Room(Structure Structure)
        {
            this.Height = Structure.Height;
            this.Width = Structure.Width;
            this.TileArray = new Tile[Width, Height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    //Adds grass tiles with no block or actor.
                    TileArray[x, y] = new GrassTile(this, x, y);
                }

            }

            for (int x = 0; x < Structure.Width; x++)
            {
                for (int y = 0; y < Structure.Height; y++)
                {
                    TileSpawner TileSpawner = Structure.TileSpawnerArray[x, y];
                    Tile NewTile = TileSpawner.SpawnTile(this, x, y,this.TileArray[x,y].WallDict);
                    
                    if (TileSpawner.ActorSpawner != null)
                    {
                        TileSpawner.ActorSpawner.SpawnActor(NewTile);
                    }
                    if(TileSpawner.BlockSpawner != null)
                    {
                        TileSpawner.BlockSpawner.SpawnBlock(NewTile);
                    }

                    //This method excludes walls on the west/south side of the border, there shouldn't be walls there anyway
                    WallSpawner TopWallSpawner = TileSpawner.WallSpawnerDict[Vector2Int.up];
                    WallSpawner RightWallSpawner = TileSpawner.WallSpawnerDict[Vector2Int.right];

                    if(TopWallSpawner != null && TopWallSpawner.Rotation == Vector2Int.up)
                    {
                        TopWallSpawner.SpawnWall(this.TileArray[x, y + 1], this.TileArray[x, y]);
                    }
                    if(TopWallSpawner != null && TopWallSpawner.Rotation == Vector2Int.down)
                    {
                        TopWallSpawner.SpawnWall(this.TileArray[x, y], this.TileArray[x, y+1]);
                    }
                    if (RightWallSpawner != null && RightWallSpawner.Rotation == Vector2Int.right)
                    {
                        RightWallSpawner.SpawnWall(this.TileArray[x+1, y], this.TileArray[x, y]);
                    }
                    if (RightWallSpawner != null && RightWallSpawner.Rotation == Vector2Int.left)
                    {
                        RightWallSpawner.SpawnWall(this.TileArray[x, y], this.TileArray[x+1, y]);
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
                //Draws the Tile
                SpriteObjectList.Add(Methods.CreateSpriteObject(Tile.Sprite, Tile.X+0.5f, Tile.Y+0.5f,Tile.Name));
                if(Tile.ActorOfTile != null)
                {
                    //Draws the actor if there is one
                    SpriteObjectList.Add(Methods.CreateSpriteObject(Tile.ActorOfTile.Sprite, Tile.X + 0.5f, Tile.Y + 0.5f,Tile.ActorOfTile.Name,2));
                }
                if (Tile.BlockOfTile != null)
                {
                    //Draws the block if there is one
                    SpriteObjectList.Add(Methods.CreateSpriteObject(Tile.BlockOfTile.Sprite, Tile.X + 0.5f, Tile.Y + 0.5f, Tile.BlockOfTile.Name, 1));
                }

                Wall RightWall = Tile.WallDict[Vector2Int.right];
                Wall UpWall = Tile.WallDict[Vector2Int.up];               
                Wall LeftWall = Tile.WallDict[Vector2Int.left];
                Wall DownWall = Tile.WallDict[Vector2Int.down];


                if (RightWall != null)
                {
                    SpriteObjectList.Add(Methods.CreateSpriteObject(RightWall.WallSprite, Tile.X + 1f, Tile.Y + 0.5f, RightWall.Name, 3, RightWall.GetAngle()));
                }
                if (UpWall != null)
                {
                    SpriteObjectList.Add(Methods.CreateSpriteObject(UpWall.WallSprite, Tile.X + 0.5f, Tile.Y + 1f, UpWall.Name, 3, UpWall.GetAngle()));
                }
                if (LeftWall != null)
                {
                    SpriteObjectList.Add(Methods.CreateSpriteObject(LeftWall.WallSprite, Tile.X, Tile.Y + 0.5f, LeftWall.Name, 3, LeftWall.GetAngle()));
                }
                if (DownWall != null)
                {
                    SpriteObjectList.Add(Methods.CreateSpriteObject(DownWall.WallSprite, Tile.X + 0.5f, Tile.Y, DownWall.Name, 3, DownWall.GetAngle()));
                }

            }
            return SpriteObjectList; //This is returned so that all sprites can be destroyed on the next turn

        }

    }

}


