using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class CraftingMenuScript : MonoBehaviour/*, IPointerDownHandler*/ {

   
    public bool isMenuOn = false;
    GameObject CraftingMenu;
    GameObject RecipeInfoPanel;
    GameObject RecipeList;
    GameObject RecipeScroll;

    public List<Recipe> recipeContainer = new List<Recipe>();
    public List<RecipeHandler> recipeHandlers = new List<RecipeHandler>();

    RecipeDatabase recipeDB;

    public int selectedItemID = 15;

    public Inventory inventory;

    //just the prefab
    public GameObject recipePanelPrefab;

    public Recipe selectedRecipe;


    void Start()
    {

        CraftingMenu = GameObject.Find("CraftingMenu");
        RecipeInfoPanel = GameObject.Find("RecipeInfoPanel");
        RecipeList = GameObject.Find("RecipeList");
        RecipeScroll = GameObject.Find("RecipeContent");

        CraftingMenu.SetActive(false);
        RecipeInfoPanel.SetActive(false);
        inventory = Inventory.Instance; //GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

        //creating recipe database
        recipeDB = new RecipeDatabase();
        recipeDB.Start();
        ////adding an item into the database;
        //// recipeContainer.Add(recipeDB.recipeDB[0]);
        ////   recipeHandlers.Add(new )
        foreach (Recipe r in recipeDB.recipeDB)
        {
            recipeContainer.Add(r);
        }

        int recipeAmmount = 0;

        float y = 0;

        for (int i = 0; i < recipeContainer.Count; i++)
        {
            GameObject panel = Instantiate(recipePanelPrefab);
            panel.GetComponent<RecipeHandler>().Index = recipeAmmount++;
            panel.GetComponent<RecipeHandler>().recipe = recipeContainer[i];
            // panel.transform.parent = RecipeList.transform.GetChild(0).transform;
            panel.transform.SetParent(RecipeScroll.transform);


            panel.name = "RecipePanel." + i;            
            if (i == 0)
            {
                y = panel.transform.position.y;
            }
            panel.GetComponent<RectTransform>().localPosition = new Vector3(0/*panel.transform.position.x*/, y, 0);
            panel.GetComponent<RecipeHandler>().SetPanelName();
            panel.GetComponent<RecipeHandler>().SetPanelIcon();

            y -= 60;
        }

  //   //   GameObject panel = Instantiate(recipePanelPrefab);
    //    Debug.Log("AAAAAAAAAAAAAAAAAAAAAA:  " + recipeDB.recipeDB[0].itemToCraftID/*selectedItemID*/);
    }


    public void test()
    {
        //GameObject.Find("Canvas").GetComponent<RecipeDatabase>();        

     //   recipeContainer.Add(recipeDB.recipeDB[0]);

        //Debug.Log("RECIPE CONTAINER:");
        //int i = 0;
        //foreach (Recipe r in recipeContainer)
        //{
        //    Debug.Log(i++);
        //    Debug.Log(r.RecipeToString());
        //}
    }

    public void LoadRecipeInfo(Recipe recipe)
    {
        //Debug.Log("AAAAAAAAAAAAAAAAAAAAAA:  " + recipeContainer[0].itemToCraftID);
        //Debug.Log("B:  " + recipe.itemToCraftID);
        //Debug.Log("C:  " + selectedItemID);

        for (int i = 0; i < RecipeInfoPanel.transform.childCount; i++)
        {
            if (RecipeInfoPanel.transform.GetChild(i).transform.name == "RecipeDescription")
            {
                RecipeInfoPanel.transform.GetChild(i).transform.GetComponent<Text>().text = recipe.itemToCraft.ItemInfo;
            }
            else if (RecipeInfoPanel.transform.GetChild(i).transform.name == "RecipeRequirements")
            {
                RecipeInfoPanel.transform.GetChild(i).transform.GetComponent<Text>().text = recipe.RecipeToString();
            }
            else if (RecipeInfoPanel.transform.GetChild(i).transform.name == "RecipeInfoImage")
            {
                RecipeInfoPanel.transform.GetChild(i).transform.GetComponent<Image>().sprite = /*Resources.Load<Sprite>("Coal");*/recipe.itemToCraft.Icon;
            }

        }
    }
    
    public void EnableRecipeInfoPanel()
    {
        RecipeInfoPanel.SetActive(true);
    }

    public void DisableRecipeInfoPanel()
    {
        RecipeInfoPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isMenuOn = !isMenuOn;
            if (isMenuOn)
            {
                CraftingMenu.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                CraftingMenu.SetActive(false);
                RecipeInfoPanel.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public void CraftItem()
    {
        if (selectedRecipe != null && IsEnoughRequiredItems(selectedRecipe))
        {
            foreach (CraftingRequirement requirement in selectedRecipe.requiredItems)
            {
                if (requirement.GetItem().GetType() == typeof(Resource))
                {
                    inventory.RemoveSomeItems(requirement.GetItemID(), requirement.GetQuantity());
                }
                //maybe add damage tool?
            }
            Debug.Log("Crafting Item..." + selectedItemID);
            inventory.AddItem(selectedItemID);
            string message = selectedRecipe.itemToCraft.Name + " crafted!";
            Notification.New().Show(message, 2, Notifications.NotificationType.Success);
            Debug.Log("Item crafted");
        }
        else
        {
            Notification.New().Show("Not enough required items!", 2, Notifications.NotificationType.Error);
            Debug.Log("Not enough items!");
        }
    }

    /// <summary>
    /// checks whether the player has enough required items to craft the desired item
    /// </summary>
    /// <returns>true, if the player is able to craft the item</returns>
    /// 
    public bool IsEnoughRequiredItems(Recipe recipe)
    {
        foreach (CraftingRequirement requirement in recipe.requiredItems)
        {
            int id = requirement.GetItemID();
            Debug.Log("reasd:" + requirement.GetItem().ID);
            if (inventory.CheckIfItemExists(id))
            {
                if (requirement.GetItem().GetType() == typeof(Resource))
                    if (inventory.CheckQuantity(id, requirement.GetQuantity()) == false)
                        return false;
                    else
                        Debug.Log("wololo");
            }
            else
                return false;
        }
        return true;
        
    }



}
