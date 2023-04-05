using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    //Interfaces for storage and sorting purposes
    
    //Floor has rooms 
    interface IFloor
    {
        Room[,] FloorMap { get; }
    }
    interface IRoom
    {
        Character[] characters { set; get; }
        Boss boss { set; get; }
        Item[] item { set; get; }
        Weapon weapon { set; get; }
        Armor armor { set; get; }
    }
    interface ICharacter
    {
        String Name { set; get; }
        String Description { get; }
        Item[] Inventory { set; get; }
        Double Health { set; get; }
        Double Attack { set; get; }
        Double Evasiveness { set; get; }
        Boolean CanMove { set; get; }
        Boolean Alive { set; get; }
        Skills skillName { set; get; }
        //Thinking about what to do with this
        Command[] actions { set; get; }
        
    }
    interface iItems
    {
        String Name { set; get; }
        String Description { get; }
        int Value { set; get; }
        int Weight { set; get; }
        int Volume { set; get; }

    }
}
