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

namespace BlockSpace
{
    public class Flower : Block
    {

        public Flower(Tile TileOfBlock = null)
        {
            this.Name = "Flower";
            this.Solid = false;
            this.Sprite = Methods.LoadSprite("Sprites/BlockSprites/Flowers");

            if (Methods.CanMoveBlock(this, TileOfBlock))
            {
                Methods.MoveBlock(this, TileOfBlock);
            }
            else
            {
                this.TileOfBlock = null;
            }
        }

    }
}