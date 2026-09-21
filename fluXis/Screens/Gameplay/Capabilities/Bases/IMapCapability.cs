using fluXis.Map;

namespace fluXis.Screens.Gameplay.Capabilities.Bases;

#nullable enable

public interface IMapCapability : IGameplayCapability
{
    PlayableMap? Load() => null;
    void Modify(PlayableMap map) { }
}
