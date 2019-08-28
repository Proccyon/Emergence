﻿//-----Usage-----//
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
using WrapperSpace;
using GenericMethods;

namespace BlockSpace
{

    //Name: Name of the block.
    //Solid: If solid is true actors cannot be on the same tile as this block. If solid is false actors can stand an move through this block.
    //IsActive: Active blocks execute behaviour method simmilar to actors. Setting IsActive to false prevents this.
    //Sprite: The appearance of the block. Sprite is always square and the same size but can have any shape(by using transparant pixels).
    //TileOfBlock: The tile the block is standing on. Most  blocks won't move.
    public class Block
    {
        public string Name;
        public bool Solid;
        public bool IsActive; 
        public Sprite Sprite;
        public int TurnNumber;
        public Tile TileOfBlock;

        //Main constructor
        public Block(string Name, bool Solid, bool IsActive,Sprite Sprite, Tile TileOfBlock)
        {
            this.Name = Name;
            this.Solid = Solid;
            this.Sprite = Sprite;
            this.IsActive = IsActive;

            if(Methods.CanMoveBlock(this,TileOfBlock))
            {
                Methods.MoveBlock(this,TileOfBlock);
            }
            else
            {
                this.TileOfBlock = null;
            }

            //Add instance to list
            if(IsActive && TileOfBlock != null)
            {
                this.TurnNumber = RoomRunner.WrapperList.AddRandom(new ObjectWrapper(this));
            }


        }

        //Empty Constructor
        public Block()
        {
            this.Name = "";
            this.Solid = false;
            this.IsActive = false;
            this.Sprite = null;
            this.TileOfBlock = null;
        }

        //Returns a block instance identical to this one, placed at given tile. Returns null if tile is occupied.
        public Block Copy(Tile NewTile)
        {
            //Create new block instance
            Block NewBlock = new Block(this.Name, this.Solid,this.IsActive, this.Sprite, NewTile);

            //Returns null if NewTile is already filled
            if(!Methods.CanMoveBlock(NewBlock,NewTile))
            {
                return null;
            }

            return NewBlock;
        }

        //If IsActive is true, this methods runs each turn
        public virtual void Behaviour()
        {
        }


    }


}


