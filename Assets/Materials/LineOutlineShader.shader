Shader"Unlit/Custom/OutlineTexture"
{
    Properties
    {
        _MainColor ("MainColor", Color) = (1,1,1,1)
        _LineColor ("LineColor", Color) = (1,1,1,1)
        _Scale ("Scale", float) = 1
        _Thickness ("Thickness", float) = 1
        _OutlineThickness ("OutlineThickness", float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

#include "UnityCG.cginc"

struct appdata
{
    float4 vertex : POSITION;
    float2 uv : TEXCOORD0;
};

struct v2f
{
    float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
    float4 vertex : SV_POSITION;
};

float _Scale;
float _Thickness;
float4 _MainColor;
float4 _LineColor;
float _OutlineThickness;
            
v2f vert(appdata v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.uv = v.uv;
    UNITY_TRANSFER_FOG(o, o.vertex);
    return o;
}

fixed4 frag(v2f i) : SV_Target
{

    float3 worldScale = float3(
                            length(float3(unity_ObjectToWorld[0].x, unity_ObjectToWorld[1].x, unity_ObjectToWorld[2].x)), // scale x axis
                            length(float3(unity_ObjectToWorld[0].y, unity_ObjectToWorld[1].y, unity_ObjectToWorld[2].y)), // scale y axis
                            length(float3(unity_ObjectToWorld[0].z, unity_ObjectToWorld[1].z, unity_ObjectToWorld[2].z)) // scale z axis
                            );
    
    float t = pow(sin((i.uv.y * 500 * worldScale.y + i.uv.x * 500 * worldScale.x) / _Scale) - _Thickness, 512);
    
    if (i.uv.x < _OutlineThickness / worldScale.x || i.uv.x > 1 - _OutlineThickness / worldScale.x ||
        i.uv.y < _OutlineThickness / worldScale.y || i.uv.y > 1 - _OutlineThickness / worldScale.y)
        t = 0;
    
    t = clamp(t, 0, 1);
                // sample the texture
    float4 col = lerp(_LineColor, _MainColor, t);
    
    return col;
}
            ENDCG
        }
    }
}
