Shader "UI/ToolBar"
{
    Properties
    {
        // ToolBar POSITION
        _Position ("Position and Size", Vector) = (0, 0, 0, 0)
        // item textures
        _MainTex1 ("Texture 1", 2D) = "white" {}
        _MainTex2 ("Texture 2", 2D) = "white" {}
        _MainTex3 ("Texture 3", 2D) = "white" {}
        _MainTex4 ("Texture 4", 2D) = "white" {}
        _MainTex5 ("Texture 5", 2D) = "white" {}
        _MainTex6 ("Texture 6", 2D) = "white" {}
        _MainTex7 ("Texture 7", 2D) = "white" {}
        _MainTex8 ("Texture 8", 2D) = "white" {}
        _MainTex9 ("Texture 9", 2D) = "white" {}
        _MainTex10 ("Texture 10", 2D) = "white" {}
        // border width(0-1)
        _BorderWidth ("Border Width", Range(0, 1)) = 0.1
        //border height(0-1)
        _BorderHeight ("Border Height", Range(0, 1)) = 0.1
        // border color
        _BorderColor ("Border Color", Color) = (0, 0, 0, 1)
        // second border color
        _SecondBorderColor ("Second Border Color", Color) = (0, 0, 0, 1)
        // selected item Color
        _SelectedItemColor ("Selected Item Color", Color) = (1, 1, 1, 1)
        // barrier Width
        _BarrierWidth ("Barrier Width", Range(0, 1)) = 0.05
        // number of items 1-10
        _NumberOfItems ("Number of Items", Int) = 1
        //selected item
        _SelectedItem ("Selected Item", Int) = 0
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
                float4 vertex : SV_POSITION;
                float screenWidth : TEXCOORD2;
                float screenHeight : TEXCOORD3;
            };

            fixed4 _Position;
            sampler2D _MainTex1;
            sampler2D _MainTex2;
            sampler2D _MainTex3;
            sampler2D _MainTex4;
            sampler2D _MainTex5;
            sampler2D _MainTex6;
            sampler2D _MainTex7;
            sampler2D _MainTex8;
            sampler2D _MainTex9;
            sampler2D _MainTex10;
            float _BorderWidth;
            float _BorderHeight;
            fixed4 _BorderColor;
            fixed4 _SecondBorderColor;
            fixed4 _SelectedItemColor;
            float _BarrierWidth;
			int _NumberOfItems;
int _SelectedItem;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                // set the vertex position to cover the whole screen
                o.vertex = UnityObjectToClipPos(v.vertex);
				// set the uv as the interporlation between vertices
                o.uv = v.uv;
                o.screenWidth = _ScreenParams.x;
                o.screenHeight = _ScreenParams.y;
                return o;
            }
            float rand_1_05(in float2 uv)
            {
                float2 noise = (frac(sin(dot(uv ,float2(12.9898,78.233)*2.0)) * 43758.5453));
                return abs(noise.x + noise.y) * 0.5;
            }
            float4 GetBorderColour(float2 uv)
            {
                if(rand_1_05(uv) > 0.1)
                    return _BorderColor;
                else
                    return _SecondBorderColor;
            }
            float4 GetSelectedColour(float2 uv)
            {
				if(rand_1_05(uv) > 0.1)
return _SelectedItemColor;
				else
					return _SecondBorderColor;
			}

            fixed4 frag (v2f i) : SV_Target
            {
                int screenWidth = i.screenWidth;
                int screenHeight = i.screenHeight;
                // sample the texture
                fixed4 col = tex2D(_MainTex1, i.uv);
                //if we are on the border, apply the border color
                if (i.uv.x < _BorderWidth || i.uv.x > 1 - _BorderWidth || i.uv.y < _BorderHeight || i.uv.y > 1 - _BorderHeight)
				{
                    //if we are on the corner, clip
                    if (i.uv.x < _BorderWidth && i.uv.y < _BorderHeight)
                    {
                        discard;
					}
                    if (i.uv.x < _BorderWidth && i.uv.y > 1 - _BorderHeight)
					                    {
						                    discard;
                    }
                    if (i.uv.x > 1 - _BorderWidth && i.uv.y < _BorderHeight)
					                    {
                    discard;
                    }
                    if (i.uv.x > 1 - _BorderWidth && i.uv.y > 1 - _BorderHeight)
					                    {
                    discard;
                    }
                     //calculate the width of each item
                    float itemWidth = (1.0 - 2 * _BorderWidth) / _NumberOfItems;
                    //calculate the item we are inside
                    int item = (int)((i.uv.x - _BorderWidth) / itemWidth);
                    
                    //work out the internal location within the item
                    float itemX = (i.uv.x - _BorderWidth - item * itemWidth);
                    if(item == _SelectedItem)
                        {
                            col = GetSelectedColour(i.uv);
                        }
                        else if(item == _SelectedItem+1)
						{
                            if(itemX < _BarrierWidth)
                            {
							    col = GetSelectedColour(i.uv);
                            }
                            else
                            {
                                col = GetBorderColour(i.uv);
                            }
						}
                        else
						{
							col = GetBorderColour(i.uv);
						}
				}
                else
                {
                    //calculate the width of each item
                    float itemWidth = (1.0 - 2 * _BorderWidth) / _NumberOfItems;
                    //calculate the item we are inside
                    int item = (int)((i.uv.x - _BorderWidth) / itemWidth);
                    //work out the internal location within the item
                    float itemX = (i.uv.x - _BorderWidth - item * itemWidth);
					//if we are on the barrier, apply the barrier color
                    if (itemX < _BarrierWidth)
					{
                        if(item == _SelectedItem || item == _SelectedItem+1)
                        {
                            col = GetSelectedColour(i.uv);
                        }
                        else
						{
							col = GetBorderColour(i.uv);
						}
					}
                    else
                    {
                        //get the UV coords to sample the item texture within
                        //the y uv is the same but subtracted by the border height on each side
                        float2 tileUV;
                        tileUV.y = i.uv.y - _BorderHeight;
                        tileUV.y /= (1-(_BorderHeight*2));
                        //flip vertical y
                        tileUV.y = 1 - tileUV.y;
                        tileUV.x = itemX - _BarrierWidth;
                        tileUV.x /= (1 - _BarrierWidth);
                        tileUV.x /= (itemWidth - _BarrierWidth);
                        //flip horizontal x
                        tileUV.x = 1 - tileUV.x;
                        //sample the item texture
						switch (item)
						{
							case 0:
								col = tex2D(_MainTex1, tileUV);
								break;
							case 1:
								col = tex2D(_MainTex2, tileUV);
								break;
							case 2:
								col = tex2D(_MainTex3, tileUV);
								break;
							case 3:
								col = tex2D(_MainTex4, tileUV);
								break;
							case 4:
								col = tex2D(_MainTex5, tileUV);
								break;
							case 5:
								col = tex2D(_MainTex6, tileUV);
								break;
							case 6:
								col = tex2D(_MainTex7, tileUV);
								break;
							case 7:
								col = tex2D(_MainTex8, tileUV);
								break;
							case 8:
								col = tex2D(_MainTex9, tileUV);
								break;
							case 9:
								col = tex2D(_MainTex10, tileUV);
								break;
                        }

                    }
                    //debug output the item
                    //col = float4(item/10.0, 0, 0, 1);
                }
                return col;
            }
            ENDCG
        }
    }
}
