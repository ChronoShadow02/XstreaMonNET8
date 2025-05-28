using System.ComponentModel;
using System.Diagnostics;
using System.Management;
using System.Text;

namespace XstreaMonNET8
{
    public class Lizenz
    {
        private readonly bool Aktivierung_Ausführen;
        private readonly string Lizenz_String;

        public static bool Lizenz_vorhanden { get; set; }

        internal string Lizenz_Bezeichnung { get; set; }

        internal string Lizenz_Programmbezeichnung { get; set; }

        internal int Lizenz_Laufzeit { get; set; }

        internal int Lizenz_Typ { get; set; }

        internal Lizenz(bool Aktivierung_Starten)
        {
            Lizenz_Bezeichnung = TXT.TXT_Description("Testversion");
            Lizenz_Programmbezeichnung = "";
            Lizenz_Typ = 0;
            Aktivierung_Ausführen = Aktivierung_Starten;
            Lizenz_String = IniFile.Read(Parameter.INI_Common, nameof(Lizenz), "LizenzValue");
            Lesen();
        }

        internal Lizenz(string Lizenz_Key)
        {
            Lizenz_Bezeichnung = TXT.TXT_Description("Testversion");
            Lizenz_Programmbezeichnung = "";
            Lizenz_Typ = 0;
            Lizenz_String = Lizenz_Key;
            Lesen();
        }

        internal void Lesen()
        {
            if (Lizenz_String.Length == 0)
            {
                Status_Set_Test();
                return;
            }

            var partes = Lizenz_String.Split('-');
            if (partes.Length != 4 && partes.Length != 5)
            {
                Status_Set_Test();
                return;
            }

            var licenseRequestOutcome = new LicenseManagerApiInterface(
                "https://duehring-edv.com",
                "ck_88ef4f6b0f31c502a51a1af86e8e0a8d0d167d69",
                "cs_89ff68a4ab12dba96a49636fabe8f94aa614f6bb")
            {
                WebClientTimeout = 5000
            }.LicenseRequest(LicenseRequestType.Retrieve, Lizenz_String);

            if (licenseRequestOutcome.APIReturnedSuccess)
            {
                var license1 = licenseRequestOutcome.Licences[0];
                if (license1.Status == LicenseStatus.Inactive)
                {
                    Status_Set_Test();
                    MessageBox.Show(TXT.TXT_Description("Die Lizenz wurde deaktiviert.") + " " +
                                    TXT.TXT_Description("Bitte setzten sie sich mit dem Support in Verbindung"));
                }
                else
                {
                    if (license1.Status == LicenseStatus.Delivered || license1.Status == LicenseStatus.Sold)
                    {
                        var license2 = Lizenz_Activate(license1, Aktivierung_Ausführen);
                        if (license2 != null)
                        {
                            license1 = license2;
                        }
                        else
                        {
                            Status_Set_Test();
                            return;
                        }
                    }

                    if (license1.Status == LicenseStatus.Active)
                    {
                        string right = (GetCPU_ID() + "|" + license1.LicenseKey).GetHashCode().ToString();
                        string activation = IniFile.Read(Parameter.INI_Common, nameof(Lizenz), "Activation");

                        if (activation != right)
                        {
                            var license3 = Lizenz_Activate(license1, Aktivierung_Ausführen);
                            if (license3 != null)
                            {
                                license1 = license3;
                            }
                            else
                            {
                                Status_Set_Test();
                                return;
                            }
                        }
                    }

                    if (!license1.ExpiresAt.HasValue)
                        Status_Set_Voll();
                    else
                        Status_Set_Time(license1);
                }
            }
            else
            {
                string right = (GetCPU_ID() + "|" + IniFile.Read(Parameter.INI_Common, nameof(Lizenz), "LizenzValue")).GetHashCode().ToString();
                string activation = IniFile.Read(Parameter.INI_Common, nameof(Lizenz), "Activation");

                if (activation == right)
                {
                    string key = IniFile.Read(Parameter.INI_Common, nameof(Lizenz), "Key");
                    if (key.Length > 0)
                    {
                        DateTime date = DateTime.Parse(Crypt(key, false));
                        Lizenz_Typ = 2;
                        Lizenz_Laufzeit = (int)(date - DateTime.Today).TotalDays;
                        Lizenz_vorhanden = DateTime.Compare(date, DateTime.Today) >= 0;
                        Lizenz_Bezeichnung = TXT.TXT_Description("Zeitlizenz");
                        Lizenz_Programmbezeichnung = " " + TXT.TXT_Description("Zeitlizenz");
                    }
                    else
                    {
                        Lizenz_Typ = 1;
                        Status_Set_Voll();
                    }
                }
                else
                {
                    Lizenz_Typ = 0;
                    Status_Set_Test();
                }
            }
        }


