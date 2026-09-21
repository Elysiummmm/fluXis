using System;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osuTK.Graphics;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace fluXis.Map.Format;

#nullable enable

public class YamlColorConverter : IYamlTypeConverter
{
    public bool Accepts(Type type) => type == typeof(Color4);

    public object? ReadYaml(IParser parser, Type type, ObjectDeserializer rootDeserializer)
    {
        var scalar = parser.Consume<Scalar>();

        if (string.IsNullOrWhiteSpace(scalar.Value))
            return null;

        if (!Colour4.TryParseHex(scalar.Value, out var c))
            return null;

        Color4 col = c;
        return col;
    }

    public void WriteYaml(IEmitter emitter, object? value, Type type, ObjectSerializer serializer)
    {
        if (value is Color4 location)
            emitter.Emit(new Scalar(location.ToHex()));
        else
            emitter.Emit(new Scalar(string.Empty));
    }
}
