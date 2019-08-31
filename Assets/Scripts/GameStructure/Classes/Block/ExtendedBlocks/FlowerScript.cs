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

        float ReproduceChance = 0.1f;
        int ReproduceRange = 5;
        public static Block ExampleInstance = new Flower();

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

        public override Block Copy(Tile NewTile)
        {
            

            //Returns null if NewTile is already filled
            if (Methods.CanMoveBlock(Flower.ExampleInstance, NewTile))
            {
                
             return new Flower(NewTile);
            }
            return null;       
        }

        //Randomly places flowers in vicinity
        public override void Behaviour()
        {
            if(Random.Range(0f, 2f) <= ReproduceChance)
            {

                
                int x = TileOfBlock.X+Random.Range(-ReproduceRange, ReproduceRange + 1);
                int y = TileOfBlock.Y+Random.Range(-ReproduceRange, ReproduceRange + 1);

                if (Methods.CanMoveBlock(Flower.ExampleInstance,x,y,TileOfBlock.RoomOfTile) && TileOfBlock.RoomOfTile.TileArray[x,y].Name == "GrassTile")
                {

                    new Flower(TileOfBlock.RoomOfTile.TileArray[x, y]);
                }
            }
        }

    }
}