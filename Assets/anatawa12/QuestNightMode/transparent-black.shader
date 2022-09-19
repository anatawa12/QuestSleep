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

            fixed _Alpha;

            float4 vert (float4 vertex : POSITION) : SV_POSITION
            {
                float4 in_clip = UnityObjectToClipPos(vertex);
                return float4(
                    sign(in_clip.x),
                    sign(in_clip.y),
                    UNITY_NEAR_CLIP_VALUE,
                    1
                );
            }

            fixed4 frag () : SV_Target
            {
                return fixed4(0, 0, 0, _Alpha);
            }
            ENDCG
        }
    }
}
