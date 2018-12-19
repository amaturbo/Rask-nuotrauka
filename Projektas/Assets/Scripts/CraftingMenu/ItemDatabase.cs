using ResourcesTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase  {


    public Dictionary<int, IItem> ItemPrefabsDatabase = new Dictionary<int, IItem>();
    

    // Use this for initialization
    public void InitiateDB ()
    {
        Tool testTool = new Tool(2, "Stone Hammer", 2.00f, 60, true, "A simple hammer. Used to build stuff", Resources.Load<Sprite>("Stone hammer"), Tools.ToolType.None);
        ItemPrefabsDatabase.Add(testTool.ID, testTool);
        

        //Resource testResource = new Resource(15, "Coal", 1.0f, "100% pure Black coal", 50);
        //ItemPrefabsDatabase.Add(testResource.ID, testResource);

        ItemPrefabsDatabase.Add(3, new Tool(3, "Stone axe", 5.00f, 100, true, "A stone axe. Used to cut wood", Resources.Load<Sprite>("Stone axe"), Tools.ToolType.Woodcutting));
        ItemPrefabsDatabase.Add(4, new Tool(4, "Stone pickaxe", 5.00f, 90, true, "A stone pickaxe. Used to mine stuff", Resources.Load<Sprite>("Stone pickaxe"), Tools.ToolType.Mining));

        ItemPrefabsDatabase.Add(16, new Resource(16, "Rock", 1.0f, "Simple rock"/*, 9*/, Resources.Load<Sprite>("Rock"), ResourceType.Stone));
        ItemPrefabsDatabase.Add(17, new Resource(17, "Wood", 1.0f, "Some wood"/*, 10*/, Resources.Load<Sprite>("Wood"), ResourceType.Wood));   

    }




	
}
