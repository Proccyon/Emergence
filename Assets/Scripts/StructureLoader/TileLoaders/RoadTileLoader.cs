﻿//Usage: Creates an instance of a tile and places it in "SceneRoom" variable in the "RoomProperties" script in the "RoomControl" gameObject.
//It effectively turns the created scene into a Room instance.
//This code should be attached to a RoadTile prefab.

//Unity imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Game imports
using StructureSpace;
using TileSpace;
using GenericMethods;

public class RoadTileLoader : MonoBehaviour
{

    void Awake()
    {

        Methods.SentTileToArray(new RoadTile(),this);

    }

}
