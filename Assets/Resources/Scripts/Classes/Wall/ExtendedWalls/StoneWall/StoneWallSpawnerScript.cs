using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using StructureSpace;
using WallSpace;
using TileSpace;
using GenericMethods;


public class StoneWallSpawnerScript : CreateSpawnerScript
{

    public override void CreateSpawner(Structure Structure, float X, float Y)
    {
        Vector2Int Rotation = gameObject.GetComponent<WallGridScript>().WallRotation;
        new StoneWallSpawner(Structure,X,Y,Rotation);
    }
}
