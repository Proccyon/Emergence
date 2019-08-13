//-----Usage-----//
//Defines the Block class. A block is a non-sentient(exceptions possible) obstacle located on a tile.
//If the block is solid actors cannot move through the block (a stone, a log, etc). 
//If the block is not solid actors can stand on the same tile as a block (a carpet, sleeping bag, etc)
//Blocks are generally unable to move but actors can interact with some blocks (a chest, furnace, etc)
//Currently blocks cannot be destroyed and have no health property, but this might change in the future


//-----UnityImports-----//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----ScriptImports-----//
using TileSpace;
using RoomSpace;
using ItemSpace;

namespace BlockSpace
{

    //Name: Name of the block.
    //Solid: If solid is true actors cannot be on the same tile as this block. If solid is false actors can stand an move through this block.
    //Sprite: The appearance of the block. Sprite is always square and the same size but can have any shape(by using transparant pixels).
    //TileOfBlock: The tile the block is standing on. Most  blocks won't move.
    public class Block
    {
        public string Name;
        public bool Solid;
        public Sprite Sprite;
        public Tile TileOfBlock;

        
        public Block(string Name, bool Solid, Sprite Sprite, Tile TileOfBlock)
        {
            this.Name = Name;
            this.Solid = Solid;
            this.Sprite = Sprite;
            this.TileOfBlock = TileOfBlock;
        }



    }


}


