using fluXis.Map.Structures;

namespace fluXis.Modes.Keys.Map.Objects;

public class Landmine : HitObject
{
    public override float DensityContribution => 0;

    public bool Hidden { get; set; }
}
