using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using StructureSpace;
using TileSpace;
using GenericMethods;


public class WoodTileSpawnerScript : CreateSpawnerScript
{

    public override void CreateSpawner(Structure Structure, float X, float Y)
    {
        new WoodTileSpawner(Structure, (int)X, (int)Y);
    }
}
