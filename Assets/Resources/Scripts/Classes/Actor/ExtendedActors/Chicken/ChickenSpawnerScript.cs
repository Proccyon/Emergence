using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ActorSpace;
using StructureSpace;
using TileSpace;


public class ChickenSpawnerScript : CreateSpawnerScript
{

    public override void CreateSpawner(Structure Structure, int X, int Y)
    {
        new ChickenSpawner(Structure.TileSpawnerArray[X,Y]);
    }
}