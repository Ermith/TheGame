sampler2D input : register(s0);
float strength;
float xPos;
float yPos;

float4 PixelShaderFunction(float2 uv: TEXCOORD) : COLOR0
{
    float4 color = tex2D(input, uv);
    float2 mid = float2(xPos, yPos);
    float d = 1 - distance(mid, uv) * strength * 4;
    d = max(0, d);
    d = min(1, d);
    return color * d;
}

technique Technique1 {
    pass Pass1 {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}