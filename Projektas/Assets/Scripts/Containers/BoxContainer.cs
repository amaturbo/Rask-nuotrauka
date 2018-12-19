using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxContainer :MonoBehaviour/*: IContainer*/ {


    // public List<IItem> itemContainer = new List<IItem>();

    //starting items IDs
    public List<int> startingItems;

    //item id pool, from which random items will be chosen
    public List<int> randomItemSelectionPool;

    //ramdom items temp container
    List<int> randomItems = new List<int>();
    
    //how many random items to add
    public int randomItemCount;
    
    //if true, it will generate random items
    public bool hasRandomItems;


    void getRandomItems()
    {
        if (randomItemSelectionPool.Count > 0)
        {
            for (int i = 0; i < randomItemCount; i++)
            {
                randomItems.Add(randomItemSelectionPool[Random.Range(0, randomItemSelectionPool.Count - 1)]);
            }
        }
    }


    public void GetItems()
    {
        Inventory inventory = Inventory.Instance;

        if (startingItems.Count > 0)
        {
            foreach (int id in startingItems)
            {
                inventory.AddItem(id);
            }
        }

        if (hasRandomItems == true)
        {
            getRandomItems();
            foreach (int id in randomItems)
            {
                inventory.AddItem(id);
            }
        }


    }



}
