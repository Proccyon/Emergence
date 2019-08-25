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

public class RoadTile : Tile
{
    public RoadTile(Room RoomOfTile = null, int X = 0, int Y = 0, Actor ActorOfTile = null, Block BlockOfTile = null)
    {
        this.RoomOfTile = RoomOfTile;
        this.X = X;
        this.Y = Y;
        this.ActorOfTile = ActorOfTile;
        this.BlockOfTile = BlockOfTile;

        this.FloorOfTile = Floor.RoadFloor;
    }

}