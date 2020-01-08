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
        public RoadTile(Room RoomOfTile, int X = 0, int Y = 0, Actor ActorOfTile = null, Block BlockOfTile = null, Dictionary<Vector2Int, Wall> WallDict = null) 
            : base(Methods.LoadSprite("Scripts/Classes/Tile/ExtendedTiles/RoadTile/RoadFloor"), "RoadTile",RoomOfTile,X,Y,ActorOfTile,BlockOfTile,WallDict)
        {}
    }

    public class RoadTileSpawner : TileSpawner
    {
        public RoadTileSpawner(Structure Structure, int X, int Y)
        {
            this.Name = "RoadTileSpawner";
            this.PlaceSpawner(Structure, X, Y);
        }

        public override Tile SpawnTile(Room Room, int TileX, int TileY)
        {            
            return new RoadTile(Room, TileX, TileY);
        }
    }
}