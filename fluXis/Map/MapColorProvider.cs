using System.Collections.Generic;
using fluXis.Graphics.UserInterface.Color;
using fluXis.Skinning.Default;
using osu.Framework.Graphics;
using osuTK.Graphics;

namespace fluXis.Map;

public class MapColorProvider : ICustomColorProvider
{
    public Colour4 Primary { get; }
    public Colour4 Secondary { get; }
    public Colour4 Middle { get; }

    public MapColorProvider(Dictionary<string, Color4> colors)
    {
        Primary = colors.TryGetValue("$primary", out var c1) ? c1 : Color4.Transparent;
        Secondary = colors.TryGetValue("$secondary", out var c2) ? c2 : Color4.Transparent;
        Middle = colors.TryGetValue("$middle", out var c3) ? c3 : Color4.Transparent;
    }

    public bool HasColorFor(int lane, int keyCount, out Colour4 colour)
    {
        var index = Theme.GetLaneColorIndex(lane, keyCount);
        colour = GetColor(index, Colour4.Transparent);
        return colour != Colour4.Transparent;
    }

    public Colour4 GetColor(int index, Colour4 fallback)
    {
        var colors = new[]
        {
            Colour4.Transparent,
            Primary,
            Secondary,
            Middle
        };

        if (index < 0 || index >= colors.Length)
            return fallback;

        var col = colors[index];

        if (col == Colour4.Transparent)
            return fallback;

        return col;
    }
}
