using System.Collections.Generic;
using System.ComponentModel;
using rhym.Format;
using YamlDotNet.Serialization;

namespace fluXis.Map.Format;

public class RawMapMetadata : RhymMetadata
{
    [DefaultValue(""), YamlMember(Alias = "flux:title")]
    public string TitleRomanized { get; set; } = string.Empty;

    [DefaultValue(""), YamlMember(Alias = "flux:artist")]
    public string ArtistRomanized { get; set; } = string.Empty;

    [DefaultValue(""), YamlMember(Alias = "flux:difficulty")]
    public string Difficulty { get; set; } = string.Empty;

    [DefaultValue(0), YamlMember(Alias = "flux:preview")]
    public int PreviewTime { get; set; }

    [YamlMember(Alias = "flux:colors")]
    public Dictionary<string, string> Colors { get; set; } = [];

    [YamlMember(Alias = "flux:sources")]
    public Dictionary<string, string> Sources { get; set; } = [];

    [YamlMember(Alias = "flux:tags")]
    public string[] Tags { get; set; } = [];
}
