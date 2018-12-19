using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class RecipeHandler : MonoBehaviour, IPointerDownHandler {

    public Recipe recipe;

    Inventory inventory;

    CraftingMenuScript CraftingMenu;

    public int Index;

    GameObject RecipeList;

    bool isEnoughItems;


    public RecipeHandler(Recipe recipe)
    {
        this.recipe = recipe;
    }

    private void Start()
    {
        CraftingMenu = GameObject.Find("Canvas").GetComponent<CraftingMenuScript>();
        RecipeList = GameObject.Find("RecipeList");
        inventory = Inventory.Instance;
       // Instantiate()
    }

    public void SetPanelName()
    {
        this.gameObject.transform.GetChild(1).transform.GetComponent<Text>().text = recipe.itemToCraft.Name;
    }
    public void SetPanelIcon()
    {
        this.gameObject.transform.GetChild(0).transform.GetComponent<Image>().sprite = /*Resources.Load<Sprite>("Wood.png");*/recipe.itemToCraft.Icon;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("selected item ID:" + recipe.itemToCraftID);
        CraftingMenu.EnableRecipeInfoPanel();

        //  CraftingMenu.test(); //testing
        CraftingMenu.LoadRecipeInfo(CraftingMenu.recipeContainer[Index]);  //testing

        CraftingMenu.selectedItemID = recipe.itemToCraftID;
        Debug.Log("selected item ID:" + recipe.itemToCraftID);

        CraftingMenu.selectedRecipe = recipe;
       // CraftingMenu.enoughItems = IsEnoughRequiredItems();

        //isEnoughItems = IsEnoughRequiredItems();
        //if (isEnoughItems)
        //    Debug.Log("enough items to craft");
        //else
        //    Debug.Log("not enough items to craft");
    }


    //========================================================================================================

    /// <summary>
    /// checks whether the player has enough required items to craft the desired item
    /// </summary>
    /// <returns>true, if the player is able to craft the item</returns>
    /// 
    //public bool IsEnoughRequiredItems()
    //{        
    //    foreach (CraftingRequirement requirement in recipe.requiredItems)
    //    {
    //        int id = requirement.GetItemID();
    //        Debug.Log("reasd:"+ requirement.GetItem().ID);
    //        if (inventory.CheckIfItemExists(id))
    //        {
    //            if (inventory.CheckQuantity(id, requirement.GetQuantity()) == false)
    //                return false;
    //        }
    //        else
    //            return false;
    //    }
    //    return true;
    //}
}
