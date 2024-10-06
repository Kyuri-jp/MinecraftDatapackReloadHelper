using System.Text;

namespace MinecraftDatapackReloadHelper.Libs.Files
{
    internal class Encode
    {
        //ref https://gorihei.com/programming/1474/

        /// <summary>
        /// 指定したファイルのエンコーディングを判別して取得します。
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        internal static Encoding GetEncoding(string filename)
        {
            // BOMを取得
            var bom = new byte[4];
            using var file = new FileStream(filename, FileMode.Open, FileAccess.Read);
            // ReSharper disable once MustUseReturnValue
            file.Read(bom, 0, 4);

            // BOMを解析
#pragma warning disable SYSLIB0001
            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return Encoding.UTF7;                             // UTF-7
#pragma warning restore SYSLIB0001
            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8;                             // UTF-8
            if (bom[0] == 0xff && bom[1] == 0xfe) return Encoding.Unicode;                                            // UTF-16LE
            if (bom[0] == 0xfe && bom[1] == 0xff) return Encoding.BigEndianUnicode;                                   // UTF-16BE
            if (bom[0] == 0xff && bom[1] == 0xfe && bom[2] == 0x00 && bom[3] == 0x00) return Encoding.Unicode;        // UTF-32LE
            if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return new UTF32Encoding(true, true); // UTF-32BE
            return Encoding.ASCII;
        }
    }
}