using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Auto Resize: Rect Transform height -> when text bar reaches 0
/// Reply: "OK"? 
/// Button link
/// Amount of texts, constraints.
/// Icon?
/// </summary>
public class PhoneUI : MonoBehaviour
{
    private bool _showMessage;
    private bool _notifyMessage = true;
    private Animation _animation;
    // Start is called before the first frame update
    void Start()
    {
        _animation = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_notifyMessage)
        {
            NotifyMessage();
        }

    }

    public void ShowMessage()
    {
        _animation.Play("Phone_Show");
        _notifyMessage = false;
    }

    public void HideMessage()
    {
        _animation.Play("Phone_Hide");
    }

    private void NotifyMessage()
    {
        _animation.Play("Phone_New_Message");
    }

    public void SetNotifyMessage()
    {
        _notifyMessage = !_notifyMessage;
    }

    public void SetShowMessage()
    {
        _showMessage = !_showMessage;
    }
}
