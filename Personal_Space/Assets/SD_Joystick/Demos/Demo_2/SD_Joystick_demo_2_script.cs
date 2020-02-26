///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//   ____ ___ _     ___ ____ ___  _   _   ____  ____   ___ ___ ____  
//  / ___|_ _| |   |_ _/ ___/ _ \| \ | | |  _ \|  _ \ / _ \_ _|  _ \ 
//  \___ \| || |    | | |  | | | |  \| | | | | | |_) | | | | || | | |
//   ___) | || |___ | | |__| |_| | |\  | | |_| |  _ <| |_| | || |_| |
//  |____/___|_____|___\____\___/|_| \_| |____/|_| \_\\___/___|____/ 
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SiliconDroid;//   YOU NEED THIS IN ORDER TO USE THE SD_Joystick CLASS

//#############################################################################################################################
//	CLASS: SD_Joystick_demo_2_script
//#############################################################################################################################
public class SD_Joystick_demo_2_script : MonoBehaviour
{

    public GameObject ObjectToControl;

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //	AWAKE
    void Awake()
    {
        const float K_F_SIZE = 0.125f;
        SD_Joystick.fnc_Create_Start();
        SD_Joystick.fnc_Create_2DStick(SD_Joystick.ANCHOR.BOTTOM_LEFT, K_F_SIZE, K_F_SIZE, K_F_SIZE);// THIS IS STICK 0
        SD_Joystick.fnc_Create_Button(SD_Joystick.ANCHOR.BOTTOM_RIGHT, K_F_SIZE, K_F_SIZE, K_F_SIZE);// THIS IS BUTTON 0
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //	UPDATE
    void Update()
    {

        //  CHECK OBJECT
        if (!ObjectToControl)
        {
            UnityEngine.Debug.LogWarning("NO GAMEOBJECT TO CONTROL");
            return;
        }
        if (!ObjectToControl.GetComponent<Rigidbody>())
        {
            UnityEngine.Debug.LogWarning("GAMEOBJECT TO CONTROL HAS NO RIGIDBODY");
            return;
        }

        //  MOVE WITH JOYSTICK
        Vector2 vJoy = SD_Joystick.fnc_2DStick_GetValue(0);
        float fFly = 0.0f;
        if (SD_Joystick.fnc_Button_GetValue(0))
        {
            fFly = 1.0f;
        }
        Vector3 vForce = new Vector3(vJoy.x, fFly, vJoy.y) * 10.0f;
        ObjectToControl.GetComponent<Rigidbody>().AddForce(vForce);

        //  RESPAWN IF GONE OFF EDGE
        if (ObjectToControl.transform.position.y < 0.0f)
        {
            ObjectToControl.transform.position = new Vector3(0, 1, 0);
            ObjectToControl.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}