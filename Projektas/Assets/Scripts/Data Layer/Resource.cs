using ResourcesTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourcesTypes
{
    public enum ResourceType
    {
        Cocos = 0,
        Wood = 1,
        Stone = 2
    }
}

public class Resource : IItem {

    public const int DefaultID          = -1;
    public const string DefaultName     = "UNNAMED_RESOURCE";
    public const float  DefaultWeight   = 0.00f;
    public const string DefaultItemInfo = "No info available";


    int     id;
    string  name;
    float   weight;
    int     quantity;
    /// <summary>
    /// Resources are unusable by default, unless on VERY rare occasions
    /// </summary>
    bool    isUsable = false;
    string  itemInfo;

    //bool isStackable;
    //int quantity;


    // properties
    public string Name
    {
        get
        {
            return name;
        }
    }

    public int Quantity
    {
        get
        {
            return quantity;
        }

        set
        {
            quantity = value;
        }
    }

    public float Weight
    {
        get
        {
            return weight;
        }

        set
        {
            weight = value;
        }
    }

    public bool IsUsable
    {
        get
        {
            return isUsable;
        }

        set
        {
            isUsable = value;
        }
    }

    public string ItemInfo
    {
        get
        {
            return itemInfo;
        }
        set
        {
            itemInfo = value;
        }
    }

    public int ID
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public int EnergyValue { get; set; }
    public ResourceType Type { get; private set; }

    public Sprite Icon { get; set; }

    // constructors
    public Resource()
    {
        id          = DefaultID; 
        name        = DefaultName;
        weight      = DefaultWeight;
        itemInfo    = DefaultItemInfo;
        Icon = null;
        EnergyValue = 10;
    }

    public Resource(int id, string name, float weight, string itemInfo, Sprite icon, ResourceType type)
    {
        this.id         = id;
        this.name       = name;
        this.weight     = weight;
        this.itemInfo   = itemInfo;
        Icon = icon;
        EnergyValue = 10;
        Type = type;
    }

    public Resource(int id, string name, float weight, string itemInfo, ResourceType type)
    {
        this.id = id;
        this.name = name;
        this.weight = weight;
        this.itemInfo = itemInfo;
        EnergyValue = 10;
        Type = type;
    }

    //methods

    public IItem Copy()
    {
        return new Resource(id, name, weight, itemInfo, Icon, Type);
    }



}
