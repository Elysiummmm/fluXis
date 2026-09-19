using System;
using fluXis.Utils;
using fluXis.Utils.Extensions;
using Midori.Utils;
using osu.Framework.Logging;
using osu.Framework.Platform;

namespace fluXis.Map.Format.Legacy;

#nullable enable

public class LegacyMapFormat : IMapFormat
{
    private readonly Storage storage;

    public LegacyMapFormat(Storage storage)
    {
        this.storage = storage;
    }

    public PlayableMap? Parse(string path)
    {
        try
        {
            if (storage.Exists(path))
                return null;

            var json = storage.ReadAllText(path);
            var map = json.Deserialize<MapInfo>();

            if (map is null)
                return null;

            var playable = new PlayableMap
            {
                ChartHash = MapUtils.GetHash(json)
            };

            playable.AddObjects(map.HitObjects);
            playable.AddObjects(map.TimingPoints);
            playable.AddObjects(map.ScrollVelocities);
            return playable;
        }
        catch (Exception e)
        {
            Logger.Error(e, "Failed to load map from path: " + storage.GetFullPath(path));
            return null;
        }
    }

    public void Save(PlayableMap map) => throw new NotImplementedException();
}
