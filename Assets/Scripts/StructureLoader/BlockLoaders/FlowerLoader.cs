//Usage: Creates an instance of an Block and places it in "SceneStructure" variable in the "StructureProperties" script in the "StructureControl" gameObject.
//This code should be attached to a Flower prefab.

//-----Unity imports-----//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----Game imports-----//
using BlockSpace;
using GenericMethods;

public class FlowerLoader : MonoBehaviour
{

    void Awake()
    {

        Methods.SentBlockToArray(new  Flower(null,false), this);

    }

}
