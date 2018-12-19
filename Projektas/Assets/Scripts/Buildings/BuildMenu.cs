using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;
using Notifications;

public class BuildMenu : MonoBehaviour/*, IPointerClickHandler*/ {
    bool isOpen;
    public bool isBuilding;
    public RectTransform container;
    Vector3 currentMousePos;
    public Buildings shelter;
    public Buildings turret;
    public Princing pricing;

    [Header("Resource id's")]
    public int coalId;
    public int woodId;
    public int rockId;
    public int clayId;
    
    GameObject obj;
    public LayerMask Ground;
    private GhostTrigger triggerGreen;
    private GhostTrigger triggerRed;
    private GameObject red;
    private GameObject green;
    private Buildings building;

    // Use this for initialization
    void Start () {
        isOpen = false;
        isBuilding = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) isOpen = !isOpen;  // control for PC
        Vector3 position = container.anchoredPosition;
        position.y = Mathf.MoveTowards(position.y, isOpen ? 285 : 0, Time.deltaTime * 1000);
        container.anchoredPosition = position;
        if (isBuilding)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, Mathf.Infinity, Ground);
            currentMousePos = hit.point;
            obj.transform.position = new Vector3(currentMousePos.x, currentMousePos.y, currentMousePos.z);

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                isBuilding = false;
                obj.SetActive(false);
                Notification.New().Show("Building cancelled", 1.5F, NotificationType.Error);
            }

            if (triggerGreen.cantBuild && green.activeInHierarchy)
            {
                obj.SetActive(false);
                triggerGreen.cantBuild = false;
                obj = red;
                obj.SetActive(true);
            }
            else if (!triggerRed.cantBuild && red.activeInHierarchy)
            {
                obj.SetActive(false);
                obj = green;
                obj.SetActive(true);
            }

            if (Input.GetMouseButtonDown(0) && green.activeInHierarchy)
            {
                isBuilding = false;
                obj.SetActive(false);
                building.Prefab = (GameObject)Instantiate(building.Prefab, obj.transform.position, Quaternion.identity);
                Notification.New().Show("Building completed!", 3, NotificationType.Success);
                RemoveResources(building);
                pricing.PaintBuilding(shelter);
                pricing.PaintBuilding(turret);
            }
        }
    }

    // controls for the smartphone
    /*public void OnPointerClick(PointerEventData eventData)
    {
        isOpen = !isOpen;
    }*/

    public void ShelterButton()
    {
        Button(shelter);
    }

    public void TurretButton()
    {
        Button(turret);
    }

    void Button(Buildings B)
    {
        if (!CheckResources(B))
        {
            Notification.New().Show("Not enough resources!", 3, NotificationType.Error);
            return;
        }
        B.Ghost = (GameObject)Instantiate(B.Ghost);
        B.Ghost.SetActive(false);
        triggerGreen = B.Ghost.GetComponent<GhostTrigger>();
        B.Red = (GameObject)Instantiate(B.Red);
        B.Red.SetActive(false);
        triggerRed = B.Red.GetComponent<GhostTrigger>();
        green = B.Ghost;
        red = B.Red;
        obj = B.Ghost;
        building = B;

        isBuilding = !isBuilding;
        obj.SetActive(true);
    }

    public bool CheckResources(Buildings building)
    {
        var inv = Inventory.Instance;
        bool AllGood = true;

        if (building.ClayCost > 0)
            if (inv.HasItem(clayId))
                if (inv.CheckQuantity(clayId, building.ClayCost))
                    AllGood = AllGood;
                else AllGood = false;
            else AllGood = false;

        if (building.CoalCost > 0)
            if (inv.HasItem(coalId))
                if (inv.CheckQuantity(coalId, building.CoalCost))
                    AllGood = AllGood;
                else AllGood = false;
            else AllGood = false;

        if (building.WoodCost > 0)
            if (inv.HasItem(woodId))
                if (inv.CheckQuantity(woodId, building.WoodCost))
                    AllGood = AllGood;
                else AllGood = false;
            else AllGood = false;

        if (building.RockCost > 0)
            if (inv.HasItem(rockId))
                if (inv.CheckQuantity(rockId, building.RockCost))
                    AllGood = AllGood;
                else AllGood = false;
            else AllGood = false;

        return AllGood;
    }

    public void RemoveResources(Buildings building)
    {
        var inv = Inventory.Instance;
        if (building.WoodCost > 0)
            inv.RemoveResourceQuantity(woodId, building.WoodCost);
        if (building.ClayCost > 0)
            inv.RemoveResourceQuantity(clayId, building.ClayCost);
        if (building.CoalCost > 0)
            inv.RemoveResourceQuantity(coalId, building.CoalCost);
        if (building.RockCost > 0)
            inv.RemoveResourceQuantity(rockId, building.RockCost);
    }

}
