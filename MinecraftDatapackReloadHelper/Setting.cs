namespace MinecraftDatapackReloadHelper
{
    internal partial class Settings
    {
        public static string Rcon_IP
        {
            set { Default.RconIp = value; }
            get { return Default.RconIp.ToString(); }
        }

        public static ushort Rcon_Port
        {
            set { Default.RconPort = value; }
            get { return Default.RconPort; }
        }

        public static string Rcon_Password
        {
            set { Default.RconPassword = value; }
            get { return Default.RconPassword.ToString(); }
        }

        public static string Client_Source
        {
            set { Default.SourcePath = value; }
            get { return Default.SourcePath.ToString(); }
        }

        public static string Client_Copy
        {
            set { Default.CopyPath = value; }
            get { return Default.CopyPath.ToString(); }
        }

        public static string Client_UploadOutput
        {
            set { Default.UploadOutput = value; }
            get { return Default.UploadOutput.ToString(); }
        }
    }
}