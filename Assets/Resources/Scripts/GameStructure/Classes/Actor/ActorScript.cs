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
using WrapperSpace;
using ActionSpace;
using GenericMethods;
using StructureSpace;

namespace ActorSpace
{

    //Name: Name of the Actor.
    //Sprite: The image belonging to the actor
    //Inventory: An array of containers. Each container holds a single item.
    //InventorySize: The maximum amount of items a character can carry. Expected to remain constant
    //MaxEnergy: The amount of energy this actor can spend each turn on performing actions.
    //Energy: The current amount of energy. Each turn actors can perform actions untill this variable reaches 0.
    //TurnNumber: The order in which actors and active blocks use Behaviour. 0 is first, highest TurnNumber is last.
    //TileOfActor: The Tile the actor is standing on.
    public class Actor
    {
        public string Name;
        public Sprite Sprite;
        public Container[] Inventory;
        public int InventorySize;
        public float MaxEnergy;
        public float Energy;
        public int TurnNumber;
        public Tile TileOfActor;


        //Initializes an actor with an empty inventory. It's safer to add items after initialization.
        public Actor(string Name, Sprite Sprite, int InventorySize, float MaxEnergy = 100, Tile TileOfActor = null, bool AddToList = true)
        {
            this.Name = Name;
            this.Sprite = Sprite;
            this.InventorySize = InventorySize;
            this.MaxEnergy = MaxEnergy;
            this.Energy = MaxEnergy;
            this.Inventory = new Container[InventorySize];
            //Fils inventory with empty containers
            for (int i = 0; i < InventorySize; i++)
            {
                Inventory[i] = new Container();
            }

            //Moves actor to the given tile if possible
            if (Methods.CanMoveActor(this, TileOfActor))
            {
                Methods.MoveActor(this, TileOfActor);
            }
            else
            {
                this.TileOfActor = null;
            }

            //Add this instance to list of all actors and active blocks. AddToList is set to false when actor is added to structure
            if (AddToList)
            {
                this.TurnNumber = RoomRunner.WrapperList.AddRandom(new ObjectWrapper(this));
            }

        }

        public Actor()
        {
            this.Name = "";
            this.Sprite = null;
            this.InventorySize = 0;
            this.MaxEnergy = 0f;
            this.Energy = 0f;
            this.TileOfActor = null;
            this.Inventory = new Container[0];

        }

        //Returns a new Actor instance identical to this one, placed on NewTile
        public virtual Actor Copy(Tile NewTile)
        {
            //Create New actor instance
            Actor NewActor = new Actor(Name, Sprite, InventorySize, MaxEnergy);

            //Checks if NewTile can hold this actor, otherwise return null
            if (Methods.CanMoveActor(NewActor, NewTile))
            {
                Methods.MoveActor(NewActor, NewTile);
            }
            else
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

        //This method is ran each turn by all actors. Actors will have to overload it.
        public virtual Action Behaviour()
        {
            return new PassAction(this);
        }


    }
    //Name: Name of the ActorSpawner. Can be used to identify the type of ActorSpawner.
    //Sprite: Sprite if the ActorSpawner. Should be the same sprite as in the editor. Unlikely to be seen in game.
    //TileSpawner: Like an actor on a tile, the ActorSpawner is placed on a TileSpawner.
    public class ActorSpawner
        {

        public string Name;
        public Sprite Sprite;
        public TileSpawner TileSpawner;

        public ActorSpawner(string Name, Sprite Sprite, TileSpawner TileSpawner)
        {
            this.Name = Name;
            this.Sprite = Sprite;
            this.TileSpawner = TileSpawner;

        }

        //This is used to spawn an actor when creating a new room based on a structure, or place a structure inside an existing room.
        public virtual void SpawnActor(Tile Tile)
        {
        }

        }



}


