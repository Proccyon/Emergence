//-----Usage-----//
//Defines the item and container class. Items are objects used by actors and blocks for various reasons. All items are located in containers.
//Each container contains at most one item. Containers themselves are either located in an actor, block or tile.
//The container class is created so that items are always located in a single class type, 
//and so that items have an easy reference to their location.


//-----UnityImports-----//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----ScriptImports-----//
using GenericMethods;


namespace ItemSpace
{

    //Name: Name of the item.
    //Container: The location of the item. Containers are stored in actors, blocks or tiles.
    //Sprite: The Image of the item. Some items need a second sprite for when the item is worn by an actor.
    public class Item
    {
        public string Name;
        public Container ContainerOfItem;
        public Sprite Sprite;


        //Basic constructor
        public Item(string Name, Container ContainerOfItem, Sprite Sprite)
        {
            this.Name = Name;
            this.Sprite = Sprite;

            //Checks if the given container is already occupied
            if(Methods.CanMoveItem(this,ContainerOfItem))
            {
                Methods.MoveItem(this, ContainerOfItem); //If not then move this item to the container
            }
            else //If it is occupied set ContainerOfItem to null
            {
                this.ContainerOfItem = null;
            }
            
        }

        //
        public Item Copy(Container NewContainer)
        {
            return new Item(this.Name,NewContainer,this.Sprite);
        }
    }

    //ItemOfContainer: The item the container is holding.
    public class Container
    {

        public Item ItemOfContainer;

        //Constructor
        public Container(Item ItemOfContainer)
        {
            this.ItemOfContainer = ItemOfContainer;
        }

        //Empty contructor. Might be better to use this and add item afterwards
        public Container()
        {
            this.ItemOfContainer = null;
        }

        //Returns a new container instance identical to this one.
        public Container Copy()
        {
            //Create new Container instance with no item inside
            Container NewContainer = new Container();
            
            //Checks if this Container contains an item
            if(this.ItemOfContainer == null)
            {
                return NewContainer; //If not return this empty container
            }
            else
            {
                //Otherwise copy the item, put it in container and return the container
                NewContainer.ItemOfContainer = this.ItemOfContainer.Copy(NewContainer);
                return NewContainer;
            }
        }

    }
}


