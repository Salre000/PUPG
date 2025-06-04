Shader "alpha" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
    }

    SubShader {
        Tags {"Queue" = "Geometry-1"}

        Pass{
            //Zwrite Off

            ColorMask 0
        }
    }
}