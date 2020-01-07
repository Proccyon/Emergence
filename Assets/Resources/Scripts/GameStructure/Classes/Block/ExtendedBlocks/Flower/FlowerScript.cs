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
        public static Block ExampleInstance = new Flower();

        public Flower(Tile TileOfBlock = null) 
            : base("Flower",false,true, Methods.LoadSprite("Scripts/GameStructure/Classes/Block/ExtendedBlocks/Flower/FlowersSprite"),TileOfBlock)
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
            : base("FlowerSpawner", Methods.LoadSprite("Scripts/GameStructure/Classes/Block/ExtendedBlocks/Flower/FlowersSprite"),TileSpawner,false)
        {}

        public override void SpawnBlock(Tile Tile)
        {
            new Flower(Tile);
        }
    }
}