Shader "Unlit/ShaderForClean"
{
   Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Mask("Mask", 2D) = "white"{ }

        _ShadowIntensity ("Shadow Intensity", Range (0, 1)) = 0.6
        //[NoScaleOffset] _MainTexShadow ("Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType" = "Transparent" "IgnoreProjector"="False" "Queue" = "Transparent" }
        Tags {"Queue"="AlphaTest" }
        //Tags { "RenderType" = "Transparent" "IgnoreProjector"="True" "Queue" = "Transparent" }
        LOD 300
        Zwrite Off

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Zwrite off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

 			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
			};

            sampler2D _MainTex;
            sampler2D _Mask;
            float4 _MainTex_ST;
            float _Radius;
            float2 _PointOffset;

            v2f vert (appdata v)
            {
                v2f o;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) :  SV_Target
            {
                float4 mainTexColor = tex2D(_MainTex, i.uv);
                float4 maksColor = tex2D(_Mask, i.uv);

                if(maksColor.g == 1)
                {
                    mainTexColor.a = 0;
                }

                return mainTexColor;
            }
            ENDCG
        }

        //
        
     
            Pass
            {
                Tags {"LightMode" = "ForwardBase" }
                Cull Back
                Blend SrcAlpha OneMinusSrcAlpha
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_fwdbase
     
                #include "UnityCG.cginc"
                #include "AutoLight.cginc"
                uniform float _ShadowIntensity;
     
                struct v2f
                {
                    float4 pos : SV_POSITION;
                    LIGHTING_COORDS(0,1)
                };
                v2f vert(appdata_base v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos (v.vertex);
                    TRANSFER_VERTEX_TO_FRAGMENT(o);
                   
                    return o;
                }
                fixed4 frag(v2f i) : COLOR
                {
                    float attenuation = LIGHT_ATTENUATION(i);
                    return fixed4(0,0,0,(1-attenuation)*_ShadowIntensity);
                }
                ENDCG
            }
     
        
        //Fallback "VertexLit"

        //

    }


}
