namespace MinecraftDatapackReloadHelper
{
    internal partial class Settings
    {
        public static string Rconip
        {
            set => Default.RconIp = value;
            get => Default.RconIp;
        }

        public static ushort Rconport
        {
            set => Default.RconPort = value;
            get => Default.RconPort;
        }

        public static string Rconpassword
        {
            set => Default.RconPassword = value;
            get => Default.RconPassword;
        }

        public static string Sourcepath
        {
            set => Default.SourcePath = value;
            get => Default.SourcePath;
        }

        public static string Copypath
        {
            set => Default.CopyPath = value;
            get => Default.CopyPath;
        }

        public static string Extractoutput
        {
            set => Default.ExtractOutput = value;
            get => Default.ExtractOutput;
        }
    }
}