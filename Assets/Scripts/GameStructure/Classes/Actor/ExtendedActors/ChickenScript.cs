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

            if (Methods.CanMoveActor(this, TileOfActor))
            {
                Methods.MoveActor(this, TileOfActor);
            }
            else
            {
                this.TileOfActor = null;
            }
        }

    }




}


