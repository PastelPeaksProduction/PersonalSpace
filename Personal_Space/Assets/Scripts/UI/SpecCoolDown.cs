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
    private bool cooling = false;
    public SpecialsManager manager;
    private GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<GameController>();
        if (isShout)
        {
            coolDownTime = manager.cooldownTimeShout + 5;
        }
        if (isSprint)
        {
            coolDownTime = manager.cooldownTimeSprint + 5;
        }
        
        timer = coolDownTime;
        image.fillAmount = 0f;
    }

    public void ReleaseSkill()
    {
        cooling = true;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameController.isGamePaused() && manager.isEnabled)
        {
            if (manager.sprintAvailable && isSprint && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown("joystick button 16") || Input.GetKeyDown(KeyCode.X)))
            {
                Sprint();
                timer = 0;
            }
            if (isCalm && (Input.GetKeyDown("joystick button 2") || Input.GetKeyDown("joystick button 18") || Input.GetKeyDown(KeyCode.Z)))
            {
                Calm();
                timer = 0;
            }
            if (manager.shoutAvailable && isShout && (Input.GetKeyDown("joystick button 1") || Input.GetKeyDown("joystick button 17") || Input.GetKeyDown(KeyCode.C)))
            {
                Space();
                timer = 0;
            }
            if (!calmUsed && cooling)
            {
                timer += Time.deltaTime;
                image.fillAmount = (coolDownTime - timer) / coolDownTime;
                if (timer > coolDownTime)
                {
                    timer = 0;
                    cooling = false;
                }
            }
            else if(calmUsed)
            {
                image.fillAmount = 0.99f;
            }

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
