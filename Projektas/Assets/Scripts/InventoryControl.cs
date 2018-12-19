using ResourcesTypes;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryControl : MonoBehaviour {
    GameObject inventory;
    List<InventoryItem> items;
    public List<GameObject> placeholders;
    public Sprite[] icons;

	// Use this for initialization
	void Start () {
        inventory = GameObject.Find("Inventory");
        inventory.SetActive(false);
        Inventory.Instance.GetItemPresets(); //it is important that presets are added as soon as possible

        CreateTestInventoryData();
        CreateInventoryItems();
        //Inventory.Instance.GetItemPresets();
	}
	
	// Update is called once per frame
	void Update ()
    {
        InventoryControling();
    }

    void InventoryControling()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventory.activeSelf)
            {
                inventory.SetActive(false);
            }
            else
            {
                CreateInventoryItems();
                inventory.SetActive(true);
            }
            Time.timeScale = 1f - Time.timeScale;
        }
    }

    public void CreateInventoryItems()
    {
        ClearPlaceHolders();
        Inventory inventory = Inventory.Instance;
        items = new List<InventoryItem>();
        for(int i = 0; i < placeholders.Count; i++)
        {
            items.Add(new InventoryItem(placeholders[i], i, inventory.GetItem(i)));
        }
    }

    private void ClearPlaceHolders()
    {
        if (items == null) return;

        for (int i = 0; i < placeholders.Count; i++)
        {
            Destroy(placeholders[i].GetComponent<EventTrigger>());
        }
    }

    void CreateTestInventoryData()
    {
        Inventory inventory = Inventory.Instance;

        //inventory.SetPreset(0, new Tool(0, "Pickaxe", 20, 20, true, "", icons[0], ToolType.Mining));
        //inventory.SetPreset(1, new Tool(1, "Axe", 1, 10, true, "", icons[0], ToolType.Woodcutting));
        //inventory.SetPreset(10, new Resource(10, "Wood", 1, "", icons[1], ResourceType.Wood));
        inventory.SetPreset(99, new Resource(99, "Cocos", 1, "", icons[2], ResourceType.Cocos));
        //inventory.AddItem(1);
        //inventory.AddItem(1);
        //inventory.AddItem(4);
      //  inventory.AddItem(10);
        //inventory.AddItem(10);
        //inventory.AddItem(99);
        //inventory.AddItem(99);
        //inventory.AddItem(99);
        inventory.AddItem(16);
        inventory.AddItem(16);
        inventory.AddItem(16);
        inventory.AddItem(16);
        inventory.AddItem(16);/*
        inventory.AddItem(17); inventory.AddItem(17); inventory.AddItem(17); inventory.AddItem(17);
        inventory.AddItem(17);*/


        //inventory.AddItem(2);
        //inventory.AddItem(16);
        //inventory.AddItem(16);
        //inventory.AddItem(16);
        //inventory.AddItem(17);
        //inventory.AddItem(17);
        //inventory.AddItem(17);
        //inventory.AddItem(17);

    }
}
