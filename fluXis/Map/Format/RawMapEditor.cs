using YamlDotNet.Serialization;

namespace fluXis.Map.Format;

public class RawMapEditor
{
    [YamlMember(Alias = "flux:time_spent")]
    public long TimeSpent { get; set; }
}
