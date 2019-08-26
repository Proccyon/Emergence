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
using GenericMethods;


namespace ActionSpace
{

    class WalkAction : Action
    {

        public WalkAction(Tile NewTile)
        {

            this.EnergyCost = 25;
            this.CanActivate = (Actor => CanMove(Actor, NewTile));
            this.Activate = (Actor => MoveActor(Actor, NewTile));
        }

        //Checks if actor can walk to given tile, NewTile has to be 1 tile away from actor
        public bool CanMove(Actor Actor, Tile NewTile)
        {

            //Returns false if Actor has no tile or tile is the same as NewTile
            if(Actor.TileOfActor == null || Actor.TileOfActor == NewTile)
            {
                return false;
            }
            //Checks for obstacles
            if(!Methods.CanMoveActor(Actor, NewTile))
            {
                return false;
            }
            //Checks if NewTile has the same room as the actor
            if(NewTile.RoomOfTile != Actor.TileOfActor.RoomOfTile)
            {
                return false;
            }
            //Checks if NewTile is 1 tile away from the actor
            return Mathf.Abs(NewTile.X - Actor.TileOfActor.X) <= 1.1f && Mathf.Abs(NewTile.X - Actor.TileOfActor.X) <= 1.1f;

        }

        public void MoveActor(Actor Actor, Tile NewTile)
        {
            Methods.MoveActor(Actor, NewTile);
        }
    }
}