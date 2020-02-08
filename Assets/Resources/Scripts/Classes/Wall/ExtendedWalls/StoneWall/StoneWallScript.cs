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
        public StoneWall(Tile ReferenceTile, Vector2Int Direction, Vector2Int Rotation) 
            : base(ReferenceTile,Direction,Rotation,"StoneWall", Methods.LoadSprite("Scripts/Classes/Wall/ExtendedWalls/StoneWall/StoneWallSprite"), Methods.LoadSprite("Scripts/Classes/Wall/ExtendedWalls/StoneWall/StoneWallPole"),true)
        {}
    }

    public class StoneWallSpawner : WallSpawner
    {

        public StoneWallSpawner(Structure Structure, float X, float Y, Vector2Int Rotation) 
            : base(Structure,X,Y,Rotation,"StoneWallSpawner", Methods.LoadSprite("Scripts/Classes/Wall/ExtendedWalls/StoneWall/StoneWallSprite"))
        {}

        public override Wall SpawnWall(Tile ReferenceTile, Vector2Int Direction)
        {
            return new StoneWall(ReferenceTile,Direction,this.Rotation);
        }
    }


}
