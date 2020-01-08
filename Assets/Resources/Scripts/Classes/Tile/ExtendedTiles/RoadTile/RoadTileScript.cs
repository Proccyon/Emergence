//Usage: Defines the RoadTile. Paths are made out of this tile. Perhaps in the future this tile will have a movement speed bonus.

//Unity imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script imports
using TileSpace;
using RoomSpace;
using ActorSpace;
using BlockSpace;
using WallSpace;
using StructureSpace;
using GenericMethods;


namespace TileSpace
{

    public class RoadTile : Tile
    {
        public RoadTile(Room RoomOfTile, int X = 0, int Y = 0, Dictionary<Vector2Int, Wall> WallDict = null, Actor ActorOfTile = null, Block BlockOfTile = null ) 
            : base(Methods.LoadSprite("Scripts/Classes/Tile/ExtendedTiles/RoadTile/RoadFloor"), "RoadTile",RoomOfTile,X,Y, WallDict,ActorOfTile, BlockOfTile)
        {}
    }

    public class RoadTileSpawner : TileSpawner
    {
        public RoadTileSpawner(Structure Structure, int X, int Y) 
            : base("RoadTileSpawner",Structure,X,Y)
        {}

        public override Tile SpawnTile(Room Room, int TileX, int TileY, Dictionary<Vector2Int, Wall> NewWallDict = null)
        {            
            return new RoadTile(Room, TileX, TileY,NewWallDict);
        }
    }
}