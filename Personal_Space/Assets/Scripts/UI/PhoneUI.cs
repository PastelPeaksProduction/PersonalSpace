using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Auto Resize: Rect Transform height -> when text bar reaches 0
/// Reply: "OK"? 
/// Button link
/// Amount of texts, constraints.
/// Icon?
/// </summary>
public class PhoneUI : MonoBehaviour
{
    public GameObject SmBar;
    public GameObject MedBar;
    public GameObject LgBar;
    public GameObject ResBar;
    public GameObject newMessage;
    public GameObject notificationImage;

    private GameController GC;
    private bool _showMessage;
    private bool _notifyMessage;
    private bool _pauseMenu;
    private Animation _animation;
    private bool _phoneUp = false;

    private void Awake()
    {
        GC = GameObject.Find("Player").GetComponent<GameController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _animation = GetComponent<Animation>();
        Debug.Log("Animation found");
    }

    // Update is called once per frame
    void Update()
    {
        if (_notifyMessage && !_pauseMenu)
        {
            NotifyMessage();
        }
    }
    public void TogglePhone()
    {
        if (_phoneUp)
        {
            HideMessage();
        }
        else
        {
            ShowMessage();
        }
        _phoneUp = !_phoneUp;
    }

    public void ShowMessage()
    {
        _animation.Play("Phone_Show");
        _notifyMessage = false;
        notificationImage.SetActive(false);
    }

    public void HideMessage()
    {
        _animation.Play("Phone_Hide");
        
    }

    private void NotifyMessage()
    {
        _animation.Play("Phone_New_Message");
    }

    public void SetNotifyMessage(string Msg)
    {
        GC.isPhoneShow = false;
        _notifyMessage = !_notifyMessage;
        ProccessMsg(Msg);
        notificationImage.SetActive(true);
    }

    private void ProccessMsg(string msg)
    {
        Debug.Log(msg.Length);
        /*InitMsgBars();
        if (msg.Length < 29)
        {
            SmBar.SetActive(true);
            SmBar.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = msg;
        }
        else if (msg.Length < 60)
        {
            MedBar.SetActive(true);
            MedBar.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = msg;
        }
        else
        {
            LgBar.SetActive(true);
            LgBar.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = msg;
        }*/
        newMessage.GetComponent<PhoneMessages>().NewMessage(msg);
    }

    private void InitMsgBars()
    {
        SmBar.SetActive(false);
        MedBar.SetActive(false);
        LgBar.SetActive(false);
    }

    public void SetShowMessage()
    {
        _showMessage = !_showMessage;
    }
    public void SetPauseMenu()
    {
        _pauseMenu = !_pauseMenu;
        gameObject.SetActive(!_pauseMenu);
    }
}
