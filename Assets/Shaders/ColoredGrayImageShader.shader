Shader "UI/TintedGreyscaleUI"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1,1,1,1)
        _Contrast("Contrast", Range(0, 2)) = 1
        _Brightness("Brightness", Range(-1, 1)) = 0
        
        [HideInInspector] _StencilComp("Stencil Comparison", Float) = 8
        [HideInInspector] _Stencil("Stencil ID", Float) = 0
        [HideInInspector] _StencilOp("Stencil Operation", Float) = 0
        [HideInInspector] _StencilWriteMask("Stencil Write Mask", Float) = 255
        [HideInInspector] _StencilReadMask("Stencil Read Mask", Float) = 255
        [HideInInspector] _ColorMask("Color Mask", Float) = 15
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
            };

            sampler2D _MainTex;
            fixed4 _Color;
            float _Contrast;
            float _Brightness;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;

            v2f vert(appdata_t v)
            {
                v2f OUT;
                OUT.worldPosition = v.vertex;
                OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);
                OUT.texcoord = v.texcoord;
                
                // Умножаем на цвет компонента Image
                OUT.color = v.color * _Color;
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                // Берем цвет текстуры (с учетом TextureSampleAdd для спрайтов)
                half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd);
                
                // Конвертируем в grayscale
                float grey = dot(color.rgb, float3(0.299, 0.587, 0.114));
                
                // Применяем яркость и контраст
                grey = saturate(grey + _Brightness);
                grey = saturate((grey - 0.5) * _Contrast + 0.5);
                
                // Применяем tint цвет (уже включает цвет из Image) и альфа-канал
                color = fixed4(grey * IN.color.rgb, color.a * IN.color.a);
                
                // Применяем маскирование UI
                color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                
                return color;
            }
            ENDCG
        }
    }
}