Shader "Custom/TintedGreyscaleShader" 
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Color("Tint Color", Color) = (1,1,1,1)
        _Contrast("Contrast", Range(0, 2)) = 1
        _Brightness("Brightness", Range(-1, 1)) = 0
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off
 
        Pass 
        {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
 
                #include "UnityCG.cginc"
 
                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };
 
                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };
 
                sampler2D _MainTex;
                float4 _MainTex_ST;
                fixed4 _Color;
                float _Contrast;
                float _Brightness;
 
                v2f vert(appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    return o;
                }
 
                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 col = tex2D(_MainTex, i.uv);
                    float grey = dot(col.rgb, float3(0.299, 0.587, 0.114));
                    
                    // Apply brightness
                    grey = saturate(grey + _Brightness);
                    
                    // Apply contrast
                    grey = saturate((grey - 0.5) * _Contrast + 0.5);
                    
                    fixed4 tinted = fixed4(grey * _Color.r, grey * _Color.g, grey * _Color.b, col.a * _Color.a);
                    return tinted;
                }
                ENDCG
        }        
    }
    FallBack "Diffuse"
}