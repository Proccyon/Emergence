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
using WallSpace;
using ItemSpace;
using RoomSpace;
using WrapperSpace;
using StructureSpace;

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

        //Same method as above but with x,y coordinates
        public static bool CanMoveActor(Actor Actor,int x,int y)
        {
            //Checks if (x,y) is inside the room   
            if(IsInsideRoom(Actor.TileOfActor.RoomOfTile,x,y))
            {
                //Uses above method
                return CanMoveActor(Actor, Actor.TileOfActor.RoomOfTile.TileArray[x, y]);
            }
            return false;
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

        //Same method as above but with x,y coordinates
        public static void MoveActor(Actor Actor, int x, int y)
        {

            if(CanMoveActor(Actor,x,y))
            {
                Tile NewTile = Actor.TileOfActor.RoomOfTile.TileArray[x, y];

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

        public static bool CanMoveBlock(Block Block, int x, int y,Room ThisRoom =null)
        {
            if(ThisRoom == null)
            {
                ThisRoom = Block.TileOfBlock.RoomOfTile;
            }
            if (IsInsideRoom(ThisRoom, x, y))
            {
                return CanMoveBlock(Block,ThisRoom.TileArray[x,y]);
            }
            return false;
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

        //Checks if a wall can be moved to the edge between NewFrontTile and NewBackTile
        public static bool CanMoveWall(Wall Wall, Tile NewFrontTile, Tile NewBackTile)
        {

            //How NewFrontTile and NewBackTile are aligned(ex. FrontTile is right of BackTile --> Rotation = (1,0))
            Vector2Int Rotation = Wall.GetRotation(NewFrontTile, NewBackTile);

            if (Rotation == Vector2Int.zero) //Returns false if NewFrontTile and NewBackTile are not next to each other
            {
                return false;
            }

            //Checks if walls already exist at NewFrontTile and NewBackTile
            return NewFrontTile.WallDict[Rotation * -1] == null && NewBackTile.WallDict[Rotation] == null;

        }

        //Moves a wall to the edge between NewFrontTile and NewBackTile
        public static void MoveWall(Wall Wall, Tile NewFrontTile, Tile NewBackTile)
        {
            //Chekcs if wall can be moved at all
            if (CanMoveWall(Wall,NewFrontTile,NewBackTile))
            {
                //The rotation of the wall after it is moved
                Vector2Int NewRotation = Wall.GetRotation(NewFrontTile, NewBackTile);

                //Reference the wall in the new tiles
                NewFrontTile.WallDict[NewRotation * -1] = Wall;
                NewBackTile.WallDict[NewRotation] = Wall;

                //Remove the reference to the wall in the old tiles
                if (Wall.FrontTile != null)
                {
                    Wall.FrontTile.WallDict[Wall.Rotation * -1] = null;
                }
                if (Wall.BackTile != null)
                {
                    Wall.BackTile.WallDict[Wall.Rotation] = null;
                }

                //Reference the new tiles in the wall
                Wall.FrontTile = NewFrontTile;
                Wall.BackTile = NewBackTile;
                Wall.Rotation = NewRotation;
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

        //Creates a gameObject with the Sprite Sprite at position x,y
        public static GameObject CreateSpriteObject(Sprite Sprite,float x,float y,string Name = "SpriteObject",int SortingOrder = 0)
        {
            
            GameObject SpriteObject = new GameObject(Name);
            SpriteObject.transform.position = new Vector3(x, y, 0);//Set position
            SpriteRenderer Renderer = SpriteObject.AddComponent<SpriteRenderer>(); //Add Sprite renderer
            Renderer.sprite = Sprite; //Set sprite
            Renderer.sortingOrder = SortingOrder; //Sorting order is the height of the draw layer

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



        //-----LoaderMethods-----//

        //Places a tile instance in the TileArray of the StructureControl in the scene. Used in GrassTileLoader etc.
        public static void SentTileToArray(Tile Tile, Component LoaderScript)
        {
            //Gets the position of the tile in the scene
            int x = (int)(LoaderScript.gameObject.transform.position.x - 0.5f);
            int y = (int)(LoaderScript.gameObject.transform.position.y - 0.5f);

            Tile.X = x;
            Tile.Y = y;

            //Finds the RoomControl GameObject by name
            GameObject StructureControl = GameObject.Find("StructureControl");

            if (StructureControl != null)
            {
                //Gets the SceneRoom instance from the RoomProperties script
                Structure SceneStructure = StructureControl.GetComponent<StructurePropertiesScript>().SceneStructure;

                //Finds the actor and block already present in the structure
                Actor OldActor = SceneStructure.TileArray[x, y].ActorOfTile;
                Block OldBlock = SceneStructure.TileArray[x, y].BlockOfTile;

                if (OldActor != null )
                {
                    //If there was already an actor on the tile (due to wrong load order) move it to correct tile
                    Methods.MoveActor(OldActor, Tile);
                }

                if(OldBlock != null)
                {
                    //If there was already a block on the tile (due to wrong load order) move it to correct tile
                    Methods.MoveBlock(OldBlock, Tile);
                }

                //Add the Tile to the array of the Structure instance
                SceneStructure.TileArray[x, y] = Tile;

            }

            //Destroys itself. A new GameObject will only be drawn if inside the camera (probably).
            MonoBehaviour.Destroy(LoaderScript.gameObject);
        }

        //Places an actor instance on a tile in the TileArray of the StructureControl in the scene. Used in ChickenLoader etc.
        public static void SentActorToArray(Actor Actor, Component LoaderScript)
        {

            //Gets the position of the tile in the scene
            int x = (int)(LoaderScript.gameObject.transform.position.x - 0.5f);
            int y = (int)(LoaderScript.gameObject.transform.position.y - 0.5f);

            //Finds the RoomControl GameObject by name
            GameObject StructureControl = GameObject.Find("StructureControl");

            if (StructureControl != null)
            {
                //Gets the SceneRoom instance from the RoomProperties script
                Structure SceneStructure = StructureControl.GetComponent<StructurePropertiesScript>().SceneStructure;

                //Finds tile the actor is standing on and move Actor to it
                Methods.MoveActor(Actor, SceneStructure.TileArray[x, y]);


            }
            //Destroys itself. A new GameObject will only be drawn if inside the camera (probably).
            MonoBehaviour.Destroy(LoaderScript.gameObject);

        }

        //Places an Block instance on a tile in the TileArray of the StructureControl in the scene. Used in FLowerLoader etc.
        public static void SentBlockToArray(Block Block, Component LoaderScript)
        {

            //Gets the position of the tile in the scene
            int x = (int)(LoaderScript.gameObject.transform.position.x - 0.5f);
            int y = (int)(LoaderScript.gameObject.transform.position.y - 0.5f);

            //Finds the RoomControl GameObject by name
            GameObject StructureControl = GameObject.Find("StructureControl");

            if (StructureControl != null)
            {
                //Gets the SceneRoom instance from the RoomProperties script
                Structure SceneStructure = StructureControl.GetComponent<StructurePropertiesScript>().SceneStructure;

                //Finds tile the actor is standing on and move Actor to it
                Methods.MoveBlock(Block, SceneStructure.TileArray[x, y]);

            }

            //Destroys itself. A new GameObject will only be drawn if inside the camera (probably).
            MonoBehaviour.Destroy(LoaderScript.gameObject);

        }

        //-----MathMethods-----//
        //Mathemetical methods not implemented in unity

        public static float Length(float x, float y)
        {
            return Mathf.Sqrt(x * x + y * y);
        }

        public static bool IsInsideRoom(Room Room,int x,int y)
        {
            if(Room == null)
            {
                return false;
            }
            return (x >= 0 && y >= 0 && x <= Room.Width - 1 && y <= Room.Height - 1);
        }


    }
}


