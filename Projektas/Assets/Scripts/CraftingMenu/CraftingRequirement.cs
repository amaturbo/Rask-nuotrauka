using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRequirement /*: MonoBehaviour*/ {

    IItem Item;
    int Quantity;

    public CraftingRequirement( IItem item, int quantity)
    {
        Item = item;
        Quantity = quantity;
    }

    public IItem GetItem()
    {
        return Item;
    }

    public int GetQuantity()
    {
        return Quantity;
    }

    public string RequirementToString()
    {
        return Item.Name + ": " + Quantity;
    }

    public int GetItemID()
    {
        return Item.ID;
    }


}
