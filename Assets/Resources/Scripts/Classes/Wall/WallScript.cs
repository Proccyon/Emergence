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
using GenericMethods;

namespace WallSpace
{

    //Name: Name of the wall.
    //Solid: If Solid then actors can not move through the wall
    //BasicSprite: The sprite of the wall when facing south. Sprite is rotated based on rotation.
    //Rotation: Direction of wall. (0,-1) = facing south, (1,0) = facing east, (0,1) = facing north, (-1,0) = facing west.
    //FrontTile: The tile in front of the wall.
    //BackTile: The tile behind the wall.

    public class Wall//(NOT DONE, NEED TO REFERENCE THIS IN FRONTTILE AND BACKTILE)
    {
        public string Name;
        public bool Solid;
        public Sprite BasicSprite;
        public Tile FrontTile;
        public Tile BackTile;
        public Vector2Int Rotation;
        public static readonly Dictionary<Vector2Int, Wall> EmptyWallDict = new Dictionary<Vector2Int, Wall>(){ {Vector2Int.down,null},{Vector2Int.up,null},{Vector2Int.left,null},{Vector2Int.right,null}};


        //Main constructor
        public Wall(string Name = "", bool Solid=false, Sprite BasicSprite=null, Tile FrontTile=null, Tile BackTile=null)
        {
            this.Name = Name;
            this.Solid = Solid;
            this.BasicSprite = BasicSprite;

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
            return  (R<= 1.1f && R>=0.9f); //Check if distance is 1
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

        //Returns a wall instance identical to this one, placed at given tile.(NOT DONE)
        public virtual Wall Copy(Tile FrontTile, Tile BackTile)
        {
            //Create new block instance
            Wall NewWall = new Wall(this.Name, this.Solid, this.BasicSprite,FrontTile,BackTile);
            if(NewWall.Rotation == Vector2Int.zero)
            {
                return null;
            }

            return NewWall;
        }


    }


}


