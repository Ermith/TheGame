sampler2D input : register(s0);
float strength;

float4 PixelShaderFunction(float2 uv: TEXCOORD) : COLOR0
{
    float4 color = tex2D(input, uv);
    float2 mid = float2(0.5, 0.5);
    float d = 1 - distance(mid, uv) / strength;
    d = max(0, d);
    return color * d;
}

technique Technique1 {
    pass Pass1 {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}