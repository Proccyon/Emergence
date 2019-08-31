 //Usage: Creates an instance of an Actor and places it in "SceneStructure" variable in the "StructureProperties" script in the "StructureControl" gameObject.
//This code should be attached to a Chicken prefab.

//-----Unity imports-----//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----Game imports-----//
using ActorSpace;
using GenericMethods;

public class ChickenLoader : MonoBehaviour
{

    void Awake()
    {

        Methods.SentActorToArray(new Chicken(), this);

    }

}
