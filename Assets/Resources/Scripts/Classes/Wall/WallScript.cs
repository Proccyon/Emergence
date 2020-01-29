//-----Usage-----//
//Defines the wall class. Walls are obstacles positioned  on the edges of a tile. Each tile can be surrounded by a maximum of 4 walls.
//If a wall is solid then actors will not be able to move through it. A tile can contain both walls and a block. 
//Some walls can be interacted with (Doors,window, etc). Walls currently can not be destroyed but this might change in the future.


//-----UnityImports-----//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----ScriptImports-----//
using TileSpace;
using RoomSpace;
using ItemSpace;
using StructureSpace;
using GenericMethods;

namespace WallSpace
{

    //Name: Name of the wall.
    //Solid: If Solid then actors can not move through the wall
    //WallSprite: The sprite of the wall when facing East. Sprite is rotated based on rotation.
    //PoleSprite: Sprite of the pole of the wall. Purely cosmetic a pole is placed at the end of a wall.
    //Rotation: Direction of wall. (0,-1) = facing south, (1,0) = facing east, (0,1) = facing north, (-1,0) = facing west.
    //FrontTile: The tile in front of the wall.
    //BackTile: The tile behind the wall.

    public class Wall//(NOT DONE, NEED TO REFERENCE THIS IN FRONTTILE AND BACKTILE)
    {
        public string Name;
        public bool Solid;
        public Sprite WallSprite;
        public Sprite PoleSprite;
        public Tile FrontTile;
        public Tile BackTile;
        public Vector2Int Rotation;
        public static readonly Dictionary<Vector2Int, Wall> EmptyWallDict = new Dictionary<Vector2Int, Wall>(){ {Vector2Int.down,null},{Vector2Int.up,null},{Vector2Int.left,null},{Vector2Int.right,null}};


        //Main constructor
        public Wall(Tile FrontTile, Tile BackTile,string Name = "", Sprite WallSprite = null,Sprite PoleSprite = null,bool Solid=false )
        {
            this.Name = Name;
            this.Solid = Solid;
            this.WallSprite = WallSprite;
            this.PoleSprite = PoleSprite;

            //Checks if given FrontTile and BackTile can contain the wall
            if(Methods.CanMoveWall(this,FrontTile,BackTile))
            {
                //Move wall to given FrontTile and BackTile(Sets the references within the tiles etc.)
                Methods.MoveWall(this,FrontTile,BackTile);
            }
            else
            {
                //If Given Tiles can not be moved to then set tiles to null
                this.FrontTile = null;
                this.BackTile = null;
                this.Rotation = Vector2Int.zero;
            }

        }
        
        //Checks if FrontTile ancd BackTile are indeed next to each other
        public static bool CheckWallTiles(Tile FrontTile, Tile BackTile)
        {
            //Checks if tiles exist and are in the same room
            if (FrontTile == null || BackTile == null || FrontTile.RoomOfTile != BackTile.RoomOfTile)
            {
                return false;
            }

            //Finds distance between FrontTile and BackTile
            var R = Mathf.Pow(Mathf.Pow((float)(FrontTile.X - BackTile.X), 2f) + Mathf.Pow((float)(FrontTile.Y - BackTile.Y), 2f),0.5f);
            return  (R<= 1.01f && R>=0.99f); //Check if distance is 1
        }

        //Sets the rotation based on position of FrontTile and BackTile
        public static Vector2Int GetRotation(Tile FrontTile,Tile BackTile)
        {
            //If FrontTile and BackTile are not next to each other
            if(!CheckWallTiles(FrontTile,BackTile))
            {
                //Vector2Int.zero means rotation can not be determined because of incorrect Front/BackTiles
                return Vector2Int.zero;
            }
            if (FrontTile.Y < BackTile.Y)
            {
                return Vector2Int.down;
            }
            if (FrontTile.X > BackTile.X)
            {
                return Vector2Int.right;
            }
            if (FrontTile.Y > BackTile.Y)
            {
                return Vector2Int.up;
            }
            if (FrontTile.X < BackTile.X)
            {
                return Vector2Int.left;
            }
            return Vector2Int.zero;
        }

        //Finds the angle corresponding to the given rotation
        //Used for drawing the wall
        public float GetAngle()
        {
            if(this.Rotation == Vector2Int.right)
            {
                return 0f;
            }
            if (this.Rotation == Vector2Int.up)
            {
                return 90f;
            }
            if (this.Rotation == Vector2Int.left)
            {
                return 180f;
            }
            if (this.Rotation == Vector2Int.down)
            {
                return 270f;
            }
            return 0;
        }

        //Checks if there is a solid wall next to a tile in a given direction
        public static bool CheckIfSolidWall(Tile Tile, Vector2Int Direction)
        {
            if (Tile == null || Direction == null || Direction == Vector2Int.zero || Tile.WallDict[Direction] == null)
            {
                return false;
            }
            return Tile.WallDict[Direction].Solid;
        }
    }


    public class WallSpawner
    {
        public string Name;
        public Sprite WallSpawnerSprite;
        public TileSpawner FrontTileSpawner;
        public TileSpawner BackTileSpawner;
        public Vector2Int Rotation;

        public static readonly Dictionary<Vector2Int, WallSpawner> EmptyWallSpawnerDict = new Dictionary<Vector2Int, WallSpawner>() { { Vector2Int.down, null }, { Vector2Int.up, null }, { Vector2Int.left, null }, { Vector2Int.right, null } };

        public WallSpawner(Structure Structure,float X,float Y, Vector2Int Rotation, string Name = "", Sprite WallSpawnerSprite=null)
        {
            
            this.Name = Name;
            this.WallSpawnerSprite = WallSpawnerSprite;
            this.Rotation = Rotation;

            int FrontX = (int)(X + Rotation.x * 0.5);
            int FrontY = (int)(Y + Rotation.y * 0.5);
            int BackX = (int)(X - Rotation.x * 0.5);
            int BackY = (int)(Y - Rotation.y * 0.5);

            if(Methods.IsInsideRoom(Structure.Width,Structure.Height,FrontX,FrontY))
            {
                this.FrontTileSpawner = Structure.TileSpawnerArray[FrontX, FrontY];
            }
            else
            {
                this.FrontTileSpawner = null;
            }
            if (Methods.IsInsideRoom(Structure.Width, Structure.Height, BackX, BackY))
            {
                this.BackTileSpawner = Structure.TileSpawnerArray[BackX, BackY];
            }
            else
            {
                this.BackTileSpawner = null;
            }
            

            if ((FrontTileSpawner == null || FrontTileSpawner.WallSpawnerDict[Rotation * -1] == null) && (BackTileSpawner == null || BackTileSpawner.WallSpawnerDict[Rotation] == null))
            {

                FrontTileSpawner.WallSpawnerDict[this.Rotation * -1] = this;
                BackTileSpawner.WallSpawnerDict[this.Rotation] = this;
            }           
        }


        public virtual Wall SpawnWall(Tile FrontTile, Tile BackTile)
        {
            return null;
        }
    }

}


