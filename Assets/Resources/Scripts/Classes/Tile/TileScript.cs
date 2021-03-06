﻿//-----Usage-----//
//Defines the Tile class. Each square on the map corresponds to a tile. A tile can contain actors, items or blocks (Sometimes not all, depending on the item, character and block). 

 


//-----UnityImports-----//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----ScriptImports-----//
using RoomSpace;
using ActorSpace;
using BlockSpace;
using WallSpace;
using GenericMethods;
using StructureSpace;

namespace TileSpace
{

    //RoomOfTile: The room the tile is in (ex. above ground, inside a dungeon). Is null when Tile is part of a structure.
    //X: The x coordinate of the tile within the room. x=0 is left
    //Y: The y coordinate of the tile within the room. y= 0 is the bottom (so x=0,y=0 is the bottom left corner of the room)
    //Sprite: The appearance of the tile. Should be square.
    //ActorOfTile: The Actor that is standing on the tile. No more than 1 actor can stand on a tile. Variable is Null if no actors are on the tile.
    //BlockOfTile: The Block standing on the tile (A table, Stone, etc.). No more than 1 block can stand on a tile. If the block is solid no actors can move to this tile (ActorOfTile should be None).
    //WallDict: A dictionary that describes the walls next to this tile. The keys are 2d vectors. So WallDict[(0,1)] = WallDict[Vector2Int.up] gives the wall north of the tile
    public class Tile
    {

        public Sprite Sprite;
        public string Name;
        public Room RoomOfTile;
        public int X;
        public int Y;
        public Actor ActorOfTile;
        public Block BlockOfTile;
        public Dictionary<Vector2Int,Wall> WallDict;

        //Main constructor when Tile is added to a room. 
        public Tile(Sprite Sprite = null, string Name = "",Room RoomOfTile = null, int X = 0,int Y = 0, Dictionary<Vector2Int, Wall> WallDict = null, Actor ActorOfTile=null, Block BlockOfTile=null )
        {
            
            this.Sprite = Sprite;
            this.Name = Name;

            this.RoomOfTile = RoomOfTile;
            this.X = X;
            this.Y = Y;
            Methods.MoveActor(ActorOfTile, this);
            Methods.MoveBlock(BlockOfTile, this);

            if (RoomOfTile != null && Methods.IsInsideRoom(RoomOfTile, X, Y))
            {

                if (RoomOfTile.TileArray[X, Y] != null)
                {
                    RoomOfTile.TileArray[X, Y].RoomOfTile = null;
                }
                RoomOfTile.TileArray[X, Y] = this;
            }

            if (WallDict == null) //If no WallDict is given
            {
                this.WallDict = new Dictionary<Vector2Int, Wall>(Wall.EmptyWallDict);
            }
            else
            {
                this.WallDict = WallDict;
            }
        }

        //Checks if an actor can stand on a given tile
        public static bool CanStandOnTile(Tile Tile)
        {
            //Return false if Tile is set to null
            if (Tile == null)
            {
                return false;
            }

            //Return false if there is a solid block at the tile.
            if (Tile.BlockOfTile != null && Tile.BlockOfTile.Solid)
            {
                return false;
            }

            //returns true if no actor is present at Tile, returns false if there is already an actor present
            return (Tile.ActorOfTile == null);
        }

        //Checks if an actor can walk from OldTile to NewTile if they are next to each other (including diagonal)
        public static bool CanMoveBetweenTiles(Tile OldTile, Tile NewTile)
        {
            //Checks if Tiles are set correctly
            if(NewTile == null || OldTile == null || OldTile.RoomOfTile != NewTile.RoomOfTile || OldTile == NewTile)
            {
                return false;
            }

            //Checks if tiles are next to each other including diagonaly (distance between diagonal tiles is sqrt(2) =1.41)
            if (Methods.Distance(OldTile, NewTile) >= 1.5)
            {
                return false;
            }
            
            //Checks the NewTile is already occupied
            if(!CanStandOnTile(NewTile))
            {
                return false;
            }

            //The direction between the tiles seperated in a horizontal and vertical part
            Vector2Int HorizontalDirection = new Vector2Int(NewTile.X - OldTile.X, 0);
            Vector2Int VerticalDirection = new Vector2Int(0, NewTile.Y - OldTile.Y);

            //Returns false if there is a solid wall in the way
            return !(Wall.CheckIfSolidWall(OldTile, HorizontalDirection) || Wall.CheckIfSolidWall(OldTile, VerticalDirection));

        }

