using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using fluXis.Graphics.Sprites.Text;
using fluXis.Map.Structures.Bases;
using fluXis.Screens.Edit.Tabs.Charting.Playfield;
using Newtonsoft.Json;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using rhym;
using YamlDotNet.Serialization;

namespace fluXis.Map.Structures.Events.Groups;

[Description("Repeat specific events by group.")]
[ResourceLocation("flux:events/loop")]
public class LoopEvent : IMapEvent
{
    [JsonProperty("time")]
    public double Time { get; set; }

    [JsonProperty("lane")]
    public int Lane { get; set; }

    [JsonProperty("target")]
    public string TargetGroup { get; set; }

    [JsonProperty("distance")]
    public double Distance { get; set; }

    [JsonProperty("count")]
    public int Count { get; set; }

    [JsonIgnore, YamlIgnore]
    string ITimedObject.Group { get; set; }

    IEnumerable<Drawable> ITimedObject.CreateObjectOverlay(EditorDrawableObject obj)
    {
        var flow = (FillFlowContainer)ITimedObject.CreateDefaultOverlay(obj).First();
        obj.GroupText = null;
        flow.Clear();

        var text = new FluXisSpriteText
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Colour = obj.TextColor,
            WebFontSize = 12
        };

        obj.DataUpdate += () => text.Text = TargetGroup;
        flow.Add(text);
        yield return flow;
    }
}
