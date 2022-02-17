Shader "Custom/TestShader"
{
    Properties
    {
        _Color ("Color", color) = (1,1,1,1) 
        _MainTex ("Texture", 2D) = "white" {}
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

            // Input to vert
            struct MeshData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            // Input to frag
            struct Interpolators
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal :TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            
            float Random (float2 uv)
            {
                return frac(sin(dot(uv,float2(12.9898,78.233)))*43758.5453123);
            }
            
            Interpolators vert (MeshData v)
            {
                Interpolators output;
                
                //v.vertex.z += Random(v.uv);
                
                float4 vertex = float4(v.vertex.xy, v.vertex.z + sin(v.vertex.x) + cos(v.vertex.y), v.vertex.w);
                
                output.vertex = UnityObjectToClipPos(vertex);
                output.normal = UnityObjectToWorldNormal(v.normal);
                output.uv = v.uv;
                
                return output;
            }

            fixed4 frag (Interpolators i) : SV_Target
            {
                return float4(i.normal, 1);
            }
            ENDCG
        }
    }
}
