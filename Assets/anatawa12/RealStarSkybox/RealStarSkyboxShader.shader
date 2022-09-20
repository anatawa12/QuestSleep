// Copyright (c) 2016 Unity Technologies
// Copyright (c) 2022 anatawa12
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
// of the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

Shader "anatawa12/RealStarSkyboxShader"
{
    Properties
    {
        [HideInInspector] _RotQuot ("RotQuot", Vector) = (0, 0, 0, 1)
        [NoScaleOffset] _Tex ("Cubemap", Cube) = "grey" {}
    }

    CustomEditor "anatawa12.RealStarSkybox.RealStarSkyboxShaderGUI"

    SubShader
    {
        Tags
        {
            "Queue"="Background" "RenderType"="Background" "PreviewType"="Skybox"
        }
        Cull Off ZWrite Off

        Pass
        {

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"

            samplerCUBE _Tex;
            float4 _RotQuot;

            #define ROTATE(vertex, degrees, horizontal, axis, back) float3(RotateInDegrees((vertex).horizontal, degrees), (vertex).axis).back


            float3 rotate_via_quaternion(float4 quot, float3 pos)
            {
                return (quot.w * quot.w - dot(quot.xyz, quot.xyz)) * pos + 2.0 * quot.xyz * dot(quot.xyz, pos) + 2 *
                    quot.w * cross(quot.xyz, pos);
            }

            float2 RotateInDegrees(float2 vertex, float degrees)
            {
                float alpha = degrees * (UNITY_PI / 180.0);
                float sina, cosa;
                sincos(alpha, sina, cosa);
                float2x2 m = float2x2(cosa, -sina, sina, cosa);
                return mul(m, vertex);
            }

            struct appdata_t
            {
                float4 vertex : POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 texcoord : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            v2f vert(appdata_t v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                float3 rotated = v.vertex.xyz;
                rotated = rotate_via_quaternion(_RotQuot, rotated);
                o.vertex = UnityObjectToClipPos(rotated);
                o.texcoord = v.vertex.xyz;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return texCUBE(_Tex, i.texcoord);
            }
            ENDCG
        }
    }


    Fallback Off

}