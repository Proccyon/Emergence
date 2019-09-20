//-----Usage-----//
//Defines the Chicken class. Chickens walk around eating flowers.

//-----UnityImports-----//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//-----ScriptImports-----//
using TileSpace;
using ItemSpace;
using GenericMethods;
using WrapperSpace;
using ActionSpace;
using RoomSpace;
using BehaviourMethods;
using BlockSpace;
using ActorSpace;

namespace ActionSpace
{

    public class EatFlowerAction : Action
    {
        public static float StaticEnergyCost = 100f;

        public EatFlowerAction(Block Flower)
        {
            this.EnergyCost = StaticEnergyCost;
            this.CanActivate = (Actor => CanEatFlower(Actor, Flower));
            this.Activate = (Actor => EatFlower(Actor, Flower));
        }

        public bool CanEatFlower(Actor Actor, Block Flower)
        {
            return (Actor.Name == "Chicken" && Flower.Name == "Flower" && Flower.TileOfBlock == Actor.TileOfActor);
        }

        public void EatFlower(Actor Actor, Block Flower)
        {
            if (Flower.TileOfBlock != null)
            {
                Flower.TileOfBlock.BlockOfTile = null;
                Flower.TileOfBlock = null;
            }
        }
    }


}


namespace ActorSpace
{
    //FlowerTile: The tile with the flower the chicken is chasing
    //ViewRadius: How far the chicken can see
    public class Chicken : Actor
    {

        int ViewRadius = 6;
        List<Tile> FlowerPath;

        //Basic constructor
        public Chicken(Tile TileOfActor = null,bool AddToList = true)
        {
            this.Name = "Chicken";
            this.InventorySize = 0;
            this.Inventory = new Container[0];
            this.Sprite = Methods.LoadSprite("Scripts/GameStructure/Classes/Actor/ExtendedActors/Chicken/Chicken");
            this.MaxEnergy = 100;
            this.Energy = this.MaxEnergy;

            if (Methods.CanMoveActor(this, TileOfActor))
            {
                Methods.MoveActor(this, TileOfActor);
            }
            else
            {
                this.TileOfActor = null;
            }

            if(AddToList)
            {
                this.TurnNumber = RoomRunner.WrapperList.AddRandom(new ObjectWrapper(this));
            }
            
        }

        //Returns a new Actor instance identical to this one, placed on NewTile
        public override Actor Copy(Tile NewTile)
        {
            //Create New actor instance
            Actor NewActor = new Chicken();

            //Checks if NewTile can hold this actor, otherwise return null
            if (Methods.CanMoveActor(NewActor, NewTile))
            {
                Methods.MoveActor(NewActor, NewTile);
            }
            else
            {
                return null;
            }

            //Copy all containers in the old inventory
            for (int i = 0; i < this.InventorySize; i++)
            {
                NewActor.Inventory[i] = this.Inventory[i].Copy();
            }
            return NewActor;

        }       

        public override Action Behaviour()
        {
            if (Energy < WalkAction.StaticEnergyCost && Energy < EatFlowerAction.StaticEnergyCost)
            {
                return new PassAction(this);
            }

            if (TileOfActor.BlockOfTile != null && TileOfActor.BlockOfTile.Name == "Flower")
            {
                FlowerPath = null;
                return new EatFlowerAction(TileOfActor.BlockOfTile);
            }

            if (FlowerPath == null)
            {
                Tile FlowerTile = FindClosestFlower();
                if(FlowerTile == null)
                {
                    return new WalkAction(FindRandomTile());
                }
                else
                {
                    FlowerPath = BM.GetPathToTile(TileOfActor, FlowerTile, this);
                    if(FlowerPath == null)
                    {
                        return new WalkAction(FindRandomTile()); 
                    }
                    return Behaviour();
                }
            }
            else
            {
                if(FlowerPath.Count != 0 && FlowerPath.Last().BlockOfTile != null && FlowerPath.Last().BlockOfTile.Name =="Flower")
                {
                    if(Methods.CanMoveActor(this,FlowerPath[0]))
                    {
                        return new WalkAction(FlowerPath[0]);
                    }
                    else
                    {
                        FlowerPath = BM.GetPathToTile(TileOfActor, FlowerPath.Last(), this);
                        return Behaviour();
                    }
                }
                else
                {
                    FlowerPath = null;
                    return this.Behaviour();
                }
            }
        }



        //Finds the flower that is closest to chicken and within ViewRadius
        public Tile FindClosestFlower()
        {

            Room ThisRoom = TileOfActor.RoomOfTile;
            float ClosestDistance = 2 * ViewRadius;
            float CurrentDistance;
            int X0 = TileOfActor.X;
            int Y0 = TileOfActor.Y;


            Tile NewFlowerTile = null;
            Tile PossibleFlowerTile = null;
            //Goes through all tiles around the actor in a square
            for (int dx = -ViewRadius; dx <= ViewRadius; dx++)
            {
                for (int dy = -ViewRadius; dy <= ViewRadius; dy++)
                {
                    CurrentDistance = Methods.Length((float)dx, (float)dy);

                    //Checks if inside ViewRadius and inside room
                    if (CurrentDistance <= ViewRadius && Methods.IsInsideRoom(ThisRoom, X0 + dx, Y0 + dy))
                    {

                        //Checks if Tile is a flower
                        PossibleFlowerTile = ThisRoom.TileArray[X0 + dx, Y0 + dy];
                        if (PossibleFlowerTile.BlockOfTile != null && PossibleFlowerTile.BlockOfTile.Name == "Flower" && CurrentDistance < ClosestDistance)
                        {

                            NewFlowerTile = ThisRoom.TileArray[X0 + dx, Y0 + dy];
                            ClosestDistance = CurrentDistance;
                        }
                    }
                }
            }
            return NewFlowerTile;
        }

        //Finds a random tile around the chicken that can be moved to
        public Tile FindRandomTile()
        {

            List<Tile> ValidTiles = new List<Tile>();
            //Goes through all tile in a square radius of 1(including diagonal)
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if(dx == 0 && dy == 0)
                    {
                        continue;
                    }

                    //Checks if actor can move to the tile at (X0+dx,Y0+dy)
                    if (Methods.CanMoveActor(this, TileOfActor.X + dx, TileOfActor.Y + dy))
                    {

                        //Add the tile at (X0+dx,Y0+dy) to the List ValidTiles
                        ValidTiles.Add(TileOfActor.RoomOfTile.TileArray[TileOfActor.X + dx, TileOfActor.Y + dy]);
                    }
                }
            }
            if (ValidTiles.Count == 0)
            {
                return null;
            }
            //Returns a random tile from ValidTiles
            return ValidTiles[(int)Random.Range(0, ValidTiles.Count)];
        }

    }

}



