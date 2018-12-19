using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace Tools
{
    public enum ToolType
    {
        None = 0,
        Mining = 1,
        Woodcutting = 2
    }
}

public class Tool : IItem {
    public const int    Default_ID               = -1;
    public const string Default_Name             = "UNNAMED_TOOL";
    public const float  Default_Weight           = 0.00f;
    public const int    Default_MaxDurability    = 100;
    public const int    Default_DamageValue      = 5;
    public const int    Default_RepairValue      = 20;
    public const string Default_ItemInfo         = "No info available";
    int id;
    string  name;
    /// <summary>
    /// Default weight should be 0.00f, must be positive
    /// </summary>
    float   weight;
    /// <summary>
    /// durability should range from 0 to Default_MaxDurability (100?)
    /// </summary>
    int     durability;
    /// <summary>
    /// tool is unusable by default
    /// </summary>
    bool    isUsable = false;
    /// <summary>
    /// tool durability property (aka can the tool still be used)
    /// </summary>
    bool    isToolDurable;
    /// <summary>
    /// provides basic information about an item
    /// </summary>
    string  itemInfo;
    int     quantity;


    //properties
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

    public string Name
    {
        get
        {
            return name;
        }
    }

    public float Weight
    {
        get { return weight; }
        set { weight = value; }
    }

    public int Durability
    {
        get
        {
            return durability;
        }
        set
        {
            durability = value;
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

    public Sprite Icon { get; set; }

    public ToolType Type { get; set; }


    //constructors
    public Tool()
    {
        id          = Default_ID;
        name        = Default_Name;
        weight      = Default_Weight;
        durability  = Default_MaxDurability;
        isToolDurable = true;
        Icon = null;
    }

    public Tool(Tool prev)
    {
        id = prev.id;
        name = prev.name;
        weight = prev.weight;
        durability = prev.durability;
        isUsable = prev.isUsable;
        isToolDurable = prev.isToolDurable;
        itemInfo = prev.itemInfo;
        Icon = prev.Icon;
        Type = prev.Type;
    }

    public Tool(int id, string name, float weight, int durability)
    {
        this.id         = id;
        this.name       = name;
        this.weight     = weight;
        this.durability = durability;
        isToolDurable   = true;
        Icon = null;
        itemInfo = Default_ItemInfo;
    }

    public Tool(int id, string name, float weight, int durability, bool isUsable, string itemInfo, Sprite icon, ToolType type)
    {
        this.id         = id;
        this.name       = name;
        this.weight     = weight;
        this.durability = durability;
        this.isUsable   = isUsable;

        // item may have 0 durability, therefore it will not be 'durable', but still repairable, etc.
        isToolDurable = isToolDurable ? true : false;

        this.itemInfo = itemInfo;
        Icon = icon;
        Type = type;
    }

    //methods

    /// <summary>
    /// Returns a copy of a tool
    /// </summary>
    /// <returns></returns>
    public IItem Copy()
    {
        return new Tool(id, name, weight, durability, isUsable, itemInfo, Icon, Type);
    }

    /// <summary>
    /// Checks if the tool can be used
    /// </summary>
    /// <returns>True, if the tool is still usable</returns>
    public bool IsToolDurable()
    {
        return this.durability > 0;
    }

    
    /// <summary>
    /// Damages the tool durability
    /// </summary>
    public void DamageTool()
    {
        DamageTool(Default_DamageValue);
    }

    /// <summary>
    /// Damages the tool durability for "damage" ammount;
    /// Makes the tool unusable if it is damaged too much (durability less or equal than 0)
    /// </summary>
    /// <param name="damage"> Ammount to damage the tool </param>
    public void DamageTool(int damage)
    {
        if (durability > 0)
            durability -= damage;

        if (durability <= 0)
            durability = 0;

        if (!IsToolDurable())
            isToolDurable = false;
    }

    /// <summary>
    /// Repairs the tool's durability by a default value
    /// </summary>
    public void RepairTool()
    {
        RepairTool(Default_RepairValue);
    }

    /// <summary>
    /// Repairs the tool's durability by "ammount"
    /// </summary>
    /// <param name="ammount"> Ammount to repair the tool </param>
    public void RepairTool(int ammount)
    {
        durability += ammount;
        isToolDurable = true;
        if (durability > Default_MaxDurability)
            durability = Default_MaxDurability;
    }

    /// <summary>
    /// Use() method for the tool. 
    /// Tool can only be used if it is durable
    /// </summary>
    public void Use()
    {
        // WIP IN THE FUTURE
        if (isToolDurable == false || isUsable == false)
            return;
        // do smth
    }

}

