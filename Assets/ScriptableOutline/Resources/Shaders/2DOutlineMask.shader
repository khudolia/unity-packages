Shader "Unlit/2DOutlineMask"
{
    Properties
    {
        [Enum(UnityEngine.Rendering.CompareFunction)] _ZTest("ZTest", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent"
            "Queue" = "Transparent+1"
        }
        LOD 100

        Pass
        {
            Name "Mask"
            Cull Off
            ZTest [_ZTest]
            ZWrite Off
            ColorMask 0

            Stencil
            {
                Ref 1
                Pass Replace
            }

        }
    }
}