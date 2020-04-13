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

namespace SiliconDroid
{
    //#############################################################################################################################
    //	CLASS: SD_Joystick_root
    //#############################################################################################################################
    public class SD_Joystick_root : SD_MonoBehaviour
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	CONSTANTS

        //  SET
        public const float K_F_PUSHZ_FACTOR = 0.1f;
        public const int K_I_MAX_TOUCH_POINTS = 8;
        public const string K_S_NAME_CONTROL = "CONTROL";
        public const string K_S_NAME_DECORATION = "DECOR";


        //  DERIVED
        public const float K_F_RAY_LENGTH = K_F_PUSHZ_FACTOR * 2.0f;

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	WORKING DATA
        private class WORKING_DATA
        {
            public float fRootPosZ;
            public List<SD_Joystick_control_1dstick> lc1DStick = new List<SD_Joystick_control_1dstick>();
            public List<SD_Joystick_control_2dstick> lc2DStick = new List<SD_Joystick_control_2dstick>();
            public List<SD_Joystick_control_button> lcButton = new List<SD_Joystick_control_button>();
            public List<SD_Joystick_control_baseclass> lcBaseclass = new List<SD_Joystick_control_baseclass>();
            public bool bHaveMultiTouch = false;
            public SD_Joystick_control_baseclass[] acTouchToControl = new SD_Joystick_control_baseclass[K_I_MAX_TOUCH_POINTS];
        }
        WORKING_DATA v = new WORKING_DATA();

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	 _   _ _   _ ___ _______   __  _______     _______ _   _ _____ ____  
        //	| | | | \ | |_ _|_   _\ \ / / | ____\ \   / / ____| \ | |_   _/ ___| 
        //	| | | |  \| || |  | |  \ V /  |  _|  \ \ / /|  _| |  \| | | | \___ \ 
        //	| |_| | |\  || |  | |   | |   | |___  \ V / | |___| |\  | | |  ___) |
        //	 \___/|_| \_|___| |_|   |_|   |_____|  \_/  |_____|_| \_| |_| |____/ 
        //	
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	AWAKE
        void Awake()
        {
            v.fRootPosZ = SD_Joystick_factory.fnc_GetPushZ();
            SD_Joystick_factory.fnc_ChildToTransform(this.transform, Camera.main.transform);
            this.transform.Translate(0, 0, v.fRootPosZ, Space.Self);
            v.bHaveMultiTouch = Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	UPDATE
        void Update()
        {
            Clock_Controls();
            Clock_Touch();
        }

        
        /*void OnGUI()
        {
            Vector3 point = new Vector3();
            Camera cam = Camera.main;
            Event currentEvent = Event.current;
            Vector2 mousePos = new Vector2();

            // Get the mouse position from Event.
            // Note that the y position from Event is inverted.
            mousePos.x = currentEvent.mousePosition.x;
            mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

            point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

            GUILayout.BeginArea(new Rect(20, 20, 250, 120));
            GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
            GUILayout.Label("Mouse position: " + mousePos);
            GUILayout.Label("World position: " + point.ToString("F3"));
            GUILayout.EndArea();
        }*/
        
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	 ____  _   _ ____  _     ___ ____ 
        //	|  _ \| | | | __ )| |   |_ _/ ___|
        //	| |_) | | | |  _ \| |    | | |    
        //	|  __/| |_| | |_) | |___ | | |___ 
        //	|_|    \___/|____/|_____|___\____|
        //
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	CREATE A 2D STICK
        public bool fnc_2DStick_Create(SD_Joystick.ANCHOR eAnchor, float fPosX, float fPosY, float fDiameter, int iCtrlSprite, int iBgndSprite)
        {
            SD_Joystick_control_2dstick cControl = new GameObject("SD_Joystick_STICK2D[" + v.lc2DStick.Count + "]").AddComponent<SD_Joystick_control_2dstick>();
            SD_Joystick_factory.fnc_ChildToTransform(cControl.transform, this.transform);
            v.lc2DStick.Add(cControl);
            v.lcBaseclass.Add(cControl);
            Bounds cBounds_scr = GetScreenBounds(eAnchor, fPosX, fPosY, fDiameter, fDiameter);
            return cControl.fnc_Create(cBounds_scr, iCtrlSprite, iBgndSprite);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	SET ALL CONTROLS VISIBLE AND ACTIVE
        int iLoop;
        public void fnc_SetVisible(bool bVisible)
        {
            for (iLoop = 0; iLoop < v.lcBaseclass.Count; iLoop++)
            {
                v.lcBaseclass[iLoop].fnc_SetVisible(bVisible);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	SET ALL CONTROLS VERTEX COLOR
        public void fnc_SetColor(Color oColor)
        {
            for (iLoop = 0; iLoop < v.lcBaseclass.Count; iLoop++)
            {
                v.lcBaseclass[iLoop].fnc_SetColor(oColor);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	SET LAST CREATED CONTROL COLOR
        public void fnc_SetLastCreatedControlColor(Color oColor)
        {
            if(v.lcBaseclass.Count < 1)
            {
                return;
            }
            v.lcBaseclass[v.lcBaseclass.Count - 1].fnc_SetColor(oColor);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	SET COLLISION LAYER
        public void fnc_SetCollisionLayer(int iLayer)
        {
            for (iLoop = 0; iLoop < v.lcBaseclass.Count; iLoop++)
            {
                v.lcBaseclass[iLoop].fnc_SetCollisionLayer(iLayer);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	GET NUM OF 2D STICKS
        public int fnc_2DStick_GetCount()
        {
            return v.lc2DStick.Count;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	SET 2D STICK VISIBLE
        public void fnc_2DStick_SetVisible(int iIndex, bool bVisible)
        {
            if (iIndex >= v.lc2DStick.Count)
            {
                LOGW("Index " + iIndex + "is out of bounds, list only has " + v.lc2DStick.Count + " entries");
                return;
            }
            v.lc2DStick[iIndex].fnc_SetVisible(bVisible);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	GET VAL OF 2D STICK
        public Vector2 fnc_2DStick_GetValue(int iIndex)
        {
            if (iIndex >= v.lc2DStick.Count)
            {
                LOGW("Index " + iIndex + "is out of bounds, list only has " + v.lc2DStick.Count + " entries");
                return Vector2.zero;
            }
            return v.lc2DStick[iIndex].GetValue();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	CREATE A 1D STICK
        public bool fnc_1DStick_Create(SD_Joystick.ANCHOR eAnchor, float fPosX, float fPosY, float fWidth, float fHeight, int iCtrlSprite, int iBgndSprite)
        {
            SD_Joystick_control_1dstick cControl = new GameObject("SD_Joystick_STICK1D[" + v.lc1DStick.Count + "]").AddComponent<SD_Joystick_control_1dstick>();
            SD_Joystick_factory.fnc_ChildToTransform(cControl.transform, this.transform);
            v.lc1DStick.Add(cControl);
            v.lcBaseclass.Add(cControl);
            Bounds cBounds_scr = GetScreenBounds(eAnchor, fPosX, fPosY, fWidth, fHeight);
            return cControl.fnc_Create(cBounds_scr, iCtrlSprite, iBgndSprite);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	GET NUM OF 1D STICKS
        public int fnc_1DStick_GetCount()
        {
            return v.lc1DStick.Count;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	SET 1D STICK VISIBLE
        public void fnc_1DStick_SetVisible(int iIndex, bool bVisible)
        {
            if (iIndex >= v.lc1DStick.Count)
            {
                LOGW("Index " + iIndex + "is out of bounds, list only has " + v.lc2DStick.Count + " entries");
                return;
            }
            v.lc1DStick[iIndex].fnc_SetVisible(bVisible);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	GET VAL OF 1D STICK
        public float fnc_1DStick_GetValue(int iIndex)
        {
            if (iIndex >= v.lc1DStick.Count)
            {
                LOGW("Index " + iIndex + "is out of bounds, list only has " + v.lc1DStick.Count + " entries");
                return 0.0f;
            }
            return v.lc1DStick[iIndex].GetValue();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	CREATE A BUTTON
        public bool fnc_Button_Create(SD_Joystick.ANCHOR eAnchor, float fPosX, float fPosY, float fDiameter, int iCtrlSprite)
        {
            SD_Joystick_control_button cControl = new GameObject("SD_Joystick_BUTTON[" + v.lcButton.Count + "]").AddComponent<SD_Joystick_control_button>();
            SD_Joystick_factory.fnc_ChildToTransform(cControl.transform, this.transform);
            v.lcButton.Add(cControl);
            v.lcBaseclass.Add(cControl);
            Bounds cBounds_scr = GetScreenBounds(eAnchor, fPosX, fPosY, fDiameter, fDiameter);
            return cControl.fnc_Create(cBounds_scr, iCtrlSprite);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	GET NUM OF BUTTONS
        public int fnc_Button_GetCount()
        {
            return v.lcButton.Count;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	SET 1D STICK VISIBLE
        public void fnc_Button_SetVisible(int iIndex, bool bVisible)
        {
            if (iIndex >= v.lcButton.Count)
            {
                LOGW("Index " + iIndex + "is out of bounds, list only has " + v.lc2DStick.Count + " entries");
                return;
            }
            v.lcButton[iIndex].fnc_SetVisible(bVisible);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	GET VAL OF BUTTON
        public bool fnc_Button_GetValue(int iIndex)
        {
            if (iIndex >= v.lcButton.Count)
            {
                LOGW("Index " + iIndex + "is out of bounds, list only has " + v.lcButton.Count + " entries");
                return false;
            }
            return v.lcButton[iIndex].GetValue();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	 ____  ____  _____     ___  _____ _____ 
        //	|  _ \|  _ \|_ _\ \   / / \|_   _| ____|
        //	| |_) | |_) || | \ \ / / _ \ | | |  _|  
        //	|  __/|  _ < | |  \ V / ___ \| | | |___ 
        //	|_|   |_| \_\___|  \_/_/   \_\_| |_____|
        //
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	GET WORLD RECT
        Rect cScreenRect;
        float fScreenDiag, fScreenW, fScreenH, fRadX, fRadY;
        Bounds cBounds_scr;
        Vector3 vCorner_0 = Vector3.zero;
        Vector3 vCorner_1 = Vector3.zero;
        private Bounds GetScreenBounds(SD_Joystick.ANCHOR eAnchor, float fPosX, float fPosY, float fWidth, float fHeight)
        {
            //  GET SCREEN MAKEUP
            cScreenRect = Camera.main.pixelRect;
            fScreenW = cScreenRect.width;
            fScreenH = cScreenRect.height;
            fScreenDiag = Mathf.Sqrt((fScreenW * fScreenW) + (fScreenH * fScreenH));
            
            //  CONTROL RADII
            fRadX = fWidth * 0.5f;
            fRadY = fHeight * 0.5f;

            //  PER ANCHOR

            //-----------------------------------------------------------------------------------------------------------------------------
            //	BOT
            if (eAnchor == SD_Joystick.ANCHOR.BOTTOM_LEFT)
            {
                vCorner_0.x = (fPosX - fRadX) * fScreenDiag;
                vCorner_0.y = (fPosY - fRadY) * fScreenDiag;
                vCorner_1.x = (fPosX + fRadX) * fScreenDiag;
                vCorner_1.y = (fPosY + fRadY) * fScreenDiag;
            }
            else if (eAnchor == SD_Joystick.ANCHOR.BOTTOM_MIDDLE)
            {
                vCorner_0.x = (fScreenW * 0.5f) + (fPosX - fRadX) * fScreenDiag;
                vCorner_0.y = (fPosY - fRadY) * fScreenDiag;
                vCorner_1.x = (fScreenW * 0.5f) + (fPosX + fRadX) * fScreenDiag;
                vCorner_1.y = (fPosY + fRadY) * fScreenDiag;
            }
            else if (eAnchor == SD_Joystick.ANCHOR.BOTTOM_RIGHT)
            {
                vCorner_0.x = fScreenW - (fPosX + fRadX) * fScreenDiag;
                vCorner_0.y = (fPosY - fRadY) * fScreenDiag;
                vCorner_1.x = fScreenW - (fPosX - fRadX) * fScreenDiag;
                vCorner_1.y = (fPosY + fRadY) * fScreenDiag;
            }
            //-----------------------------------------------------------------------------------------------------------------------------
            //	TOP
            else if (eAnchor == SD_Joystick.ANCHOR.TOP_LEFT)
            {
                vCorner_0.x = (fPosX - fRadX) * fScreenDiag;
                vCorner_0.y = fScreenH - (fPosY + fRadY) * fScreenDiag;
                vCorner_1.x = (fPosX + fRadX) * fScreenDiag;
                vCorner_1.y = fScreenH - (fPosY - fRadY) * fScreenDiag;
            }
            else if (eAnchor == SD_Joystick.ANCHOR.TOP_MIDDLE)
            {
                vCorner_0.x = (fScreenW * 0.5f) + (fPosX - fRadX) * fScreenDiag;
                vCorner_0.y = fScreenH - (fPosY + fRadY) * fScreenDiag;
                vCorner_1.x = (fScreenW * 0.5f) + (fPosX + fRadX) * fScreenDiag;
                vCorner_1.y = fScreenH - (fPosY - fRadY) * fScreenDiag;
            }
            else if (eAnchor == SD_Joystick.ANCHOR.TOP_RIGHT)
            {
                vCorner_0.x = fScreenW - (fPosX + fRadX) * fScreenDiag;
                vCorner_0.y = fScreenH - (fPosY + fRadY) * fScreenDiag;
                vCorner_1.x = fScreenW - (fPosX - fRadX) * fScreenDiag;
                vCorner_1.y = fScreenH - (fPosY - fRadY) * fScreenDiag;
            }
            //-----------------------------------------------------------------------------------------------------------------------------
            //	SIDES
            else if (eAnchor == SD_Joystick.ANCHOR.LEFT_MIDDLE)
            {
                vCorner_0.x = (fPosX - fRadX) * fScreenDiag;
                vCorner_0.y = (fScreenH * 0.5f) + (fPosY - fRadY) * fScreenDiag;
                vCorner_1.x = (fPosX + fRadX) * fScreenDiag;
                vCorner_1.y = (fScreenH * 0.5f) + (fPosY + fRadY) * fScreenDiag;
            }
            else if (eAnchor == SD_Joystick.ANCHOR.RIGHT_MIDDLE)
            {
                vCorner_0.x = fScreenW - (fPosX + fRadX) * fScreenDiag;
                vCorner_0.y = (fScreenH * 0.5f) + (fPosY - fRadY) * fScreenDiag;
                vCorner_1.x = fScreenW - (fPosX - fRadX) * fScreenDiag;
                vCorner_1.y = (fScreenH * 0.5f) + (fPosY + fRadY) * fScreenDiag;
            }

            //  SET
            cBounds_scr.min = vCorner_0;
            cBounds_scr.max = vCorner_1;

            //  DONE
            return cBounds_scr;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	CLOCK CONTROLS
        private void Clock_Controls()
        {
            for (iLoop = 0; iLoop < v.lcBaseclass.Count; iLoop++)
            {
                v.lcBaseclass[iLoop].fnc_Clock();
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	CLOCK TOUCH
        Touch cTouch;
        private void Clock_Touch()
        {
            //  MULTI TOUCH
            if (v.bHaveMultiTouch)
            {
                for (iLoop = 0; iLoop < Mathf.Min(Input.touchCount, K_I_MAX_TOUCH_POINTS); iLoop++)
                {
                    cTouch = Input.GetTouch(iLoop);

                    if(cTouch.phase == TouchPhase.Began)
                    {
                        Touch_Down(cTouch.fingerId, cTouch.position);
                    }
                    else if (cTouch.phase == TouchPhase.Moved)
                    {
                        Touch_Move(cTouch.fingerId, cTouch.position);
                    }
                    else if (cTouch.phase == TouchPhase.Ended)
                    {
                        Touch_End(cTouch.fingerId, cTouch.position);
                    }
                }
            }
            //  MOUSE
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Touch_Down(0, Input.mousePosition);
                }
                else if (Input.GetMouseButton(0))
                {
                    Touch_Move(0, Input.mousePosition);
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    Touch_End(0, Input.mousePosition);
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	TOUCH PROCESS: DOWN
        RaycastHit cHit;
        Ray cRay;
        SD_Joystick_control_baseclass cBase;
        private void Touch_Down(int iTouchID, Vector3 vScreenPos)
        {
            cRay = Camera.main.ScreenPointToRay(vScreenPos);
            Physics.Raycast(cRay, out cHit, K_F_RAY_LENGTH);
            if (cHit.collider)
            {
                cBase = cHit.transform.GetComponent<SD_Joystick_control_baseclass>();
                if (cBase)
                {
                    if(BindTouchToControl(iTouchID, cBase))
                    {
                        cHit.point = Camera.main.transform.InverseTransformPoint(cHit.point);
                        cBase.fnc_Touch_Start(cHit.point);
                    }
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	BIND TOUCH ID TO CONTROL
        private bool BindTouchToControl(int iTouchID, SD_Joystick_control_baseclass cBase)
        {
            for(iLoop = 0; iLoop < K_I_MAX_TOUCH_POINTS; iLoop++)
            {
                if(v.acTouchToControl[iLoop] == cBase)
                {
                    return false;// TOUCH BOUND TO OTHER CONTROL ALREADY
                }
            }
            v.acTouchToControl[iTouchID] = cBase;
            return true;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	TOUCH PROCESS: MOVE
        Vector3 vWorldPoint;
        private void Touch_Move(int iTouchID, Vector3 vScreenPos)
        {
            if(v.acTouchToControl[iTouchID] == null)
            {
                return;
            }
            vScreenPos.z = v.fRootPosZ;
            vScreenPos = Camera.main.ScreenToWorldPoint(vScreenPos);
            vScreenPos = Camera.main.transform.InverseTransformPoint(vScreenPos);
            v.acTouchToControl[iTouchID].fnc_Touch_Move(vScreenPos);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	TOUCH PROCESS: END
        private void Touch_End(int iTouchID, Vector3 vScreenPos)
        {
            if (v.acTouchToControl[iTouchID] == null)
            {
                return;
            }
            vScreenPos.z = v.fRootPosZ;
            vScreenPos = Camera.main.ScreenToWorldPoint(vScreenPos);
            vScreenPos = Camera.main.transform.InverseTransformPoint(vScreenPos);
            v.acTouchToControl[iTouchID].fnc_Touch_End(vScreenPos);
            v.acTouchToControl[iTouchID] = null;
        }
    }
}