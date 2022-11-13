Shader "Custom/ShaderModel" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_BumpTex("Normal", 2D) = "bump"{}
		_SpecularTex("Specular", 2D) = "black"{}
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Glossiness ("Smoothness", 2D) = "black"{}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf StandardSpecular fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpTex;
		sampler2D _SpecularTex;
		sampler2D _Glossiness;
		half _Mettalic;

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _Color;
		UNITY_INSTANCING_CBUFFER_START(Props)
		UNITY_INSTANCING_CBUFFER_END

		void surf (Input IN, inout SurfaceOutputStandardSpecular o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			fixed4 s = tex2D(_SpecularTex, IN.uv_MainTex);
			fixed4 g = tex2D(_Glossiness, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Normal = UnpackNormal(tex2D(_BumpTex, IN.uv_MainTex));
			o.Specular = (s.r + s.g + s.b) * 0.35f;
			o.Smoothness = 0.6f;
			//o.Metallic = _Mettalic;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
