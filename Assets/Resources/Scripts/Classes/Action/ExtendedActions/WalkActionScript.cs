//-----Usage-----//
//Defines the walk Action. This action lets an actor move 1 tile (including diagonally).



//-----UnityImports-----//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----ScriptImports-----//
using ActionSpace;
using ActorSpace;
using TileSpace;
using RoomSpace;
using WallSpace;
using GenericMethods;


namespace ActionSpace
{

    class WalkAction : Action
    {
        public static float StaticEnergyCost = 100f;
        public WalkAction(Tile NewTile)
        {

            this.EnergyCost = StaticEnergyCost;
            this.CanActivate = (Actor => CanMove(Actor, NewTile));
            this.Activate = (Actor => MoveActor(Actor, NewTile));
        }

        //Checks if actor can walk to given tile, NewTile has to be 1 tile away from actor (can be diagonal)
        public bool CanMove(Actor Actor, Tile NewTile)
        {
            return Actor != null && Tile.CanMoveBetweenTiles(Actor.TileOfActor, NewTile);
        }

        public void MoveActor(Actor Actor, Tile NewTile)
        {
            Methods.MoveActor(Actor, NewTile);
        }

    }
}