using fluXis.Map;
using fluXis.Modes.Gameplay;
using fluXis.Modes.Keys.Gameplay;
using fluXis.Mods;
using fluXis.Screens.Gameplay.Ruleset;
using rhym;

namespace fluXis.Modes.Keys;

public class KeysGameMode : GameMode
{
    public override ResourceLocation Location => new("flustix", "keys");

    public override void RegisterObjects(RhymIO io)
    {
    }

    public override PlayableGameMode CreatePlayable(RulesetContainer ruleset, MapInfo map, MapEvents events, IMod[] mods) => new KeysPlayableGameMode(ruleset, map, events, mods);
}
