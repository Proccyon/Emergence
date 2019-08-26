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
using GenericMethods;

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
        public Sprite Sprite;
        public Container[] Inventory;
        public int InventorySize;
        public Tile TileOfActor;
        
        //Initializes an actor with an empty inventory. It's safer to add items after initialization.
        public Actor(string Name, Sprite Sprite, int InventorySize, Tile TileOfActor)
        {
            this.Name = Name;
            this.Sprite = Sprite;
            this.InventorySize = InventorySize;
            this.Inventory = new Container[InventorySize];
            //Fils inventory with empty containers
            for (int i = 0; i < InventorySize; i++)
            {
                Inventory[i] = new Container();
            }

            if (Methods.CanMoveActor(this, TileOfActor))
            {
                Methods.MoveActor(this, TileOfActor);
            }
            else
            {
                this.TileOfActor = null;
            }   
            
        }
       
        public Actor()
        {
            this.Name = "";
            this.Sprite = null;
            this.InventorySize = 0;
            this.TileOfActor = null;
            this.Inventory = new Container[0];

        }

        //Returns a new Actor instance identical to this one, placed on NewTile
        public Actor Copy(Tile NewTile)
        {
            //Create New actor instance
            Actor NewActor = new Actor(this.Name,this.Sprite, this.InventorySize, NewTile);

            //Checks if NewTile can hold this actor, otherwise return null
            if(!Methods.CanMoveActor(NewActor,NewTile))
            {
                return null;
            }

            //Copy all containers in the old inventory
            for (int i = 0; i < this.InventorySize; i++)
            {
                NewActor.Inventory[i] = this.Inventory[i].Copy();
            }
            return NewActor;
            
        }
        
    }


}


