﻿//Usage: Defines the WoodTile. This tile is common in buildings.

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



public class WoodTile : Tile
{
    public WoodTile(Room RoomOfTile, int X = 0, int Y = 0, Dictionary<Vector2Int, Wall> WallDict = null, Actor ActorOfTile = null, Block BlockOfTile = null) 
        : base(Methods.LoadSprite("Scripts/Classes/Tile/ExtendedTiles/WoodTile/WoodFloor"),"WoodTile",RoomOfTile,X,Y, WallDict,ActorOfTile, BlockOfTile)
    {}

}

public class WoodTileSpawner : TileSpawner
{
    public WoodTileSpawner(Structure Structure, int X, int Y) 
        : base("WoodTileSpawner",Structure,X,Y)
    {}

    public override Tile SpawnTile(Room Room, int TileX, int TileY, Dictionary<Vector2Int, Wall> NewWallDict = null)
    {
        return new WoodTile(Room, TileX, TileY,NewWallDict);
    }
}