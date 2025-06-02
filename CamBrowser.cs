using Microsoft.Web.WebView2.Core;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Timer = System.Windows.Forms.Timer;

namespace XstreaMonNET8
{
    public partial class CamBrowser : Form
    {
        public CamBrowser()
        {
            InitializeComponent();

            Pri_Website_ID = -1;
            Pri_User_loggedOn = false;
            Site_Check = new Timer();
            Site_Hash = "";
            Pri_Navigation_Online = true;
            Pri_Navigation_Records = true;
            Pri_Navigation_Token = true;
            Pri_Last_Check_URL = "";
            Token_List = new List<int>();
            Token_Timer = new Timer();
            Model_List_Online = new List<string>();

            this.Load += CamBrowser_Load;
            this.FormClosed += CamBrowser_Closed;
            this.FormClosing += CamBrowser_Closing;
        }

        // Propiedades
        internal string WebView_Source
        {
            get { return Pri_WebView_Source; }
            set
            {
                if (value != null)
                {
                    WV_Site.Source = new Uri(value);
                    Pri_WebView_Source = value;
                }
            }
        }

        private int Current_Website_ID
        {
            get { return Pri_Website_ID; }
            set
            {
                if (Pri_Website_ID != value && value > -1)
                {
                    Pri_Website_ID = value;
                    Online_User_Load();
                }
                else
                {
                    Pri_Website_ID = value;
                }
            }
        }

        private bool User_loggedOn
        {
            get { return Pri_User_loggedOn; }
            set
            {
                Pri_User_loggedOn = value;
                Navigation_Show();
            }
        }

        private Class_Model Current_Model_Class
        {
            get { return _Current_Model_Class; }
            set
            {
                if (_Current_Model_Class != null)
                {
                    _Current_Model_Class.Model_Record_Change -= Record_Change_Run;
                }

                _Current_Model_Class = value;

                if (_Current_Model_Class != null)
                {
                    _Current_Model_Class.Model_Record_Change += Record_Change_Run;
                }
            }
        }

        // Métodos de eventos
        private void CamBrowser_Load(object sender, EventArgs e)
        {
            try
            {
                Navigation_Load();
                Manual_Records_Change();
                Class_Record_Manual.Evt_List_Changed += Manual_Records_Change;
                WV_Site.Source = new Uri(WebView_Source);
                Token_Timer.Interval = 1000;

                this.TOT_Browser = new System.Windows.Forms.ToolTip();

                ((ToolStrip)CBB_Add.Owner).ShowItemToolTips = true;

                CBB_Add.ToolTipText = "Kanal hinzufügen";
                CBB_Back.ToolTipText = "Klicken zum Zurückkehren";
                CBB_Galerie.ToolTipText = "Galerie öffnen";
                CBB_Next.ToolTipText = "Klicken zum Fortführen";
                CBB_Record.ToolTipText = "Aufnahme";
                CBB_Record_Automatik.ToolTipText = "Automatische Aufnahme";
                CBB_Refresh.ToolTipText = "Aktualisieren";
                CBB_Model_Down.ToolTipText = "Nächstes Model";
                CBB_Model_Up.ToolTipText = "Vorheriges Model";

                CMI_Off.Text = "Alle aus";
                CMI_Off.ToolTipText = "Schaltet alle Listen aus";
                CMI_Online.Text = "Online";
                CMI_Online.ToolTipText = "Blendet alle Online Kanäle ein";
                CMI_Records.Text = "Aufnahmen";
                CMI_Records.ToolTipText = "Alle manuellen Aufnahmen anzeigen";
                CMI_Token.Text = "Token";
                CMI_Token.ToolTipText = "Zeigt die Tokenleiste an";

                TOT_Browser.SetToolTip(SPE_Intervall_Sekunden, "Interval in Sekunden");
                TOT_Browser.SetToolTip(BTN_Token_Intervall, "Sendet die Token");

                Site_Check.Interval = 2500;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en CamBrowser_Load: " + ex.Message);
            }
        }

        private void CamBrowser_Closed(object sender, EventArgs e)
        {
            try
            {
                CamBrowser_Cache_Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en CamBrowser_Closed: " + ex.Message);
            }
        }

