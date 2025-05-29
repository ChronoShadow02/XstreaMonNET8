namespace XstreaMonNET8
{
    internal static class Database_Connect
    {
        internal static string? _Aktiv_Datenbank;

        internal static string Aktiv_Datenbank()
        {
            try
            {
                return _Aktiv_Datenbank ??=
                    "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"" + Modul_Ordner.Database_Pfad() + "\\CamRecorder.mdb\";Persist Security Info=False";
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Parameter.Aktiv_Datenbank");
                return null!;
            }
        }
    }
}
