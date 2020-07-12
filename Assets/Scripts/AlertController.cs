using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlertController : MonoBehaviour
{
    public enum AlertEnum
    {
        Info,
        Warning,
        Danger,
        Failure
    }
    
    public Image Warning;

    public TextMeshProUGUI Text;

    public Color InfoColor = Color.white;
    public Color WarningColor = Color.yellow;
    public Color DangerColor = Color.red;
    public Color SuccessColor = Color.blue;

    public AlertEnum AlertStatus;

    public bool Blinking = false;

    public float BlinkingTime;
    public float BlinkingTimeStart;

    private bool BlinkOn = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Blinking)
        {
            if (Time.time - BlinkingTimeStart > BlinkingTime)
            {
                BlinkOn = !BlinkOn;
                BlinkingTimeStart = Time.time;
            }
        }
        
        Color color = GetAlertColor(AlertStatus);
        
        Text.color = color;
        if (!BlinkOn)
        {
            color.a = 0.0f;
        }
        Warning.color = color;
    }

    private Color GetAlertColor(AlertEnum alertStatus)
    {
        Color color = InfoColor;
        bool blinking = Blinking;
        Blinking = false;
        BlinkingTime = 0.0f;
        switch (alertStatus)
        {
            case AlertEnum.Info:
                color = InfoColor;
                BlinkOn = false;
                Text.gameObject.SetActive(false);
                break;
            case AlertEnum.Warning:
                color = WarningColor;
                Blinking = true;
                BlinkingTime = 0.5f;
                Text.gameObject.SetActive(false);
                break;
            case AlertEnum.Danger:
                color = DangerColor;
                Blinking = true;
                BlinkingTime = 0.4f;
                Text.gameObject.SetActive(false);
                break;
            case AlertEnum.Failure:
                color = SuccessColor;
                Blinking = true;
                BlinkingTime = 0.25f;
                Text.gameObject.SetActive(false);
                break;
            default:
                color = InfoColor;
                break;
        }

        if (Blinking && !blinking)
        {
            BlinkingTimeStart = Time.time;
            BlinkOn = false;
        }

        return color;
    }
}
