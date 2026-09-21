using System.Collections.Generic;
using fluXis.Map.Structures;

namespace fluXis.Modes.Gameplay.Lines;

public static class TimingLineGenerator
{
    public static IEnumerable<TimingLine> Generate(TimingPoint[] points, double end)
    {
        for (int i = 0; i < points.Length; i++)
        {
            var point = points[i];

            if (point.HideLines || point.Signature == 0)
                continue;

            var target = i + 1 < points.Length ? points[i + 1].Time : end;
            var increase = point.Signature * point.MsPerBeat;
            var position = point.Time;

            if (increase < .1f)
                continue;

            while (position < target)
            {
                yield return new TimingLine { Time = position };

                position += increase;
            }
        }
    }
}