        private void CamBrowser_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                if (Class_Record_Manual.Manual_Record_List.Count > 0)
                {
                    var result = MessageBox.Show(
                        $"Es laufen noch {Class_Record_Manual.Manual_Record_List.Count} Aufnahmen. Sollen die Aufnahmen beendet werden?",
                        "Aufnahmen beenden",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);

                    switch (result)
                    {
                        case DialogResult.Cancel:
                            e.Cancel = true;
                            return;
                        case DialogResult.Yes:
                            for (int i = 0; i < Class_Record_Manual.Manual_Record_List.Count; i++)
                            {
                                Class_Record_Manual.Stop_Record(Class_Record_Manual.Manual_Record_List[0]);
                            }
                            break;
                    }
                }
                Site_Check.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en CamBrowser_Closing: " + ex.Message);
            }
        }

        private void TIM_Cache_Clear_Tick(object sender, EventArgs e)
        {
            CamBrowser_Cache_Clear();
        }

        private async void CamBrowser_Cache_Clear()
        {
            try
            {
                if (WV_Site.CoreWebView2?.Profile != null)
                {
                    await WV_Site.CoreWebView2.Profile.ClearBrowsingDataAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en CamBrowser_Cache_Clear: " + ex.Message);
            }
        }

        private void Navigation_Show()
        {
            try
            {
                PAN_Manual_Records.Visible = Class_Record_Manual.Manual_Record_List.Count > 0 && Pri_Navigation_Records;
                PAN_Token.Visible = Pri_Navigation_Token && User_loggedOn;

                bool flag = false;
                foreach (Control control in PAN_Online.Controls)
                {
                    if (control is Con_User_Online userOnline && userOnline.Pro_Class_Model.Get_Pro_Model_Online())
                    {
                        flag = true;
                        break;
                    }
                }

                PAN_Online.Visible = Pri_Navigation_Online && flag;
                PAN_Right.Visible = (Class_Record_Manual.Manual_Record_List.Count > 0 && Pri_Navigation_Records) ||
                                    (Pri_Navigation_Token && User_loggedOn) ||
                                    (Pri_Navigation_Online && flag);

                Model_List_Online_Create();

                if (Model_List_Online.Count > 1)
                {
                    CBS_Model_Navigation.Visible = true;
                    CBB_Model_Down.Visible = true;
                    CBB_Model_Up.Visible = true;
                }
                else
                {
                    CBS_Model_Navigation.Visible = false;
                    CBB_Model_Down.Visible = false;
                    CBB_Model_Up.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en Navigation_Show: " + ex.Message);
            }
        }

        private void Navigation_Load()
        {
            try
            {
                CMI_Online.Checked = Convert.ToBoolean(IniFile.Read("Common", "Browser", "Online", "True"));
                CMI_Records.Checked = Convert.ToBoolean(IniFile.Read("Common", "Browser", "Records", "True"));
                CMI_Token.Checked = Convert.ToBoolean(IniFile.Read("Common", "Browser", "Token", "True"));

                Pri_Navigation_Online = CMI_Online.Checked;
                Pri_Navigation_Records = CMI_Records.Checked;
                Pri_Navigation_Token = CMI_Token.Checked;

                CMI_Off.Checked = !CMI_Online.Checked && !CMI_Records.Checked && !CMI_Token.Checked;
                Navigation_Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en Navigation_Load: " + ex.Message);
            }
        }

        private void CMI_Online_CheckChanged(object sender, EventArgs e)
        {
            IniFile.Write("Common", "Browser", "Online", CMI_Online.Checked.ToString());
            Pri_Navigation_Online = CMI_Online.Checked;
            Online_User_Load();
            Navigation_Load();
        }

        private void CMI_Records_CheckChanged(object sender, EventArgs e)
        {
            IniFile.Write("Common", "Browser", "Records", CMI_Records.Checked.ToString());
            Navigation_Load();
        }

        private void CMI_Token_CheckChanged(object sender, EventArgs e)
        {
            IniFile.Write("Common", "Browser", "Token", CMI_Token.Checked.ToString());
            Navigation_Load();
        }

        private void CMI_Off_CheckChanged(object sender, EventArgs e)
        {
            IniFile.Write("Common", "Browser", "Online", (!CMI_Off.Checked).ToString());
            IniFile.Write("Common", "Browser", "Records", (!CMI_Off.Checked).ToString());
            IniFile.Write("Common", "Browser", "Token", (!CMI_Off.Checked).ToString());
            Navigation_Load();
        }

        private async void Site_Check_Elapsed(object sender, EventArgs e)
        {
            try
            {
                string url = WV_Site.Source.AbsoluteUri;
                string html = await WV_Site.ExecuteScriptAsync("document.documentElement.outerHTML;");
                html = Regex.Unescape(html).Replace("\"", "");

                CBB_Save.Visible = Sites.Site_Is_Galerie(url, html, Current_Model_Class);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Site_Check_Elapsed: {ex.Message}");

                // si no es E_ACCESSDENIED, vuelve a arrancar el timer
                if (ex.HResult != -2147024891)
                    Site_Check.Start();
            }
        }

        private void WV_Site_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            try
            {
                WV_Site_Completed();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en WV_Site_NavigationCompleted: " + ex.Message);
            }
        }

        private async void WV_Site_Completed()
        {
            try
            {
                Current_URL = WV_Site.Source.ToString().Replace("www.", "");
                var (modelName, websiteId) = Sites.Site_Model_URL_Read(Current_URL);
                Current_Model_Name = modelName;

                if (websiteId == -1)
                {
                    Text = "CamBrowser";
                }
                else
                {
                    var website = Sites.Website_Find(websiteId);
                    Text = $"CamBrowser - {website.Pro_Name}" +
                          (Current_Model_Name.Length > 0 ? $" - {Current_Model_Name}" : "");
                }

                if (Current_Website_ID != websiteId)
                {
                    Current_Website_ID = websiteId;
                }

                string html = await WV_Site.ExecuteScriptAsync("document.documentElement.outerHTML;");
                html = Regex.Unescape(html).Replace("\"", "");

                Current_Model_Class = Class_Model_List.Class_Model_Find(Current_Website_ID, Current_Model_Name).Result;

                if (Current_Model_Class != null)
                {
                    if (Token_Model_GUID != Current_Model_Class.Pro_Model_GUID)
                    {
                        Token_Count = 0;
                        Token_Send = 0;
                    }

                    CBB_Record.Enabled = true;
                    CBB_Record_Automatik.Enabled = true;
                    CBB_Galerie.Enabled = true;

                    LAB_Info.Visible = Current_Model_Class.Pro_Model_Info.Length > 0;
                    ((ToolStrip)LAB_Info.Owner).ShowItemToolTips = true;
                    LAB_Info.ToolTipText = Current_Model_Class.Pro_Model_Info;

                    bool isOnline = Sites.Model_Online(Current_Model_Name, Current_Website_ID, html) != 0;
                    Current_Model_Class.Set_Pro_Model_Online(false, isOnline);

                    LAB_Token.Text = $"Token: {Token_Send}\r\nAnzahl: {Token_Count}\r\nGesamt: {Current_Model_Class.Pro_Model_Token}";

                    CBB_Add.Visible = false;
                    CBB_Record.Visible = true;
                    CBB_Record_Automatik.Visible = true;
                    CBB_Galerie.Visible = true;
                    CBS_Model_Navigation.Visible = true;
                    CommandBarSeparator3.Visible = true;
                }
                else
                {
                    CBB_Add.Visible = true;
                    CBB_Record.Visible = false;
                    CBB_Record_Automatik.Visible = false;
                    CBB_Galerie.Visible = false;
                    CBS_Model_Navigation.Visible = false;
                    CommandBarSeparator3.Visible = false;

                    Token_Count = 0;
                    Token_Send = 0;
                }

                LAB_Token.Visible = Current_Model_Class != null;
                CBB_Back.Enabled = WV_Site.CanGoBack;
                CBB_Next.Enabled = WV_Site.CanGoForward;

                Record_Button_Change();
                User_loggedOn = Sites.WebSite_IsLogin(Current_Website_ID, html) && Current_Model_Class != null;
                Navigation_Show();

                TIM_Cache_Clear.Start();
                Site_Check.Start();
                Online_User_SetCurrent();

                if (Sites.Site_Is_Galerie(WV_Site.Source.AbsoluteUri, html, Current_Model_Class))
                {
                    CBB_Save.Visible = true;
                }
                else
                {
                    CBB_Save.Visible = false;
                }

                WV_Site.PerformLayout();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en WV_Site_Completed: " + ex.Message);
            }
        }

        private void WV_Site_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            try
            {
                Site_Check.Stop();
                Current_Model_Name = "";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en WV_Site_NavigationStarting: " + ex.Message);
            }
        }

        private void WV_Site_SourceChanged(object sender, CoreWebView2SourceChangedEventArgs e)
        {
            try
            {
                WV_Site_Completed();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en WV_Site_SourceChanged: " + ex.Message);
            }
        }

        private void WV_Site_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            try
            {
                if (WV_Site.CoreWebView2 != null)
                {
                    WV_Site.CoreWebView2.NewWindowRequested += CoreWebView2_NewWindowRequested;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en WV_Site_CoreWebView2InitializationCompleted: " + ex.Message);
            }
        }

        private void CoreWebView2_NewWindowRequested(object sender, CoreWebView2NewWindowRequestedEventArgs e)
        {
            try
            {
                Sites.CamBrowser_Open(e.Uri);
                e.Handled = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en CoreWebView2_NewWindowRequested: " + ex.Message);
            }
        }

        private void Manual_Records_Change()
        {
            try
            {
                // Agregar nuevos registros manuales
                foreach (var manualRecord in Class_Record_Manual.Manual_Record_List)
                {
                    bool exists = false;
                    foreach (Control control in PAN_Manual_Records.Controls)
                    {
                        if (control is Control_Manual_Record recordControl &&
                            recordControl.Pro_Record_Stream == manualRecord.Pro_Channel_Stream)
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (!exists)
                    {
                        var newRecordControl = new Control_Manual_Record(manualRecord)
                        {
                            Dock = DockStyle.Top
                        };

                        newRecordControl.Evt_Record_Close += Manual_Records_Close;
                        PAN_Manual_Records.Controls.Add(newRecordControl);
                    }
                }

                // Eliminar controles de registros que ya no existen
                for (int i = PAN_Manual_Records.Controls.Count - 1; i >= 0; i--)
                {
                    if (PAN_Manual_Records.Controls[i] is Control_Manual_Record control)
                    {
                        if (Class_Record_Manual.Find_Record(control.Pro_Channel_Name, control.Pro_Record_Stream.Pro_Website_ID) == null)
                        {
                            if (PAN_Manual_Records.InvokeRequired)
                            {
                                PAN_Manual_Records.Invoke((MethodInvoker)delegate
                                {
                                    PAN_Manual_Records.Controls.Remove(control);
                                    control.Dispose();
                                });
                            }
                            else
                            {
                                PAN_Manual_Records.Controls.Remove(control);
                                control.Dispose();
                            }
                        }
                    }
                }

                if (InvokeRequired)
                {
                    Invoke((MethodInvoker)delegate { Record_Button_Change(); });
                }
                else
                {
                    Record_Button_Change();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en Manual_Records_Change: " + ex.Message);
            }
        }

        private void Record_Button_Change()
        {
            try
            {
                if (Current_Model_Class != null)
                {
                    if (Current_Model_Class.Pro_Model_Stream_Record != null)
                    {
                        CBB_Record_Automatik.Image = Resources.RecordAutomatikStop32;
                    }
                    else
                    {
                        CBB_Record_Automatik.Image = Resources.RecordAutomatic32;
                    }

                    var record = Class_Record_Manual.Find_Record(Current_Model_Class.Pro_Model_Name, Current_Model_Class.Pro_Website_ID);
                    if (record?.Pro_Channel_Stream != null)
                    {
                        CBB_Record.Image = Resources.control_stop_icon32;
                    }
                    else
                    {
                        CBB_Record.Image = Resources.control_record_icon32;
                    }
                }

                Navigation_Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en Record_Button_Change: " + ex.Message);
            }
        }

        private void Record_Change_Run(bool Record_Run)
        {
            try
            {
                if (Record_Run)
                {
                    CBB_Record_Automatik.Image = Resources.RecordAutomatikStop32;
                }
                else
                {
                    CBB_Record_Automatik.Image = Resources.RecordAutomatic32;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en Record_Change_Run: " + ex.Message);
            }
        }

        private void Manual_Records_Close(Control_Manual_Record Con)
        {
            Class_Record_Manual.Stop_Record(Con.Pro_Channel_Name, Con.Pro_Record_Stream.Pro_Website_ID);
        }

        private async void CBB_Galerie_Click(object sender, EventArgs e)
        {
            try
            {
                if (Current_Model_Class == null)
                    return;

                try
                {
                    await Task.Run(() => Current_Model_Class.Dialog_Model_View_Show());
                }
                catch (Exception ex)
                {
                    Cursor = Cursors.Default;
                    Console.WriteLine("Error en CBB_Galerie_Click: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en CBB_Galerie_Click: " + ex.Message);
            }
        }

        private void CBB_Back_Click(object sender, EventArgs e)
        {
            WV_Site?.GoBack();
        }

        private void CBB_Next_Click(object sender, EventArgs e)
        {
            WV_Site?.GoForward();
        }

        private void CBB_Refresh_Click(object sender, EventArgs e)
        {
            CamBrowser_Cache_Clear();
            WV_Site?.Reload();
        }

        private void CBB_Record_Click(object sender, EventArgs e)
        {
            try
            {
                if (Current_Model_Class != null)
                {
                    var record = Class_Record_Manual.Find_Record(Current_Model_Class.Pro_Model_Name, Current_Model_Class.Pro_Website_ID);

                    if (record?.Pro_Channel_Stream == null)
                    {
                        WV_Site.ResetText();
                        Class_Record_Manual.New_Record(
                            Current_Model_Class.Pro_Model_Name,
                            Current_Model_Class.Pro_Website_ID,
                            new Class_Stream_Record(Current_Model_Class));
                    }
                    else
                    {
                        Class_Record_Manual.Stop_Record(Current_Model_Class.Pro_Model_Name, Current_Model_Class.Pro_Website_ID);
                    }
                }

                WV_Site_Completed();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en CBB_Record_Click: " + ex.Message);
            }
        }

        private void CBB_Record_Automatik_Click(object sender, EventArgs e)
        {
            try
            {
                if (Current_Model_Class == null)
                    return;

                Current_Model_Class.Pro_Model_Record = !Current_Model_Class.Pro_Model_Record;

                if (Current_Model_Class.Pro_Model_Stream_Record != null)
                {
                    CBB_Record_Automatik.Image = Resources.RecordAutomatikStop32;
                }
                else
                {
                    CBB_Record_Automatik.Image = Resources.RecordAutomatic32;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en CBB_Record_Automatik_Click: " + ex.Message);
            }
        }

        private void CBB_Add_Click(object sender, EventArgs e)
        {
            try
            {
                Form_Main.Instance.Chanel_Add(WV_Site.Source.ToString());
                WV_Site_Completed();
                Online_User_Load();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en CBB_Add_Click: " + ex.Message);
            }
        }

        private void Token_Send_Click(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.Button button && button.Tag != null)
            {
                int tokenValue = Convert.ToInt32(button.Tag);
                Send_Token(tokenValue);
            }
        }

        private void BTN_Token_Text_Click(object sender, EventArgs e)
        {
            try
            {
                string[] parts = TXB_Intervall_Token.Text.Split('-');
                foreach (string part in parts)
                {
                    int tokenValue;
                    if (int.TryParse(part, out tokenValue) && tokenValue > 0)
                    {
                        Token_List_Add(tokenValue);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en BTN_Token_Text_Click: " + ex.Message);
            }
        }

        private async void Send_Token(int Token_Value)
        {
            await Task.CompletedTask;
            try
            {
                if (Token_Value <= 0)
                    return;

                WV_Site.Focus();
                SendKeys.Send("^(s)" + Token_Value + "{ENTER}");

                Token_Send += Token_Value;
                Token_Count++;

                if (Current_Model_Class != null)
                {
                    Current_Model_Class.Pro_Model_Token += Token_Value;
                }

                LAB_Token.Text = $"Token: {Token_Send}\r\nAnzahl: {Token_Count}\r\nGesamt: {Current_Model_Class?.Pro_Model_Token ?? 0}";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en Send_Token: " + ex.Message);
            }
        }

        private void Token_List_Add(int Token_Value)
        {
            try
            {
                this.Token_List.Add(Token_Value);
                this.BTN_Token_Intervall.Text = this.Token_List.Count.ToString();
                this.BTN_Token_Intervall.Visible = this.Token_List.Count > 0;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.Token_List_Add");
            }
        }

        private void Token_Timer_Elapsed(object Sender, EventArgs e)
        {
            try
            {
                if (this.Token_List.Count > 0)
                {
                    this.Send_Token(this.Token_List[0]);
                    this.Token_List.RemoveAt(0);
                }

                this.BTN_Token_Intervall.Text = this.Token_List.Count.ToString();
                this.BTN_Token_Intervall.Visible = this.Token_List.Count > 0 ||
                                                   this.TXB_Intervall_Token.Text.Length > 0;

                if (this.Token_List.Count == 0)
                {
                    this.Token_Timer.Stop();
                    this.BTN_Token_Intervall.CheckState = CheckState.Unchecked;
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.Token_Timer_Elapsed");
            }
        }

        private void RadSpinEditor1_ValueChanged(object sender, EventArgs e)
        {
            this.Token_Timer.Interval =
                Convert.ToInt32(this.SPE_Intervall_Sekunden.Value * 1000M);
        }

        private void BTN_Token_Intervall_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                bool isOn = BTN_Token_Intervall.Checked;   // estado actual

                // ---- Al marcar, llena la lista de token si está vacía ----
                if (isOn && this.Token_List.Count == 0)
                {
                    foreach (var item in this.TXB_Intervall_Token.Text.Split('-'))
                    {
                        int val = ValueBack.Get_CInteger(
                                      ValueBack.Get_Zahl_Extract_From_String(item));
                        if (val > 0) this.Token_List_Add(val);
                    }
                }

                // ---- Lógica de envío / pausa ----
                if (this.Token_List.Count == 1)
                {
                    this.Token_Timer_Elapsed(null, null);
                    BTN_Token_Intervall.Checked = false;           // se desactiva solo
                }
                else if (!isOn)                                    // botón desmarcado
                {
                    this.Token_Timer.Stop();
                    this.BTN_Token_Intervall.Image = Resources.Start24;
                }
                else                                               // botón marcado
                {
                    this.Token_Timer_Elapsed(null, null);
                    if (this.Token_List.Count > 1) this.Token_Timer.Start();
                    this.BTN_Token_Intervall.Image = Resources.Pause24;
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex,
                    "CamBrowser.BTN_Token_Intervall_CheckedChanged");
            }
        }

        private void TXB_Intervall_Token_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int TruePart = 0;
                this.Token_List.Clear();
                int num = 0;

                string newValue = TXB_Intervall_Token.Text;

                foreach (var valStr in newValue.Split('-'))
                {
                    int val = ValueBack.Get_CInteger(valStr);
                    if (val > 0)
                    {
                        TruePart++;
                        num += val;
                    }
                }

                this.SPE_Intervall_Sekunden.Visible = TruePart > 1;
                this.BTN_Token_Intervall.Visible = TruePart > 0 || this.Token_List.Count > 0;
                this.BTN_Token_Intervall.Text = TruePart > 1 ? TruePart.ToString() : string.Empty;

                this.TOT_Browser.SetToolTip(
                    this.BTN_Token_Intervall,
                    $"{TXT.TXT_Description("Sendet die Token")} {TXT.TXT_Description("Gesamt")}: {num}");
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.TXB_Intervall_Token_TextChanged");
            }
        }

        private void Online_User_SetCurrent()
        {
            try
            {
                foreach (Con_User_Online control in this.PAN_Online.Controls)
                {
                    bool isCurrent = (this.Current_URL ?? string.Empty)
                                     .ToLower()
                                     .Contains(control.Pro_Class_Model.Pro_Model_Name.ToLower());
                    control.Pro_Is_Current = isCurrent;
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.Online_User_SetCurrent");
            }
        }

        private void Online_User_Load()
        {
            try
            {
                if (!this.Pri_Navigation_Online) return;

                foreach (Class_Model model in Class_Model_List.Model_List)
                {
                    if (this.Current_Website_ID != model.Pro_Website_ID) continue;

                    bool exists = false;
                    foreach (Con_User_Online c in this.PAN_Online.Controls)
                    {
                        if (c.Pro_Class_Model == model) { exists = true; break; }
                    }

                    if (!exists)
                    {
                        var ctrl = new Con_User_Online
                        {
                            Pro_Class_Model = model,
                            Dock = DockStyle.Top,
                            Visible = model.Get_Pro_Model_Online()
                        };
                        ctrl.MouseMove += this.Online_User_MouseMove;
                        ctrl.MouseLeave += this.Online_User_Mouse_Leave;
                        this.PAN_Online.Controls.Add(ctrl);
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.CamBrowser_Cache_Clear");
            }
        }

        private void Online_User_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                var u = (Con_User_Online)sender;
                this.Control_Model_Info1.Pro_Model = u.Pro_Class_Model;

                int x = this.PAN_Right.Left - this.Control_Model_Info1.Width;
                int y = this.PointToScreen(u.Location).Y + u.Parent.Location.Y + 20;
                if (y + this.Control_Model_Info1.Height > this.Height)
                    y = this.Height - this.Control_Model_Info1.Height;

                this.Control_Model_Info1.Location = new Point(x, y);
                this.Control_Model_Info1.Control_Visible = true;
                this.Control_Model_Info1.BringToFront();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.CamBrowser_Cache_Clear");
            }
        }

        private void Online_User_Mouse_Leave(object sender, EventArgs e)
        {
            this.Control_Model_Info1.Pro_Model_Preview = null;
            this.Control_Model_Info1.Pro_Model_GUID = Guid.Empty;
            this.Control_Model_Info1.Pro_Model_Info = string.Empty;
            this.Control_Model_Info1.Control_Visible = false;
        }

        private void CBB_Model_Up_Click(object sender, EventArgs e)
        {
            try
            {
                this.Model_List_Online_Create();
                if (this.Model_List_Online.Count == 0) return;

                int idx = this.Model_List_Online
                              .IndexOf(this.Current_Model_Class.Pro_Model_GUID.ToString());
                if (idx == -1) idx = 0;

                var guid = this.Model_List_Online[idx != this.Model_List_Online.Count - 1 ? idx + 1 : 0];
                var model = Class_Model_List.Class_Model_Find(ValueBack.Get_CUnique(guid)).Result;
                var site = Sites.Website_Find(model.Pro_Website_ID);
                if (site == null) return;

                this.WebView_Source = string.Format(site.Pro_Model_URL, model.Pro_Model_Name);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.CBB_Model_Up_Click");
            }
        }

        private void CBB_Model_Down_Click(object sender, EventArgs e)
        {
            try
            {
                this.Model_List_Online_Create();
                if (this.Model_List_Online.Count == 0) return;

                int idx = this.Model_List_Online
                              .IndexOf(this.Current_Model_Class?.Pro_Model_GUID.ToString());
                if (idx == -1) idx = 0;

                var guid = this.Model_List_Online[idx != 0 ? idx - 1 : this.Model_List_Online.Count - 1];
                var model = Class_Model_List.Class_Model_Find(ValueBack.Get_CUnique(guid)).Result;
                var site = Sites.Website_Find(model.Pro_Website_ID);
                if (site == null) return;

                this.WebView_Source = string.Format(site.Pro_Model_URL, model.Pro_Model_Name);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.CBB_Model_Down_Click");
            }
        }

        private void Model_List_Online_Create()
        {
            try
            {
                this.Model_List_Online.Clear();

                foreach (Class_Model m in Class_Model_List.Model_List)
                {
                    if (this.Current_Website_ID == m.Pro_Website_ID &&
                        m.Get_Pro_Model_Online() &&
                        m.Timer_Online_Change.BGW_Result == 1)
                    {
                        this.Model_List_Online.Add(m.Pro_Model_GUID.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.Model_List_Online_Create");
            }
        }

        private void LAB_Info_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (this.Current_Model_Class == null) return;

                Dialog_Model_Info dlg = new Dialog_Model_Info
                {
                    StartPosition = FormStartPosition.CenterParent
                };
                dlg.TXB_Memo.Text = this.Current_Model_Class.Pro_Model_Info;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this.Current_Model_Class.Pro_Model_Info = dlg.TXB_Memo.Text;
                    this.Current_Model_Class.Model_Online_Changed();

                    ((ToolStrip)LAB_Info.Owner).ShowItemToolTips = true;
                    LAB_Info.ToolTipText = this.Current_Model_Class.Pro_Model_Info;
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.CMI_Info_Click");
            }
        }

        private async void CBB_Save_Click(object sender, EventArgs e)
        {
            try
            {
                string html = Regex.Unescape(
                    await this.WV_Site.ExecuteScriptAsync("document.documentElement.outerHTML;"))
                    .Replace("\"", "");

                Sites.Download_Galerie_Movie(
                    this.WV_Site.Source.AbsoluteUri, html, this.Current_Model_Class);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.CMI_Info_Click");
            }
        }
    }
}