Shader "Custom/Unlit/Intersection_Color_Rim_Bumped"
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

	    _BumpMap("Normal Map", 2D) = "bump" {}

	}
	SubShader
	{
		Tags { "Queue"="Transparent" "RenderType"="Transparent"  }
        LOD 100

        UsePass "Custom/Unlit/Intersection_Color/PASS" // first

		UsePass "Custom/DiffuseBumped_UnlitRimColorBumped/UNLIT_RIM_COLOR_BUMPED" // second
	}

	FallBack "Unlit/Color"
}