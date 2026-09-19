using System.IO;
using osu.Framework.Platform;

namespace fluXis.Utils.Extensions;

public static class StorageExtensions
{
    public static string ReadAllText(this Storage storage, string path)
    {
        using var reader = new StreamReader(storage.GetStream(path));
        return reader.ReadToEnd();
    }
}
