using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using StructureSpace;
using TileSpace;
using GenericMethods;


public class GrassTileSpawnerScript : CreateSpawnerScript
{

    public override void CreateSpawner(Structure Structure, int X, int Y)
    {
        new GrassTileSpawner(Structure,X,Y);
    }
}
