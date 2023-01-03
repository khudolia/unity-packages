Shader "Custom/2DOutlineSurfaceShader"
{
    Properties
    {
        [Enum(UnityEngine.Rendering.CompareFunction)] _ZTest("ZTest", Float) = 0

        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        [HideInInspector] _2DOutlineWidth ("Outline Width", float) = 0
        [HideInInspector] _2DOutlineWidthMax ("Outline Width Max", float) = 0

    }
    SubShader
    {
        Tags
        {
            "Queue" = "Transparent+1"
            "RenderType" = "Transparent"
        }

        // render outline
        Pass
        {
            Cull Back
            ZWrite On
            ZTest [_ZTest]

            Stencil
            {
                Ref 1
                Comp NotEqual
            }

            CGPROGRAM
            //include useful shader functions
            #include "UnityCG.cginc"

            //define vertex and fragment shader
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            fixed4 _OutlineColor;
            uniform float _2DOutlineWidth;
            uniform float _2DOutlineWidthMax;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;

                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 pos : SV_POSITION;

                UNITY_VERTEX_OUTPUT_STEREO
            };

            v2f vert(appdata v)
            {
                v2f o;

                /// calculates and sets the built-in unity_StereoEyeIndex and unity_InstanceID shader variables
                /// to the correct values based on which eye the GPU is currently rendering.
                UNITY_SETUP_INSTANCE_ID(v);

                /// initializes all v2f values to 0.
                UNITY_INITIALIZE_OUTPUT(v2f, o);

                /// tells the GPU which eye in the texture array it should render to, based on the value of unity_StereoEyeIndex.
                /// This macro also transfers the value of unity_StereoEyeIndex from the vertex shader
                /// so that it will be accessible in the fragment shader only if UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
                /// is called in the fragment shader frag method.
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                /// making shader works even on the layers where unity's fog is enabled
                // UNITY_TRANSFER_FOG(o, o.vertex);

                const float3 world_scale = float3(
                    // scale x axis
                    length(float3(unity_ObjectToWorld[0].x, unity_ObjectToWorld[1].x, unity_ObjectToWorld[2].x)),
                    // scale y axis
                    length(float3(unity_ObjectToWorld[0].y, unity_ObjectToWorld[1].y, unity_ObjectToWorld[2].y)),
                    // scale z axis
                    length(float3(unity_ObjectToWorld[0].z, unity_ObjectToWorld[1].z, unity_ObjectToWorld[2].z))
                );

                /// dividing by object's scale to make outline width the same on every object
                v.vertex.xyz += v.normal * clamp(_2DOutlineWidth, 0, _2DOutlineWidthMax) * .005f / world_scale;

                o.pos = UnityObjectToClipPos(v.vertex);

                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                return _OutlineColor;
            }
            ENDCG
        }
    }
    FallBack "Standard"
}