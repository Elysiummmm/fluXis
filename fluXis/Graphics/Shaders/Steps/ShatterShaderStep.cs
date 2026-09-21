using System.Runtime.InteropServices;
using fluXis.Map.Structures.Events;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Shaders.Types;

namespace fluXis.Graphics.Shaders.Steps;

public class ShatterShaderStep : ShaderStep<ShatterShaderStep.ShatterParameters>
{
    protected override string FragmentShader => "Shatter";
    public override ShaderType Type => ShaderType.Shatter;

    public override bool ShouldRender => Strength2 > 0;

    public override void UpdateParameters(IFrameBuffer current)
    {
        ParameterBuffer.Data = ParameterBuffer.Data with
        {
            TexSize = current.Size,
            CellSize = Strength * 10f,
            DistortionStrength = Strength2 * 0.2f,
            PhaseSpeed = Strength3,
            Time = (float)Time.Current / 1000f
        };
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public record struct ShatterParameters
    {
        public UniformVector2 TexSize;
        public UniformFloat CellSize;
        public UniformFloat DistortionStrength;
        public UniformFloat PhaseSpeed;
        public UniformFloat Time;
        public UniformPadding8 pad;
    }
}
