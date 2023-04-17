using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    //Interfaces for storage and sorting purposes
    
    //Floor has rooms 
    interface IFloor
    {
        Room[,] FloorMap { set;  get; }
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
        string Name { get; }
        string Description { get; }
        List<Item> Inventory { set; get; }
        Weapon EquippedWeapon { set; get; }
        Armor EquippedArmor { set; get; }
        Boolean CanMove { set; get; }
        Boolean Alive { set; get; }
        Skills aptitudeLvl { set; get; }
        //Thinking about what to do with this
        Command[] actions { set; get; }
        
    }
    interface iItems
    {
        string Name { set; get; }
        string Description { get; }
        int Value { set; get; }
        int Weight { set; get; }
        int Volume { set; get; }

    }
}
