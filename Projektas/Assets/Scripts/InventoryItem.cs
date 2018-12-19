using ResourcesTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem {
    public int Index { get; private set; }
    public GameObject Obj { get; private set; }
    public IItem Item { get; private set; }

    public InventoryItem(GameObject obj, int index, IItem item)
    {
        Obj = obj;
        Index = index;
        Item = item;
        SetImage(item == null ? null : item.Icon);
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) => { OnClick(); });
        Obj.AddComponent<EventTrigger>().triggers.Add(entry);
        if(Item == null || Item.Quantity == 0)
        {
            Obj.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            Obj.transform.GetChild(0).gameObject.SetActive(true);
            Obj.transform.GetChild(0).gameObject.GetComponent<Text>().text = item.Quantity.ToString();
        }
    }

    public void SetImage(Sprite sprite)
    {
        Obj.GetComponent<Image>().sprite = sprite;
        if(sprite == null)
        {
            Obj.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        }
        else
        {
            Obj.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    public void OnClick()
    {
        if (Item is Resource)
        {
            Resource res = Item as Resource;
            if (res.Type == ResourceType.Cocos)
            {
                Player.GetPlayer().EatFood(Item as Resource);
                Inventory.Instance.RemoveItem(Item);
                GameObject.Find("InventoryCanvas").GetComponent<InventoryControl>().CreateInventoryItems();
            }
        }
    }
}
