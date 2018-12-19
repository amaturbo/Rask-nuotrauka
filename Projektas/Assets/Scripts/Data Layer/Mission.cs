using Missions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Missions
{
    public enum MissionType
    {
        Walk = 0,
        CutDownTrees = 1,
        PickupCocos = 2
    }
}

public class Mission {
    public string Description { get; private set; }
    public List<IItem> Rewards { get; private set; }
    public MissionType Type { get; private set; }
    public double Requirements { get; private set; }
    public double Progress { get; set; }
    public string ProgressPercentage { get { return String.Format("{0}%", (int)(Progress / Requirements * 100)); } }
    private GameObject _missionCanvas;

    public Mission(string description, MissionType type, double requirements, List<IItem> rewards)
    {
        Description = description;
        Type = type;
        Requirements = requirements;
        Rewards = new List<IItem>(rewards);
        Progress = 0;
        _missionCanvas = GameObject.Find("MissionsCanvas");
    }

    public void AddProgress(double amount)
    {
        Progress += amount;
        UpdateProgress();
        if(Progress / Requirements >= 1)
        {
            FinalizeMission();
        }
    }

    public void FinalizeMission()
    {
        GivePlayerAwards();
        Player.GetPlayer().CompleteMission();
    }

    public void GivePlayerAwards()
    {
        foreach (IItem item in Rewards)
        {
            Inventory.Instance.AddItem(item.ID);
        }
        string text = String.Format("You have completed mission! As a reward you got {0} x {1}", Rewards.Count, Rewards[0].Name);
        Notification.New().Show(text, 5, Notifications.NotificationType.Success);
    }

    public void UpdateText()
    {
        Text text = _missionCanvas.transform.GetChild(0).GetComponentInChildren<Text>();
        text.text = Description;
    }

    public void UpdateProgress()
    {
        Text text = _missionCanvas.transform.GetChild(1).GetComponentInChildren<Text>();
        text.text = ProgressPercentage;
        Image img = _missionCanvas.transform.GetChild(1).GetChild(1).GetComponent<Image>();
        img.fillAmount = (float)(Progress / Requirements);
    }

    public void Update()
    {
        UpdateText();
        UpdateProgress();
    }

}
