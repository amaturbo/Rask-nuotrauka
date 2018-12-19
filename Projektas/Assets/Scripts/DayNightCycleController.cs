using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DayNightCycleController : MonoBehaviour
{
    public float TimeRateMins = 1; // default 1 minute
    public float GameTime;
    // sunrise - 8, sunset - 20
    [SerializeField]
    private GameObject dirLight; // directional light

    public float day = 1;
    private GameObject dayText;


    void Awake()
    {
        GameTime = 13;
        dirLight.transform.rotation = Quaternion.Euler(0, dirLight.transform.rotation.y, dirLight.transform.rotation.z);
        dayText = GameObject.Find("DayText");
    }

    void Update()
    {
        ManageTime();
        RotateLight();
        dayText.GetComponent<Text>().text = "Day " + day + ", " + (int)GameTime + " hour";
    }

    private void ManageTime()
    {
        GameTime += Time.deltaTime * 24 / 60 / TimeRateMins; // divided by 60(converts to hour); multiplied by 24 * 6 (converts default cycle to 10mins)
        if (GameTime > 24)
        {
            GameTime -= 24;
            day++;
        }
    }

    private void RotateLight()
    {
        float xTarget;// when GameTime = 8, then x = 0
        if (GameTime > 8)
        {
            xTarget = (GameTime - 8) * 15;
        }
        else
        {
            xTarget = (GameTime + 16) * 15;
        }
        if (xTarget > 360)
            xTarget -= 360;
        dirLight.transform.rotation = Quaternion.Euler(new Vector3(xTarget, dirLight.transform.rotation.y, dirLight.transform.rotation.z));
    }
}
