using System.Diagnostics;
using System.Net;

namespace XstreaMonNET8
{
    internal static class Modul_Update
    {
        internal static bool Update_Check()
        {
            string updateVersionNumber;
            bool result;

            try
            {
                using WebClient client = new WebClient();
                client.Headers.Add("user-agent", $"XstreaMon {Application.ProductVersion}");
                updateVersionNumber = client.DownloadString("https://xstreamon.com/crupdate");
            }
            catch (Exception)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(updateVersionNumber) &&
                UpdateVersion_isGreater(Application.ProductVersion, updateVersionNumber))
            {
                DialogResult dialog = MessageBox.Show(
                    string.Format(TXT.TXT_Description("Es ist ein Update verfügbar. Möchten Sie das Update {0} herunterladen?"), updateVersionNumber),
                    TXT.TXT_Description("Update verfügbar"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dialog == DialogResult.Yes)
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "https://xstreamon.com/",
                        UseShellExecute = true
                    });
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }

            return result;
        }

        public static bool UpdateVersion_isGreater(string originalVersion, string updateVersion)
        {
            try
            {
                string[] originalParts = originalVersion.Split('.');
                string[] updateParts = updateVersion.Split('.');

                for (int i = 0; i < originalParts.Length && i < updateParts.Length; i++)
                {
                    if (int.TryParse(originalParts[i], out int original) &&
                        int.TryParse(updateParts[i], out int updated))
                    {
                        if (original < updated)
                            return true;
                        if (original > updated)
                            return false;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
