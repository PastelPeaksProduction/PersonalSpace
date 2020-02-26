///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//   ____ ___ _     ___ ____ ___  _   _   ____  ____   ___ ___ ____  
//  / ___|_ _| |   |_ _/ ___/ _ \| \ | | |  _ \|  _ \ / _ \_ _|  _ \ 
//  \___ \| || |    | | |  | | | |  \| | | | | | |_) | | | | || | | |
//   ___) | || |___ | | |__| |_| | |\  | | |_| |  _ <| |_| | || |_| |
//  |____/___|_____|___\____\___/|_| \_| |____/|_| \_\\___/___|____/ 
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiliconDroid
{
    //#############################################################################################################################
    //	CLASS: SD_Joystick
    //#############################################################################################################################
    public class SD_Joystick : SD_MonoBehaviour
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	CONSTANTS
        private const string K_S_ERROR_NULL_ROOT = "NULL ROOT, PLEASE CALL SD_Joystick.fnc_Create_Start() FIRST.";

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	ENUMS
        public enum ANCHOR
        {
            BOTTOM_LEFT,
            BOTTOM_MIDDLE,
            BOTTOM_RIGHT,
            TOP_LEFT,
            TOP_MIDDLE,
            TOP_RIGHT,
            LEFT_MIDDLE,
            RIGHT_MIDDLE,
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	WORKING DATA
        private class WORKING_DATA
        {
            public SD_Joystick_root cRoot;
        }
        static WORKING_DATA v = new WORKING_DATA();

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	 ____  _   _ ____  _     ___ ____ 
        //	|  _ \| | | | __ )| |   |_ _/ ___|
        //	| |_) | | | |  _ \| |    | | |    
        //	|  __/| |_| | |_) | |___ | | |___ 
        //	|_|    \___/|____/|_____|___\____|
        //
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Destroys any existing controls.
        /// </summary>
        /// <returns></returns>
        public static bool fnc_Destroy_All()
        {
            if (v.cRoot)
            {
                LOGM("Destroying existing on screen joystick...");
                DestroyImmediate(v.cRoot.gameObject);
            }
            return true;
        }

        /// <summary>
        /// Set all controls active and visible ( or not ).
        /// </summary>
        /// <param name="bVisible"></param>
        public static void fnc_SetVisible(bool bVisible)
        {
            if (!v.cRoot)
            {
                LOGW(K_S_ERROR_NULL_ROOT);
                return;
            }
            v.cRoot.fnc_SetVisible(bVisible);
        }

        /// <summary>
        /// Set all controls visual opacity.
        /// </summary>
        /// <param name="bVisible"></param>
        public static void fnc_SetOpacity(float fOpacity = 1.0f)
        {
            SD_Joystick_factory.fnc_SetMaterialOpacity(fOpacity);
        }

        /// <summary>
        /// Set all controls vertex coloring.
        /// </summary>
        /// <param name="bVisible"></param>
        public static void fnc_SetColor(Color oColor)
        {
            if (!v.cRoot)
            {
                LOGW(K_S_ERROR_NULL_ROOT);
                return;
            }
            v.cRoot.fnc_SetColor(oColor);
        }

        /// <summary>
        /// Set color of the last created control. This way you can have each control be a different color.
        /// </summary>
        /// <param name="bVisible"></param>
        public static void fnc_SetLastCreatedControlColor(Color oColor)
        {
            if (!v.cRoot)
            {
                LOGW(K_S_ERROR_NULL_ROOT);
                return;
            }
            v.cRoot.fnc_SetLastCreatedControlColor(oColor);
        }

        /// <summary>
        /// Set the physics layer for the colliders that make up the joystick. This is a specialised function, for example if you have your camera inside some collider you may want to set the josytick to use a different layer to avoid glitching.
        /// </summary>
        /// <param name="bVisible"></param>
        public static void fnc_SetCollisionLayer(int iLayer)
        {
            if (!v.cRoot)
            {
                LOGW(K_S_ERROR_NULL_ROOT);
                return;
            }
            v.cRoot.fnc_SetCollisionLayer(iLayer);
        }

        /// <summary>
        /// Start the creation of a new on screen joystick.
        /// </summary>
        /// <returns></returns>
        public static bool fnc_Create_Start()
        {
            fnc_Destroy_All();
            v.cRoot = new GameObject("SD_Joystick_ROOT").AddComponent<SD_Joystick_root>();
            LOGM("CREATED JOYSTICK ROOT");
            return true;
        }

        /// <summary>
        /// Create a 2D joystick.
        /// </summary>
        /// <returns></returns>
        public static bool fnc_Create_2DStick(ANCHOR eAnchor, float fPosX, float fPosY, float fDiameter, int iCtrlSprite = 3, int iBgndSprite = 42)
        {
            if (!v.cRoot)
            {
                LOGW(K_S_ERROR_NULL_ROOT);
                return false;
            }
            return v.cRoot.fnc_2DStick_Create(eAnchor, fPosX, fPosY, fDiameter, iCtrlSprite, iBgndSprite);
        }

        /// <summary>
        /// Get the number of 1D sticks in the current on screen joystick.
        /// </summary>
        /// <returns></returns>
        public static int fnc_1DStick_GetCount()
        {
            if (!v.cRoot)
            {
                return 0;
            }
            return v.cRoot.fnc_1DStick_GetCount();
        }

        /// <summary>
        /// Set the specified 1D stick active and visible ( or not ).
        /// </summary>
        /// <returns></returns>
        public static void fnc_1DStick_SetVisible(int iIndex, bool bVisible)
        {
            if (!v.cRoot)
            {
                return;
            }
            v.cRoot.fnc_1DStick_SetVisible(iIndex, bVisible);
        }

        /// <summary>
        /// Get the current value of the specified 1D stick.
        /// </summary>
        /// <returns></returns>
        public static float fnc_1DStick_GetValue(int iIndex)
        {
            if (!v.cRoot)
            {
                return 0.0f;
            }
            return v.cRoot.fnc_1DStick_GetValue(iIndex);
        }

        /// <summary>
        /// Create a 1D joystick.
        /// </summary>
        /// <returns></returns>
        public static bool fnc_Create_1DStick(ANCHOR eAnchor, float fPosX, float fPosY, float fWidth, float fHeight, int iCtrlSprite = -1, int iBgndSprite = 34)
        {
            if (!v.cRoot)
            {
                LOGW(K_S_ERROR_NULL_ROOT);
                return false;
            }

            if(iCtrlSprite == -1)
            {
                if(fWidth < fHeight)
                {
                    iCtrlSprite = 1;
                }
                else
                {
                    iCtrlSprite = 2;
                }
            }

            return v.cRoot.fnc_1DStick_Create(eAnchor, fPosX, fPosY, fWidth, fHeight, iCtrlSprite, iBgndSprite);
        }

        /// <summary>
        /// Get the number of 2D sticks in the current on screen joystick.
        /// </summary>
        /// <returns></returns>
        public static int fnc_2DStick_GetCount()
        {
            if (!v.cRoot)
            {
                return 0;
            }
            return v.cRoot.fnc_2DStick_GetCount();
        }

        /// <summary>
        /// Set the specified 2D stick active and visible ( or not ).
        /// </summary>
        /// <returns></returns>
        public static void fnc_2DStick_SetVisible(int iIndex, bool bVisible)
        {
            if (!v.cRoot)
            {
                return;
            }
            v.cRoot.fnc_2DStick_SetVisible(iIndex, bVisible);
        }

        /// <summary>
        /// Get the current value of the specified 2D stick.
        /// </summary>
        /// <returns></returns>
        public static Vector2 fnc_2DStick_GetValue(int iIndex)
        {
            if (!v.cRoot)
            {
                return Vector2.zero;
            }
            return v.cRoot.fnc_2DStick_GetValue(iIndex);
        }

        /// <summary>
        /// Create a button.
        /// </summary>
        /// <returns></returns>
        public static bool fnc_Create_Button(ANCHOR eAnchor, float fPosX, float fPosY, float fDiameter, int iCtrlSprite = 0)
        {
            if (!v.cRoot)
            {
                LOGW(K_S_ERROR_NULL_ROOT);
                return false;
            }
            return v.cRoot.fnc_Button_Create(eAnchor, fPosX, fPosY, fDiameter, iCtrlSprite);
        }

        /// <summary>
        /// Get the number of buttons in the current on screen joystick.
        /// </summary>
        /// <returns></returns>
        public static int fnc_Button_GetCount()
        {
            if (!v.cRoot)
            {
                return 0;
            }
            return v.cRoot.fnc_Button_GetCount();
        }

        /// <summary>
        /// Set the specified 2D stick active and visible ( or not ).
        /// </summary>
        /// <returns></returns>
        public static void fnc_Button_SetVisible(int iIndex, bool bVisible)
        {
            if (!v.cRoot)
            {
                return;
            }
            v.cRoot.fnc_Button_SetVisible(iIndex, bVisible);
        }

        /// <summary>
        /// Get the current value of the specified button.
        /// </summary>
        /// <returns></returns>
        public static bool fnc_Button_GetValue(int iIndex)
        {
            if (!v.cRoot)
            {
                return false;
            }
            return v.cRoot.fnc_Button_GetValue(iIndex);
        }
    }
}