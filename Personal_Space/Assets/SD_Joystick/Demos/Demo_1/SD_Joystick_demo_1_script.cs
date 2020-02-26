///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//   ____ ___ _     ___ ____ ___  _   _   ____  ____   ___ ___ ____  
//  / ___|_ _| |   |_ _/ ___/ _ \| \ | | |  _ \|  _ \ / _ \_ _|  _ \ 
//  \___ \| || |    | | |  | | | |  \| | | | | | |_) | | | | || | | |
//   ___) | || |___ | | |__| |_| | |\  | | |_| |  _ <| |_| | || |_| |
//  |____/___|_____|___\____\___/|_| \_| |____/|_| \_\\___/___|____/ 
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using SiliconDroid;

public class SD_Joystick_demo_1_script : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //	MONOBEHAVIOR AWAKE
    void Awake()
    {
        CreateControls();
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //	MONOBEHAVIOR UPDATE
    string sInfo_old = "";
    string sInfo_new = "";
    bool bVisible = true;
    void Update()
    {
        //  TEST OPACITY MODULATION
        //SD_Joystick.fnc_SetOpacity(0.75f + Mathf.Sin(Time.time * 10) * 0.25f);

        //  MOVE CAMERA
        Camera.main.transform.Translate(0, 0, Time.deltaTime * 5.0f, Space.Self);
        Camera.main.transform.Rotate(0, Time.deltaTime * 5.0f, 0, Space.Self);

        //  KEYS
        if (Input.GetKeyDown(KeyCode.D))
        {
            SD_Joystick.fnc_Destroy_All();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            CreateControls();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            bVisible = !bVisible;
            SD_Joystick.fnc_SetVisible(bVisible);
        }

        sInfo_new = "CONTROL STATE:\n";

        sInfo_new += "2D STICKS: ";
        for (int iLoop = 0; iLoop < SD_Joystick.fnc_2DStick_GetCount(); iLoop++)
        {
            sInfo_new += SD_Joystick.fnc_2DStick_GetValue(iLoop).ToString();
            sInfo_new += " , ";
        }
        sInfo_new += "    |    ";

        sInfo_new += "1D STICKS: ";
        for (int iLoop = 0; iLoop < SD_Joystick.fnc_1DStick_GetCount(); iLoop++)
        {
            sInfo_new += SD_Joystick.fnc_1DStick_GetValue(iLoop).ToString();
            sInfo_new += " , ";
        }
        sInfo_new += "    |    ";

        sInfo_new += "BTNS: ";
        for (int iLoop = 0; iLoop < SD_Joystick.fnc_Button_GetCount(); iLoop++)
        {
            sInfo_new += SD_Joystick.fnc_Button_GetValue(iLoop).ToString();
            sInfo_new += " , ";
        }
        sInfo_new += "    |    ";

        if (sInfo_old != sInfo_new)
        {
            UnityEngine.Debug.Log(sInfo_new);
            sInfo_old = sInfo_new;
        }
        //*/
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //	GET A RANDOM COLOR
    Color RndColor()
    {
        float fRnd = Random.Range(0, 6.0f);
        if(fRnd > 5.0f)
        {
            return Color.red;
        }
        else if (fRnd > 4.0f)
        {
            return Color.yellow;
        }
        else if (fRnd > 3.0f)
        {
            return Color.green;
        }
        else if (fRnd > 2.0f)
        {
            return Color.blue;
        }
        else if (fRnd > 1.0f)
        {
            return Color.magenta;
        }
        return Color.white;
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //	CREATE SOME CONTROLS
    //  SET
    const float K_F_STICK_DIAM = 0.1f;
    const float K_F_BUTTON_DIAM = 0.1f;
    const float K_F_EDGE_PAD = 0.015f;

    const float K_I_STICKS_2D = 4;
    const float K_I_STICKS_1D = 4;
    const float K_I_BUTTONS = 4;

    //  DERIVED
    const float K_F_STICK_1D_LENGTH = K_F_STICK_DIAM * 1.5f;
    const float K_F_STICK_RADI = K_F_STICK_DIAM * 0.5f;
    const float K_F_BUTTON_RADI = K_F_BUTTON_DIAM * 0.5f;

    void CreateControls()
    {
        //  START NEW CONTROL 
        SD_Joystick.fnc_Create_Start();

        //  2D STICKS
        float fOrgY = K_F_EDGE_PAD + K_F_STICK_RADI;
        for (int iLoop = 0; iLoop < K_I_STICKS_2D; iLoop++)
        {
            SD_Joystick.fnc_Create_2DStick(SD_Joystick.ANCHOR.BOTTOM_LEFT, K_F_EDGE_PAD + K_F_STICK_RADI, fOrgY, K_F_STICK_DIAM);
            SD_Joystick.fnc_SetLastCreatedControlColor(RndColor());
            fOrgY += K_F_EDGE_PAD + K_F_STICK_DIAM;
        }

        //  1D STICKS
        fOrgY = K_F_EDGE_PAD + K_F_STICK_RADI;
        for (int iLoop = 0; iLoop < K_I_STICKS_2D; iLoop++)
        {
            if (iLoop % 2 == 0)
            {
                SD_Joystick.fnc_Create_1DStick(SD_Joystick.ANCHOR.BOTTOM_MIDDLE, 0.0f, fOrgY, K_F_STICK_1D_LENGTH, K_F_STICK_DIAM);
            }
            else
            {
                SD_Joystick.fnc_Create_1DStick(SD_Joystick.ANCHOR.BOTTOM_MIDDLE, 0.0f, fOrgY, K_F_STICK_DIAM, K_F_STICK_1D_LENGTH);
            }
            SD_Joystick.fnc_SetLastCreatedControlColor(RndColor());
            fOrgY += K_F_EDGE_PAD + K_F_STICK_DIAM;
        }

        //  BUTTONS
        fOrgY = K_F_EDGE_PAD + K_F_STICK_RADI;
        for (int iLoop = 0; iLoop < K_I_BUTTONS; iLoop++)
        {
            SD_Joystick.fnc_Create_Button(SD_Joystick.ANCHOR.BOTTOM_RIGHT, K_F_EDGE_PAD + K_F_STICK_RADI, fOrgY, K_F_STICK_DIAM);
            SD_Joystick.fnc_SetLastCreatedControlColor(RndColor());
            fOrgY += K_F_EDGE_PAD + K_F_STICK_DIAM;
        }

        //  LOG WHAT WAS CREATED
        UnityEngine.Debug.Log("CREATED " + SD_Joystick.fnc_2DStick_GetCount() + " 2D STICKS");
        UnityEngine.Debug.Log("CREATED " + SD_Joystick.fnc_1DStick_GetCount() + " 1D STICKS");
        UnityEngine.Debug.Log("CREATED " + SD_Joystick.fnc_Button_GetCount() + " BUTTONS");
    }
}