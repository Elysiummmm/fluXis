using fluXis.Database.Maps;
using fluXis.Map;
using fluXis.Modes;
using osu.Framework.Graphics;

namespace fluXis.Screens.Edit.Tabs.Verify;

public interface IVerifyContext
{
    GameModeManager Modes { get; }
    PlayableMap Map { get; }
    RealmMap RealmMap { get; }

    int MaxKeyCount => RealmMap.KeyCount * (Map.IsDualSplit ? 2 : 1);
    RealmMapSet MapSet => RealmMap.MapSet;

    void LoadComponent(Drawable drawable);
}
