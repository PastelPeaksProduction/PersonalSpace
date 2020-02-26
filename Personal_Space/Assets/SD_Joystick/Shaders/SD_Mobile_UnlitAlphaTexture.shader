///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//   ____ ___ _     ___ ____ ___  _   _   ____  ____   ___ ___ ____  
//  / ___|_ _| |   |_ _/ ___/ _ \| \ | | |  _ \|  _ \ / _ \_ _|  _ \ 
//  \___ \| || |    | | |  | | | |  \| | | | | | |_) | | | | || | | |
//   ___) | || |___ | | |__| |_| | |\  | | |_| |  _ <| |_| | || |_| |
//  |____/___|_____|___\____\___/|_| \_| |____/|_| \_\\___/___|____/ 
//	
//	MOBILE: UNLIT COLOR WITH ALPHA
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

Shader "SiliconDroid/Mobile_UnlitAlphaTexture"
{
	Properties
	{
		_Alpha ("Alpha", Range (0.0,1.0)) = 1.0 
		_MainTex ("Base (RGB) Transparency (A)", 2D) = "" { }
	}

	Category
	{
		Lighting Off
		ZWrite Off
		Cull back
		Blend SrcAlpha OneMinusSrcAlpha
		Tags {Queue=Transparent}
		SubShader
		{
			Pass
			{
				SetTexture [_MainTex] 
				{
				   constantColor (1, 1, 1, [_Alpha])
				   Combine texture * primary, texture * constant
				}
			}
		} 
	}
}

