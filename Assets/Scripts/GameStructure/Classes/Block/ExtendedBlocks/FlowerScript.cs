//-----Usage-----//
//Defines the flower block. Flowers can be eaten by chickens.

//-----UnityImports-----//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----GameImports-----//
using BlockSpace;
using TileSpace;
using GenericMethods;
using WrapperSpace;

namespace BlockSpace
{
    public class Flower : Block
    {

        public Flower(Tile TileOfBlock = null)
        {
            this.Name = "Flower";
            this.Solid = false;
            this.IsActive = true;
            this.Sprite = Methods.LoadSprite("Sprites/BlockSprites/Flowers");

            if (Methods.CanMoveBlock(this, TileOfBlock))
            {
                Methods.MoveBlock(this, TileOfBlock);
            }
            else
            {
                this.TileOfBlock = null;
            }

            //Add instance to list
            if (IsActive && TileOfBlock != null)
            {
                this.TurnNumber = RoomRunner.WrapperList.AddRandom(new ObjectWrapper(this));
            }
        }

        public override void Behaviour()
        {

        }

    }
}