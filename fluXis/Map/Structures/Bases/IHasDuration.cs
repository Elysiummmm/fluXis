using fluXis.Screens.Edit;
using fluXis.Screens.Edit.UI.Variable.Preset;
using fluXis.Utils.Attributes;
using fluXis.Utils.Inspect;

namespace fluXis.Map.Structures.Bases;

public interface IHasDuration : ITimedObject
{
    [CustomCreateMethod(typeof(IHasDuration), nameof(CreateVariableDuration))]
    double Duration { get; set; }

    static EditorVariableLength<IHasDuration> CreateVariableDuration(ObjectProperty _, IHasDuration obj, EditorInspectContext ctx)
        => new(ctx.Map, obj, ctx.Map.Playable.GetTimingPoint(obj.Time).MsPerBeat) { UpdateMap = ctx.Update };
}

public static class HasDurationExtensions
{
    public static double GetEndTime(this ITimedObject obj) => obj.Time + ((obj as IHasDuration)?.Duration ?? 0);
}
