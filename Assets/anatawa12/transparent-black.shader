Shader "Unlit/transparent-black"
{
    Properties
    {
       _Alpha("Alpha", Range(0, 1)) = 1
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        LOD 100
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
    		Lighting Off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            fixed _Alpha;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.vertex.x = sign(o.vertex.x);
                o.vertex.y = sign(o.vertex.y);
                o.vertex.z = UNITY_NEAR_CLIP_VALUE;
                o.vertex.w = 1;
                return o;
            }

            fixed4 frag (v2f _) : SV_Target
            {
                return fixed4(0, 0, 0, _Alpha);
            }
            ENDCG
        }
    }
}
