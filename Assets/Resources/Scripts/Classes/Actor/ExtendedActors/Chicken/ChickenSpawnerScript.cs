using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ActorSpace;
using StructureSpace;
using TileSpace;


public class ChickenSpawnerScript : CreateSpawnerScript
{

    public override void CreateSpawner(Structure Structure, float X, float Y)
    {
        new ChickenSpawner(Structure.TileSpawnerArray[(int)X,(int)Y]);
    }
}