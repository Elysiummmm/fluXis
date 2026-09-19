namespace fluXis.Map.Format;

#nullable enable

public interface IMapFormat
{
    PlayableMap? Parse(string path);
    void Save(PlayableMap map);
}
