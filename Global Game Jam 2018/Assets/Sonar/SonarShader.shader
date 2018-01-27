Shader "Unlit/SonarShader"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MaxDistance("Max Distance", Float) = 10
		_FlashRate("Flash rate", Float) = 100
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 worldPos : TEXCOORD0;
			};

			uniform float3 sonarOrigin;
			uniform float3 sonarDirection;

			float4 _Color;
			float _MaxDistance;
			float _FlashRate;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float3 sonarToFragPos = i.worldPos.xyz - sonarOrigin;
				float dist = min(length(sonarToFragPos), _MaxDistance);

				float intensity = 1 - (dist / _MaxDistance);

				float t = sin(_Time.y * _FlashRate * intensity);

				return _Color * t * intensity;
			}

			ENDCG
		}
	}
}
