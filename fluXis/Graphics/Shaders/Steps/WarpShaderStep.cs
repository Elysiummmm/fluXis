using System.Runtime.InteropServices;
using fluXis.Map.Structures.Events;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Shaders.Types;

namespace fluXis.Graphics.Shaders.Steps;

public class WarpShaderStep : ShaderStep<WarpShaderStep.WarpParameters>
{
    protected override string FragmentShader => "Warp";
    public override ShaderType Type => ShaderType.Warp;

    public override bool ShouldRender => Strength3 > 0;

    public override void UpdateParameters(IFrameBuffer current) => ParameterBuffer.Data = ParameterBuffer.Data with
    {
        TexSize = current.Size,
        PhaseSpeed = Strength,
        Scale = Strength2 + 4f,
        Iterations = (int)Strength3,
        Time = (float)Time.Current / 1000f
    };

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public record struct WarpParameters
    {
        public UniformVector2 TexSize;
        public UniformFloat PhaseSpeed;
        public UniformFloat Scale;
        public UniformInt Iterations;
        public UniformFloat Time;
        public UniformPadding8 pad;
    }
}
