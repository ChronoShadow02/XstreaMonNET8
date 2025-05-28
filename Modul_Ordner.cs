using System.Text;

namespace XstreaMonNET8
{
    internal static class Modul_Ordner
    {
        private static string Pri_Ordner_Pfad = "";
        private static string Pri_Favoriten_Path = "";
        private static string Pri_Database_Path = "";

        internal static string Ordner_Pfad()
        {
            try
            {
                if (Pri_Ordner_Pfad.Length == 0)
                {
                    string inidef = IniFile.Read(Parameter.INI_Common, "Directory", "Records").Trim();
                    if (inidef.EndsWith("\\"))
                    {
                        inidef = inidef.Substring(0, inidef.Length - 1);
                        IniFile.Write(Parameter.INI_Common, "Directory", "Records", inidef);
                    }

                    Pri_Ordner_Pfad = inidef;

                    if (Pri_Ordner_Pfad.Length == 0)
                    {
                        using FolderBrowserDialog dialog = new FolderBrowserDialog
                        {
                            Description = TXT.TXT_Description("Geben Sie das Datenverzeichnis für die Aufnahmen an"),
                            ShowNewFolderButton = true
                        };

                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            Pri_Ordner_Pfad = dialog.SelectedPath.Trim();
                            IniFile.Write(Parameter.INI_Common, "Directory", "Records", Pri_Ordner_Pfad);
                        }
                        else
                        {
                            return "";
                        }
                    }

                    if (!Directory.Exists(Pri_Ordner_Pfad))
                    {
                        DialogResult result = MessageBox.Show(
                            TXT.TXT_Description("Der Datenpfad wurde nicht gefunden. Möchten Sie die Einstellungen überprüfen?"),
                            TXT.TXT_Description("Datenpfad fehlt"),
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Hand
                        );

                        if (result == DialogResult.Yes)
                        {
                            if (new Dialog_Einstellungen().ShowDialog() == DialogResult.OK)
                            {
                                string newPath = IniFile.Read(Parameter.INI_Common, "Directory", "Records");
                                if (!Directory.Exists(newPath))
                                {
                                    Directory.CreateDirectory(newPath);
                                }
                                Pri_Ordner_Pfad = newPath.Trim();
                                return Ordner_Pfad(); // Recursivo
                            }
                            else
                            {
                                return "";
                            }
                        }
                    }

                    Favoriten_Pfad();
                    Database_Pfad();

                    string tempPath = Path.Combine(Parameter.CommonPath, "Temp");
                    if (!Directory.Exists(tempPath))
                        Directory.CreateDirectory(tempPath);
                }

                return Pri_Ordner_Pfad.Trim();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Parameter.Ordner_Pfad = " + Pri_Ordner_Pfad);
                MessageBox.Show(ex.Message);
                Application.Exit();
                return "";
            }
        }

        internal static string Favoriten_Pfad()
        {
            try
            {
                if (Pri_Favoriten_Path.Length == 0)
                {
                    string inidef = IniFile.Read(Parameter.INI_Common, "Directory", "Favorites").Trim();
                    if (string.IsNullOrWhiteSpace(inidef))
                    {
                        inidef = Path.Combine(Pri_Ordner_Pfad, "Favorites");
                        IniFile.Write(Parameter.INI_Common, "Directory", "Favorites", inidef);
                    }

                    if (inidef.EndsWith("\\"))
                    {
                        inidef = inidef.Substring(0, inidef.Length - 1);
                        IniFile.Write(Parameter.INI_Common, "Directory", "Favorites", inidef);
                    }

                    Pri_Favoriten_Path = inidef;

                    if (!Directory.Exists(Pri_Favoriten_Path))
                    {
                        Directory.CreateDirectory(Pri_Favoriten_Path);
                    }
                }

                return Pri_Favoriten_Path.Trim();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Parameter.Favoriten_Pfad = " + Pri_Ordner_Pfad);
                MessageBox.Show(ex.Message);
                Application.Exit();
                return "";
            }
        }

        internal static string Database_Pfad()
        {
            try
            {
                if (Pri_Database_Path.Length == 0)
                {
                    string inidef = IniFile.Read(Parameter.INI_Common, "Directory", "Database").Trim();
                    if (string.IsNullOrWhiteSpace(inidef))
                    {
                        inidef = Pri_Ordner_Pfad;
                        IniFile.Write(Parameter.INI_Common, "Directory", "Database", inidef);
                    }

                    if (inidef.EndsWith("\\"))
                    {
                        inidef = inidef.Substring(0, inidef.Length - 1);
                        IniFile.Write(Parameter.INI_Common, "Directory", "Database", inidef);
                    }

                    Pri_Database_Path = inidef;

                    if (!Directory.Exists(Pri_Database_Path))
                    {
                        Directory.CreateDirectory(Pri_Database_Path);
                    }
                }

                return Pri_Database_Path.Trim();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Parameter.Database_Pfad = " + Pri_Database_Path);
                MessageBox.Show(ex.Message);
                return Pri_Database_Path;
            }
        }

        internal static bool DateiInBenutzung(string Dateipfad)
        {
            try
            {
                using FileStream stream = new FileStream(Dateipfad, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                return false;
            }
            catch (IOException)
            {
                return true;
            }
        }

        internal static string DateiPfadName(string Dateipfad)
        {
            string baseName = Path.Combine(Path.GetDirectoryName(Dateipfad) ?? "", Path.GetFileNameWithoutExtension(Dateipfad));
            string ext = Path.GetExtension(Dateipfad);
            int i = 1;
            string finalName = baseName + ext;

            while (File.Exists(finalName))
            {
                finalName = $"{baseName} ({i++}){ext}";
            }

            return finalName;
        }

        internal static string DateiName(string Datei_Pfad, string Datei_Name)
        {
            try
            {
                Datei_Name = CheckDateiname(Datei_Name);

                if (!Directory.Exists(Datei_Pfad))
                    Directory.CreateDirectory(Datei_Pfad);

                string nameWithoutExt = Path.GetFileNameWithoutExtension(Datei_Name);
                string ext = Path.GetExtension(Datei_Name);
                int i = 1;
                string newName = nameWithoutExt + ext;

                while (File.Exists(Path.Combine(Datei_Pfad, newName)))
                {
                    newName = $"{nameWithoutExt} ({i++}){ext}";
                }

                return newName;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Modul_Ordner.DateiName");
                return null;
            }
        }

        public static string CheckDateiname(string sDateiname)
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            var sb = new StringBuilder();

            foreach (char c in sDateiname)
            {
                if (Array.IndexOf(invalidChars, c) == -1)
                    sb.Append(c);
            }

            return sb.ToString();
        }
    }
}
