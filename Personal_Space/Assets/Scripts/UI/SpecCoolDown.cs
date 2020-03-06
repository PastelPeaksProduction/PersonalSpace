using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpecCoolDown : MonoBehaviour
{
    private float coolDownTime = 0;
    float timer;
    public Image image;
    public bool isSprint;
    public bool isCalm;
    public bool isShout;
    private bool calmUsed = false;
    public SpecialsManager manager;
    // Start is called before the first frame update
    void Start()
    {
        if (isShout)
        {
            coolDownTime = manager.cooldownTimeShout;
        }
        if (isSprint)
        {
            coolDownTime = manager.cooldownTimeSprint;
        }
        
        timer = 0;
    }

    public void ReleaseSkill()
    {
        if(timer > coolDownTime)
        {
            timer = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isSprint && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown("joystick button 16") || Input.GetKeyDown(KeyCode.X)))
        {
            Sprint();
        }
        if (isCalm && (Input.GetKeyDown("joystick button 2") || Input.GetKeyDown("joystick button 18") || Input.GetKeyDown(KeyCode.Z)))
        {
            Calm();
        }
        if (isShout && (Input.GetKeyDown("joystick button 1") || Input.GetKeyDown("joystick button 17") || Input.GetKeyDown(KeyCode.C)))
        {
            Space();
        }
        if (!calmUsed)
        {
            timer += Time.deltaTime;
            image.fillAmount = (coolDownTime - timer) / coolDownTime;
        }
        else
        {
            image.fillAmount = 0.99f;
        }
        
    }

    public void Sprint()
    {
        ReleaseSkill();
    }

    public void Calm()
    {
        calmUsed = true;
        image.fillAmount = 1;
    }

    public void Space()
    {
        ReleaseSkill();
    }
}
