using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationsControl : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        Notification.New();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartHiding(float time)
    {
        CancelInvoke();
        InvokeRepeating("Hide", time, 0.01f);
    }

    private void Hide()
    {
        Color32 temp = Notification.New().Color;
        Debug.Log(temp.a);
        temp.a -= 2;
        Notification.New().Color = temp;
        if (Notification.New().Color.a <= 0)
        {
            this.gameObject.SetActive(false);
            CancelInvoke();
        }
    }
}
