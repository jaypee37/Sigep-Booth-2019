Shader "Custom/NeonGlow"
{
	Properties
	{
		_MainTex ("Color Map", 2D) = "white" {}

		_Color1 ("First Color", Color) = (1, 1, 1, 1)
		_Strength1 ("First Strength", Range(0, 1)) = 1.0

		_Color2 ("Second Color", Color) = (1, 1, 1, 1)
		_Strength2 ("Second Strength", Range(0, 1)) = 1.0

		_Color3 ("Third Color", Color) = (1, 1, 1, 1)
		_Strength3 ("Third Strength", Range(0, 1)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM

		#pragma surface surf Standard
        
        struct Input
        {
            float2 uv_MainTex;
        };

		sampler2D _MainTex;

		fixed4 _Color1;
		fixed4 _Color2;
		fixed4 _Color3;

		half _Strength1;
		half _Strength2;
		half _Strength3;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			fixed4 col = tex2D(_MainTex, IN.uv_MainTex);
			o.Emission = col.r * _Color1 * _Strength1 + col.g * _Color2 * _Strength2 + col.b * _Color3 * _Strength3;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
