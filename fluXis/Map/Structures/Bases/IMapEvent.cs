using System;
using System.Linq;

namespace fluXis.Map.Structures.Bases;

/// <summary>
/// Used to dynamically get event types
/// </summary>
public interface IMapEvent : ITimedObject
{
    static Type[] GetAllTypes() =>
    [
        .. typeof(IMapEvent).Assembly.GetTypes()
                            .Where(x => x.IsAssignableTo(typeof(IMapEvent)))
                            .Where(x => x.IsClass && !x.IsAbstract)
    ];
}
