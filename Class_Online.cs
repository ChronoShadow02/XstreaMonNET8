using System.Data;
using System.Data.OleDb;
namespace XstreaMonNET8
{
    public class Class_Online
    {
        internal static async void New_Row(Guid NewGUID, Guid Model_GUID)
        {
            try
            {
                using OleDbConnection oleDbConnection = new(Database_Connect.Aktiv_Datenbank());
                using OleDbDataAdapter adapter = new($"Select * From DT_Online where Online_GUID = '{NewGUID}'", oleDbConnection.ConnectionString);
                using OleDbCommandBuilder dbCommandBuilder = new(adapter);
                using DataSet dataSet = new();

                await oleDbConnection.OpenAsync().ConfigureAwait(false);
                if (oleDbConnection.State != ConnectionState.Open)
                    return;

                adapter.Fill(dataSet, "DT_Records");
                oleDbConnection.Close();

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    DataRow row = dataSet.Tables[0].NewRow();
                    row["Online_GUID"] = NewGUID;
                    row["Model_GUID"] = ValueBack.Get_CUnique(Model_GUID);
                    row["Online_Beginn"] = DateTime.Now;
                    row["Online_Ende"] = DateTime.Now;
                    dataSet.Tables[0].Rows.Add(row);
                }
                else
                {
                    dataSet.Tables[0].Rows[0]["Online_Ende"] = DateTime.Now;
                }

                adapter.Update(dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Online.New_Row");
            }
        }

        internal static async void End_Write(Guid Online_GUID)
        {
            try
            {
                using OleDbConnection oleDbConnection = new(Database_Connect.Aktiv_Datenbank());
                using OleDbDataAdapter adapter = new($"Select * From DT_Online where Online_GUID = '{Online_GUID}'", oleDbConnection.ConnectionString);
                using OleDbCommandBuilder dbCommandBuilder = new(adapter);
                using DataSet dataSet = new();

                await oleDbConnection.OpenAsync().ConfigureAwait(false);
                if (oleDbConnection.State != ConnectionState.Open)
                    return;

                adapter.Fill(dataSet, "DT_Records");
                oleDbConnection.Close();

                if (dataSet.Tables[0].Rows.Count == 1)
                {
                    dataSet.Tables[0].Rows[0]["Online_Ende"] = DateTime.Now;
                    adapter.Update(dataSet.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Online.End_Write");
            }
        }

        internal static async Task<DateTime> End_Read(Guid Model_GUID)
        {
            try
            {
                using OleDbConnection oleDbConnection = new(Database_Connect.Aktiv_Datenbank());
                using OleDbDataAdapter adapter = new($"Select Max(Online_Ende) as MaxEnde From DT_Online where Model_GUID = '{Model_GUID}' GROUP BY Model_GUID", oleDbConnection.ConnectionString);
                using OleDbCommandBuilder dbCommandBuilder = new(adapter);
                using DataSet dataSet = new();

                await oleDbConnection.OpenAsync().ConfigureAwait(false);
                if (oleDbConnection.State == ConnectionState.Open)
                {
                    adapter.Fill(dataSet, "DT_Records");
                    oleDbConnection.Close();

                    if (dataSet.Tables[0].Rows.Count == 1)
                    {
                        return Convert.ToDateTime(dataSet.Tables[0].Rows[0]["MaxEnde"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Online.End_Read");
            }

            return DateTime.Now;
        }

        internal static async void Online_Delete(Guid Model_GUID)
        {
            try
            {
                using OleDbConnection oleDbConnection = new(Database_Connect.Aktiv_Datenbank());
                using OleDbDataAdapter adapter = new($"Select * From DT_Online where Model_GUID = '{Model_GUID}'", oleDbConnection.ConnectionString);
                using OleDbCommandBuilder dbCommandBuilder = new(adapter);
                using DataSet dataSet = new();

                await oleDbConnection.OpenAsync().ConfigureAwait(false);
                if (oleDbConnection.State != ConnectionState.Open)
                    return;

                adapter.Fill(dataSet, "DT_Online");
                oleDbConnection.Close();

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                        row.Delete();

                    adapter.Update(dataSet.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Online.Online_Delete");
            }
        }
    }
}
