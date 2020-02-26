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
    //	CLASS: SD_Joystick_factory
    //#############################################################################################################################
    public class SD_Joystick_factory : SD_MonoBehaviour
    {

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	CONSTANTS
        const string K_S_MATERIAL = "sd_joystick_material";

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	UV TILE CLASS
        public class UV_TILE
        {
            public float fU0;
            public float fU1;
            public float fV0;
            public float fV1;
            public float fVM;
            public float fUM;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	 ____  _   _ ____  _     ___ ____ 
        //	|  _ \| | | | __ )| |   |_ _/ ___|
        //	| |_) | | | |  _ \| |    | | |    
        //	|  __/| |_| | |_) | |___ | | |___ 
        //	|_|    \___/|____/|_____|___\____|
        //
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	GET UV TILE
        const int K_I_ATLAS_COLS = 8;
        const int K_I_ATLAS_ROWS = 8;
        const float K_F_ATLAS_ROW_SPAN = 1.0f / (float)K_I_ATLAS_ROWS;
        const float K_F_ATLAS_COL_SPAN = 1.0f / (float)K_I_ATLAS_COLS;
        const float K_F_ATLAS_ROW_SPAN_HALF = K_F_ATLAS_ROW_SPAN * 0.5f;
        const float K_F_ATLAS_COL_SPAN_HALF = K_F_ATLAS_COL_SPAN * 0.5f;
        static int iCol, iRow;
        static UV_TILE cUV = new UV_TILE();
        public static UV_TILE fnc_GetUV(int iAtlasIndex)
        {
            iCol = iAtlasIndex % K_I_ATLAS_COLS;
            cUV.fU0 = iCol * K_F_ATLAS_COL_SPAN;
            cUV.fU1 = cUV.fU0 + K_F_ATLAS_COL_SPAN;
            cUV.fUM = cUV.fU0 + K_F_ATLAS_ROW_SPAN_HALF;
            iRow = iAtlasIndex / K_I_ATLAS_ROWS;
            cUV.fV0 = iRow * K_F_ATLAS_ROW_SPAN;
            cUV.fV1 = cUV.fV0 + K_F_ATLAS_COL_SPAN;
            cUV.fVM = cUV.fV0 + K_F_ATLAS_ROW_SPAN_HALF;
            return cUV;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	CREATE A QUAD AND PASS IT BACK
        static float fRadX, fRadY;
        public static Transform fnc_CreateMesh(GameObject oObj, float fWidth, float fHeight, int iSprite)
        {
            return fnc_CreateMesh(oObj, fWidth, fHeight, fnc_GetUV(iSprite));
        }
        public static Transform fnc_CreateMesh(GameObject oObj, float fWidth, float fHeight, UV_TILE cUV)
        {
            //  DATA
            fRadX = fWidth * 0.5f;
            fRadY = fHeight * 0.5f;
            Vector3[] avVertices = null;
            Vector2[] avUV = null;
            int[] aiIndeces = null;

            //-----------------------------------------------------------------------------------------------------------------------------
            //	SQUARE
            if (fWidth == fHeight)
            {
                //  VERTS
                avVertices = new Vector3[4];

                avVertices[0] = new Vector3(-fRadX, -fRadY);
                avVertices[1] = new Vector3(fRadX, -fRadY);

                avVertices[2] = new Vector3(-fRadX, fRadY);
                avVertices[3] = new Vector3(fRadX, fRadY);
                
                //  UVS
                avUV = new Vector2[4];

                avUV[0] = new Vector2(cUV.fU0, cUV.fV0);
                avUV[1] = new Vector2(cUV.fU1, cUV.fV0);

                avUV[2] = new Vector2(cUV.fU0, cUV.fV1);
                avUV[3] = new Vector2(cUV.fU1, cUV.fV1);

                //  INDEX
                aiIndeces = new int[6];

                aiIndeces[0] = 0;
                aiIndeces[1] = 2;
                aiIndeces[2] = 3;

                aiIndeces[3] = 0;
                aiIndeces[4] = 3;
                aiIndeces[5] = 1;
            }
            //-----------------------------------------------------------------------------------------------------------------------------
            //	HORIZ RECT
            else if (fWidth > fHeight)
            {
                //  VERTS
                avVertices = new Vector3[8];

                float fX0 = -fRadX;
                float fX1 = -fRadX + fRadY;
                float fX2 = fRadX - fRadY;
                float fX3 = fRadX;

                avVertices[0] = new Vector3(fX0, -fRadY);
                avVertices[1] = new Vector3(fX0, fRadY);

                avVertices[2] = new Vector3(fX1, -fRadY);
                avVertices[3] = new Vector3(fX1, fRadY);

                avVertices[4] = new Vector3(fX2, -fRadY);
                avVertices[5] = new Vector3(fX2, fRadY);

                avVertices[6] = new Vector3(fX3, -fRadY);
                avVertices[7] = new Vector3(fX3, fRadY);

                //  UVS
                avUV = new Vector2[8];

                avUV[0] = new Vector2(cUV.fU0, cUV.fV0);
                avUV[1] = new Vector2(cUV.fU0, cUV.fV1);
                avUV[2] = new Vector2(cUV.fUM, cUV.fV0);
                avUV[3] = new Vector2(cUV.fUM, cUV.fV1);
                avUV[4] = new Vector2(cUV.fUM, cUV.fV0);
                avUV[5] = new Vector2(cUV.fUM, cUV.fV1);
                avUV[6] = new Vector2(cUV.fU1, cUV.fV0);
                avUV[7] = new Vector2(cUV.fU1, cUV.fV1);

                //  INDEX
                aiIndeces = new int[18];

                aiIndeces[0] = 0;
                aiIndeces[1] = 1;
                aiIndeces[2] = 3;

                aiIndeces[3] = 0;
                aiIndeces[4] = 3;
                aiIndeces[5] = 2;

                aiIndeces[6] = 2;
                aiIndeces[7] = 3;
                aiIndeces[8] = 5;

                aiIndeces[9] = 2;
                aiIndeces[10] = 5;
                aiIndeces[11] = 4;

                aiIndeces[12] = 4;
                aiIndeces[13] = 5;
                aiIndeces[14] = 7;

                aiIndeces[15] = 4;
                aiIndeces[16] = 7;
                aiIndeces[17] = 6;
            }
            //-----------------------------------------------------------------------------------------------------------------------------
            //	VERT RECT
            else if (fWidth < fHeight)
            {
                //  VERTS
                avVertices = new Vector3[8];

                float fY0 = -fRadY;
                float fY1 = -fRadY + fRadX;
                float fY2 = fRadY - fRadX;
                float fY3 = fRadY;

                avVertices[0] = new Vector3(-fRadX, fY0);
                avVertices[1] = new Vector3( fRadX, fY0);

                avVertices[2] = new Vector3(-fRadX, fY1);
                avVertices[3] = new Vector3(fRadX, fY1);

                avVertices[4] = new Vector3(-fRadX, fY2);
                avVertices[5] = new Vector3(fRadX, fY2);

                avVertices[6] = new Vector3(-fRadX, fY3);
                avVertices[7] = new Vector3(fRadX, fY3);

                //  UVS
                avUV = new Vector2[8];

                avUV[0] = new Vector2(cUV.fU0, cUV.fV0);
                avUV[1] = new Vector2(cUV.fU0, cUV.fV1);
                avUV[2] = new Vector2(cUV.fUM, cUV.fV0);
                avUV[3] = new Vector2(cUV.fUM, cUV.fV1);
                avUV[4] = new Vector2(cUV.fUM, cUV.fV0);
                avUV[5] = new Vector2(cUV.fUM, cUV.fV1);
                avUV[6] = new Vector2(cUV.fU1, cUV.fV0);
                avUV[7] = new Vector2(cUV.fU1, cUV.fV1);

                //  INDEX
                aiIndeces = new int[18];

                aiIndeces[0] = 3;
                aiIndeces[1] = 1;
                aiIndeces[2] = 0;

                aiIndeces[3] = 2;
                aiIndeces[4] = 3;
                aiIndeces[5] = 0;

                aiIndeces[6] = 5;
                aiIndeces[7] = 3;
                aiIndeces[8] = 2;

                aiIndeces[9] = 4;
                aiIndeces[10] = 5;
                aiIndeces[11] = 2;

                aiIndeces[12] = 7;
                aiIndeces[13] = 5;
                aiIndeces[14] = 4;

                aiIndeces[15] = 6;
                aiIndeces[16] = 7;
                aiIndeces[17] = 4;
            }

            //  MAKE MESH
            Mesh cMesh = new Mesh();
            cMesh.vertices = avVertices;
            cMesh.uv = avUV;
            cMesh.SetIndices(aiIndeces, MeshTopology.Triangles, 0, true);

            //  APPLY MESH TO OBJ
            oObj = fnc_SetMesh(oObj, cMesh);
            if(oObj == null)
            {
                return null;
            }

            //  DONE
            return oObj.transform;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	CHILD IT
        public static void fnc_ChildToTransform(Transform tObj, Transform tPar, bool bPushZ = false)
        {
            tObj.parent = tPar;
            tObj.SetPositionAndRotation(tPar.position, tPar.rotation);
            if (bPushZ)
            {
                tObj.Translate(0.0f, 0.0f, Camera.main.nearClipPlane * SD_Joystick_root.K_F_PUSHZ_FACTOR);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	CONVERT BOUNDS FROM SCREEN TO WORLD ( LOCALSPACE OPTION )
        static Vector3 vCorner;
        static float fPushZ;
        public static Bounds fnc_Bounds_ScreenToWorld(Bounds cBounds, int iPushZ = 0, Transform tInTransformSpace = null)
        {
            //  PUSH Z
            fPushZ = fnc_GetPushZ(iPushZ);

            //  CONVERT MIN
            vCorner = cBounds.min;
            vCorner.z = fPushZ;
            vCorner = Camera.main.ScreenToWorldPoint(vCorner);
            if (tInTransformSpace)
            {
                vCorner = tInTransformSpace.transform.InverseTransformPoint(vCorner);
            }
            cBounds.min = vCorner;

            //  CONVERT MAX
            vCorner = cBounds.max;
            vCorner.z = fPushZ;
            vCorner = Camera.main.ScreenToWorldPoint(vCorner);
            if (tInTransformSpace)
            {
                vCorner = tInTransformSpace.transform.InverseTransformPoint(vCorner);
            }
            cBounds.max = vCorner;

            //  DONE
            return cBounds;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	GET CONTROL Z PLANE RELATIVE TO CAMERA
        public static float fnc_GetPushZ(int iPushLevel = 0)
        {
            return Camera.main.nearClipPlane * (1.0f + SD_Joystick_root.K_F_PUSHZ_FACTOR * (iPushLevel + 1));
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	SET COLLIDER
        public static void fnc_SetAsCollider(Transform tObj)
        {
            tObj.gameObject.AddComponent<MeshCollider>();
            //tObj.name = "~" + tObj.name;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	SET MAT OPA
        public static bool fnc_SetMaterialOpacity(float fOpacity)
        {
            if (cMaterial == null)
            {
                cMaterial = Resources.Load(K_S_MATERIAL, typeof(Material)) as Material;
                if (cMaterial == null)
                {
                    LOGW("CANT LOAD MATERIAL: " + K_S_MATERIAL);
                    return false;
                }
            }
            cMaterial.SetFloat("_Alpha", fOpacity);
            return true;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	 ____  ____  _____     ___  _____ _____ 
        //	|  _ \|  _ \|_ _\ \   / / \|_   _| ____|
        //	| |_) | |_) || | \ \ / / _ \ | | |  _|  
        //	|  __/|  _ < | |  \ V / ___ \| | | |___ 
        //	|_|   |_| \_\___|  \_/_/   \_\_| |_____|
        //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	SET MATERIAL
        public static Material cMaterial = null;
        private static GameObject fnc_SetMesh(GameObject oObj, Mesh cMesh)
        {
            if (oObj == null)
            {
                oObj = new GameObject();
            }

            if (cMesh == null)
            {
                LOGW("NULL MESH");
                return oObj;
            }
            cMesh.RecalculateBounds();

            MeshRenderer cMeshRen = oObj.AddComponent<MeshRenderer>();
            if (cMeshRen == null)
            {
                LOGW("NULL MESH RENDERER");
                return oObj;
            }

            MeshFilter cMeshFil = oObj.AddComponent<MeshFilter>();
            if (cMeshFil == null)
            {
                LOGW("NULL MESH FILTER");
                return oObj;
            }
            cMeshFil.mesh = cMesh;

            if(cMaterial == null)
            {
                cMaterial = Resources.Load(K_S_MATERIAL, typeof(Material)) as Material;
                if (cMaterial == null)
                {
                    LOGW("CANT LOAD MATERIAL: " + K_S_MATERIAL);
                    return oObj;
                }
            }

            cMeshRen.material = cMaterial;

            return oObj;
        }
    }
}