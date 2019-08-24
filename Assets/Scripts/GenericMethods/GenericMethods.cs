 //-----Usage-----//
//Defines most of the methods used throughout the game.

//-----UnityImports-----//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----ScriptImports-----//
using TileSpace;
using ActorSpace;
using BlockSpace;
using ItemSpace;
using RoomSpace;

namespace GenericMethods
{

    //Stores all methods within this class.
    //There might be a more elegant way to store methods but this will do for now.
    public static class Methods
    {

        //-----Movement Methods-----//
        //Methods that move instances, for example move an actor to a new tile.

        //Checks if an actor can move to a given tile. Returns true if possible, returns false if not possible.
        public static bool CanMoveActor(Actor Actor, Tile NewTile)
        {
            //Return false if NewTile is set to null
            if (NewTile == null)
            {
                return false;
            }

            //Return false if there is a solid block at the new tile.
            if (NewTile.BlockOfTile != null && NewTile.BlockOfTile.Solid)
            {
                return false;
            }

            //returns true if no actor is present at NewTile, returns false if there is already an actor present
            return (NewTile.ActorOfTile == null);
        }

        //If Possible moves an actor to a given tile.
        public static void MoveActor(Actor Actor, Tile NewTile)
        {
            if (CanMoveActor(Actor, NewTile))
            {
                //Checks if Actor was previously on a tile
                if (Actor.TileOfActor != null)
                { 
                    Actor.TileOfActor.ActorOfTile = null; //Removes actor from old tile
                }
                Actor.TileOfActor = NewTile; //changes tile propery of the actor
                NewTile.ActorOfTile = Actor; //changes actor property of the new tile
            }
        }

        //Checks if a block can be moved to a given tile
        public static bool CanMoveBlock(Block Block,Tile NewTile)
        {
            
            //Return false if given tile is null
            if(NewTile == null)
            {
                return false;
            }

            //Return false if there is an actor at the new tile and block is solid
            if(NewTile.ActorOfTile != null && Block.Solid)
            {
                return false;
            }

            //Return true if NewTile contains no block, Return false if NewTile does contain a block
            return (NewTile.BlockOfTile == null);

        }
         
        public static void MoveBlock(Block Block, Tile NewTile)
        {
            //Checks if Block can indeed be moved to the new tile
            if(CanMoveBlock(Block,NewTile))
            {
                if(Block.TileOfBlock != null)
                {
                    Block.TileOfBlock.BlockOfTile = null; //Removes block of previous tile
                }
                Block.TileOfBlock = NewTile; //Changes tile property of Block
                NewTile.BlockOfTile = Block; //Changes block property of NewTile             

            }

        }



        //Checks if an item can be moved to a given container.
        public static bool CanMoveItem(Item Item, Container NewContainer)
        {
            return (NewContainer.ItemOfContainer == null); //Checks if the target container already holds an item.
        }

        //Moves an item to a new container.
        public static void MoveItem(Item Item, Container NewContainer)
        {
            if (CanMoveItem(Item, NewContainer))
            {
                if(Item.ContainerOfItem != null)
                {
                    Item.ContainerOfItem.ItemOfContainer = null;//Empties the container that used to hold the item.
                }
                
                Item.ContainerOfItem = NewContainer; //Changes the container of the item.
                NewContainer.ItemOfContainer = Item; //Changes the item of the new container.
            }
        }

        


        //-----Sprite methods-----//
        //Methods that do something with sprites.

        //Loads a png file from assets and returns it as a sprite. The png is seen as a texture somehow.
        //The png file should be in the "Recources" folder otherwise it gives a nullpointer exception.
        //Path is NOT just the path given when clicking "copy path" on an asset. Remove parts like this
        //Assets/Resources/Sprites/FloorSprites/GrassFloor.png ---> Sprites/FloorSprites/GrassFloor

        public static Sprite LoadSprite(string Path)
        {
            Texture2D MyTexture = Resources.Load(Path) as Texture2D;
            return Sprite.Create(MyTexture, new Rect(0, 0, MyTexture.width, MyTexture.height), new Vector2(0.5f, 0.5f));
        }

        public static GameObject CreateSpriteObject(Sprite Sprite,float x,float y)
        {
            
            GameObject SpriteObject = new GameObject("SpriteObject");
            SpriteObject.transform.position = new Vector3(x, y, 0);//Set position
            SpriteRenderer Renderer = SpriteObject.AddComponent<SpriteRenderer>(); //Add Sprite renderer
            Renderer.sprite = Sprite; //Set sprite

            //Adjust the scale so sprite is 100 by 100
            SpriteObject.transform.localScale *= 100 / Sprite.rect.width;
            return SpriteObject;
        }

        //-----OtherMethods-----//
        //Methods that do not fit a category

        //Make a copy of a tile array and set the room of all the tiles to TargetRoom
        public static Tile[,] CopyTileArray(Tile[,] TileArray, Room TargetRoom)
        {
            //Creates a new empty array
            Tile[,] NewTileArray = new Tile[TileArray.GetLength(0), TileArray.GetLength(1)];

            //Goes through all the old tiles
            foreach (Tile OldTile in TileArray)
            {
                //Creates a copy of each tile
                NewTileArray[OldTile.X, OldTile.Y] = OldTile.Copy(TargetRoom);
            }

            return NewTileArray;
        }

    }
}


