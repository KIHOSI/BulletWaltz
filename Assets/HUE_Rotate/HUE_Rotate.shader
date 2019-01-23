// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "PostEffect/Colors_HUE_Rotate" {
Properties 
{
_MainTex ("Base (RGB)", 2D) = "white" {}
_Rotation ("_Rotation", Range(0.0, 6.3)) = 1.0
_ScreenResolution ("_ScreenResolution", Vector) = (0.,0.,0.,0.)
}
SubShader 
{
Pass
{
ZTest Always
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#pragma target 3.0
#pragma glsl
#include "UnityCG.cginc"


uniform sampler2D _MainTex;
uniform float _Rotation;
uniform float _Distortion;
uniform float4 _ScreenResolution;

struct appdata_t
{
float4 vertex   : POSITION;
float4 color    : COLOR;
float2 texcoord : TEXCOORD0;
};

struct v2f
{
half2 texcoord  : TEXCOORD0;
float4 vertex   : SV_POSITION;
fixed4 color    : COLOR;
};   

v2f vert(appdata_t IN)
{
v2f OUT;
OUT.vertex = UnityObjectToClipPos(IN.vertex);
OUT.texcoord = IN.texcoord;
OUT.color = IN.color;

return OUT;
}


float4 frag (v2f i) : COLOR
{
float c = cos(_Rotation);
float s = sin(_Rotation);

float4x4 hueRotation =	
float4x4( 	 0.299,  0.587,  0.114, 0.0,
0.299,  0.587,  0.114, 0.0,
0.299,  0.587,  0.114, 0.0,
0.000,  0.000,  0.000, 1.0) +

float4x4(	 0.701, -0.587, -0.114, 0.0,
-0.299,  0.413, -0.114, 0.0,
-0.300, -0.588,  0.886, 0.0,
0.000,  0.000,  0.000, 0.0) * c +

float4x4(	 0.168,  0.330, -0.497, 0.0,
-0.328,  0.035,  0.292, 0.0,
1.250, -1.050, -0.203, 0.0,
0.000,  0.000,  0.000, 0.0) * s;


fixed4 pixel = tex2D(_MainTex, i.texcoord.xy);

fixed4 truepixel = 0.0;

for(int a = 0 ; a < 4 ; a++) {
for(int b = 0 ; b < 4 ; b++) {
truepixel[a] += pixel[b] * hueRotation[a][b];
}
}

return truepixel;
}

ENDCG
}

}
}