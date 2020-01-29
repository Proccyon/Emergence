using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WallSpace;
using TileSpace;
using StructureSpace;
using GenericMethods;

namespace WallSpace
{
    public class StoneWall : Wall
    {
        public StoneWall(Tile FrontTile,Tile BackTile) 
            : base(FrontTile,BackTile,"StoneWall", Methods.LoadSprite("Scripts/Classes/Wall/ExtendedWalls/StoneWall/StoneWall"), Methods.LoadSprite("Scripts/Classes/Wall/ExtendedWalls/StoneWall/StoneWallPole"),true)
        {}
    }

    public class StoneWallSpawner : WallSpawner
    {

        public StoneWallSpawner(Structure Structure, float X, float Y, Vector2Int Rotation) 
            : base(Structure,X,Y,Rotation,"StoneWallSpawner", Methods.LoadSprite("Scripts/Classes/Wall/ExtendedWalls/StoneWall/StoneWall"))
        {}

        public override Wall SpawnWall(Tile FrontTile, Tile BackTile)
        {
            return new StoneWall(FrontTile, BackTile);
        }
    }


}
