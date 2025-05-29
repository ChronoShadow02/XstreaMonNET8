namespace XstreaMonNET8
{
    public class Class_Driveinfo
    {
        private long FolderSize;

        internal string Letter { get; set; }
        internal double Total_Size { get; set; }
        internal double Freespace { get; set; }
        internal double UsedSpace { get; set; }
        internal double Record_Space { get; set; }

        internal Class_Driveinfo(string DriveLetter)
        {
            FolderSize = 0L;
            Letter = DriveLetter;
            Refresh();
        }

        internal void Refresh()
        {
            try
            {
                if (Letter.StartsWith("\\\\"))
                {
                    Total_Size = 0.0;
                    UsedSpace = 0.0;
                    Freespace = 0.0;
                    Record_Space = 0.0;
                }
                else
                {
                    foreach (DriveInfo drive in DriveInfo.GetDrives())
                    {
                        if (Letter.StartsWith(drive.Name, StringComparison.OrdinalIgnoreCase))
                        {
                            Letter = drive.Name;
                            Total_Size = drive.TotalSize;
                            UsedSpace = drive.TotalSize - drive.AvailableFreeSpace;
                            Freespace = drive.AvailableFreeSpace;
                            Record_Space = GetFolderSize(Modul_Ordner.Ordner_Pfad());
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Driveinfo.Refresh");
            }
        }

        private double GetFolderSize(string root)
        {
            FolderSize = 0L;
            SeekFiles(root);
            return FolderSize;
        }

        private void SeekFiles(string root)
        {
            try
            {
                string[] files = Directory.GetFiles(root);
                foreach (string file in files)
                {
                    try
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        FolderSize += fileInfo.Length;
                    }
                    catch { /* Ignorar archivos inaccesibles */ }
                }

                string[] directories = Directory.GetDirectories(root);
                foreach (string directory in directories)
                {
                    SeekFiles(directory);
                }
            }
            catch { /* Ignorar carpetas inaccesibles */ }
        }
    }
}
