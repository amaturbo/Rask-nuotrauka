using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Tools;
using Missions;
using UnityEngine.SceneManagement;
using Notifications;

public class PlayerController : MonoBehaviour
{
    private const float StopDistance = 0.1f;
    private const string GroundTag = "Ground",
        PickableItemTag = "PickableItem",
        WorkableItemTag = "WorkableItem",
        ShelterTag = "Shelter";

    private const int PickAxeId = 0,
        AxeId = 1;

    [SerializeField]
    private float runningEnergyUsage = 1,
        energyAutoRegeneration = 1,
        hungerRateInDays,
        hungerToMinusPerWorkingSec,
        speed = 15,
        speedWithoutEnergy = 5;
    [SerializeField]
    private Slider hungerSlider,
        energySlider;
    [SerializeField]
    private AudioClip[] sounds;
    private float hungerToMinusPerSec;
    private NavMeshAgent agent;
    private Animator anim;
    private bool isRunning,
        isWorking,
        isRed;
    private GameObject target = null,
        deathPanel;
    private Player player;
    private WorkableItemInfo workableItem = null;

    public GameObject shelter;
    public BuildMenu buildMenu;
    public Princing pricing;
    public MainMenu mainMenu;
    public Vector3 _lastPos;
    bool isMouseOverMenu;

    #region events
    void Start()
    {
        player = Player.GetPlayer();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        ResolveHungerToMinus();
        InvokeRepeating("UpdateEverySec", 1, 1);
        _lastPos = player.Obj.transform.position;
        isMouseOverMenu = false;
        deathPanel = GameObject.Find("DeathPanel");
        deathPanel.SetActive(false);
    }

    private void Update()
    {
        MouseClicks();
        Movement();
        CalculateWalkedDistance();
        UpdatePlayerParams();
    }

    // update which occurs every second
    private void UpdateEverySec()
    {
        player.SetHunger(player.HungerLevel - hungerToMinusPerSec);
        if (isRunning && Player.GetPlayer().EnergyLevel > 10)
            player.SetEnergy(player.EnergyLevel - runningEnergyUsage);
        else
            player.SetEnergy(player.EnergyLevel + energyAutoRegeneration);

        Text t = energySlider.transform.parent.Find("Text").gameObject.GetComponent<Text>();
        if (player.EnergyLevel < 10)
        {
            if (isRed)
                t.color = Color.white;
            else
            {
                t.color = Color.red;
                //Notification.New().Show("Not enough energy!", 1.5f, NotificationType.Error);
            }
            isRed = !isRed;
        }
        else
        {
            t.color = Color.white;
            isRed = false;
        }

    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == ShelterTag)
        {
            energyAutoRegeneration -= col.gameObject.GetComponent<ShelterRestTrigger>().restSpeed;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (target && col.gameObject == target)
        {
            if (col.gameObject.tag == PickableItemTag)
            {
                // TODO: Add item to inventory
                //Inventory.Instance.AddItem();

                col.gameObject.GetComponent<BoxContainer>().GetItems();
                print("item picked up!!! \n Implement adding to inventory.");
                anim.SetTrigger("Pickup");
                Destroy(col.gameObject);
                target = null;
                isRunning = false;
                Notification.New().Show("You picked up item!", 0.5f, NotificationType.Success);
                pricing.PaintBuilding(buildMenu.shelter);
                pricing.PaintBuilding(buildMenu.turret);
                if (player.CurrentMission.Type == MissionType.PickupCocos)
                {
                    player.CurrentMission.AddProgress(1);
                }
            }
            else if (col.gameObject.tag == WorkableItemTag)
            {
                workableItem = col.gameObject.GetComponent<WorkableItemInfo>();
                isRunning = false;
                if (Inventory.Instance.HasItem(workableItem.requiredItemId))
                {
                    isWorking = true;
                    // TODO: start visuals for working
                    InvokeRepeating("WorkResource", 1, 1);
                    ControlWorkAnimation(true);
                }
                else
                {
                    Notification.New().Show("You don't have required item!", 3, NotificationType.Error);
                }
            }
        }
        if (col.gameObject.tag == ShelterTag)
        {
            energyAutoRegeneration += col.gameObject.GetComponent<ShelterRestTrigger>().restSpeed;
        }
    }

    private void MouseClicks()
    {
        if (Input.GetMouseButtonDown(0) && !buildMenu.isBuilding && !isMouseOverMenu)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, Mathf.Infinity);

            if (isWorking)
                StopWorking();

