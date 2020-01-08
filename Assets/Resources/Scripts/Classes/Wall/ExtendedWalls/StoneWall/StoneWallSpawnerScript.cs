using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using StructureSpace;
using WallSpace;
using TileSpace;
using GenericMethods;


public class StoneWallSpawnerScript : CreateSpawnerScript
{

    public override void CreateSpawner(Structure Structure, int X, int Y)
    {
        MonoBehaviour.print("jaap");
        Vector2Int Rotation = gameObject.GetComponent<WallGridScript>().WallRotation;
        TileSpawner FrontTileSpawner = WallSpawner.FindFrontTileSpawner(Structure, X, Y,Rotation);
        TileSpawner BackTileSpawner = WallSpawner.FindBackTileSpawner(Structure, X, Y, Rotation);
        new StoneWallSpawner(FrontTileSpawner, BackTileSpawner);
    }
}
