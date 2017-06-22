﻿Shader "Custom/Toon Lit Receive Shadows" {
	Properties{
		_Color( "Main Color", Color ) = (.5,.5,.5,1)
		_OutlineColor( "Outline Color", Color ) = (0,0,0,1)
		_Outline( "Outline width", Range( .002, 0.03 ) ) = .005
		_MainTex( "Base (RGB)", 2D ) = "white" {}
	_Ramp( "Toon Ramp (RGB)", 2D ) = "gray" {}
	}

		SubShader{
		Tags{"RenderType" = "Opaque"}
		UsePass "Toon/Lit/FORWARD"
		UsePass "Toon/Basic Outline/OUTLINE"
		LOD 200

		CGPROGRAM
#pragma surface surf ToonRamp

		sampler2D _Ramp;

	// custom lighting function that uses a texture ramp based
	// on angle between light direction and normal
#pragma lighting ToonRamp exclude_path:prepass
	inline half4 LightingToonRamp( SurfaceOutput s, half3 lightDir, half atten ) {
#ifndef USING_DIRECTIONAL_LIGHT
		lightDir = normalize( lightDir );
#endif

		half d = (dot( s.Normal, lightDir ) * 0.5 + 0.5) * atten; // ここで減衰率を適用させる
		half3 ramp = tex2D( _Ramp, float2(d,d) ).rgb; // Toon Rampの色を取得するためのパラメータとしてattenを利用する

		half4 c;
		c.rgb = s.Albedo * _LightColor0.rgb * ramp * 2; // attenを削除
		c.a = 0;
		return c;
	}

	sampler2D _MainTex;
	float4 _Color;

	struct Input {
		float2 uv_MainTex : TEXCOORD0;
	};

	void surf( Input IN, inout SurfaceOutput o ) {
		half4 c = tex2D( _MainTex, IN.uv_MainTex ) * _Color;
		o.Albedo = c.rgb;
		o.Alpha = c.a;
	}
	ENDCG

	}

		Fallback "Diffuse"
}