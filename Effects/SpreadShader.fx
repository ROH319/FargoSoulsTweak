float3 edgeColor;
float3 centerColor;

float mainOpacity;
float centerOpacity;
float halfSpreadAngle;
sampler uImage0 : register(s0);

float4 main(float2 uv : TEXCOORD) : COLOR
{
    // 计算三角形左上角、左下角和右边中点在纹理坐标系中的位置
    float2 rightTop = float2(1, 0);
    float2 rightBottom = float2(1, 1);
    float2 leftMiddle = float2(0, 0.5);

    // 计算当前像素点在三角形内的权重
    float3 weights = float3(0, 0, 0);
    weights.x = saturate(1 - abs((uv.y - leftMiddle.y) / (rightTop.y - leftMiddle.y)));
    weights.y = saturate(1 - abs((uv.y - leftMiddle.y) / (rightBottom.y - leftMiddle.y)));
    weights.z = saturate(1 - abs((uv.x - leftMiddle.x) / (rightTop.x - leftMiddle.x)));

    // 根据权重插值计算当前像素点的颜色
    float4 color = tex2D(uImage0, uv);
    float4 gradientColor = color * weights.x + color * weights.y + color * weights.z;

    return gradientColor;
}

technique tech
{
    pass P0
    {
        PixelShader = compile ps_3_0 main();

    }
}
