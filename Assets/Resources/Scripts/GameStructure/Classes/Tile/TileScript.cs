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

        public TileSpawner(string Name = "",Structure Structure = null, int X = 0, int Y = 0)
        {
            this.Name = Name;
            PlaceSpawner(Structure, X, Y);
        }

        //Spawns a tile at the given position in a room.
        public virtual Tile SpawnTile(Room Room, int TileX,int TileY)
        {
            return null;
        }

        public void PlaceSpawner(Structure Structure, int X, int Y)
        {
            this.X = X;
            this.Y = Y;
            this.Structure = Structure;

            if (Structure != null)
            {
                if (Structure.TileSpawnerArray[X, Y] != null)
                {
                    if (Structure.TileSpawnerArray[X, Y].ActorSpawner != null)
                    {
                        this.ActorSpawner = Structure.TileSpawnerArray[X, Y].ActorSpawner;
                        Structure.TileSpawnerArray[X, Y].ActorSpawner.TileSpawner = this;
                        Structure.TileSpawnerArray[X, Y].ActorSpawner = null;
                    }

                    if (Structure.TileSpawnerArray[X, Y].BlockSpawner != null && (this.ActorSpawner == null || Structure.TileSpawnerArray[X, Y].BlockSpawner.Solid == false))
                    {
                        this.BlockSpawner = Structure.TileSpawnerArray[X, Y].BlockSpawner;
                        Structure.TileSpawnerArray[X, Y].BlockSpawner.TileSpawner = this;
                        Structure.TileSpawnerArray[X, Y].BlockSpawner = null;
                    }
                    Structure.TileSpawnerArray[X, Y].Structure = null;
                }
                
                Structure.TileSpawnerArray[X, Y] = this;
            }
        }
    }
}





