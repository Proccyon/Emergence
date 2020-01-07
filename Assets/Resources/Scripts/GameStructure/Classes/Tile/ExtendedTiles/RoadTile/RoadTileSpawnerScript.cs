using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using StructureSpace;
using TileSpace;
using GenericMethods;


public class RoadTileSpawnerScript : CreateSpawnerScript
{

    public override void CreateSpawner(Structure Structure, int X, int Y)
    {
        new RoadTileSpawner(Structure, X, Y);
    }
}