        internal void Laufzeit_Benachrichtigung()
        {
            try
            {
                if (Lizenz_Typ == 2 && Lizenz_Laufzeit < 8)
                {
                    string msg = Lizenz_Laufzeit > 0
                        ? string.Format(TXT.TXT_Description("Ihre Lizenz für XstreaMon läuft in {0} Tagen ab. Möchten sie eine neue Lizenz bestellen?"), Lizenz_Laufzeit)
                        : TXT.TXT_Description("Ihre Lizenz ist abgelaufen. Möchten sie eine neue Lizenz bestellen?");
                    string header = Lizenz_Laufzeit > 0
                        ? TXT.TXT_Description("Lizenz läuft demnächst ab")
                        : TXT.TXT_Description("Lizenz abgelaufen");

                    Lizenz_Empfehlung_Show(msg, header);
                }
                else if (Lizenz_Typ == 0)
                {
                    Lizenz_Empfehlung_Show(
                        TXT.TXT_Description("Sie benutzen die Testversion von XstreaMon. Möchten sie XstreaMon freischalten?"),
                        TXT.TXT_Description("Testversion"));
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Lizenz.Laufzeit_Benachrichtigung");
            }
        }

        private void Lizenz_Empfehlung_Show(string MSG_Text, string MSG_Header)
        {
            try
            {
                Modul_StatusScreen.Status_Show(TXT.TXT_Description(""));
                if (MessageBox.Show(MSG_Text, MSG_Header, MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.Yes)
                {
                    new Dialog_Einstellungen { StartPosition = FormStartPosition.CenterParent }.Show();
                    Process.Start("https://duehring-edv.com/?cat=40/");
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Lizenz.Lizenz_Empfehlung_Show");
            }
        }

        private void Status_Set_Test()
        {
            Lizenz_vorhanden = false;
            Lizenz_Bezeichnung = TXT.TXT_Description("Testversion");
            Lizenz_Programmbezeichnung = " " + TXT.TXT_Description("Testversion");
            Lizenz_Typ = 0;
        }

        private void Status_Set_Voll()
        {
            Lizenz_vorhanden = true;
            Lizenz_Bezeichnung = TXT.TXT_Description("Vollversion");
            Lizenz_Typ = 1;
        }

        private void Status_Set_Time(License data)
        {
            try
            {
                Lizenz_Typ = 2;
                string label = "";
                long? productId = data.ProductID;

                if (productId == 620L) label = TXT.TXT_Description("Jahresversion");
                else if (productId == 69L) label = TXT.TXT_Description("Monatsversion");

                DateTime now = Nettime.GetTime();
                DateTime expires = data.ExpiresAt ?? now;

                if (data.Status == LicenseStatus.Delivered)
                {
                    expires = now.AddDays(data.ValidFor ?? 0);
                }

                if (data.Status == LicenseStatus.Active)
                {
                    DateTime localDate = DateTime.Parse(Crypt(IniFile.Read(Parameter.INI_Common, nameof(Lizenz), "Key"), false));
                    if (expires != localDate)
                    {
                        IniFile.Write(Parameter.INI_Common, nameof(Lizenz), "Key", Crypt(expires.ToString(), true));
                    }
                }

                Lizenz_Laufzeit = (int)(expires - now).TotalDays;

                if (expires >= now)
                {
                    Lizenz_vorhanden = true;
                    string suffix = Lizenz_Laufzeit == 1 ? TXT.TXT_Description("Tag") : TXT.TXT_Description("Tage");
                    Lizenz_Bezeichnung = $"{label} - {Lizenz_Laufzeit} {suffix}";

                    if (productId == 69L || productId == 620L && Lizenz_Laufzeit <= 31)
                    {
                        Lizenz_Programmbezeichnung = $" ({Lizenz_Laufzeit} {suffix})";
                    }
                    else if (productId == 620L)
                    {
                        Lizenz_Programmbezeichnung = $" ({(int)(expires - now).TotalDays / 30} {TXT.TXT_Description("Monate")})";
                    }
                }
                else
                {
                    Lizenz_vorhanden = false;
                    Lizenz_Bezeichnung = $"{label} - {TXT.TXT_Description("Abgelaufen")}";
                    Lizenz_Programmbezeichnung = $" {label} - {TXT.TXT_Description("Abgelaufen")}";
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Lizenz.Status_Set_Time");
            }
        }

        private string GetCPU_ID()
        {
            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor");
                foreach (ManagementObject obj in searcher.Get())
                {
                    if (obj["ProcessorId"] != null)
                        return obj["ProcessorId"].ToString();
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Lizenz.GetCPU_ID");
            }

            return string.Empty;
        }

        public string Crypt(string Inp, bool Mode)
        {
            try
            {
                string key = "smc5PE78tcC6vjUp";
                StringBuilder output = new();
                int keyIndex = 0;

                if (Mode)
                {
                    foreach (char c in Inp)
                    {
                        int val = c ^ key[keyIndex++ % key.Length];
                        output.Append(val.ToString("X2"));
                    }
                }
                else
                {
                    for (int i = 0; i < Inp.Length; i += 2)
                    {
                        string hex = Inp.Substring(i, 2);
                        byte val = Convert.ToByte(hex, 16);
                        char decrypted = (char)(val ^ key[keyIndex++ % key.Length]);
                        output.Append(decrypted);
                    }
                }

                return output.ToString();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Lizenz.Crypt");
                return null!;
            }
        }

        private License Lizenz_Activate(License LizenzFile, bool Activate_Start)
        {
            try
            {
                var api = new LicenseManagerApiInterface("https://duehring-edv.com", "ck_88ef4f6b0f31c502a51a1af86e8e0a8d0d167d69", "cs_89ff68a4ab12dba96a49636fabe8f94aa614f6bb")
                {
                    WebClientTimeout = 5000
                };

                var validate = api.LicenseRequest(LicenseRequestType.Validate, LizenzFile.LicenseKey);
                var retrieve = api.LicenseRequest(LicenseRequestType.Retrieve, LizenzFile.LicenseKey);
                var license = retrieve.Licences[0];
                var now = Nettime.GetTime();

                if (license.ProductID is 71L or 69L or 620L == false)
                {
                    MessageBox.Show(TXT.TXT_Description("Der Lizenzkey ist nicht für XstreaMon zugelassen.") + " " + TXT.TXT_Description("Bitte setzten sie sich mit dem Support in Verbindung"));
                    return LizenzFile;
                }

                if (license.ValidFor > 0 && !license.TimesActivated.HasValue)
                {
                    DateTime baseDate = string.IsNullOrEmpty(IniFile.Read(Parameter.INI_Common, nameof(Lizenz), "Key"))
                        ? now
                        : DateTime.Parse(Crypt(IniFile.Read(Parameter.INI_Common, nameof(Lizenz), "Key"), false));

                    if (baseDate < now)
                        baseDate = now;

                    license.ExpiresAt = baseDate.AddDays(license.ValidFor ?? 0);
                }

                if (Activate_Start && (license.Status == LicenseStatus.Delivered || license.Status == LicenseStatus.Sold || license.Status == LicenseStatus.Active &&
                    (GetCPU_ID() + "|" + license.LicenseKey).GetHashCode() != int.Parse(IniFile.Read(Parameter.INI_Common, nameof(Lizenz), "Key"))))
                {
                    license.TimesActivated = (license.TimesActivated ?? 0) + 1;
                    license.UpdatedAt = now;
                    license.Status = LicenseStatus.Active;
                    var update = api.LicenseRequest(LicenseRequestType.Update, license.LicenseKey, license);
                    IniFile.Write(Parameter.INI_Common, nameof(Lizenz), "Activation", (GetCPU_ID() + "|" + license.LicenseKey).GetHashCode().ToString());
                    IniFile.Write(Parameter.INI_Common, nameof(Lizenz), "Key", Crypt(license.ExpiresAt.ToString(), true));
                }

                return license;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Lizenz.Lizenz_Activate");
                return null;
            }
        }
    }
}
