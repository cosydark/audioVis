Shader "全屏蔽别哔哔写的Shader/baseShader"
{
    Properties
    {
        _Color ("颜色混合", COLOR) = (1,1,1,1)
    }
    SubShader
    {
        Cull Off
        Pass
        {
            Tags{"LightMode"="ForwardBase"}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            //
            fixed4 _Color;
            //
            struct a2v
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldNormal : COLOR0;
                float3 worldPos : COLOR1;
                float2 uv : COLOR2;
            };

            v2f vert (a2v v)
            {
                v2f o = (v2f)0;
                //
                o.pos = UnityObjectToClipPos(v.vertex);
                //
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _Color;
            }
            ENDCG
        }
    }
    Fallback "Diffuse"
}
