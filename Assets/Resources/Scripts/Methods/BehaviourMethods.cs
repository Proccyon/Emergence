//-----Usage-----//
//Defines methods used in .Behaviour in more than one actor. For example a behaviour that lets an actor walk to a given spot.

//-----UnityImports-----//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//-----GameImports-----//
using ActionSpace;
using TileSpace;
using ActorSpace;
using GenericMethods;

namespace BehaviourMethods
{

    public static class BM
    {
        //Returns a list of tiles going from StartTile to EndTile that WalkingActor can walk over
        public static List<Tile> GetPathToTile(Tile StartTile,Tile EndTile, Actor WalkingActor)
        {
            //Returns null of StartTile and EndTile are not in same room
            if(StartTile.RoomOfTile != EndTile.RoomOfTile)
            {
                return null;
            }

            Tile[,] TileArray = StartTile.RoomOfTile.TileArray; //Convenience
            var WalkerList = new List<List<Tile>>(); //Walkers walk over the room trying to find EndTile
            var NewWalkerList = new List<List<Tile>>();
            List<Tile> NewWalker = new List<Tile>();

            //Array indicating if walkers have walked over tile at (x,y). Filled with false now.
            bool[,] WalkedArray = new bool[StartTile.RoomOfTile.Width, StartTile.RoomOfTile.Height];

            //Start at the first tile
            WalkerList.Add(new List<Tile> {StartTile});

            int X0;
            int Y0;

            while (WalkerList.Count > 0)
            {
                
                //Goes through all walkers and add newwalkers to all tiles next to them
                foreach (List<Tile> Walker in WalkerList)
                {
                    X0 = Walker.Last().X;//Gets the coordinates of last tile in list
                    Y0 = Walker.Last().Y;

                    //Goes through all tiles in 1 square radius
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        for (int dy = -1; dy <= 1; dy++)
                        {
                            //Checks if given Tile can be walked over and if walkers have not reached it yet
                            //Second condition will not be out of bounds because the first condition will then be false
                            if (Tile.CanMoveBetweenTiles(TileArray[X0,Y0],dx,dy) && !WalkedArray[X0 + dx, Y0 + dy])
                            {
                                //Set WalkedArray element to true so it cant be moved to again
                                WalkedArray[X0 + dx, Y0 + dy] = true;

                                //Fill NewWalker with path of old walker plus the new tile
                                NewWalker = new List<Tile>();
                                foreach (Tile OldTile in Walker)
                                {
                                    NewWalker.Add(OldTile);
                                }
                                NewWalker.Add(TileArray[X0 + dx, Y0 + dy]);

                                if ((X0 + dx, Y0 + dy) == (EndTile.X, EndTile.Y))
                                {
                                    NewWalker.RemoveAt(0);
                                    return NewWalker;
                                }
                                NewWalkerList.Add(NewWalker);

                            }
                        }
                    }
                }
                WalkerList = NewWalkerList;
                NewWalkerList = new List<List<Tile>>();

            }
            //No path is found so return null
            return null;

        }
    }

}
