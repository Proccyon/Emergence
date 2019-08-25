//-----Usage-----//
//Reads scenes with a given name and saves the information within is a static Structure instance.
//These can than be accesed through the game by writing SctructureLoader.StructureName.

//-----UnityImports-----//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//-----GameImports-----//
using StructureSpace;

public class StructureLoader : MonoBehaviour
{
    //Reads the scene with name SceneName and and sets the tiles in TileArray of Structure
    public void LoadStructure(string SceneName, Structure Structure)
    {
        //Loads the scene additively(GameObjects in loaded scene are added to current scene)
        AsyncOperation Op = SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);

        //op.completed is an event that triggers when scene is loaded. 
        Op.completed += (UsedOp => DoUnload(UsedOp, SceneName, Structure));
    }

    //Unloads the scene from LoadStructure and adds information to Structure instance
    public void DoUnload(AsyncOperation Op, string SceneName, Structure Structure)
    {
        //Finds the loaded scene
        Scene LoadedScene = SceneManager.GetSceneByName(SceneName);
        //Gets all objects in the scene
        GameObject[] ObjectArray = LoadedScene.GetRootGameObjects();
        //Finds object with name 'StructureContol'
        foreach (GameObject Object in ObjectArray)
        {
            if (Object.name == "StructureControl")
            {
                //Sets the variables of Structure instance
                Structure.Height = Object.GetComponent<StructurePropertiesScript>().StructureHeight;
                Structure.Width = Object.GetComponent<StructurePropertiesScript>().StructureWidth;
                Structure.TileArray = Object.GetComponent<StructurePropertiesScript>().SceneStructure.TileArray;
            }

        }

        SceneManager.UnloadSceneAsync(SceneName);
    }

    //-----Input-----//

    public static Structure OverworldStructure = new Structure();

    void Awake()
    {
        LoadStructure("OverworldScene",OverworldStructure);
    }



}
