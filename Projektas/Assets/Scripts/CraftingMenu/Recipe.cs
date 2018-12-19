using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


public class Recipe  {

    public int itemToCraftID;
    public IItem itemToCraft;    
    public List<CraftingRequirement> requiredItems = new List<CraftingRequirement>();

  //  public int recipePanelIndex;
    

    public Recipe() { }

    public Recipe(int id, IItem item, List<CraftingRequirement> requirements)
    {
        itemToCraftID = id;
        itemToCraft = item;
        requiredItems = requirements;
        Debug.Log("======================================================RECIPE ID SOMETHING: " + itemToCraftID);
        
    //    this.gameObject.transform.GetChild(1).transform.GetComponent<Text>().text = "two";
    }

    public string RecipeToString()
    {
        string recipeRequirements = "";
        for (int i = 0; i < requiredItems.Count; i++)
        {
            recipeRequirements += requiredItems[i].RequirementToString() + Environment.NewLine;
        }
        return recipeRequirements;
    }

    public void PrintID()
    {
        Debug.Log("++++++++++++++++++++PRINT ID: " + itemToCraftID);
    }



//========================================================================================================

    /// <summary>
    /// checks whether the player has enough required items to craft the desired item
    /// </summary>
    /// <returns>true, if the player is able to craft the item</returns>
    //public bool IsEnoughRequiredItems()
    //{
    //    foreach (CraftingRequirement reqs in requiredItems)
    //    {
    //        int count = reqs.GetQuantity();




    //        if (reqs.GetItem().GetType() == typeof(Resource))
    //        {
    //            for (int i = 0; i < inventory.items.Count; i++)
    //            {
    //                if (reqs.GetItem().ID == inventory.items[i].ID)
    //                {
    //                    Resource tempResource = (Resource)inventory.items[i];

    //                    //    count = count - tempResource.;
    //                }
    //            }
    //        }

    //        else
    //        {
    //            for (int i = 0; i < inventory.items.Count; i++)
    //            {
    //                if (reqs.GetItem().ID == inventory.items[i].ID)
    //                    count--;
    //            }
    //            if (count > 0)
    //                return false;
    //        }
    //    }
    //    return true;
    //}
    //public bool IsEnounghResources()
    //{

    //}


}
