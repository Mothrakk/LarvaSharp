using LarvaSharp.LarvaLibs.Configuration;

namespace LarvaSharp.LarvaLibs
{
    /// <summary>
    /// Class for interacting with the configuration files.
    /// </summary>
    internal static class Config
    {
        public static ConfigItem AutoStart { get; private set; } = new ConfigItem("autostart");
    }
}