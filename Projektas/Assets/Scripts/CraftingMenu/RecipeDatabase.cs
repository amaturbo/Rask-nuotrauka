using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeDatabase  {


    public List<Recipe> recipeDB = new List<Recipe>();
    public Dictionary<int, IItem> itemDB;
    public Inventory inventory;


	// Use this for initialization
	public void Start () {

        inventory = Inventory.Instance;//GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        itemDB = inventory.itemPresets;

        List<CraftingRequirement> reqs = new List<CraftingRequirement>();
        //stone axe

        
        reqs.Add(new CraftingRequirement(itemDB[2], 1));
        reqs.Add(new CraftingRequirement(itemDB[17], 2));
        reqs.Add(new CraftingRequirement(itemDB[16], 2));
        recipeDB.Add(new Recipe(3, itemDB[3], reqs));
        reqs = new List<CraftingRequirement>();
        

        //stone pickaxe
        //reqs.Add(new CraftingRequirement(itemDB[2], 1));
        //reqs.Add(new CraftingRequirement(itemDB[16], 4));
        //reqs.Add(new CraftingRequirement(itemDB[17], 2));

        //recipeDB.Add(new Recipe(4, itemDB[4], reqs));
        //reqs = new List<CraftingRequirement>();

        //stone hammer
        reqs.Add(new CraftingRequirement(itemDB[16], 1));
        reqs.Add(new CraftingRequirement(itemDB[17], 1));

        recipeDB.Add(new Recipe(2, itemDB[2], reqs));
        reqs = new List<CraftingRequirement>();
    }


}
