using fluXis.Map.Structures;

namespace fluXis.Modes.Keys.Map.Objects;

public class Tick : HitObject
{
    public override float DensityContribution => 0.1f;

    public bool Small { get; set; }
}
