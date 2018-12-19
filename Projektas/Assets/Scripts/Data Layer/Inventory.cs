using ResourcesTypes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tools;
using UnityEngine;

public class Inventory {

    //----singleton
    /// <summary>
    /// Instance of the inventory
    /// </summary>
    private static Inventory instance;

    private Inventory() { }

    public static Inventory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Inventory();
            }
            return instance;
        }
    }
    //----singleton


    //properties
    List<IItem> items = new List<IItem>();

    public Dictionary<int, IItem> itemPresets = new Dictionary<int, IItem>();

    float totalWeight = 0.00f;


    //constructors


    //methods

    public void SetPreset(int ind, IItem item)
    {
        itemPresets[ind] = item;
    }



    public void AddResourceQuantity(int id, int quantityToAdd)
    {
        items.Find(t => t.ID == id).Quantity += quantityToAdd;
    }

    public void RemoveResourceQuantity(int id, int quantityToRemove)
    {
        var res = items.Find(t => t.ID == id).Quantity -= quantityToRemove;
        if (res <= 0)
        {
            IItem item = itemPresets[id];
            RemoveItem(item);
        }
    }

    public bool CheckQuantity(int id, int quantityNeeded)
    {
        //DOESNT WORK IF THE ITEM DOES NOT EXIST IN INVENTORY
        int qty = items.Find(t => t.ID == id).Quantity;
        Debug.Log(items.Find(t => t.ID == id).Quantity + "quaantity" + id);
        if (qty >= quantityNeeded)
            return true;
        else return false;
    }

    public bool CheckIfItemExists(int id)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == id)
                return true;
        }
        return false;
    }



    public void AddItem(int id)
    {
        IItem preset = itemPresets[id];
        IItem newItem = preset.Copy();
        if (newItem is Resource)
        {
            if(GetItemById(newItem.ID) == null)
            {
                newItem.Quantity++;
                items.Add(newItem);
            }
            else
            {
                GetItemById(newItem.ID).Quantity++;
            }
        }
        else
        {
            items.Add(newItem);
        }
        totalWeight += newItem.Weight;
        //Test
        //newItem.ItemInfo = "Nauja info";        
    }

    public void AddItem(IItem item)
    {

        totalWeight += item.Weight;
        if (item is Resource)
        {
            if (GetItemById(item.ID) == null)
            {
                item.Quantity++;
                items.Add(item);
            }
            else
            {
                GetItemById(item.ID).Quantity++;
            }
        }
        else
        {
            items.Add(item);
        }
    }

    public IItem GetItem(int id)
    {
        if(id >= items.Count || id < 0)
        {
            return null;
        }

        return items[id];
    }

    public IItem GetItemById(int id)
    {
        return items.Where(t => t.ID == id).FirstOrDefault();
    }

    /// <summary>
    /// This method should create Item Preset database
    /// </summary>
    public void GetItemPresets()
    {
        CreateItemPresets();
        //work in progress
        // insert code to get item presets from a database file
    }

    public void CreateItemPresets()
    {;
        ItemDatabase itemDB = new ItemDatabase();
        itemDB.InitiateDB();
        foreach(KeyValuePair<int, IItem> entry in itemDB.ItemPrefabsDatabase )
        {
            SetPreset(entry.Key, entry.Value);
        }
        //itemPresets = new Dictionary<int, IItem>(itemDB.ItemPrefabsDatabase);      
        //itemPresets = itemDB.ItemPrefabsDatabase;


        ////Tool PickAxe =      new Tool(0, "Pick Axe", 1, 10),
        ////    Axe =           new Tool(1, "Axe", 1, 10);

        //Resource Wood =     new Resource(100, "Wood", 1, "", ResourceType.Wood),
        //    Stone =         new Resource(101, "Stone", 1, "", ResourceType.Stone);

        ////itemPresets.Add(PickAxe.ID, PickAxe);
        ////itemPresets.Add(Axe.ID, Axe);
        //itemPresets.Add(Wood.ID, Wood);
        //itemPresets.Add(Stone.ID, Stone);
        //Debug.Log(itemPresets[3].ItemInfo);
    }

    void TestInventory()
    {
        GetItemPresets();

        int id = 2;
        var type = itemPresets[id].GetType();
        Debug.Log("Name: " + /*inv.*/itemPresets[id].Name + " , Type: " + type);
        Debug.Log("Name: " + /*inv.*/itemPresets[15].Name + " , Type: " + itemPresets[15].GetType());

        AddItem(id);
        Debug.Log("Name: " + /*inv.*/itemPresets[id].Name + " , Type: " + itemPresets[id].GetType() + " Info: " + itemPresets[id].ItemInfo);
        Debug.Log("Name: " + /*inv.*/items[0].Name + " , Type: " + items[0].GetType() + " Info: " + items[0].ItemInfo);

    }

    public void RemoveSomeItems(int id, int count)
    {
        IItem item = GetItemById(id);
        for (int i = 0; i < count; i++)
        {
            RemoveItem(item);
        }
    }

    // WORK IN PROGRESS
    public void RemoveItem(IItem item)
    {
        // work in progess

        totalWeight -= item.Weight;
        item.Quantity--;
        if (item.Quantity < 1)
        {
            items.Remove(item);
        }
        // or
        //IItem temp = items[index];
        //temp.Destroy();
        //items[index] = null; 
    }

    public bool HasItem(int id)
    {
        return (items.Where(t => t.ID == id).Count() > 0);
    }
}
