using fluXis.Map;
using fluXis.Scoring;
using fluXis.Scoring.Processing;
using fluXis.Scoring.Processing.Health;
using fluXis.Screens.Gameplay.Ruleset;

namespace fluXis.Screens.Gameplay.HUD;

public interface IHUDDependencyProvider
{
    RulesetContainer Ruleset { get; }
    PlayableMap Map { get; }

    JudgementProcessor JudgementProcessor { get; }
    HealthProcessor HealthProcessor { get; }
    ScoreProcessor ScoreProcessor { get; }
    HitWindows HitWindows { get; }

    float PlaybackRate { get; }
    double CurrentTime { get; }
}
