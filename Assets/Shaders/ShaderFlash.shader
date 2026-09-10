Shader "Custom/ShaderFlash"
{
    
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Tint ("Tint", Color) = (1, 1, 1, 1)
        _FlashColor ("Warna Flash", Color) = (1, 1, 1, 1)
        _FlashAmount ("Jumlah Flash", Range(0, 1)) = 0
        _GelombangAmplitudo ("Gelombang Amplitudo", Float) = 0
        _GelombangFrekuensi ("Gelombang Frekuensi", Float) = 4
        _GelombangKecepatan ("Gelombang Kecepatan", Float) = 3
    }

    SubShader
    {
        Tags
        {
            "RenderPipeline" = "UniversalPipeline"
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            Name "Unlit2D"
            Tags { "LightMode" = "Universal2D" }

            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float4 _Tint;
                float4 _FlashColor;
                float _FlashAmount;
                float _GelombangAmplitudo;
                float _GelombangFrekuensi;
                float _GelombangKecepatan;
            CBUFFER_END

            struct Atribut
            {
                float4 posisiObjek : POSITION;
                float2 uv : TEXCOORD0;
                float4 warnaVertex : COLOR;
            };

            struct KeFragment
            {
                float4 posisiClip : SV_POSITION;
                float2 uv : TEXCOORD0;
                half4 warnaVertex : COLOR;
            };

            KeFragment Vert(Atribut masuk)
            {
                float3 pos = masuk.posisiObjek.xyz;
                pos.y += sin(pos.x * _GelombangFrekuensi + _Time.y * _GelombangKecepatan) * _GelombangAmplitudo;

                KeFragment keluar;
                keluar.posisiClip = TransformObjectToHClip(pos);
                keluar.uv = TRANSFORM_TEX(masuk.uv, _MainTex);
                keluar.warnaVertex = masuk.warnaVertex;
                return keluar;
            }

            half4 Frag(KeFragment masuk) : SV_Target
            {
                half4 teks = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, masuk.uv);
                half4 kolom = teks * (half4)_Tint * masuk.warnaVertex;
                kolom.rgb = lerp(kolom.rgb, _FlashColor.rgb, (half)_FlashAmount);
                return kolom;
            }
            ENDHLSL
        }
    }

}
