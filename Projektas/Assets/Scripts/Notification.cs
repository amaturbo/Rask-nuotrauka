using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Notifications;
using UnityEngine.UI;

namespace Notifications
{
    public enum NotificationType
    {
        Success,
        Error
    }
}

public class Notification {

    private static Notification Instance = new Notification();
    private GameObject _panel;
    private GameObject _text;
    Image panel;
    private string Text
    {
        get
        {
            return _text.GetComponent<Text>().text;
        }
        set
        {
            _text.GetComponent<Text>().text = value;
        }
    }

    public Color32 Color
    {
        get 
        {
            return panel.color;
        }
        set 
        {
            panel.color = value;
        }
    }

    private bool Active
    {
        get
        {
            return _panel.activeSelf;
        }
        set
        {
            _panel.SetActive(value);
        }
    }

    private Notification()
    {
        _panel = GameObject.Find("NotificationPanel");
        _text = _panel.transform.GetChild(0).gameObject;
        panel = _panel.GetComponent<Image>();
        Active = false;
    }

    public static Notification New()
    {
        return Instance;
    }

    public void Show(string text, float time, NotificationType type)
    {
        Active = true;
        Text = text;
        if(type == NotificationType.Success)
        {
            Color = new Color32(22, 184, 83, 210);
        }
        else if(type == NotificationType.Error)
        {
            Color = new Color32(255, 0, 0, 210);
        }
        _panel.GetComponent<NotificationsControl>().StartHiding(time);
    }

}
