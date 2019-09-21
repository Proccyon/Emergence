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
using GenericMethods;

public class GrassTile : Tile
{
    public GrassTile(Room RoomOfTile = null, int X = 0, int Y = 0, Actor ActorOfTile = null, Block BlockOfTile = null)
    {
        this.RoomOfTile = RoomOfTile;
        this.X = X;
        this.Y = Y;
        this.ActorOfTile = ActorOfTile;
        this.BlockOfTile = BlockOfTile;

        this.Sprite = Methods.LoadSprite("Scripts/GameStructure/Classes/Tile/ExtendedTiles/GrassTile/GrassFloor");
        this.Name = "GrassTile";
    }

}