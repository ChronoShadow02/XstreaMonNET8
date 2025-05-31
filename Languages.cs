#nullable disable
namespace XstreaMonNET8
{
    internal static class Languages
    {
        private static List<Class_Language> Language_List;

        internal static void Language_load()
        {
            Language_List = new List<Class_Language>()
            {
                new Class_Language("bg", "Български"),
                new Class_Language("cs", "Česky"),
                new Class_Language("da", "Dansk"),
                new Class_Language("de", "Deutsch"),
                new Class_Language("el", "Ελληνική"),
                new Class_Language("en", "English"),
                new Class_Language("es", "Español"),
                new Class_Language("et", "Eesti"),
                new Class_Language("fi", "Suomalainen"),
                new Class_Language("fr", "Français"),
                new Class_Language("hu", "угорська"),
                new Class_Language("id", "Indonesian"),
                new Class_Language("it", "Italiano"),
                new Class_Language("ja", "日本語"),
                new Class_Language("ko", "한국어"),
                new Class_Language("lt", "Lietuviškas"),
                new Class_Language("lv", "Latvieši"),
                new Class_Language("nb", "Norsk"),
                new Class_Language("nl", "Nederlands"),
                new Class_Language("pl", "Polski"),
                new Class_Language("pt", "Português"),
                new Class_Language("ro", "Românesc"),
                new Class_Language("ru", "Русский"),
                new Class_Language("sk", "Slovenská"),
                new Class_Language("sl", "Slovenski"),
                new Class_Language("sv", "Svenska"),
                new Class_Language("tr", "Türkçe"),
                new Class_Language("uk", "Українська"),
                new Class_Language("zh", "中文")
            };
        }

        internal static Class_Language Language_Find(string countryCode)
        {
            if (Language_List == null)
            {
                Language_load();
            }

            foreach (Class_Language language in Language_List)
            {
                if (string.Equals(language.Pro_Code, countryCode, StringComparison.OrdinalIgnoreCase))
                {
                    return language;
                }
            }

            return null;
        }

        internal static void Languages_Set()
        {
            try
            {
                // Asegurarse de que la lista de idiomas esté cargada
                Language_load();

                string iniPath = IniFile.Read(Parameter.INI_Common, "Language", "Files");
                bool fileExists = !string.IsNullOrEmpty(iniPath) && File.Exists(iniPath);
                DirectoryInfo langDir = new DirectoryInfo(Path.Combine(Application.StartupPath, "Language"));
                FileInfo[] availableFiles = langDir.Exists
                    ? langDir.GetFiles("*.ini", SearchOption.TopDirectoryOnly)
                    : Array.Empty<FileInfo>();

                if (!fileExists)
                {
                    if (availableFiles.Length > 1)
                    {
                        using (OpenFileDialog openFileDialog = new OpenFileDialog())
                        {
                            openFileDialog.Filter = "*.ini|*.ini";
                            openFileDialog.Title = "Select languages file";
                            openFileDialog.InitialDirectory = langDir.FullName;
                            if (openFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                iniPath = openFileDialog.FileName;
                                IniFile.Write(Parameter.INI_Common, "Language", "Files", iniPath);
                            }
                        }
                    }
                    else if (availableFiles.Length == 1)
                    {
                        // Si solo hay un archivo .ini, usarlo automáticamente
                        iniPath = availableFiles[0].FullName;
                        IniFile.Write(Parameter.INI_Common, "Language", "Files", iniPath);
                    }
                }

                // Instanciar TXT con la ruta de archivo de idioma
                if (!string.IsNullOrEmpty(iniPath))
                {
                    TXT txt = new TXT(iniPath);
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Languages.Languages_Set");
            }
        }
    }
}
