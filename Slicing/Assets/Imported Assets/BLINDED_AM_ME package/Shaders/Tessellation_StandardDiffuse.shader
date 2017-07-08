
// based on tessellation shader from Unity standard assets
// with a fix to tiling

// https://docs.unity3d.com/Manual/SL-SurfaceShaderTessellation.html

Shader "Custom/Tessellation_StandardDiffuse" {
	Properties {

		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0

		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
		_BumpMap ("Normalmap", 2D) = "bump" {}

		_ParallaxHeight("Parallax Height", Range (0.0, 1.0)) = 0.5
		_ParallaxTiling("Parallax Tiling", float) = 1.0
		_ParallaxOffsetX("Parallax OffsetX", float) = 0
		_ParallaxOffsetY("Parallax OffsetY", float) = 0
		_ParallaxMap   ("Heightmap (A)", 2D) = "black" {}
		_EdgeLength    ("Edge length", Range(3,50)) = 10

	}
	SubShader { 
		Tags { "RenderType"="Opaque" }
		LOD 800
		
	CGPROGRAM
	#pragma surface surf Standard addshadow vertex:disp tessellate:tessEdge
	// Use shader model 3.0 target, to get nicer looking lighting
	#pragma target 3.0
	#include "Tessellation.cginc"

	half _Glossiness;
	half _Metallic;

	fixed4    _Color;
	sampler2D _MainTex;
	sampler2D _BumpMap;

	float     _ParallaxHeight;
	float     _ParallaxTiling;
	float     _ParallaxOffsetX;
	float     _ParallaxOffsetY;


	sampler2D _ParallaxMap;
	float     _EdgeLength;


	struct Input {
		float2 uv_MainTex;
		float2 uv_BumpMap;
	};


	void surf (Input IN, inout SurfaceOutputStandard o) {

		fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
		o.Albedo = tex.rgb * _Color.rgb;
		o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
//		o.Emission
		o.Metallic = _Metallic;
		o.Smoothness = _Glossiness;
//		o.Occlusion
		o.Alpha = tex.a * _Color.a;

//		fixed3 Albedo;      // base (diffuse or specular) color
//	    fixed3 Normal;      // tangent space normal, if written
//	    half3 Emission;
//	    half Metallic;      // 0=non-metal, 1=metal
//	    half Smoothness;    // 0=rough, 1=smooth
//	    half Occlusion;     // occlusion (default 1)
//	    fixed Alpha;        // alpha for transparencies
		
	}

	struct appdata {
		float4 vertex : POSITION;
		float4 tangent : TANGENT;
		float3 normal : NORMAL;
		float2 texcoord : TEXCOORD0;
		float2 texcoord1 : TEXCOORD1;
		float2 texcoord2 : TEXCOORD2;
	};

	void disp (inout appdata v)
	{

		float d = tex2Dlod(_ParallaxMap, float4(v.texcoord.xy * _ParallaxTiling + float2(_ParallaxOffsetX,_ParallaxOffsetY),0,0)).a * _ParallaxHeight;

		v.vertex.xyz += v.normal * d;
	}

	float4 tessEdge (appdata v0, appdata v1, appdata v2)
	{
		return UnityEdgeLengthBasedTessCull (v0.vertex, v1.vertex, v2.vertex, _EdgeLength, _ParallaxHeight * 1.5f);
	}

	ENDCG
	}

FallBack "Diffuse"
}
