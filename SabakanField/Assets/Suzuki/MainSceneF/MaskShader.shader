Shader "Custom/MaskShader"
{

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            Stencil{
                Ref 1
                Comp Always
                Pass Replace
                }
         
                ZWrite Off
                ZTest Always
                ColorMask 0
        }
    }
}
