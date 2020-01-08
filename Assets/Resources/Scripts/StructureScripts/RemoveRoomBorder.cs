//Usage: Automatically destroys the room borders when the game runs.

//Unity imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveRoomBorder : MonoBehaviour
{

    void Awake()
    {
        //Destroys itself when game starts
        Destroy(gameObject);

    }

}
