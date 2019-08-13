//-----Usage-----//
//Defines the actor class. Actors are characters that move around the room like the player character, NPC's and monsters. Actors are always located on a single tile.


//-----UnityImports-----//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----ScriptImports-----//
using TileSpace;
using RoomSpace;
using ItemSpace;

namespace ActorSpace
{

    //Name: Name of the Actor.
    //Inventory: An array of containers. Each container holds a single item.
    //InventorySize: The maximum amount of items a character can carry. Expected to remain constant
    //FreeSpace: Current amount of free inventory slots of the character.
    //TileOfActor: The Tile the actor is standing on.
    public class Actor
    {
        public string Name;
        public Container[] Inventory;
        public int InventorySize;
        public int FreeSpace;
        public Tile TileOfActor;

        //Initializes an actor with an empty inventory. It's safer to add items after initialization.
        public Actor(string Name, int InventorySize, Tile TileOfActor)
        {
            this.Name = Name;
            this.Inventory = new Container[InventorySize];
            this.FreeSpace = InventorySize;
            this.InventorySize = InventorySize;
            this.TileOfActor = TileOfActor;
        }



    }


}


