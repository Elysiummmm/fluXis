using System.Collections.Generic;
using rhym.Format;
using YamlDotNet.Serialization;

namespace fluXis.Map.Format;

public class RawMapMetadata : RhymMetadata
{
    [YamlMember(Alias = "flux:title")]
    public string TitleRomanized { get; set; } = string.Empty;

    [YamlMember(Alias = "flux:artist")]
    public string ArtistRomanized { get; set; } = string.Empty;

    [YamlMember(Alias = "flux:difficulty")]
    public string Difficulty { get; set; } = string.Empty;

    [YamlMember(Alias = "flux:preview")]
    public int PreviewTime { get; set; }

    [YamlMember(Alias = "flux:colors")]
    public Dictionary<string, string> Colors { get; set; } = [];

    [YamlMember(Alias = "flux:sources")]
    public Dictionary<string, string> Sources { get; set; } = [];

    [YamlMember(Alias = "flux:tags")]
    public List<string> Tags { get; set; } = [];
}
