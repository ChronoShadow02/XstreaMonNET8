using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace XstreaMonNET8
{
    internal class IniFile
    {
        private readonly string strFilename;

        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileStringA", CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int GetPrivateProfileString(
            string lpApplicationName,
            string lpKeyName,
            string lpDefault,
            StringBuilder lpReturnedString,
            int nSize,
            string lpFileName);

        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileStringA", CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int WritePrivateProfileString(
            string lpApplicationName,
            string lpKeyName,
            string lpString,
            string lpFileName);

        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileIntA", CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int GetPrivateProfileInt(
            string lpApplicationName,
            string lpKeyName,
            int nDefault,
            string lpFileName);

        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileStringA", CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int FlushPrivateProfileString(
            int lpApplicationName,
            int lpKeyName,
            int lpString,
            string lpFileName);

        internal IniFile(string filename)
        {
            strFilename = filename;
        }

        public string FileName => strFilename;

        internal string GetString(string section, string key, string defaultValue)
        {
            var result = new StringBuilder(256);
            int length = GetPrivateProfileString(section, key, defaultValue, result, result.Capacity, strFilename);
            return length > 0 ? result.ToString(0, length) : string.Empty;
        }

        internal int GetInteger(string section, string key, int defaultValue)
        {
            return GetPrivateProfileInt(section, key, defaultValue, strFilename);
        }

        internal bool GetBoolean(string section, string key, bool defaultValue)
        {
            int fallback = defaultValue ? 1 : 0;
            return GetPrivateProfileInt(section, key, fallback, strFilename) == 1;
        }

        internal void WriteString(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, strFilename);
            Flush();
        }

        internal void WriteInteger(string section, string key, int value)
        {
            WriteString(section, key, value.ToString(CultureInfo.InvariantCulture));
        }

        internal void WriteBoolean(string section, string key, bool value)
        {
            // -1 = true, 0 = false (comportamiento heredado de VB)
            WriteString(section, key, (value ? -1 : 0).ToString(CultureInfo.InvariantCulture));
        }

        private void Flush()
        {
            FlushPrivateProfileString(0, 0, 0, strFilename);
        }

        public static string Read(string iniPath, string iniSet, string iniVal, string iniDef = "")
        {
            return new IniFile(iniPath).GetString(iniSet, iniVal, iniDef);
        }

        public static void Write(string iniPath, string iniSet, string iniVal, string iniDef)
        {
            try
            {
                new IniFile(iniPath).WriteString(iniSet, iniVal, iniDef);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "INI Write Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
