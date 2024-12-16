Shader "Unlit/PlayerBallShader"
{
    Properties {
		_MainTex ("Base (RGB) Mask (A)", 2D) = "white" {}
		_Transparency ("Transparency", Range(0,1)) = 1
	}
	SubShader {
		Tags { "Queue"="Transparent" "RenderType"="Transparent" "PreviewType" = "Plane"}
		Cull Back
		ZTest Off
		ZWrite Off
		Lighting On
		Blend SrcAlpha OneMinusSrcAlpha

		Pass {
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 2.0
				
				#include "UnityCG.cginc"

				struct appdata {
					float4 vertex : POSITION;
					half2 uv : TEXCOORD0;
				};

				struct v2f {
					float4 vertex : SV_POSITION;
					half2 uv : TEXCOORD0;
				};

				#define COMPRESSION_FORCE 0.2f;
				sampler2D _MainTex;
				half4 _MainTex_ST;
				float _Transparency;
				float _CompressionSpeed;
				
				v2f vert (appdata v)
				{
					v2f o;

					float timeBounce = sin(_CompressionSpeed) * COMPRESSION_FORCE;
					
	                v.vertex.y += timeBounce * v.vertex.y;
					v.vertex.x -= timeBounce * v.vertex.x;

					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.uv,_MainTex);
					
					return o;
				}

				fixed4 frag (v2f i) : SV_Target
	            {
	                fixed4 col = tex2D(_MainTex, i.uv);
	            	col.a *= _Transparency;
	                return col;
	            }
			ENDCG
		}
	}
}

