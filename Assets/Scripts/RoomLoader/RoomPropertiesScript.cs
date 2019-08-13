﻿//Usage: Holds variables of the room in this scene. The heigth and width of the room are assigned in the editor.
//The instance of the room is held in this object.

//Unity imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Game imports
using RoomSpace;
using TileSpace;
using ActorSpace;

public class RoomPropertiesScript : MonoBehaviour
{

    public int RoomHeight; //The heigth of the room. Is set in the editor.
    public int RoomWidth; //The heigth of the room. Is set in the editor.
    public Room SceneRoom; //The Room instance.
    public List<GameObject> ObjectList;



    void Awake()
    {
        SceneRoom = new Room(RoomHeight, RoomWidth); //Creates room with empty grass tiles       
    }

    void Start()
    {

        ObjectList = SceneRoom.RenderRoom();


    }

    void Update()
    {



        foreach (GameObject SpriteObject in ObjectList)
        {
            GameObject.Destroy(SpriteObject);
        }

         ObjectList = SceneRoom.RenderRoom();

    }
}
