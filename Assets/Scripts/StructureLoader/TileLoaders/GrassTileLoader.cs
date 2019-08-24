//Usage: Creates an instance of a tile and places it in "SceneRoom" variable in the "RoomProperties" script in the "RoomControl" gameObject.
//It effectively turns the created scene into a Room instance.
//This code should be attached to a GrassTile prefab.

//Unity imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Game imports
using RoomSpace;
using TileSpace;

public class GrassTileLoader : MonoBehaviour
{

    GameObject RoomControl;
    Room SceneRoom;
    Tile Tile;
    int x;
    int y;

    void Awake()
    {
        
        //Gets the position of the tile in the scene
        x = (int)(transform.position.x - 0.5f);
        y = (int)(transform.position.y - 0.5f);

        //Finds the RoomControl GameObject by name
        RoomControl = GameObject.Find("StructureControl");

        if (RoomControl != null)
        {
            //Gets the SceneRoom instance from the RoomProperties script
            SceneRoom = RoomControl.GetComponent<StructurePropertiesScript>().SceneRoom;
            
            //Creates a new Tile instance.
            Tile = new GrassTile(SceneRoom, x, y);

            //Add the Tile to the array of the Room instance
            SceneRoom.TileArray[x, y] = Tile;

        }

        //Destroys itself. A new GameObject will only be drawn if inside the camera (probably).
        Destroy(gameObject);

    }


}
