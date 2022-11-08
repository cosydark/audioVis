Shader "全屏蔽别哔哔写的Shader/rampMap"
{
    Properties
    {
        _Color ("颜色混合", COLOR) = (1,1,1,1)
        _RampTex ("渐变纹理", 2D) = "White" {}
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
            sampler2D _RampTex;
            float4 _RampTex_ST;
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
                o.worldNormal = normalize( UnityObjectToWorldNormal (v.normal));
                o.worldPos = mul (unity_ObjectToWorld, v.vertex).xyz;//转世界坐标
                o.uv = TRANSFORM_TEX (v.uv, _RampTex);
                //
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed3 worldLightDir = normalize (UnityWorldSpaceLightDir (i.worldPos));
                //
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;
                //
                fixed halfLambet = 0.5 * dot (i.worldNormal, worldLightDir) + 0.5;// 0-1
                //法线与光线夹角的余弦，余弦在 0-派/2之间单调递减
                fixed3 diffuseColor = tex2D (_RampTex, fixed2 (halfLambet, 0)).rgb * _Color;
                //
                fixed3 diffuse = _LightColor0.rgb * diffuseColor;
                //
                fixed3 viewDir = normalize (UnityWorldSpaceViewDir (i.worldPos));
                fixed3 halfDir = normalize (worldLightDir + viewDir);
                //fixed specular = _LightColor0.rgb * _Specular.rgb * pow (saturate (dot (i.worldNormal, halfDir)), _Gloss);
                //
                return fixed4 (ambient + diffuse, 1);
            }
            ENDCG
        }
    }
    Fallback "Diffuse"
}
