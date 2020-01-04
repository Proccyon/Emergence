//-----Usage-----//
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
    //x: The x coordinate of the tile within the room. x=0 is left
    //y: The y coordinate of the tile within the room. y= 0 is the bottom (so x=0,y=0 is the bottom left corner of the room)
    //Sprite: The appearance of the tile. Should be square.
    //ActorOfTile: The Actor that is standing on the tile. No more than 1 actor can stand on a tile. Variable is Null if no actors are on the tile.
    //BlockOfTile: The Block standing on the tile (A table, Stone, etc.). No more than 1 block can stand on a tile. If the block is solid no actors can move to this tile (ActorOfTile should be None).

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
        public Tile(Sprite Sprite = null, string Name = "",Room RoomOfTile = null, int X = 0,int Y = 0, Actor ActorOfTile=null, Block BlockOfTile=null, Dictionary<Vector2Int, Wall> WallDict = null)
        {

            this.Sprite = Sprite;
            this.Name = Name;

            this.RoomOfTile = RoomOfTile;
            this.X = X;
            this.Y = Y;
            this.ActorOfTile = ActorOfTile;
            this.BlockOfTile = BlockOfTile;
            if(WallDict == null) //If no WallDict is given
            {
                this.WallDict = new Dictionary<Vector2Int, Wall>(Wall.EmptyWallDict); 
            }
            else
            {
                this.WallDict = WallDict;
            }

        }


        //Returns a new tile instance identical to this one. NewTile is placed in given NewRoom at NewX,NewY.
        public Tile Copy(Room NewRoom,int NewX=-1,int NewY=-1)
        {
            //If x or y is not given set NewX,NewY to X,Y of old tile
            if(NewX <= 0 || NewY <= 0)
            {
                NewX = this.X;
                NewY = this.Y;
            }
            //Create new tile
            Tile NewTile = new Tile(this.Sprite, this.Name,NewRoom, NewX, NewY);

            //Creates a copy of Actor and block and moves it to NewTile(if they exist)
            if(this.ActorOfTile != null)
            {
                Actor NewActor = this.ActorOfTile.Copy(NewTile);
            }
            if (this.BlockOfTile != null)
            {
                Block NewBlock = this.BlockOfTile.Copy(NewTile);
            }

            return NewTile;

        }
    }

    //Add comments later
    public class TileSpawner
    {
        public string Name;
        public Structure Structure;
        public int X;
        public int Y;

        public TileSpawner(string Name,Structure Structure, int X, int Y)
        {
            this.Name = Name;
            this.Structure = Structure;
            this.X = X;
            this.Y = Y;
        }

        //Spawns a tile at the given position in a room.
        public virtual void SpawnTile(Room Room, int TileX,int TileY)
        {
        }

    }

}





