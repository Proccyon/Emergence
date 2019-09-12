//-----Usage-----//
//Defines the pass Action. This will drain all remaining energy skipping the turn.



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

    class PassAction : Action
    {

        public PassAction(Actor Actor)
        {
            this.EnergyCost = Actor.Energy;
            this.CanActivate = ReturnTrue;
            this.Activate = DoNothing;
        }

        //Checks if actor can walk to given tile, NewTile has to be 1 tile away from actor
        
        public bool ReturnTrue(Actor Actor)
        {
            return true;
        }

        public void DoNothing(Actor Actor)
        {
        }
    }
}