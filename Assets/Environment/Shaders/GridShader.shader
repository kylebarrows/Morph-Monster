Shader "Unlit/GridShader"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
        _DiffuseMap("Diffuse Map", 2D) = "white" {}
        _Color ("Tint", Color) = (0, 0, 0, 1)
		_TextureScale ("Texture Scale",float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM

            #include "UnityCG.cginc"

            #pragma vertex vert
            #pragma fragment frag

            sampler2D _DiffuseMap;
            float4 _DiffuseMap_ST;
            float _TextureScale;
            fixed4 _Color;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
				//calculate the position in clip space to render the object
				o.vertex = UnityObjectToClipPos(v.vertex);
				//calculate world position of vertex
				float4 worldPos = mul(unity_ObjectToWorld, v.vertex);
				//change UVs based on tiling and offset of texture
				o.uv = TRANSFORM_TEX(worldPos.xy, _DiffuseMap);

                o.uv /= _TextureScale;
				return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_DiffuseMap, i.uv);

                col *= _Color;
                return col;
            }
            ENDCG
        }
    }
    Fallback "Standard"
}
