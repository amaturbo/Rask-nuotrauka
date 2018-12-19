using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Princing : MonoBehaviour {

    public BuildMenu BM;
    public Text shelterRock;
    public Text shelterWood;
    public Text turretRock;
    public Text turretWood;

    void Start () {
        shelterRock.text = BM.shelter.RockCost.ToString();
        shelterWood.text = BM.shelter.WoodCost.ToString();
        turretRock.text = BM.turret.RockCost.ToString();
        turretWood.text = BM.turret.WoodCost.ToString();
        Paint(BM.shelter, shelterRock, shelterWood);
        Paint(BM.turret, turretRock, turretWood);
    }
	
	void Update () {

    }

    public void PaintBuilding(Buildings building)
    {
        if (building == BM.shelter)
            Paint(building, shelterRock, shelterWood);
        else if(building == BM.turret)
            Paint(building, turretRock, turretWood);
    }

    void Paint(Buildings building, Text rock, Text wood)
    {
        var inv = Inventory.Instance;
        Color colorG = new Color(0, 1, 0, 1);
        Color colorR = new Color(1, 0, 0, 1);
        rock.color = colorG;
        wood.color = colorG;

        /*if (building.ClayCost > 0)
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
            else AllGood = false;*/

        if (building.WoodCost > 0)
            if (inv.HasItem(BM.woodId))
                if (inv.CheckQuantity(BM.woodId, building.WoodCost))
                    wood.color = colorG;
                else wood.color = colorR;
            else wood.color = colorR;

        if (building.RockCost > 0)
            if (inv.HasItem(BM.rockId))
                if (inv.CheckQuantity(BM.rockId, building.RockCost))
                    rock.color = colorG;
                else rock.color = colorR;
            else rock.color = colorR;
    }
}
