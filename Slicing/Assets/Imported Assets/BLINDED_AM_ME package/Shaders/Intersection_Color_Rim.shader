
//	For world space positions and texture coordinates, use float precision.
//	For everything else (vectors, HDR colors, etc.), start with half precision. Increase only if necessary.
//	For very simple operations on texture data, use fixed precision.

Shader "Custom/Unlit/Intersection_Color_Rim"
{
	Properties
	{

		_MainColor("Main Color", Color) = (1, 1, 1, 0.25)
		_IntersectionColor("Intersection Color", Color) = (1, 1, 1, 1) 
        _IntersectionMax("Intersection Max", Range(0.01,5)) = 1 
        _IntersectionDamper("Intersection Damper", Range(0,1)) = 0.0

        // rim values
        _RimColor ("Rim Color", Color) = (1,1,1,1)
	    _RimPower ("Rim Power", Range(0.5,8.0)) = 3.0

	}
	SubShader
	{
		Tags { "Queue"="Transparent" "RenderType"="Transparent"  }
        LOD 100

        UsePass "Custom/Unlit/Intersection_Color/PASS" // first

		UsePass "Custom/Diffuse_UnlitRimColor/UNLIT_RIM_COLOR" // second
	}

	FallBack "Unlit/Color"
}


