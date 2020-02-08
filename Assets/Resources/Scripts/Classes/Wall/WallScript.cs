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
    //WallSprite: The sprite of the wall when facing East. Sprite is rotated based on rotation when drawn.
    //PoleSprite: Sprite of the pole of the wall. Purely cosmetic a pole is placed at the end of a wall.
    //Rotation: Direction of wall. (0,-1) = facing south, (1,0) = facing east, (0,1) = facing north, (-1,0) = facing west.
    //FrontTile: The tile in front of the wall.
    //BackTile: The tile behind the wall.
    //PolePriority: Decides which PoleSprite is shown when two walls are next to each other. Pole with highest PolePriority is shown.

    public class Wall
    {
        public string Name;
        public bool Solid;
        public Sprite WallSprite;
        public Sprite PoleSprite;
        public Tile FrontTile;
        public Tile BackTile;
        public Vector2Int Rotation;
        public int PolePriority;
        public static readonly Dictionary<Vector2Int, Wall> EmptyWallDict = new Dictionary<Vector2Int, Wall>(){ {Vector2Int.down,null},{Vector2Int.up,null},{Vector2Int.left,null},{Vector2Int.right,null}};


        //Main constructor
        //ReferenceTile can be any tile next to the wall, there are two options
        //Direction is a 2d vector pointing from ReferenceTile to the wall
        //Rotation is a 2d vector pointing in the direction the wall is looking at
        public Wall(Tile ReferenceTile, Vector2Int Direction,Vector2Int Rotation,string Name = "", Sprite WallSprite = null,Sprite PoleSprite = null,bool Solid=false,int PolePriority = 0 )
        {
            this.Name = Name;
            this.Solid = Solid;
            this.WallSprite = WallSprite;
            this.PoleSprite = PoleSprite;
            this.PolePriority = PolePriority;
            //Moves the wall to the given position and sets all refrences
            Methods.MoveWall(this,ReferenceTile,Direction,Rotation);
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

    //WallSpawner are placed inside a structure. They spawn a wall when initializing a room.
    //Name: Name of the wallSpawner.
    //WallSpawnerSprite: The sprite of the WallSpawner when facing East. 
    //Rotation: Direction of wall. (0,-1) = facing south, (1,0) = facing east, (0,1) = facing north, (-1,0) = facing west.
    //FrontTileSpawner: The tileSpawner in front of the wall.
    //BackTileSpawner: The tileSpawner behind the wall.
    public class WallSpawner
    {
        public string Name;
        public Sprite WallSpawnerSprite;
        public TileSpawner FrontTileSpawner;
        public TileSpawner BackTileSpawner;
        public Vector2Int Rotation;

        public static readonly Dictionary<Vector2Int, WallSpawner> EmptyWallSpawnerDict = new Dictionary<Vector2Int, WallSpawner>() { { Vector2Int.down, null }, { Vector2Int.up, null }, { Vector2Int.left, null }, { Vector2Int.right, null } };

        //Currently only used in Methods.LoadStructure. X and Y are the position of the prefab in the editor(-0.5 see LoadStructure).
        public WallSpawner(Structure Structure,float X,float Y, Vector2Int Rotation, string Name = "", Sprite WallSpawnerSprite=null)
        {
            
            this.Name = Name;
            this.WallSpawnerSprite = WallSpawnerSprite;
            this.Rotation = Rotation;

            //Positions of FrontTile and BackTile
            int FrontX = (int)(X + Rotation.x * 0.5);
            int FrontY = (int)(Y + Rotation.y * 0.5);
            int BackX = (int)(X - Rotation.x * 0.5);
            int BackY = (int)(Y - Rotation.y * 0.5);

            //Checks if FrontTile is inside the room, if not set it to null
            if(Methods.IsInsideRoom(Structure.Width,Structure.Height,FrontX,FrontY))
            {
                this.FrontTileSpawner = Structure.TileSpawnerArray[FrontX, FrontY];
                this.FrontTileSpawner.WallSpawnerDict[Rotation * -1] = this;
            }
            else
            {
                this.FrontTileSpawner = null;
            }
            //Checks if BackTile is inside the room, if not set it to null
            if (Methods.IsInsideRoom(Structure.Width, Structure.Height, BackX, BackY))
            {
                this.BackTileSpawner = Structure.TileSpawnerArray[BackX, BackY];
                this.BackTileSpawner.WallSpawnerDict[Rotation] = this;
            }
            else
            {
                this.BackTileSpawner = null;
            }
                  
        }

        //This method is overriden in child WallSpawners. It initializes a Wall at the given position
        public virtual Wall SpawnWall(Tile ReferenceTile, Vector2Int Direction)
        {
            return null;
        }
    }

}


