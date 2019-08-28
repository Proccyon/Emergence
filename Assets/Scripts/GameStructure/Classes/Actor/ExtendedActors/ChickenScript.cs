//-----Usage-----//
//Defines the Chicken class. Chickens walk around eating flowers.

//-----UnityImports-----//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----ScriptImports-----//
using TileSpace;
using ItemSpace;
using GenericMethods;
using WrapperSpace;

namespace ActorSpace
{

    public class Chicken : Actor
    {
        
        //Basic constructor
        public Chicken(Tile TileOfActor = null)
        {
            this.Name = "Chicken";
            this.InventorySize = 0;
            this.Inventory = new Container[0];
            this.Sprite = Methods.LoadSprite("Sprites/ActorSprites/Chicken");
            this.MaxEnergy = 100;
            this.Energy = this.MaxEnergy;

            if (Methods.CanMoveActor(this, TileOfActor))
            {
                Methods.MoveActor(this, TileOfActor);
            }
            else
            {
                this.TileOfActor = null;
            }

            if (TileOfActor.RoomOfTile != null)
            {
                this.TurnNumber = RoomRunner.WrapperList.AddRandom(new ObjectWrapper(this));
            }
        }

    }




}


