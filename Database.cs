using Microsoft.VisualBasic;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
using System.Runtime.InteropServices;

namespace XstreaMonNET8
{
    internal class Database
    {
        internal List<Class_Database> Pro_Datatables
        {
            get
            {
                var proDatatables = new List<Class_Database>();

                var fields1 = new List<Class_Datatable_Field>
                {
                    new("User_GUID", "GUID", true),
                    new("User_Name", "Text(255)", false),
                    new("User_Record", "YESNO", false),
                    new("User_Online", "YESNO", false),
                    new("Last_Online", "DateTime", false),
                    new("User_Visible", "YESNO", false),
                    new("Aufnahmestop_Auswahl", "Int", false),
                    new("Aufnahmestop_Minuten", "Int", false),
                    new("Aufnahmestop_Größe", "Int", false),
                    new("Videoqualität", "Int", false),
                    new("Benachrichtigung", "YESNO", false),
                    new("Website_ID", "Int", false),
                    new("User_Gender", "Int", false),
                    new("User_Country", "Text(255)", false),
                    new("User_Memo", "Text(255)", false),
                    new("User_Favorite", "YESNO", false),
                    new("User_Birthday", "DateTime", false),
                    new("User_Language", "Text(255)", false),
                    new("Recorder_ID", "Int", false),
                    new("User_Description", "Text(255)", false),
                    new("User_Directory", "Text(255)", false),
                    new("User_Create", "DateTime", false),
                    new("User_Deaktiv", "YESNO", false),
                    new("SaveFormat", "Int", false),
                    new("User_Token", "Int", false),
                    new("User_LastVisit", "DateTime", false)
                };
                proDatatables.Add(new Class_Database("DT_User", fields1));

                var fields2 = new List<Class_Datatable_Field>
                {
                    new("Online_GUID", "GUID", true),
                    new("Model_GUID", "GUID", false),
                    new("Online_Beginn", "Datetime", false),
                    new("Online_Ende", "DateTime", false)
                };
                proDatatables.Add(new Class_Database("DT_Online", fields2));

                var fields3 = new List<Class_Datatable_Field>
                {
                    new("Record_GUID", "GUID", true),
                    new("User_GUID", "GUID", false),
                    new("User_Name", "Text(255)", false),
                    new("Record_Name", "Text(255)", false),
                    new("Record_Beginn", "DateTime", false),
                    new("Record_Ende", "DateTime", false),
                    new("Record_PID", "Int", false),
                    new("Maschine", "Text(255)", false),
                    new("Record_Länge_Minuten", "Int", false),
                    new("Record_Favorit", "YESNO", false),
                    new("Record_Convert_Ext", "Text(255)", false),
                    new("Record_Encoder_ID", "Int", false),
                    new("Record_Resolution", "Text(255)", false),
                    new("Record_FrameRate", "Int", false),
                    new("Record_M3U8", "Memo", false)
                };
                proDatatables.Add(new Class_Database("DT_Record", fields3));

                return proDatatables;
            }
        }

        internal string Pro_Datenpath { get; set; }

        internal Database(string datenpath)
        {
            Pro_Datenpath = datenpath;
        }

        internal void Check()
        {
            string connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Pro_Datenpath};Persist Security Info=False";
            foreach (var proDatatable in Pro_Datatables)
            {
                using var connection = new OleDbConnection(connectionString);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    using var adapter = new OleDbDataAdapter($"SELECT TOP 1 * FROM {proDatatable.Datatable_Name}", connection);
                    using var dataSet = new DataSet();
                    adapter.Fill(dataSet, proDatatable.Datatable_Name);

                    foreach (var field in proDatatable.Datatable_fields)
                    {
                        if (dataSet.Tables[0].Columns.IndexOf(field.Pro_Field_Name) == -1)
                        {
                            using var transaction = connection.BeginTransaction();
                            using var command = connection.CreateCommand();
                            command.Transaction = transaction;
                            command.CommandText = $"ALTER TABLE {proDatatable.Datatable_Name} ADD COLUMN {field.Pro_Field_Name} {field.Pro_Field_Typ} {(field.Pro_Field_Identifer ? "PRIMARY KEY" : "")}";
                            try
                            {
                                command.ExecuteNonQuery();
                                transaction.Commit();
                            }
                            catch
                            {
                                // Ignorar errores de alteración de tabla
                            }
                        }
                    }
                }

