//Usage: Defines the GrassTile. Most of the space outside towns will be this tile.

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

    public class GrassTile : Tile
    {
        public GrassTile(Room RoomOfTile, int X=0, int Y=0, Actor ActorOfTile =null, Block BlockOfTile=null, Dictionary<Vector2Int, Wall> WallDict=null) 
            : base(Methods.LoadSprite("Scripts/Classes/Tile/ExtendedTiles/GrassTile/GrassFloor"),"GrassTile",RoomOfTile, X, Y, ActorOfTile, BlockOfTile, WallDict)
        {}
    }

    public class GrassTileSpawner : TileSpawner
    {
        public GrassTileSpawner(Structure Structure, int X, int Y)
        {
            this.Name = "GrassTileSpawner";
            this.PlaceSpawner(Structure, X, Y);
        }

        public override Tile SpawnTile(Room Room, int TileX, int TileY)
        {
            return new GrassTile(Room, TileX, TileY);
        }
    }
}