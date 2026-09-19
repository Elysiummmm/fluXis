using System;
using fluXis.Map.Structures.Bases;
using fluXis.Modes;
using fluXis.Utils;
using fluXis.Utils.Extensions;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Platform;
using rhym;

namespace fluXis.Map.Format;

#nullable enable

public class RhymMapFormat : IMapFormat
{
    private readonly Storage storage;
    private readonly GameModeManager modes;

    public RhymMapFormat(Storage storage, GameModeManager modes)
    {
        this.storage = storage;
        this.modes = modes;
    }

    public PlayableMap? Parse(string path)
    {
        if (storage.Exists(path))
            return null;

        var raw = storage.ReadAllText(path);
        var io = createIo();
        var parsed = io.ParseAs<RawMapFile, RawMapAssets, RawMapMetadata, ITimedObject>(raw);

        var playable = new PlayableMap
        {
            ChartHash = MapUtils.GetHash(raw)
        };

        playable.AddObjects(parsed.Objects);
        return playable;
    }

    public void Save(PlayableMap map) => throw new NotImplementedException();

    private RhymIO createIo()
    {
        var io = new RhymIO();
        modes.Loaded.ForEach(x => x.RegisterObjects(io));
        return io;
    }
}
