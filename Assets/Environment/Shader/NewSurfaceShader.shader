Shader "Custom/ColorAndTransparentTexture"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Main Texture", 2D) = "white" {}
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        
        CGPROGRAM
        #pragma surface surf Lambert alpha
        
        struct Input
        {
            float2 uv_MainTex;
        };
        
        sampler2D _MainTex;
        fixed4 _Color;
        
        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 texColor = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = _Color.rgb * texColor.rgb;
            o.Alpha = texColor.a;
        }
        ENDCG
    }
    
    FallBack "Diffuse"
}