            if (hit.transform.gameObject.tag == GroundTag || hit.transform.gameObject.tag == ShelterTag)
            {
                MoveTo(new Vector3(hit.point.x, hit.point.y, hit.point.z));
                target = null;
            }
            else if (hit.transform.gameObject.tag == PickableItemTag ||
                hit.transform.gameObject.tag == WorkableItemTag)
            {
                MoveTo(new Vector3(hit.point.x, hit.point.y, hit.point.z));
                target = hit.transform.gameObject;
            }
        }
    }
    #endregion

    #region functions
    private void WorkResource()
    {
        var inv = Inventory.Instance;
        workableItem.resourceQty--;
        if (inv.HasItem(workableItem.resourceId))
            inv.AddResourceQuantity(workableItem.resourceId, 1);
        else
            inv.AddItem(workableItem.resourceId);
        pricing.PaintBuilding(buildMenu.shelter);
        pricing.PaintBuilding(buildMenu.turret);

        //Tool temp = (Tool)inv.GetItemById(workableItem.requiredItemId);
        //inv.RemoveItem(temp);
        //temp.Durability -= workableItem.itemUsagePerResource;
        //inv.AddItem(temp);

        player.SetHunger(player.HungerLevel - hungerToMinusPerWorkingSec);
        if (workableItem.resourceQty < 1)
        {
            StopWorking();
            Destroy(workableItem.gameObject);
            WorkMissionProgress(workableItem);
            Notification.New().Show("You cut down tree!", 3, NotificationType.Success);
        }
    }

    private void WorkMissionProgress(WorkableItemInfo workableItem)
    {
        if (player.CurrentMission.Type == MissionType.CutDownTrees)
        {
            if (workableItem.RequiredToolType == ToolType.Woodcutting)
            {
                player.CurrentMission.AddProgress(1);
            }
        }
    }

    private void StopWorking()
    {
        ControlWorkAnimation(false);
        isWorking = false;
        CancelInvoke("WorkResource");
        // TODO: stop visuals for working
    }

    private void Movement()
    {
        if (player.EnergyLevel <= 10)
        {
            agent.speed = speedWithoutEnergy;
        }
        else
        {
            agent.speed = speed;
        }
        if ((!target && agent.remainingDistance < StopDistance) || !isRunning)
        {
            agent.Stop();
            Player.GetPlayer().audioSource.Stop();
            isRunning = false;
        }
        anim.SetBool("isRunning", isRunning);
        // Running sounds
        if (!Player.GetPlayer().audioSource.isPlaying && isRunning == true)
        {
            Player.GetPlayer().audioSource.Play();
        }
    }

    private void MoveTo(Vector3 destination)
    {
        agent.SetDestination(destination);
        agent.Resume();
        isRunning = true;
    }

    private void Death()
    {
        deathPanel.SetActive(true);
        Time.timeScale = 0.0f;
        int day = (int)GameObject.Find("GM").GetComponent<DayNightCycleController>().day - 1;
        int hour = (int)GameObject.Find("GM").GetComponent<DayNightCycleController>().GameTime - 13;
        if (hour < 0)
        {
            day--;
            hour = 24 + hour;
        }
        if (day < 0)
            day = 0;
        deathPanel.transform.Find("ScoreText").gameObject.GetComponent<Text>().text =
            "You survived: " + day + " days, " + hour + " hours";
        mainMenu.Count(day, hour);
    }

    private void UpdatePlayerParams()
    {
        hungerSlider.value = player.HungerLevel;
        if (player.HungerLevel <= 0)
            Death();

        energySlider.value = player.EnergyLevel;
    }

    private void ResolveHungerToMinus()
    {
        var dayInSeconds = GameObject.Find("GM").GetComponent<DayNightCycleController>().TimeRateMins * 60;
        hungerToMinusPerSec = 100 / (hungerRateInDays * dayInSeconds);
    }

    // add hunger level, when eating something
    public void AddHungerLevel(float value)
    {
        player.SetHunger(player.HungerLevel + value);
    }

    private void ControlWorkAnimation(bool action)
    {
        switch (workableItem.RequiredToolType)
        {
            case ToolType.Mining:
                anim.SetBool("isMining", action);
                break;
            case ToolType.Woodcutting:
                anim.SetBool("isWoodcutting", action);
                if (action == true)
                {
                    AudioSource.PlayClipAtPoint(sounds[0], Player.GetPlayer().Obj.transform.position);
                }
                break;
        }
    }

    private void CalculateWalkedDistance()
    {
        double distance = Vector3.Magnitude(Player.GetPlayer().Obj.transform.position - _lastPos) / 10;
        if (Player.GetPlayer().CurrentMission.Type == MissionType.Walk)
        {
            Player.GetPlayer().CurrentMission.AddProgress(distance);
        }
        _lastPos = Player.GetPlayer().Obj.transform.position;
    }

    public void MouseHoverChange(bool value)
    {
        isMouseOverMenu = value;
    }

    public void TakeDamage(float damage)
    {
        player.SetHunger(player.HungerLevel - damage);
    }

    // reiktu perkelt i kita scripta
    public void Restart()
    {
        SceneManager.LoadScene("MainMenu");
    }
    #endregion
}