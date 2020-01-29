using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BlockSpace;
using TileSpace;
using StructureSpace;


public class FlowerSpawnerScript : CreateSpawnerScript
{

    public override void CreateSpawner(Structure Structure, float X, float Y)
    {
        new FlowerSpawner(Structure.TileSpawnerArray[(int)X, (int)Y]);
    }
}
