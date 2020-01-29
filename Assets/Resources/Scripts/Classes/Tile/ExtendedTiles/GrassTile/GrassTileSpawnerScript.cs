using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using StructureSpace;
using TileSpace;
using GenericMethods;


public class GrassTileSpawnerScript : CreateSpawnerScript
{

    public override void CreateSpawner(Structure Structure, float X, float Y)
    {
        new GrassTileSpawner(Structure,(int)X,(int)Y);
    }
}
