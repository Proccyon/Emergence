//Usage: Holds variables of the structure in this scene. The heigth and width of the room are assigned in the editor.
//The instance of the structure is held in this object.

//Unity imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Game imports
using StructureSpace;
using TileSpace;
using ActorSpace;

public class StructurePropertiesScript : MonoBehaviour
{

    public int StructureHeight; //The heigth of the structure. Is set in the editor.
    public int StructureWidth; //The heigth of the structure. Is set in the editor.
    public bool SpawnBorders = true; //Wheter or not black borders are created to indicate the edge of the structure
    public Structure SceneStructure; //The structure instance.
    public List<GameObject> ObjectList;

    //This should run before the Awake() of the TileLoaders. (set in Script Execution Order)
    void Awake()
    {
        SceneStructure = new Structure(StructureHeight, StructureWidth);
    }

}


public class CreateSpawnerScript : MonoBehaviour
{
    public virtual void CreateSpawner(Structure Structure, float x, float y)
    { }
}