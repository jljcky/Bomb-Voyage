﻿Shader "Unlit/ToonShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _ShadeColor ("Shade Color", Color) = (0, 0, 0, 1)
        _Threshold ("Threshold", Range (-1, 1)) = 0
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineWidth ("Outline Width", Range(0.01, 0.3)) = 0.1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque"}
		LOD 100
        
        //outline using inverted hull method
        Pass
        {
            Lighting Off
            Cull Front
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"
            
            //sampler2D _MainTex;
            float4 _OutlineColor;
            float _OutlineWidth;
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 norm : NORMAL;
                
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
            
            v2f vert (appdata v)
            {
                v2f o;
                v.vertex.xyz += v.norm *_OutlineWidth;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            
            fixed4 frag (v2f i) : COLOR
            {
                return _OutlineColor;
            }
            ENDCG
        }
        
        //toon
		Pass
		{
            Tags {
                "LightMode"="ForwardBase" 
            }
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"
            
            sampler2D _MainTex;
            float4 _Color;
            float4 _ShadeColor;
            float _Threshold;
            
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
                float3 norm : NORMAL;
                
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
                float3 norm : NORMAL;
			};
            
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
                o.norm = UnityObjectToWorldNormal(v.norm);
                TRANSFER_SHADOW(o)
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
                float diff = step(_Threshold, dot(_WorldSpaceLightPos0, i.norm));
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                fixed shadow = SHADOW_ATTENUATION(i);
                fixed4 shade = _ShadeColor * (1-diff) + fixed4(1,1,1,1) * diff;
                col.rgba *= shade;
				return col;
			}
			ENDCG
		}
        
        //shadows
        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
	}
}
