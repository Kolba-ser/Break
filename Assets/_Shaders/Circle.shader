Shader "Unlit/Circle"
{
    Properties 
    {
        [HideInInspector]
        _MainTexture("Heightmap", 2D) = "white" 
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTexture;
            float2 _HeightRemap;
            float3 _RealSize;

            float2 _Center;
            float _Radius;
            float _Smoothness;


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float height = UnpackHeightmap(tex2D(_MainTexture, i.uv));
                float2 position = i.uv * _RealSize.xz;
                float nh = 1 - smoothstep(_Radius, _Radius + _Smoothness, distance(position, _Center));
                float newHeight = saturate(_HeightRemap.x + nh * (_HeightRemap.y - _HeightRemap.x));
                return PackHeightmap(newHeight);
            }
            ENDCG
        }
    }
}
