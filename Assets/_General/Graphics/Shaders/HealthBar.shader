Shader "Custom/Bar3D"
{
    Properties
    {
        _Fill("Fill Amount", Range(0,1)) = 1
        _FillColor("Fill Color", Color) = (0,1,0,1)
        _EmptyColor("Empty Color", Color) = (1,0,0,1)
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalRenderPipeline" }
        LOD 100

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            CBUFFER_START(UnityPerMaterial)
                float _Fill;
                float4 _FillColor;
                float4 _EmptyColor;
            CBUFFER_END

            Varyings vert (Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                return OUT;
            }

            half4 frag (Varyings IN) : SV_Target
            {
                // заполненная часть — слева направо
                float filled = step(IN.uv.x, _Fill);
                float4 color = lerp(_EmptyColor, _FillColor, filled);
                return color;
            }
            ENDHLSL
        }
    }
}
