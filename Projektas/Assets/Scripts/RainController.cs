using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainController : MonoBehaviour {
    public int repeatFrom;
    public int repeatTo;
    private Light lt;
    public GameObject dirLight;
    private float repeater;
    private bool cloudy = false;

	void Start () {
        lt = dirLight.GetComponent<Light>();
        repeater = Random.Range(repeatFrom, repeatTo);
        gameObject.SetActive(!gameObject.activeInHierarchy);
        InvokeRepeating("RainCycle", repeater, repeater);
	}
	

	private void RainCycle () {
        cloudy = !cloudy;
        if (gameObject.activeInHierarchy)
            StartCoroutine(waiting());
        else
            gameObject.SetActive(!gameObject.activeInHierarchy);
	}

    private void Update()
    {
        lt.intensity = Mathf.MoveTowards(lt.intensity, cloudy ? 0.5F : 1F, Time.deltaTime/5);
    }

    IEnumerator waiting()
    {
        yield return new WaitForSeconds(4.0F);
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}