        //Checks if an actor can walk from OldTile to a tile (dx,dy) away
        public static bool CanMoveBetweenTiles(Tile OldTile, int dx, int dy)
        {
            if(OldTile == null || OldTile.RoomOfTile == null || !Methods.IsInsideRoom(OldTile.RoomOfTile,OldTile.X+dx,OldTile.Y+dy))
            {
                return false;
            }
            return CanMoveBetweenTiles(OldTile,OldTile.RoomOfTile.TileArray[OldTile.X+dx,OldTile.Y+dy]);
        }

    }

    //Add comments later
    public class TileSpawner
    {
        public string Name;
        public Structure Structure;
        public int X;
        public int Y;
        public ActorSpawner ActorSpawner;
        public BlockSpawner BlockSpawner;
        public Dictionary<Vector2Int, WallSpawner> WallSpawnerDict;

        public TileSpawner(string Name = "",Structure Structure = null, int X = 0, int Y = 0)
        {
            this.Name = Name;
            this.X = X;
            this.Y = Y;
            this.Structure = Structure;
            this.WallSpawnerDict = new Dictionary<Vector2Int, WallSpawner>(WallSpawner.EmptyWallSpawnerDict);


            if (Structure != null)
            {
                TileSpawner OldTileSpawner = Structure.TileSpawnerArray[X, Y];
                if (OldTileSpawner != null)
                {
                    //Moves the walls that where at OldTileSpaner to this tile
                    WallSpawner UpWallSpawner = OldTileSpawner.WallSpawnerDict[Vector2Int.up];
                    WallSpawner RightWallSpawner = OldTileSpawner.WallSpawnerDict[Vector2Int.right];
                    WallSpawner DownWallSpawner = OldTileSpawner.WallSpawnerDict[Vector2Int.down];
                    WallSpawner LeftWallSpawner = OldTileSpawner.WallSpawnerDict[Vector2Int.left];

                    if (UpWallSpawner != null)
                    {
                        Methods.MoveWallSpawner(UpWallSpawner, this, Vector2Int.up,UpWallSpawner.Rotation);
                    }
                    if (RightWallSpawner != null)
                    {
                        Methods.MoveWallSpawner(RightWallSpawner, this, Vector2Int.right, RightWallSpawner.Rotation);
                    }
                    if (DownWallSpawner != null)
                    {
                        Methods.MoveWallSpawner(DownWallSpawner, this, Vector2Int.down, DownWallSpawner.Rotation);
                    }
                    if (LeftWallSpawner != null)
                    {
                        Methods.MoveWallSpawner(LeftWallSpawner, this, Vector2Int.left, LeftWallSpawner.Rotation);
                    }

                    //If there already was a TileSpawner at (X,Y) with an ActorSpawner move the ActorSpawner to this
                    if (OldTileSpawner.ActorSpawner != null)
                    {
                        this.ActorSpawner = OldTileSpawner.ActorSpawner;
                        OldTileSpawner.ActorSpawner.TileSpawner = this;
                        OldTileSpawner.ActorSpawner = null;
                    }
                    //If there already was a TileSpawner at (X,Y) with a |BlockSpawner move the BlockSpawner to this
                    if (OldTileSpawner.BlockSpawner != null && (this.ActorSpawner == null || OldTileSpawner.BlockSpawner.Solid == false))
                    {
                        this.BlockSpawner = Structure.TileSpawnerArray[X, Y].BlockSpawner;
                        OldTileSpawner.BlockSpawner.TileSpawner = this;
                        OldTileSpawner.BlockSpawner = null;
                    }        
                    OldTileSpawner.Structure = null;
                }

                Structure.TileSpawnerArray[X, Y] = this;
            }
        }

        //Spawns a tile at the given position in a room.
        public virtual Tile SpawnTile(Room Room, int TileX,int TileY, Dictionary<Vector2Int, Wall> WallDict = null)
        {
            return null;
        }
    }
}





