using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Image WButton;

    public Image QButton;

    public Image MouseBase;

    public Image MouseLeft;

    public Image MouseRight;

    public AlertController AlertController;
    
    // Start is called before the first frame update
    void Start()
    {
        CanClick = false;
        QButton.color = AlertController.InfoColor;
        WButton.color = AlertController.InfoColor;
        MouseLeft.color = AlertController.InfoColor;
        MouseRight.color = AlertController.InfoColor;
        MouseBase.color = AlertController.InfoColor;
    }

    public bool LeftMouseDown { get; private set; } = false;
    public bool RightMouseDown { get; private set; } = false;
    public bool QButtonDown { get; private set; } = false;
    public bool WButtonDown { get; private set; } = false;
    public bool OverrideControls { get; private set; } = false;

    public void SetLeftMouseDown(bool down)
    {
        LeftMouseDown = down;
    }
    
    public void SetRightMouseDown(bool down)
    {
        RightMouseDown = down;
    }
    
    public void SetQButtonDown(bool down)
    {
        QButtonDown = down;
    }
    
    public void SetWButtonDown(bool down)
    {
        WButtonDown = down;
    }
    
    public void SetOverrideControls(bool overrideControls)
    {
        OverrideControls = overrideControls;
    }

    public bool AllDown()
    {
        return LeftMouseDown && RightMouseDown && QButtonDown && WButtonDown;
    }

    // Update is called once per frame
    void Update()
    {
        if (!OverrideControls)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (CanClick)
                {
                    MouseLeft.color = AlertController.SuccessColor;
                }
                else
                {
                    MouseLeft.color = AlertController.DangerColor;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                MouseLeft.color = AlertController.InfoColor;
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (CanClick)
                {
                    MouseRight.color = AlertController.SuccessColor;
                }
                else
                {
                    MouseRight.color = AlertController.DangerColor;
                }
            }
            else if (Input.GetMouseButtonUp(1))
            {
                MouseRight.color = AlertController.InfoColor;
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                if (CanClick)
                {
                    WButton.color = AlertController.SuccessColor;
                }
                else
                {
                    WButton.color = AlertController.DangerColor;
                }
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                WButton.color = AlertController.InfoColor;
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (CanClick)
                {
                    QButton.color = AlertController.SuccessColor;
                }
                else
                {
                    QButton.color = AlertController.DangerColor;
                }
            }
            else if (Input.GetKeyUp(KeyCode.Q))
            {
                QButton.color = AlertController.InfoColor;
            }
        }
        else
        {
            if (LeftMouseDown)
            {
                if (CanClick)
                {
                    MouseLeft.color = AlertController.SuccessColor;
                }
                else
                {
                    MouseLeft.color = AlertController.DangerColor;
                }
            }
            else
            {
                MouseLeft.color = AlertController.InfoColor;
            }

            if (RightMouseDown)
            {
                if (CanClick)
                {
                    MouseRight.color = AlertController.SuccessColor;
                }
                else
                {
                    MouseRight.color = AlertController.DangerColor;
                }
            }
            else
            {
                MouseRight.color = AlertController.InfoColor;
            }

            if (WButtonDown)
            {
                if (CanClick)
                {
                    WButton.color = AlertController.SuccessColor;
                }
                else
                {
                    WButton.color = AlertController.DangerColor;
                }
            }
            else
            {
                WButton.color = AlertController.InfoColor;
            }

            if (QButtonDown)
            {
                if (CanClick)
                {
                    QButton.color = AlertController.SuccessColor;
                }
                else
                {
                    QButton.color = AlertController.DangerColor;
                }
            }
            else
            {
                QButton.color = AlertController.InfoColor;
            }
        }

        // var color1 = QButton.color;
        // color1.a = 1.0f;
        // QButton.color = color1;
        // var color2 = WButton.color;
        // color2.a = 1.0f;
        // WButton.color = color2;
        // var color3 = MouseLeft.color;
        // color3.a = 1.0f;
        // MouseLeft.color = color3;
        // var color4 = MouseRight.color;
        // color4.a = 1.0f;
        // MouseRight.color = color4;
    }

    public void SetAllowInput(bool flag)
    {
        CanClick = flag;
    }

    private bool CanClick = false;
}
