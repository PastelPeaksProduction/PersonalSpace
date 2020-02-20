using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhonePauseUI : MonoBehaviour
{
    public PhoneUI PhoneUI;
    private Animation _animation;
    // Start is called before the first frame update
    void Start()
    {
        _animation = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMessage()
    {
        gameObject.SetActive(true);
        PhoneUI.SetPauseMenu();
       // _animation = GetComponent<Animation>();
      //  _animation.Play("Phone_Pause_Menu"); 
    }

    public void HideMessage()
    {
        gameObject.SetActive(false);
        PhoneUI.SetPauseMenu();
        // _animation.Play("Phone_Pause_Menu_Hide");
    }


}
