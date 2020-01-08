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

        float ReproduceChance = 0.2f;
        int ReproduceRange = 5;
        public static Block ExampleInstance = new Flower(null);

        public Flower(Tile TileOfBlock) 
            : base(TileOfBlock,"Flower", Methods.LoadSprite("Scripts/Classes/Block/ExtendedBlocks/Flower/FlowersSprite"),false,true)
        {}

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

    public class FlowerSpawner : BlockSpawner
    {
        public FlowerSpawner(TileSpawner TileSpawner) 
            : base(TileSpawner,"FlowerSpawner", Methods.LoadSprite("Scripts/Classes/Block/ExtendedBlocks/Flower/FlowersSprite"),false)
        {}

        public override void SpawnBlock(Tile Tile)
        {
            new Flower(Tile);
        }
    }
}