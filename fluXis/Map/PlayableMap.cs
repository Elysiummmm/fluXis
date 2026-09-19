using System.Collections.Generic;
using fluXis.Map.Structures.Bases;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Extensions.ListExtensions;
using osu.Framework.Lists;

namespace fluXis.Map;

public class PlayableMap
{
    #region Hashes

    public string ChartHash { get; set; } = string.Empty;

    #endregion

    #region Objects

    public SlimReadOnlyListWrapper<ITimedObject> Objects => objects.AsSlimReadOnly();
    private readonly List<ITimedObject> objects = [];

    public void Add(ITimedObject obj) => objects.Add(obj);
    public void AddObjects(IEnumerable<ITimedObject> objs) => objs.ForEach(Add);

    #endregion
}
