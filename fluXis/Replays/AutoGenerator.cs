using System.Collections.Generic;
using System.Linq;
using fluXis.Input;
using fluXis.Map;
using fluXis.Map.Structures;
using fluXis.Map.Structures.Bases;
using fluXis.Modes.Keys.Map.Objects;
using fluXis.Online.API.Models.Users;
using fluXis.Screens.Gameplay.Input;

namespace fluXis.Replays;

// TODO: make per-mode auto generators
public class AutoGenerator
{
    /// <summary>
    /// How long the key is held down for
    /// </summary>
    private const float key_down_time = 50;

    private PlayableMap map { get; }
    private int mode { get; }
    private bool dual { get; }
    private bool split { get; }

    private List<FluXisGameplayKeybind> keys { get; }

    private List<ReplayFrame> frames { get; } = new();

    public AutoGenerator(PlayableMap map, int mode)
    {
        this.map = map;
        this.mode = mode;
        dual = map.IsDual;
        split = map.IsDualSplit;

        keys = GameplayInput.GetKeys(mode, dual).ToList();
    }

    public Replay Generate()
    {
        frames.Clear();
        generateFrames();

        var replay = new Replay
        {
            PlayerID = APIUser.AutoPlay.ID,
            Frames = frames
        };

        return replay;
    }

    private void generateFrames()
    {
        var objs = map.ObjectsOfType<HitObject>();

        if (objs.Length == 0)
            return;

        if (keys.Count <= 0)
            return;

        var actions = generateActions().GroupBy(a => a.Time).OrderBy(g => g.First().Time);
        var currentKeys = new List<FluXisGameplayKeybind>();

        foreach (var action in actions)
        {
            foreach (var point in action)
            {
                var key = keys[point.Lane - 1];

                switch (point)
                {
                    case PressAction:
                        currentKeys.Add(key);

                        if (dual && !split)
                            currentKeys.Add(keys[point.Lane - 1 + mode]);

                        break;

                    case ReleaseAction:
                        currentKeys.Remove(key);

                        if (dual && !split)
                            currentKeys.Remove(keys[point.Lane - 1 + mode]);

                        break;
                }
            }

            frames.Add(new ReplayFrame(action.First().Time, currentKeys.Cast<int>().ToArray()));
        }
    }

#nullable enable
    private IEnumerable<IAction> generateActions()
    {
        var columns = map.ObjectsOfType<HitObject>().GroupBy(x => x.Lane);

        foreach (var column in columns)
        {
            var objects = column.OrderBy(x => x.Time).ToList();
            var pressed = false;
            double blockedUntil = 0;

            for (int i = 0; i < objects.Count; i++)
            {
                var currentObject = objects[i];
                if (currentObject is Landmine) continue;

                if (currentObject.Time < blockedUntil)
                    continue;

                HitObject? nextObjectInColumn = objects.Count == i + 1 ? null : objects[i + 1];
                var releaseTime = calculateReleaseTime(currentObject, nextObjectInColumn);

                if (!pressed)
                {
                    pressed = true;
                    yield return new PressAction { Time = currentObject.Time, Lane = currentObject.Lane };
                }

                if (releaseTime is not null)
                {
                    pressed = false;
                    blockedUntil = releaseTime.Value;
                    yield return new ReleaseAction { Time = releaseTime.Value, Lane = currentObject.Lane };
                }
            }
        }
    }
#nullable disable

    private double? calculateReleaseTime(HitObject currentObject, HitObject nextObject)
    {
        var endTime = currentObject.GetEndTime();

        if (currentObject is LongNote)
            return endTime;

        if (nextObject is Tick)
        {
            var diff = nextObject.Time - currentObject.Time;

            if (diff < 200)
                return null;
        }

        var enoughTimeToRelease = nextObject == null || nextObject.Time > endTime + key_down_time;
        return endTime + (enoughTimeToRelease ? key_down_time : (nextObject.Time - endTime) * 0.9f);
    }

    private interface IAction
    {
        double Time { get; set; }
        int Lane { get; set; }
    }

    private class PressAction : IAction
    {
        public double Time { get; set; }
        public int Lane { get; set; }
    }

    private class ReleaseAction : IAction
    {
        public double Time { get; set; }
        public int Lane { get; set; }
    }
}
