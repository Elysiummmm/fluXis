using System;
using System.Collections.Generic;
using System.Linq;
using fluXis.Audio.Transforms;
using fluXis.Map;
using fluXis.Map.Structures.Bases;
using fluXis.Modes;
using fluXis.Modes.Gameplay;
using fluXis.Mods;
using fluXis.Online.API.Models.Users;
using fluXis.Scoring;
using fluXis.Scoring.Processing.Health;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace fluXis.Screens.Gameplay.Ruleset;

public partial class RulesetContainer : CompositeDrawable
{
    public PlayableMap Map { get; }
    public List<IMod> Mods { get; }
    public APIUser CurrentPlayer { get; init; }

    public float Rate { get; }
    public Bindable<float> ScrollSpeed { get; set; } = new(3);

    public GameMode Mode { get; }
    public PlayableGameMode PlayableMode { get; }

    public virtual bool AsyncScoreCalculations => false;
    public HitWindows HitWindows { get; private set; }
    public ReleaseWindows ReleaseWindows { get; private set; }
    public LandmineWindows LandmineWindows { get; private set; }

    private readonly Dictionary<string, ScrollGroup> scrolls = new();
    public IReadOnlyDictionary<string, ScrollGroup> ScrollGroups => scrolls;

    public event Action OnDeath;

    public bool AllowReverting { get; set; }
    public bool AlwaysShowKeys { get; set; }
    public BindableBool IsPaused { get; } = new();

    public bool CatchingUp { get; protected set; }
    public TransformableClock ParentClock { get; set; }
    public Drawable ShakeTarget { get; set; }
    public DebugText DebugText { get; }

    private DependencyContainer dependencies;

    protected override bool ForceChildUpdate => true;

    public RulesetContainer(GameMode mode, PlayableMap map, List<IMod> mods)
    {
        Map = map;
        Mods = mods;

        Mode = mode;
        PlayableMode = mode.CreatePlayable(this, map, [.. mods]);

        Rate = Mods.OfType<RateMod>().FirstOrDefault()?.Rate ?? 1;

        DebugText = new DebugText();

        ShakeTarget ??= this;
    }

    [BackgroundDependencyLoader]
    private void load()
    {
        RelativeSizeAxes = Axes.Both;

        dependencies.CacheAs(this);

        createHitWindows();
        createScrollGroups();

        InternalChildrenEnumerable = new Drawable[]
        {
            PlayableMode,
            DebugText
        }.Concat(scrolls.Values.ToArray());
    }

    public HealthProcessor CreateHealthProcessor() => Mode.CreateHealthProcessor(Map, [.. Mods], Clock, PlayableMode.InBreak, OnDeath);

    private void createHitWindows()
    {
        var difficulty = Math.Clamp(Map.AccuracyDifficulty == 0 ? 8 : Map.AccuracyDifficulty, 1, 10);
        difficulty *= Mods.Any(m => m is HardMod) ? 1.5f : 1;

        HitWindows = new HitWindows(difficulty, Rate);
        ReleaseWindows = new ReleaseWindows(difficulty, Rate);
        LandmineWindows = new LandmineWindows(difficulty, Rate);
    }

    private void createScrollGroups()
    {
        // creating groups
        for (int i = 0; i < PlayableMode.DefaultGroupCount; i++)
            scrolls[$"${i + 1}"] = new ScrollGroup { Name = $"${i + 1}" };

        var events = Map.ObjectsOfType<IHasGroups>().ToList();
        var groups = events.SelectMany(x => x.Groups).Distinct().Order().ToList();

        foreach (var group in groups)
        {
            if (group.StartsWith('$'))
                continue;

            if (!scrolls.ContainsKey(group))
                scrolls[group] = new ScrollGroup { Name = group };
        }

        scrolls.ForEach(x => LoadComponent(x.Value));

        // populating groups
        foreach (var ev in events)
        {
            if (ev.Groups.Count == 0)
            {
                foreach (var (_, group) in scrolls.Where(x => x.Key.StartsWith('$')))
                    ev.Apply(group);
            }
            else
            {
                foreach (var group in ev.Groups)
                {
                    if (scrolls.TryGetValue(group, out var scroll))
                        ev.Apply(scroll);
                }
            }
        }

        scrolls.ForEach(x => x.Value.InitMarkers());
    }

    protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) => dependencies = new DependencyContainer(base.CreateChildDependencies(parent));
}
