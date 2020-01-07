 //-----Usage-----//

//-----UnityImports-----//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----GameImports-----//
using RoomSpace;
using ActorSpace;
using TileSpace;
using BlockSpace;
using ActionSpace;
using StructureSpace;
using WrapperSpace;
using GenericMethods;

namespace WrapperSpace
{
    //Class that is used so that actors and blocks can be in the same list
    public class ObjectWrapper
    {
        public Actor Actor;
        public Block Block;

        public ObjectWrapper(Actor Actor)
        {
            this.Actor = Actor;
            this.Block = null;
        }

        public ObjectWrapper(Block Block)
        {
            this.Block = Block;
            this.Actor = null;

        }
    }

    //Adds an ObjectWrapper instance to a random position in a list
    public static class MyExtensions
    {
        public static int AddRandom(this List<ObjectWrapper> WrapperList, ObjectWrapper NewWrapper)
        {
            int RandomInt = (int)(Random.Range(0, WrapperList.Count + 1));
            WrapperList.Insert(RandomInt, NewWrapper);

            if(RoomRunner.IsRunning && RandomInt <= RoomRunner.RunCount) 
            {
                RoomRunner.RunCount += 1;
            }

            return RandomInt;
        }
    }

}



public class RoomRunner : MonoBehaviour
{


    public static List<ObjectWrapper> WrapperList = new List<ObjectWrapper>();//List of all actors and active blocks wrapped in a wrapper
    public static List<Room> RoomList =  new List<Room>(); //List of all existing rooms
    public List<GameObject> SpriteList = new List<GameObject>(); //List of all drawn sprite objects
    public static Room ActiveRoom; //The Room that is currently being drawn
    public static int RunCount = 0; //When running a turn, this is the 'i' equivalent of the for loop. It's static so AddRandom can acces it.
    public static bool IsRunning = false; //Wheter or not DoTurn is running
    public static int ActionLimit = 100; //Max amount of actions allowed to perform per turn
    public static int TurnCount = 0;

    public Structure OverworldStructure;

    void Awake()
    {
        OverworldStructure = Methods.LoadStructure("StructurePrefabs/OverworldPrefab");

        RoomList.Add(new Room(OverworldStructure)); //Add a room based on the overworld structure. Will add more rooms later
        ActiveRoom = RoomList[0];//Set the room that is being drawn. Later on this will depend on the player.
        
        SpriteList = ActiveRoom.RenderRoom(); //Draws the room. Destroy all objects in this List before redrawing
    }


    //Runs .Behaviour method for all actors and active blocks. These are stored in WrapperList
    public void DoTurn()
    {
        MonoBehaviour.print("Turn Done");
        TurnCount += 1;
        MonoBehaviour.print("TurnCount: "+TurnCount.ToString());
        int ActionCount = 0; //Used to prevent endless loop
        IsRunning = true; //Might read this in other scripts
        Action NewAction;

        for (RunCount = 0; RunCount < WrapperList.Count; RunCount++) //Goes through WrapperList
        {
            ObjectWrapper Wrapper = WrapperList[RunCount]; //Convenience

            //Remove Wrapper from list if it is empty or actor/block tiles are inconsistent.
            if
            (
            (Wrapper.Actor == null && Wrapper.Block == null)
            ||(Wrapper.Actor != null && (Wrapper.Actor.TileOfActor == null || Wrapper.Actor.TileOfActor.ActorOfTile != Wrapper.Actor))
            ||(Wrapper.Block != null && (Wrapper.Block.TileOfBlock == null || Wrapper.Block.TileOfBlock.BlockOfTile != Wrapper.Block))
            )
            {
                WrapperList.RemoveAt(RunCount);
                RunCount -= 1;
                continue;
            }

            //Checks if actor is set
            if(Wrapper.Actor != null)
            {
                
                ActionCount = 0; 
                Wrapper.Actor.TurnNumber = RunCount; // Sets turn order so actor can use this information
                Wrapper.Actor.Energy = Wrapper.Actor.MaxEnergy; //Resets energy to max

                //Keep running .Behaviour untill energy runs out
                while(Wrapper.Actor.Energy > 0)
                {
                    //Prevents endless loop, some actions might not cost energy
                    if(ActionCount >= ActionLimit)
                    {
                        print("Action Count exceeded limit for "+Wrapper.Actor.Name);
                        continue;
                    }

                    //Runs .Behaviour to get the action the actor wants to perform
                    NewAction = Wrapper.Actor.Behaviour();

                    //Checks if Actor has enought energy and nothing is preventing the action. Actors should check this themselves, this just prevents bugs
                    if(NewAction.EnergyCost <= Wrapper.Actor.Energy && NewAction.CanActivate(Wrapper.Actor))
                    {
                        //Performs the action
                        NewAction.Activate(Wrapper.Actor);
                        Wrapper.Actor.Energy -= NewAction.EnergyCost;
                    }
                    else
                    {
                        //Sends a message so the bug can be found
                        print(Wrapper.Actor.Name +" instance returned action that could not be activated!");
                        continue;
                    }
                    ActionCount += 1;
                }
            }

            //Blocks have no Action equivalent. .Behaviour does stuff itself
            if(Wrapper.Block != null)
            {
                Wrapper.Block.TurnNumber = RunCount;
                Wrapper.Block.Behaviour();
            }
        }

        IsRunning = false;

        //Draws the room
        foreach(GameObject Sprite in SpriteList)
        {
            Destroy(Sprite);
        }
        SpriteList = ActiveRoom.RenderRoom();

    }
}
