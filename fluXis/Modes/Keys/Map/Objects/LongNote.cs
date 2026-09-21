using fluXis.Map.Structures;
using fluXis.Map.Structures.Bases;

namespace fluXis.Modes.Keys.Map.Objects;

public class LongNote : HitObject, IHasDuration
{
    public double Duration { get; set; }
}