                connection.Close();
            }
        }

        internal static void Backup()
        {
            try
            {
                string path = Path.Combine(IniFile.Read(Parameter.INI_Common, "Directory", "Database"), "CamRecorder.mdb");
                if (!File.Exists(path)) return;

                string connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={path};Persist Security Info=False";
                var jetEngine = Activator.CreateInstance(Type.GetTypeFromProgID("JRO.JetEngine")!);
                string backupPath = Path.Combine(Parameter.CommonPath, "Backup.tmp");

                if (File.Exists(backupPath))
                    File.Delete(backupPath);

                try
                {
                    object[] args =
                    {
                        connectionString,
                        $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={backupPath};Jet OLEDB:Engine Type=5"
                    };
                    jetEngine!.GetType().InvokeMember("CompactDatabase", BindingFlags.InvokeMethod, null, jetEngine, args);
                    Marshal.ReleaseComObject(jetEngine);
                    File.Copy(backupPath, path, true);
                }
                catch
                {
                    File.Copy(path, backupPath, true);
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Parameter.Backup");
            }
        }

        internal static bool DB_File_Check()
        {
            try
            {
                string dbPath = Path.Combine(Modul_Ordner.Database_Pfad(), "CamRecorder.mdb");
                string backup = Path.Combine(Parameter.CommonPath, "Backup.tmp");

                if (!File.Exists(dbPath))
                {
                    if (File.Exists(backup))
                    {
                        Modul_StatusScreen.Status_Show(TXT.TXT_Description(""));
                        if (MessageBox.Show(TXT.TXT_Description("Die Datenbank konnte nicht gefunden werden. Möchten sie das Backup laden?"),
                                TXT.TXT_Description("Datenbank fehlt"),
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Hand) == DialogResult.Yes)
                        {
                            File.Copy(backup, dbPath, true);
                        }
                    }

                    string startup = Path.Combine(Application.StartupPath, "CamRecorder.mdb");
                    if (File.Exists(startup) && !File.Exists(dbPath))
                    {
                        Modul_StatusScreen.Status_Show(TXT.TXT_Description(""));
                        if (MessageBox.Show(TXT.TXT_Description("Es konnte keine Datenbank gefunden werden. Möchten sie mit einer neuen Datenbank starten?"),
                                TXT.TXT_Description("Datenbank fehlt"),
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Hand) == DialogResult.Yes)
                        {
                            File.Copy(startup, dbPath, true);
                        }
                    }
                }

                var db = new Database(dbPath);
                db.Check();
                db.DB_Clean();

                return File.Exists(dbPath);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Parameter.DB_File_Check = " + Modul_Ordner.Database_Pfad());
                if (ex.HResult == -2147467259)
                    Database_Defekt(ex);
                return false;
            }
        }

        internal void DB_Clean()
        {
            try
            {
                Modul_StatusScreen.Status_Show(TXT.TXT_Description("Datenbank wird bereinigt"));
                using var connection = new OleDbConnection(Database_Connect.Aktiv_Datenbank());
                connection.Open();
                if (connection.State != ConnectionState.Open) return;

                var deleteCmd = $"DELETE * FROM DT_Online WHERE Online_Beginn < {ValueBack.Get_SQL_Date(DateAndTime.DateAdd(DateInterval.Day, -14.0, DateAndTime.Now))}";
                using var command = new OleDbCommand(deleteCmd, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Modul_StatusScreen.Status_Show(TXT.TXT_Description(""));
                Parameter.Error_Message(ex, nameof(DB_Clean));
            }
        }

        internal static void Database_Defekt(Exception ex)
        {
            Modul_StatusScreen.Status_Show(TXT.TXT_Description(""));
            string path = Path.Combine(IniFile.Read(Parameter.INI_Common, "Directory", "Database"), "CamRecorder.mdb");
            string backup = Path.Combine(Parameter.CommonPath, "Backup.tmp");

            if (File.Exists(backup))
            {
                if (MessageBox.Show(TXT.TXT_Description("Es ist ein Problem mit der Datenbank aufgetreten!") + "\r\n" + ex.Message + "\r\n\r\n" + TXT.TXT_Description("Möchten sie das letzte Backup laden?"),
                        TXT.TXT_Description("Datenbank defekt"),
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Hand) == DialogResult.Yes)
                {
                    File.Delete(path);
                    File.Copy(backup, path, true);
                    Application.Restart();
                }
                else
                {
                    Application.Exit();
                }
            }
            else if (MessageBox.Show(TXT.TXT_Description("Es ist ein Problem mit der Datenbank aufgetreten!") + "\r\n" + ex.Message + "\r\n\r\n" + TXT.TXT_Description("Möchten sie mit einer neuen Datenbank starten?"),
                         TXT.TXT_Description("Datenbank defekt"),
                         MessageBoxButtons.YesNo,
                         MessageBoxIcon.Hand) == DialogResult.Yes)
            {
                File.Delete(path);
                File.Copy(Path.Combine(Application.StartupPath, "CamRecorder.mdb"), path, true);
                Application.Restart();
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
