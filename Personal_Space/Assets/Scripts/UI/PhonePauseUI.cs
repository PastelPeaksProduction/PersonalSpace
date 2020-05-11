using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PhonePauseUI : MonoBehaviour
{
    public PhoneUI PhoneUI;
    private Animation _animation;
    public Button selected;
    public EventSystem system;
    public SpecialsManager specials;
    
    // Start is called before the first frame update
    void Start()
    {
        _animation = GetComponent<Animation>();
        specials = GameObject.FindGameObjectWithTag("Player").GetComponent<SpecialsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetJoystickNames().Length > 0)
        {
            if(Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
            {
                system.SetSelectedGameObject(selected.gameObject);
            }
        }
    }

    public void ShowMessage()
    {
        gameObject.SetActive(true);
        PhoneUI.SetPauseMenu();
        system.SetSelectedGameObject(selected.gameObject);
        // _animation = GetComponent<Animation>();
        //  _animation.Play("Phone_Pause_Menu"); 
        specials.isEnabled = false;
        //StartCoroutine("WaitOne", false);
    }

    public void HideMessage()
    {
        gameObject.SetActive(false);
        PhoneUI.SetPauseMenu();
        // _animation.Play("Phone_Pause_Menu_Hide");
        //specials.isEnabled = true;
        StartCoroutine("WaitOne", true);
    }

    IEnumerator WaitOne(bool val)
    {
        yield return new WaitForSeconds(1);
        specials.isEnabled = val;
    }

}
