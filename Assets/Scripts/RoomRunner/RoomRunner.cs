//-----Usage-----//

//-----UnityImports-----//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----GameImports-----//
using RoomSpace;
using ActorSpace;

public class RoomRunner : MonoBehaviour
{


    public static List<Actor> ActorList = new List<Actor>();
    public static List<Room> RoomList =  new List<Room>();
    public static Room ActiveRoom;
    public List<GameObject> SpriteList = new List<GameObject>();

    void Start()
    {
        RoomList.Add(new Room(StructureLoader.OverworldStructure));
        ActiveRoom = RoomList[0];
        List<GameObject> SpriteList = ActiveRoom.RenderRoom();
    }


    void Update()
    {

        //foreach(GameObject Sprite in SpriteList)
        //{
        //    Destroy(Sprite);
        //}
        //List<GameObject> SpriteList = ActiveRoom.RenderRoom();
    }
}
