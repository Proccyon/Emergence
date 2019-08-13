//-----Usage-----//
//Defines the Tile class and Floor class. Each square on the map corresponds to a tile. A tile can contain characters, items or blocks (Sometimes not all, depending on the item, character and block). 
//Each tile has a floor property that determines how the tile looks and other effects (ex. a swamp floor might slow characters).
 


//-----UnityImports-----//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----ScriptImports-----//
using RoomSpace;
using ActorSpace;
using BlockSpace;
using GenericMethods;

namespace TileSpace
{

    //RoomOfTile: The room the tile is in (ex. above ground, inside a dungeon)
    //x: The x coordinate of the tile within the room. x=0 is left
    //y: The y coordinate of the tile within the room. y= 0 is the bottom (so x=0,y=0 is the bottom left corner of the room)
    //FloorOfTile: The floor of the tile. Determines the appearance and properties of the tile itself. (Ex. a wooden floor, a road, water etc.)
    //ActorOfTile: The Actor that is standing on the tile. No more than 1 actor can stand on a tile. Variable is Null if no actors are on the tile.
    //BlockOfTile: The Block standing on the tile (A table, Stone, etc.). No more than 1 block can stand on a tile. If the block is solid no actors can move to this tile (ActorOfTile should be None).
    public class Tile
    {
        public Room RoomOfTile;
        public int X;
        public int Y;
        public Floor FloorOfTile;
        public Actor ActorOfTile;
        public Block BlockOfTile;

        //Main constructor. 
        public Tile(Room RoomOfTile, int X,int Y, Floor FloorOfTile, Actor ActorOfTile=null, Block BlockOfTile=null)
        {
            this.RoomOfTile = RoomOfTile;
            this.X = X;
            this.Y = Y;
            this.FloorOfTile = FloorOfTile;
            this.ActorOfTile = ActorOfTile;
            this.BlockOfTile = BlockOfTile;

        }

        //Empty constructor. This is not intended to be used.
        public Tile()
        {
            this.RoomOfTile = null;
            this.X = 0;
            this.Y = 0;
            this.FloorOfTile = Floor.WoodFloor;
            this.ActorOfTile = null;
            this.BlockOfTile = null;

        }
    }


    //Name: Name of the floor. Not important but might be usefull later.
    //Sprite: The image of the floor. This is supposed to be a square png image of fixed size.
    public class Floor
    {

        public readonly string Name;
        public readonly Sprite Sprite;

        //-----Floor instances-----//
        public static Floor WoodFloor = new Floor("WoodenFloor", Methods.LoadSprite("Sprites/FloorSprites/WoodFloor"));
        public static Floor RoadFloor = new Floor("Road", Methods.LoadSprite("Sprites/FloorSprites/RoadFloor"));
        public static Floor GrassFloor = new Floor("Grass", Methods.LoadSprite("Sprites/FloorSprites/GrassFloor"));

        //Main constructor
        public Floor(string Name, Sprite Sprite)
        {
            this.Name = Name;
            this.Sprite = Sprite;
        }


    }


}





