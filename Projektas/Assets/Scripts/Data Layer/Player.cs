using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private static Player _instance = new Player();
    public GameObject Obj { get; private set; }
    public AudioSource audioSource { get; private set; }
    public float HungerLevel { get; private set; }
    public float EnergyLevel { get; private set; }
    public Queue<Mission> Missions { get; private set; }
    public Mission CurrentMission { get { return Missions.Peek(); } }

    private Player()
    {
        Obj = GameObject.FindGameObjectWithTag("Player");
        audioSource = Obj.GetComponent<AudioSource>();
        LoadData();
        Missions = new Queue<Mission>();
    }

    public static Player GetPlayer()
    {
        return _instance;
    }

    private void LoadData()
    {
        // it's temporary till normal loading
        HungerLevel = 100;
        EnergyLevel = 100;
    }

    public void SaveData()
    {

    }

    public void SetHunger(float value)
    {
        if (value >= 100)
            HungerLevel = 100;
        else
            HungerLevel = value;
    }

    public void SetEnergy(float value)
    {
        if (value >= 100)
            EnergyLevel = 100;
        else
            EnergyLevel = value;
    }

    public void AddMission(Mission mission)
    {
        Missions.Enqueue(mission);
    }

    public void CompleteMission()
    {
        Missions.Dequeue();
        CurrentMission.Update();
    }

    public void EatFood(Resource food)
    {
        HungerLevel += food.EnergyValue;
        EnergyLevel += food.EnergyValue * 3;
    }
}
