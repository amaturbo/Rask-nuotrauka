using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Missions;

public class MissionsControl : MonoBehaviour {

    bool _expanded;
    bool _isMoving;
	// Use this for initialization
	void Start () {
        CreateMissions();
        _expanded = false;
	}

    void FixedUpdate()
    {
        MovePanel();
    }

    public void CreateMissions()
    {
        Player player = Player.GetPlayer();
        var rewards = new List<IItem>();
        rewards.Add(Inventory.Instance.itemPresets[16]);
        player.AddMission(new Mission("Walk 30 meters", MissionType.Walk, 30, rewards));
        rewards = new List<IItem>();
        rewards.Add(Inventory.Instance.itemPresets[99]);
        rewards.Add(Inventory.Instance.itemPresets[99]);
        rewards.Add(Inventory.Instance.itemPresets[99]);
        player.AddMission(new Mission("Cut down 3 trees", MissionType.CutDownTrees, 3, rewards));
        rewards = new List<IItem>();
        rewards.Add(Inventory.Instance.itemPresets[2]);
        player.AddMission(new Mission("Pick up 10 cocos", MissionType.PickupCocos, 10, rewards));
        Player.GetPlayer().CurrentMission.Update();
    }

    public void ToggleMissionView()
    {
        _isMoving = true;
        _expanded = !_expanded;
    }

    void MovePanel()
    {
        GameObject panel = this.gameObject.transform.GetChild(0).gameObject;
        Vector3 poz = panel.GetComponent<RectTransform>().anchoredPosition;
        if(_isMoving)
        {
            if(_expanded)
            {
                if(poz.x < 340)
                {
                    float calc = Mathf.Lerp(poz.x, 340, Time.deltaTime * 10);
                    poz.x = calc;
                    panel.GetComponent<RectTransform>().anchoredPosition = poz;
                }
                else
                {
                    _isMoving = false;
                }

            }
            else
            {
                if (poz.x > -360)
                {
                    float calc = Mathf.Lerp(poz.x, -360, Time.deltaTime * 10);
                    poz.x = calc;
                    panel.GetComponent<RectTransform>().anchoredPosition = poz;
                }
                else
                {
                    _isMoving = false;
                }
            }
        }
    }
}
