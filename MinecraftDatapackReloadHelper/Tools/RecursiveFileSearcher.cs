using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftDatapackReloadHelper.Tools
{
    internal class RecursiveFileSearcher
    {

        private bool found = false;
        internal bool RecursiveFileExists(string begin, string name)
        {
            found = false;
            Search(begin, name);
            return found;
        }

        private void Search(string begin, string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(begin);
            ArgumentException.ThrowIfNullOrEmpty(name);

            if (File.Exists(Path.Combine(begin, name)))
            {
                found = true;
                return;
            }

            DirectoryInfo directoryInfo = Directory.GetParent(begin);
            try
            {
                begin = directoryInfo.FullName;
            }
            catch (NullReferenceException)
            {
                found = false;
                return;
            }
            Search(begin, name);
        }
    }
}
