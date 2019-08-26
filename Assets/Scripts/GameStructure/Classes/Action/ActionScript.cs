//-----Usage-----//
//Defines the action class. Each actor has an amount of energy each turn which it can spend on performing actions.
//Actions are things like walking, picking up an item or attacking another actor. This class is made to prevent bugs like actors teleporting over the map.



//-----UnityImports-----//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----ScriptImports-----//
using TileSpace;
using ActorSpace;
using GenericMethods;


namespace ActionSpace
{

    //CanActivate: A delegate that determines if an action can be performed (For example actors cant walk through walls).
    //Activate: The delegate that performs the action, for example moving the actor.
    //EnergyCost: Energy needed to perform this action once. 
    public class Action
    {

        public delegate bool ConditionDelegate(Actor Actor);
        public delegate void ActivateDelegate(Actor Actor);
        public ConditionDelegate CanActivate;
        public ActivateDelegate Activate;

        public float EnergyCost;

        public Action(ConditionDelegate CanActivate, ActivateDelegate Activate,float EnergyCost)
        {
            this.CanActivate = CanActivate;
            this.Activate = Activate;
            this.EnergyCost = EnergyCost;
        }
        
        public Action()
        {
            this.CanActivate = null;
            this.Activate = null;
            this.EnergyCost = 0;
        }
    }


}


