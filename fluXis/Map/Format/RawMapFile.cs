using fluXis.Map.Structures.Bases;
using rhym;
using rhym.Format;
using YamlDotNet.Serialization;

namespace fluXis.Map.Format;

public class RawMapFile : RhymFile<RawMapAssets, RawMapMetadata, ITimedObject>
{
    [YamlMember(Alias = "flux:mode")]
    public ResourceLocation GameMode { get; set; }

    [YamlMember(Alias = "flux:props")]
    public RawMapProperties Properties { get; set; }

    [YamlMember(Alias = "flux:editor")]
    public RawMapEditor Editor { get; set; }
}
