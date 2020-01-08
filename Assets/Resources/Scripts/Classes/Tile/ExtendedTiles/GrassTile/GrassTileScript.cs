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
        public GrassTile(Room RoomOfTile, int X=0, int Y=0, Dictionary<Vector2Int, Wall> WallDict = null, Actor ActorOfTile =null, Block BlockOfTile=null ) 
            : base(Methods.LoadSprite("Scripts/Classes/Tile/ExtendedTiles/GrassTile/GrassFloor"),"GrassTile",RoomOfTile, X, Y, WallDict, ActorOfTile, BlockOfTile)
        {}
    }

    public class GrassTileSpawner : TileSpawner
    {
        public GrassTileSpawner(Structure Structure, int X, int Y) 
            : base("GrassTileSpawner",Structure,X,Y)
        {}

        public override Tile SpawnTile(Room Room, int TileX, int TileY, Dictionary<Vector2Int, Wall> NewWallDict = null)
        {
            return new GrassTile(Room, TileX, TileY,NewWallDict);
        }
    }
}