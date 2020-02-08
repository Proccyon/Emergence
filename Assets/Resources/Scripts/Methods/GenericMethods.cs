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

        //Checks if an actor can move to a given tile. Does not check for walls, this should be seen as a teleport of sorts
        public static bool CanMoveActor(Actor Actor, Tile NewTile)
        {
            return (Actor != null && Tile.CanStandOnTile(NewTile));
        }

        //Same method as above but with x,y coordinates
        public static bool CanMoveActor(Actor Actor, int x, int y)
        {
            //Checks if (x,y) is inside the room   
            if (IsInsideRoom(Actor.TileOfActor.RoomOfTile, x, y))
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

            if (CanMoveActor(Actor, x, y))
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
        public static bool CanMoveBlock(Block Block, Tile NewTile)
        {

            //Return false if given tile or block is null
            if (NewTile == null || Block == null)
            {
                return false;
            }

            //Return false if there is an actor at the new tile and block is solid
            if (NewTile.ActorOfTile != null && Block.Solid)
            {
                return false;
            }

            //Return true if NewTile contains no block, Return false if NewTile does contain a block
            return (NewTile.BlockOfTile == null);

        }

        public static bool CanMoveBlock(Block Block, int x, int y, Room ThisRoom = null)
        {
            if (ThisRoom == null)
            {
                ThisRoom = Block.TileOfBlock.RoomOfTile;
            }
            if (IsInsideRoom(ThisRoom, x, y))
            {
                return CanMoveBlock(Block, ThisRoom.TileArray[x, y]);
            }
            return false;
        }

        public static void MoveBlock(Block Block, Tile NewTile)
        {
            //Checks if Block can indeed be moved to the new tile
            if (CanMoveBlock(Block, NewTile))
            {
                if (Block.TileOfBlock != null)
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
                if (Item.ContainerOfItem != null)
                {
                    Item.ContainerOfItem.ItemOfContainer = null;//Empties the container that used to hold the item.
                }

                Item.ContainerOfItem = NewContainer; //Changes the container of the item.
                NewContainer.ItemOfContainer = Item; //Changes the item of the new container.
            }
        }

        //Checks if a wall can be moved to ReferenceTile.WallDict[Direction]
        public static bool CanMoveWall(Tile ReferenceTile, Vector2Int Direction, Vector2Int Rotation)
        {
            //Check if everything is set
            if (ReferenceTile == null || Direction == null || Rotation == null || ReferenceTile.WallDict == null)
            {
                return false;
            }
            //Check if direction is parallel to Rotation
            if(!(Direction == Rotation || Direction == Rotation *-1))
            {
                return false;
            }
            //Check if room is set
            if(ReferenceTile.RoomOfTile == null)
            {
                return false;
            }
            //Finally check if the spot is already occupied by another wall
            return ReferenceTile.WallDict[Direction] == null;           
        }

        //Moves a wall to ReferenceTile.WallDict[Direction] and change rotation
        public static void MoveWall(Wall Wall,Tile ReferenceTile, Vector2Int Direction, Vector2Int Rotation)
        {
            //Check if wall can be moved at all
            if (CanMoveWall(ReferenceTile,Direction,Rotation))
            {
                ReferenceTile.WallDict[Direction] = Wall; //Reference this in the reference tile
                Tile OppositeTile = null;

                //Checks if the tile on the other side of the wall is inside the room
                if (Methods.IsInsideRoom(ReferenceTile.RoomOfTile, ReferenceTile.X + Direction.x, ReferenceTile.Y + Direction.y))
                {
                    //Find the tile on the other side of the wall
                    OppositeTile = ReferenceTile.RoomOfTile.TileArray[ReferenceTile.X + Direction.x, ReferenceTile.Y + Direction.y];
                    OppositeTile.WallDict[Direction * -1] = Wall; //Reference this in the opposite tile
                }

                if (Direction == Rotation)
                {
                    Wall.FrontTile = OppositeTile;
                    Wall.BackTile = ReferenceTile;
                }
                else
                {
                    Wall.FrontTile = ReferenceTile;
                    Wall.BackTile = OppositeTile;
                }
                Wall.Rotation = Rotation;
            }
        }

        //Checks if a wallSpawner can be moved to the given direction
        public static bool CanMoveWallSpawner(TileSpawner ReferenceTileSpawner, Vector2Int Direction, Vector2Int Rotation)
        {
            //Check if everything is set
            if (ReferenceTileSpawner == null || Direction == null || Rotation == null || ReferenceTileSpawner.WallSpawnerDict == null)
            {
                return false;
            }
            //Check if direction is parallel to Rotation
            if (!(Direction == Rotation || Direction == Rotation * -1))
            {
                return false;
            }
            //Check if structure is set
            if (ReferenceTileSpawner.Structure == null)
            {
                return false;
            }
            //Finally check if the spot is already occupied by another wallSpawner
            return ReferenceTileSpawner.WallSpawnerDict[Direction] == null;
        }

        //Moves a given wall spawner to the new position
        public static void MoveWallSpawner(WallSpawner WallSpawner, TileSpawner ReferenceTileSpawner, Vector2Int Direction, Vector2Int Rotation)
        {
            if (CanMoveWallSpawner(ReferenceTileSpawner, Direction, Rotation) && WallSpawner != null)
            {

                ReferenceTileSpawner.WallSpawnerDict[Direction] = WallSpawner; //Reference this in the reference tile
                TileSpawner OppositeTileSpawner = null;

                //Checks if the tileSpawner on the other side of the wallSpawner is inside the room
                if (Methods.IsInsideRoom(ReferenceTileSpawner.Structure.Width, ReferenceTileSpawner.Structure.Height, ReferenceTileSpawner.X + Direction.x, ReferenceTileSpawner.Y + Direction.y))
                {
                    //Find the tile on the other side of the wallSpawner
                    OppositeTileSpawner = ReferenceTileSpawner.Structure.TileSpawnerArray[ReferenceTileSpawner.X + Direction.x, ReferenceTileSpawner.Y + Direction.y];
                    OppositeTileSpawner.WallSpawnerDict[Direction * -1] = WallSpawner; //Reference this in the opposite tile
                }

                if (Direction == Rotation)
                {
                    WallSpawner.FrontTileSpawner = OppositeTileSpawner;
                    WallSpawner.BackTileSpawner = ReferenceTileSpawner;
                }
                else
                {
                    WallSpawner.FrontTileSpawner = ReferenceTileSpawner;
                    WallSpawner.BackTileSpawner = OppositeTileSpawner;
                }
                WallSpawner.Rotation = Rotation;
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
        public static GameObject CreateSpriteObject(Sprite Sprite,float x,float y,string Name = "SpriteObject",int SortingOrder = 0,float Angle=0,bool AdjustScale=true)
        {
            
            GameObject SpriteObject = new GameObject(Name);
            SpriteObject.transform.position = new Vector3(x, y, 0);//Set position
            SpriteObject.transform.eulerAngles = new Vector3(0, 0, Angle);
            SpriteRenderer Renderer = SpriteObject.AddComponent<SpriteRenderer>(); //Add Sprite renderer
            Renderer.sprite = Sprite; //Set sprite
            Renderer.sortingOrder = SortingOrder; //Sorting order is the height of the draw layer

            if (AdjustScale)
            {
                //Adjust the scale so sprite is 100 by 100
                SpriteObject.transform.localScale *= 100 / Sprite.rect.height;
            }
            return SpriteObject;
        }

        //-----MathMethods-----//
        //Mathemetical methods not implemented in unity

        //Length of 2-dimensional vector (x,y)
        public static float Length(float x, float y)
        {
            return Mathf.Sqrt(x * x + y * y);
        }

        //Distance between two vectors (x1,y1) and (x2,y2)
        public static float Distance(float x1, float y1, float x2, float y2)
        {
            return Length(x1 - x2, y1 - y2);
        }

        //Distance between two tiles, returns -1 if tiles not set correctly
        public static float Distance(Tile Tile1, Tile Tile2)
        {
            if (Tile1 == null || Tile2 == null || Tile1.RoomOfTile != Tile2.RoomOfTile)
            {
                return -1;
            }

            return Distance(Tile1.X, Tile1.Y, Tile2.X, Tile2.Y);
        }

        public static bool IsInsideRoom(Room Room,int x,int y)
        {
            if(Room == null)
            {
                return false;
            }
            return (x >= 0 && y >= 0 && x <= Room.Width - 1 && y <= Room.Height - 1);
        }

        public static bool IsInsideRoom(int Width, int Height, int x, int y)
        {
            return (x >= 0 && y >= 0 && x <= Width - 1 && y <= Height - 1);
        }


        //-----StructureMethods-----//
        //Methods related to structures. Might move this later if a better spot is found.

        //A selection of blocks is saved as a prefab. This methods creates a structure based on that prefab.
        //Only works during runtime.
        public static Structure LoadStructure(string PrefabName)
        {

            //The RoomControl prefab, prefab that contains everything in the structure
            var RoomControl = (GameObject)Resources.Load(PrefabName, typeof(GameObject));
            
            //Gets structure  height and width set inside the prefab
            int StructureHeight = RoomControl.GetComponent<StructurePropertiesScript>().StructureHeight;
            int StructureWidth = RoomControl.GetComponent<StructurePropertiesScript>().StructureWidth;
            
            //Create a new empty structure we will fill afterwards
            Structure Structure = new Structure(StructureHeight, StructureWidth);
            
            //All the scripts inside one SpawnerObject, see the for loop
            Component[] Scripts;

            //Goes through each child of RoomControl (Tiles, blocks, actors, etc.)
            foreach (Transform SpawnerObject in RoomControl.transform)
            {
                float x = SpawnerObject.position.x-0.5f;
                float y = SpawnerObject.position.y-0.5f;

                //Gets all CreateSpawnerScripts inside SpawnerObject, defined in StructurePropertiesScript
                //All CreateSpawnerScripts have a method CreateSpawner that adds a spawner to the sctructure
                Scripts = SpawnerObject.GetComponents(typeof(CreateSpawnerScript));

                //Loops through all the scripts to find the one we need.
                foreach(CreateSpawnerScript Script in Scripts)
                {
                    Script.CreateSpawner(Structure, x, y);
                    break;
                }
               
            }
            return Structure;
        }

    }
}


