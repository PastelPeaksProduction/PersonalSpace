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
    //	CLASS: SD_Joystick_control_baseclass
    //#############################################################################################################################
    public class SD_Joystick_control_baseclass : SD_MonoBehaviour
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	WORKING DATA
        protected class BASECLASS_DATA
        {
            public Transform tParentGroup;
            public Vector3 vTouchDown;
            public Vector3 vOriginalPos;

            public Transform tControlToMove;
            public Transform tControlToPush;
            public Transform tControlDecor;

            public bool bVisible = true;
            public bool bHasFocus = false;
            public bool bGotOriginalPos = false;
            public float fMoveLimit;
        }
        protected BASECLASS_DATA b = new BASECLASS_DATA();

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	 ____  _   _ ____  _     ___ ____ 
        //	|  _ \| | | | __ )| |   |_ _/ ___|
        //	| |_) | | | |  _ \| |    | | |    
        //	|  __/| |_| | |_) | |___ | | |___ 
        //	|_|    \___/|____/|_____|___\____|
        //
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	TOUCH START
        public void fnc_Touch_Start(Vector3 vPos)
        {
            if (!b.bVisible)
            {
                return;
            }
            if (!b.bGotOriginalPos)
            {
                b.vOriginalPos = this.transform.localPosition;
            }
            b.bHasFocus = true;
            b.vTouchDown = vPos;
            if (b.tControlToPush)
            {
                b.tControlToPush.localScale = Vector3.one * 0.9f;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	TOUCH END
        public void fnc_Touch_End(Vector3 vPos)
        {
            if (!b.bVisible)
            {
                return;
            }
            if (b.tControlToMove)
            {
                b.tControlToMove.localPosition = b.vOriginalPos;
            }
            b.bHasFocus = false;
            if (b.tControlToPush)
            {
                b.tControlToPush.localScale = Vector3.one;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	SET ACTIVE AND VISIBLE
        public void fnc_SetVisible(bool bVisible)
        {
            fnc_Touch_End(Vector3.zero);
            b.bVisible = bVisible;

            if (b.tControlToMove)
            {
                fnc_SetTransformVisible(b.tControlToMove, bVisible);
            }
            if (b.tControlToPush)
            {
                fnc_SetTransformVisible(b.tControlToPush, bVisible);
            }
            if (b.tControlDecor)
            {
                fnc_SetTransformVisible(b.tControlDecor, bVisible);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	SET COLOR
        public void fnc_SetColor(Color oColor)
        {
            if (b.tControlToMove)
            {
                fnc_SetTransformColor(b.tControlToMove, oColor);
            }
            if (b.tControlToPush)
            {
                fnc_SetTransformColor(b.tControlToPush, oColor);
            }
            if (b.tControlDecor)
            {
                fnc_SetTransformColor(b.tControlDecor, oColor);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	SET COLLISION LAYER
        public void fnc_SetCollisionLayer(int iLayer)
        {
            if (b.tControlToMove)
            {
                b.tControlToMove.gameObject.layer = iLayer;
            }
            if (b.tControlToPush)
            {
                b.tControlToPush.gameObject.layer = iLayer;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	 ___ _   _ _   _ _____ ____  ___ _____ _____ ____  
        //	|_ _| \ | | | | | ____|  _ \|_ _|_   _| ____|  _ \ 
        //	 | ||  \| | |_| |  _| | |_) || |  | | |  _| | | | |
        //	 | || |\  |  _  | |___|  _ < | |  | | | |___| |_| |
        //	|___|_| \_|_| |_|_____|_| \_\___| |_| |_____|____/ 
        //
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	CLOCK PER FRAME OPTION
        public virtual void fnc_Clock() { }//IMPLEMENTED IN INHERITOR CLASS

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	TOUCH MOVE
        Vector3 vActivePos;
        public virtual void fnc_Touch_Move(Vector3 vPos) { }//IMPLEMENTED IN INHERITOR CLASS

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	 ____  ____  _____     ___  _____ _____ 
        //	|  _ \|  _ \|_ _\ \   / / \|_   _| ____|
        //	| |_) | |_) || | \ \ / / _ \ | | |  _|  
        //	|  __/|  _ < | |  \ V / ___ \| | | |___ 
        //	|_|   |_| \_\___|  \_/_/   \_\_| |_____|
        //
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	SET VIS HELPER
        private void fnc_SetTransformVisible(Transform tObj, bool bVisible)
        {
            if (tObj.GetComponent<Collider>())
            {
                tObj.GetComponent<Collider>().enabled = bVisible;
            }
            if (tObj.GetComponent<MeshRenderer>())
            {
                tObj.GetComponent<MeshRenderer>().enabled = bVisible;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	SET COLOR HELPER
        private void fnc_SetTransformColor(Transform tObj, Color oColor)
        {
            Mesh oMesh = GetMesh(tObj);
            if (!oMesh)
            {
                LOGW("OBJECT HAS NO MESH");
                return;
            }

            Vector3[] avVertices = oMesh.vertices;
            if (avVertices.Length <= 0)
            {
                LOGW("OBJECT HAS NO VERTS");
                return;
            }
            Color[] aoColors = new Color[avVertices.Length];
            for (int iLoop = 0; iLoop < avVertices.Length; iLoop++)
            {
                aoColors[iLoop] = oColor;
            }
            oMesh.colors = aoColors;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	GET MESH
        private Mesh GetMesh(Transform tObj)
        {
            return GetMesh(tObj.gameObject);
        }
        private Mesh GetMesh(GameObject oObj)
        {
            Mesh oMesh = null;

            if (oObj.GetComponent<MeshFilter>())
            {
                oMesh = oObj.GetComponent<MeshFilter>().mesh;
                if (oMesh)
                {
                    return oMesh;
                }
            }

            if (oObj.GetComponent<SkinnedMeshRenderer>())
            {
                oMesh = oObj.GetComponent<SkinnedMeshRenderer>().sharedMesh;
                if (oMesh)
                {
                    return oMesh;
                }
            }
            LOGW("CANT FIND MESH IN " + oObj.name);
            return oMesh;
        }
    }
}