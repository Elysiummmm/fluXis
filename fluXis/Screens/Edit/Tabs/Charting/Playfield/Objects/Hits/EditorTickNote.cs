using System.Collections.Generic;
using fluXis.Modes.Keys.Map.Objects;
using fluXis.Skinning.Default.HitObject;
using osu.Framework.Graphics;

namespace fluXis.Screens.Edit.Tabs.Charting.Playfield.Objects.Hits;

public partial class EditorTickNote : EditorDrawableHitObject<Tick>
{
    private Drawable tickNotePiece;

    public EditorTickNote(Tick hit)
        : base(hit)
    {
    }

    protected override IEnumerable<Drawable> CreateContent()
        => [tickNotePiece = new DefaultTickNote(false).With(d => d.RelativeSizeAxes = Axes.X)];

    protected override void Update()
    {
        base.Update();
        tickNotePiece.Width = Data.Small ? 0.8f : 1f;
    }
}
