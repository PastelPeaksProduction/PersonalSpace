///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//   ____ ___ _     ___ ____ ___  _   _   ____  ____   ___ ___ ____  
//  / ___|_ _| |   |_ _/ ___/ _ \| \ | | |  _ \|  _ \ / _ \_ _|  _ \ 
//  \___ \| || |    | | |  | | | |  \| | | | | | |_) | | | | || | | |
//   ___) | || |___ | | |__| |_| | |\  | | |_| |  _ <| |_| | || |_| |
//  |____/___|_____|___\____\___/|_| \_| |____/|_| \_\\___/___|____/ 
//
//  ADDS COMMON FUNCTIONS TO NEW SCRIPTS ( JUST FOR #INCLUDE LIKE BEHAVIOR )
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SiliconDroid
{
    //#####################################################################################################################
    //	CLASS: SD_MonoBehaviour
    //#####################################################################################################################
    public class SD_MonoBehaviour : MonoBehaviour
	{
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //   ___ _   _ _   _ _____ ____  ___ _____ _____ ____    _____ _   _ _   _  ____ _____ ___ ___  _   _ ____  
        //  |_ _| \ | | | | | ____|  _ \|_ _|_   _| ____|  _ \  |  ___| | | | \ | |/ ___|_   _|_ _/ _ \| \ | / ___| 
        //   | ||  \| | |_| |  _| | |_) || |  | | |  _| | | | | | |_  | | | |  \| | |     | |  | | | | |  \| \___ \ 
        //   | || |\  |  _  | |___|  _ < | |  | | | |___| |_| | |  _| | |_| | |\  | |___  | |  | | |_| | |\  |___) |
        //  |___|_| \_|_| |_|_____|_| \_\___| |_| |_____|____/  |_|    \___/|_| \_|\____| |_| |___\___/|_| \_|____/ 
        //                                                                                                        
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //  LOG MESSAGE
        protected static void LOGM(float fNumber) { LOGM("" + fNumber); }
        protected static void LOGM(string sText)
		{
            UnityEngine.Debug.Log(LogPrefix() + sText);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //  LOG WARNING
        protected static void LOGW(float fNumber) { LOGW("" + fNumber); }
        protected static void LOGW(string sText)
		{
            UnityEngine.Debug.LogWarning(LogPrefix() + sText);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //  LOG ERROR
        protected static void LOGE(float fNumber) { LOGE("" + fNumber); }
        protected static void LOGE(string sText)
		{
            UnityEngine.Debug.LogError(LogPrefix() + sText);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	LOG HELPER
        static string sClassName;
        static int iPos, iFnd;
        static StackFrame cStack;
        private static String LogPrefix()
        {
            for(iPos = 3; iPos >= 0; iPos--)
            {
                cStack = new StackTrace(true).GetFrame(iPos);
                if (cStack != null)
                {
                    break;
                }
            }
            if(cStack == null)
            {
                return "NULL STACK!: ";
            }
            sClassName = cStack.GetFileName();
            iPos = 0;
            iFnd = -1;
            for(; ; )
            {
                iPos = sClassName.IndexOf('\\', iPos);
                if(iPos > -1)
                {
                    iFnd = ++iPos;
                }
                else
                {
                    break;
                }
            }
            if(iFnd > -1)
            {
                sClassName = sClassName.Substring(iFnd);
            }
            sClassName = sClassName.Replace(".cs", "");
            return "." + DateTime.Now.ToString("fff") + ": " + sClassName  + "." + cStack.GetMethod().Name + "[" + cStack.GetFileLineNumber() + ", " + cStack.GetFileColumnNumber() + "]: ";
        }
    }
}
