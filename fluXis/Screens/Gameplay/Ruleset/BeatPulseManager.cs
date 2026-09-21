using System;
using fluXis.Map;
using fluXis.Map.Structures.Events;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace fluXis.Screens.Gameplay.Ruleset;

public partial class BeatPulseManager : CompositeDrawable
{
    public override bool RemoveCompletedTransforms => false;

    private PlayableMap map { get; }
    private Drawable target { get; }

    private float targetScale
    {
        get => target.Scale.X;
        set => target.Scale = new Vector2(value);
    }

    public BeatPulseManager(PlayableMap map, BeatPulseEvent[] events, Drawable target)
    {
        this.map = map;
        this.target = target;
        generate(events);
    }

    public void Rebuild(BeatPulseEvent[] ev)
    {
        ClearTransforms();
        generate(ev);
    }

    private void generate(BeatPulseEvent[] events)
    {
        for (int i = 0; i < events.Length; i++)
        {
            var ev = events[i];

            if (Math.Abs(ev.Strength - 1) < 0.0001f || ev.Interval < 0.01f)
                continue;

            var end = i + 1 < events.Length ? events[i + 1].Time : map.EndTime;

            var t = ev.Time;

            while (t < end)
            {
                var timing = map.GetTimingPoint(t);
                var ms = timing.MsPerBeat * Math.Clamp(ev.Interval, 0.01f, 4);

                var inDuration = ms * ev.ZoomIn;
                var outDuration = ms * (1 - ev.ZoomIn);

                using (BeginAbsoluteSequence(t))
                {
                    this.TransformTo(nameof(targetScale), ev.Strength, inDuration, Easing.OutQuint)
                        .Then().TransformTo(nameof(targetScale), 1f, outDuration, Easing.OutQuint);
                }

                t += ms;
            }
        }
    }
}
