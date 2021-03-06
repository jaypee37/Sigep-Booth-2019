﻿Shader "Custom/Powerbox"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_NormalTex ("Normal Map", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
		_MetallicTex("Metallic Map", 2D) = "white" {}

		_EmissiveTex ("Emission Map", 2D) = "white" {}
		_OnButtonColor ("On Button Color", Color) = (1, 1, 1, 1)
		_Button1Color ("Button Set 1 Color", Color) = (1, 1, 1, 1)
		_Button2Color ("Button Set 2 Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
		sampler2D _NormalTex;
		sampler2D _MetallicTex;
		sampler2D _EmissiveTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
		fixed4 _OnButtonColor;
		fixed4 _Button1Color;
		fixed4 _Button2Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
			o.Normal = tex2D(_NormalTex, IN.uv_MainTex);
            // Metallic and smoothness come from slider variables
            o.Metallic = tex2D (_MetallicTex, IN.uv_MainTex) * _Metallic;
            o.Smoothness = _Glossiness;
			fixed4 eCol = tex2D (_EmissiveTex, IN.uv_MainTex);
			o.Emission = eCol.r * _Button1Color + eCol.g * _Button2Color + eCol.b * _OnButtonColor;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
