using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpecCoolDown : MonoBehaviour
{
    public float coolDownTime = 2;
    float timer;
    public Image image;
    public bool isSprint;
    public bool isCalm;
    public bool isShout;
    private bool calmUsed = false;
    // Start is called before the first frame update
    void Start()
    {
        timer = coolDownTime;
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

        if (isSprint && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown("joystick button 16") || Input.GetKeyDown(KeyCode.A)))
        {
            ReleaseSkill();
        }
        if (isCalm && (Input.GetKeyDown("joystick button 2") || Input.GetKeyDown("joystick button 18") || Input.GetKeyDown(KeyCode.X)))
        {
            //ReleaseSkill();
            calmUsed = true;
        }
        if (isShout && (Input.GetKeyDown("joystick button 1") || Input.GetKeyDown("joystick button 17") || Input.GetKeyDown(KeyCode.B)))
        {
            ReleaseSkill();
        }
        if (!calmUsed)
        {
            timer += Time.deltaTime;
            image.fillAmount = (coolDownTime - timer) / coolDownTime;
        }
        else
        {
            image.fillAmount = 1;
        }
        
    }
}
