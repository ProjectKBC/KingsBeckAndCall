Shader "Custom/RiaColliderDebugMode" {
	Properties
	{
	}
	SubShader
	{
		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard alpha
		#pragma target 3.0

		struct Input
		{
			float3 worldPos;
		};

		float _radius;
	
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			float dist = distance(fixed3(0,0,0), IN.worldPos);
			float radius = _radius;
			if (radius < dist)
			{
				o.Albedo = fixed4(255/255.0, 0/255.0, 0/255.0, 0.6);
				o.Alpha = 0.6;
			}
			else
			{
				o.Albedo = fixed4(1,1,1,0);
				o.Alpha = 0.0;
			}
		}
		ENDCG
	}
	FallBack "Diffuse"
}