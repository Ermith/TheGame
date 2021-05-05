sampler TextureSampler : register(s0);
Texture2D light;
sampler2D lightSampler = sampler_state {
    Texture = <light>;
};

float4 PixelShaderFunction(float2 coords: TEXCOORD0) : COLOR0
{
    float4 color = tex2D(TextureSampler, coords);
    float4 color2 = tex2D(lightSampler, coords);
    return color * color2;
}

technique Technique1 {
    pass Pass1 {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}