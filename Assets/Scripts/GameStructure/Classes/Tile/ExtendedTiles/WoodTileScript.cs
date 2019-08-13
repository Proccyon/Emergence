//Usage: Defines the WoodTile. This tile is common in buildings.

//Unity imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script imports
using TileSpace;
using RoomSpace;
using ActorSpace;
using BlockSpace;

public class WoodTile : Tile
{
    public WoodTile(Room RoomOfTile, int X, int Y, Actor ActorOfTile=null, Block BlockOfTile=null)
    {
        this.RoomOfTile = RoomOfTile;
        this.X = X;
        this.Y = Y;
        this.ActorOfTile = ActorOfTile;
        this.BlockOfTile = BlockOfTile;

        this.FloorOfTile = Floor.WoodFloor;
    }

}
