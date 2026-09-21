using System;
using fluXis.Map;
using fluXis.Modes.Gameplay;
using fluXis.Modes.Keys.Gameplay;
using fluXis.Modes.Keys.Map.Objects;
using fluXis.Mods;
using fluXis.Screens.Gameplay.Ruleset;
using rhym;

namespace fluXis.Modes.Keys;

public class KeysGameMode : GameMode
{
    public override ResourceLocation Location => new("flux", "keys");

    public override void RegisterObjects(RhymIO io)
    {
        io.RegisterObject<Note>("flux:keys/note");
        io.RegisterObject<LongNote>("flux:keys/long");
        io.RegisterObject<Tick>("flux:keys/tick");
        io.RegisterObject<Landmine>("flux:keys/mine");
    }

    public override PlayableGameMode CreatePlayable(RulesetContainer ruleset, PlayableMap map, IMod[] mods) => new KeysPlayableGameMode(ruleset, map, mods);

    public static int ParseMode(ResourceLocation loc)
    {
        var count = 4;
        var split = loc.Path.Split('/', StringSplitOptions.RemoveEmptyEntries);

        for (var i = 0; i < split.Length; i++)
        {
            switch (i)
            {
                case 1:
                    count = int.Parse(split[i]);
                    break;
            }
        }

        return count;
    }
}
