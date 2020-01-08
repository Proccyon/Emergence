using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WallSpace;
using TileSpace;
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

        public StoneWallSpawner(TileSpawner FrontTileSpawner, TileSpawner BackTileSpawner) 
            : base(FrontTileSpawner,BackTileSpawner,"StoneWallSpawner", Methods.LoadSprite("Scripts/Classes/Wall/ExtendedWalls/StoneWall/StoneWall"))
        {}

        public override Wall SpawnWall(Tile FrontTile, Tile BackTile)
        {
            return new StoneWall(FrontTile, BackTile);
        }
    }


}
