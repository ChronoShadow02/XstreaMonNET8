namespace XstreaMonNET8
{
    public class CamBrowser : Form
    {
        #region ToolStrip y elementos relacionados
        private ToolStrip toolStripMain;
        private ToolStripButton CBB_Back;
        private ToolStripButton CBB_Next;
        private ToolStripButton CBB_Refresh;
        private ToolStripDropDownButton CBB_Navigation;
        private ToolStripSeparator CommandBarSeparator1;
        private ToolStripSeparator RadMenuSeparatorItem1;
        #endregion

        #region ToolStripMenuItems
        private ToolStripMenuItem CMI_Records;
        private ToolStripMenuItem CMI_Token;
        private ToolStripMenuItem CMI_Online;
        private ToolStripMenuItem CMI_Off;
        #endregion

        #region Paneles
        private Panel PAN_Token;
        private Panel PAN_Right;
        private Panel PAN_Online;
        private Panel PAN_Manual_Records;
        private Panel CBS_Model_Navigation;
        #endregion

        #region Labels
        private Label LAB_Token;
        #endregion

        #region TextBoxes
        private TextBox TXB_Intervall_Token;
        #endregion

        #region NumericUpDowns
        private NumericUpDown SPE_Intervall_Sekunden;
        #endregion

        #region CheckBoxes
        private CheckBox BTN_Token_Intervall;
        #endregion

        #region Botones
        private Button BTN_Token_1;
        private Button BTN_Token_2;
        private Button BTN_Token_5;
        private Button BTN_Token_10;
        private Button BTN_Token_15;
        private Button BTN_Token_25;
        private Button BTN_Token_50;
        private Button CBB_Model_Up;
        private Button CBB_Model_Down;
        #endregion

        #region WebView2
        private Microsoft.Web.WebView2.WinForms.WebView2 WV_Site;
        #endregion

        #region Variables auxiliares
        private List<string> Model_List_Online = new();
        #endregion


        public CamBrowser()
        {
            InitializeComponent();
            InicialiceWebView();
            InicialicePanelToken();
            InicialicePanelLateralDerecho();
            InicialiceNavegacionModelo();
        }

        private void InitializeComponent()
        {
            // ToolStrip
            toolStripMain = new ToolStrip();

            // Botones
            CBB_Back = new ToolStripButton()
            {
                Name = "CBB_Back",
                Text = "Back",
                Image = Resources.Back32, // reemplaza por tu imagen real
                DisplayStyle = ToolStripItemDisplayStyle.Image
            };
            CBB_Back.Click += CBB_Back_Click;

            CBB_Next = new ToolStripButton()
            {
                Name = "CBB_Next",
                Text = "Next",
                Image = Resources.Next32,
                DisplayStyle = ToolStripItemDisplayStyle.Image
            };
            CBB_Next.Click += CBB_Next_Click;

            CommandBarSeparator1 = new ToolStripSeparator();

            CBB_Refresh = new ToolStripButton()
            {
                Name = "CBB_Refresh",
                Text = "Refresh",
                Image = Resources.refresh_32,
                DisplayStyle = ToolStripItemDisplayStyle.Image
            };
            CBB_Refresh.Click += CBB_Refresh_Click;

            // DropDown
            CBB_Navigation = new ToolStripDropDownButton()
            {
                Name = "CBB_Navigation",
                Text = "Navigation",
                Image = Resources.NavigationOnline32
            };

            CMI_Records = new ToolStripMenuItem("Aufnahmen") { CheckOnClick = true };
            CMI_Token = new ToolStripMenuItem("Token") { CheckOnClick = true };
            CMI_Online = new ToolStripMenuItem("Online") { CheckOnClick = true };
            RadMenuSeparatorItem1 = new ToolStripSeparator();
            CMI_Off = new ToolStripMenuItem("Aus") { CheckOnClick = true };

            CBB_Navigation.DropDownItems.AddRange(new ToolStripItem[]
            {
                CMI_Records, CMI_Token, CMI_Online, RadMenuSeparatorItem1, CMI_Off
            });

            // Agregar al ToolStrip
            toolStripMain.Items.AddRange(new ToolStripItem[]
            {
                CBB_Back,
                CBB_Next,
                CommandBarSeparator1,
                CBB_Refresh,
                CBB_Navigation
            });

            // Form config
            this.Controls.Add(toolStripMain);
            this.Text = "CamBrowser";
            this.WindowState = FormWindowState.Maximized;
        }

        private void CBB_Back_Click(object sender, EventArgs e)
        {
            // Lógica para ir atrás
        }

        private void CBB_Next_Click(object sender, EventArgs e)
        {
            // Lógica para ir adelante
        }

        private void CBB_Refresh_Click(object sender, EventArgs e)
        {
            // Lógica para refrescar
        }

        private void InicialicePanelToken()
        {
            PAN_Token = new Panel
            {
                Name = "PAN_Token",
                AutoSize = true,
                Dock = DockStyle.Top,
                Visible = false
            };

            // Etiqueta token
            LAB_Token = new Label
            {
                Name = "LAB_Token",
                Text = "Token:",
                Dock = DockStyle.Top,
                Height = 20,
                TextAlign = ContentAlignment.MiddleLeft
            };

            // TextBox para entrada de intervalo
            TXB_Intervall_Token = new TextBox
            {
                Name = "TXB_Intervall_Token",
                PlaceholderText = "100 or 1-5-15-1",
                Dock = DockStyle.Top,
                Height = 30
            };

            // NumericUpDown como reemplazo de RadSpinEditor
            SPE_Intervall_Sekunden = new NumericUpDown
            {
                Name = "SPE_Intervall_Sekunden",
                Minimum = 1,
                Maximum = 120,
                Value = 1,
                Dock = DockStyle.Top,
                Height = 30,
                TextAlign = HorizontalAlignment.Center
            };

            // Toggle estilo botón
            BTN_Token_Intervall = new CheckBox
            {
                Name = "BTN_Token_Intervall",
                Appearance = Appearance.Button,
                Text = "Enviar",
                Dock = DockStyle.Top,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Botones de token
            BTN_Token_1 = CreeBotonToken("1");
            BTN_Token_2 = CreeBotonToken("2");
            BTN_Token_5 = CreeBotonToken("5");
            BTN_Token_10 = CreeBotonToken("10");
            BTN_Token_15 = CreeBotonToken("15");
            BTN_Token_25 = CreeBotonToken("25");
            BTN_Token_50 = CreeBotonToken("50");

            // Agrega controles al panel
            PAN_Token.Controls.AddRange(new Control[]
            {
                LAB_Token,
                TXB_Intervall_Token,
                SPE_Intervall_Sekunden,
                BTN_Token_Intervall,
                BTN_Token_1,
                BTN_Token_2,
                BTN_Token_5,
                BTN_Token_10,
                BTN_Token_15,
                BTN_Token_25,
                BTN_Token_50
            });

            // Finalmente lo agregamos al formulario (o al panel lateral si existe)
            this.Controls.Add(PAN_Token);
        }

        private static Button CreeBotonToken(string valor)
        {
            return new Button
            {
                Name = "BTN_Token_" + valor,
                Text = $"{valor} Token",
                Tag = valor,
                Dock = DockStyle.Top,
                Height = 40,
                Font = new Font("Segoe UI", 8.25f, FontStyle.Bold)
            };
        }

        private void InicialicePanelLateralDerecho()
        {
            // Panel lateral derecho
            PAN_Right = new Panel
            {
                Name = "PAN_Right",
                Dock = DockStyle.Right,
                Width = 200, // Ajusta a tu gusto
                AutoScroll = true
            };

            // Panel de usuarios en línea
            PAN_Online = new Panel
            {
                Name = "PAN_Online",
                Dock = DockStyle.Fill,
                AutoScroll = true
            };

            // Panel de grabaciones manuales
            PAN_Manual_Records = new Panel
            {
                Name = "PAN_Manual_Records",
                Dock = DockStyle.Top,
                AutoScroll = true,
                AutoSize = true,
                Height = 100 // puede ajustarse dinámicamente si es necesario
            };

            // Agrega subpaneles al panel derecho
            PAN_Right.Controls.Add(PAN_Online);
            PAN_Right.Controls.Add(PAN_Token); // ya fue agregado anteriormente
            PAN_Right.Controls.Add(PAN_Manual_Records);

            // Agrega el panel derecho al formulario
            this.Controls.Add(PAN_Right);
        }

        private void InicialiceWebView()
        {
            WV_Site = new Microsoft.Web.WebView2.WinForms.WebView2
            {
                Name = "WV_Site",
                Dock = DockStyle.Fill,
                DefaultBackgroundColor = Color.White,
                ZoomFactor = 1.0
            };

            this.Controls.Add(WV_Site);
        }

        private void InicialiceNavegacionModelo()
        {
            // Separador visual (opcional)
            CBS_Model_Navigation = new Panel
            {
                Name = "CBS_Model_Navigation",
                Height = 10,
                Dock = DockStyle.Top,
                BackColor = SystemColors.ControlDark
            };

            // Botón Modelo Abajo
            CBB_Model_Down = new Button
            {
                Name = "CBB_Model_Down",
                Text = "Modelo ↓",
                Dock = DockStyle.Top,
                Height = 40,
                Visible = false, // como estaba por defecto
                Image = Resources.Down, // asegúrate que exista en Resources
                TextAlign = ContentAlignment.MiddleLeft,
                ImageAlign = ContentAlignment.MiddleRight
            };
            CBB_Model_Down.Click += CBB_Model_Down_Click;

            // Botón Modelo Arriba
            CBB_Model_Up = new Button
            {
                Name = "CBB_Model_Up",
                Text = "Modelo ↑",
                Dock = DockStyle.Top,
                Height = 40,
                Visible = false,
                Image = Resources.Up,
                TextAlign = ContentAlignment.MiddleLeft,
                ImageAlign = ContentAlignment.MiddleRight
            };
            CBB_Model_Up.Click += CBB_Model_Up_Click;

            // Agrega al panel lateral derecho
            PAN_Right.Controls.Add(CBB_Model_Up);
            PAN_Right.Controls.Add(CBB_Model_Down);
            PAN_Right.Controls.Add(CBS_Model_Navigation);
        }

        private void Model_List_Online_Create()
        {
            Model_List_Online.Clear();

            foreach (var model in Class_Model_List.Model_List)
            {
                if (model.Pro_Website_ID == Current_Website_ID &&
                    model.Pro_Model_Online &&
                    model.Timer_Online_Change?.BGW_Result == 1)
                {
                    Model_List_Online.Add(model.Pro_Model_GUID.ToString());
                }
            }
        }

        private async void CBB_Model_Up_Click(object sender, EventArgs e)
        {
            try
            {
                Model_List_Online_Create();

                if (Model_List_Online.Count == 0 || Current_Model_Class == null)
                    return;

                int index = Model_List_Online.IndexOf(Current_Model_Class.Pro_Model_GUID.ToString());
                if (index == -1)
                    index = 0;

                string siguienteGuid = Model_List_Online[(index + 1) % Model_List_Online.Count];

                var modelo = await Class_Model_List.Class_Model_Find(ValueBack.Get_CUnique(siguienteGuid));
                var sitio = Sites.Website_Find(modelo.Pro_Website_ID);

                if (sitio != null)
                    WebView_Source = string.Format(sitio.Pro_Model_URL, modelo.Pro_Model_Name);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al subir de modelo: " + ex.Message);
            }
        }

        private async void CBB_Model_Down_Click(object sender, EventArgs e)
        {
            try
            {
                Model_List_Online_Create();

                if (Model_List_Online.Count == 0 || Current_Model_Class == null)
                    return;

                int index = Model_List_Online.IndexOf(Current_Model_Class.Pro_Model_GUID.ToString());
                if (index == -1)
                    index = 0;

                int nuevoIndex = index > 0 ? index - 1 : Model_List_Online.Count - 1;
                string anteriorGuid = Model_List_Online[nuevoIndex];

                var modelo = await Class_Model_List.Class_Model_Find(ValueBack.Get_CUnique(anteriorGuid));
                var sitio = Sites.Website_Find(modelo.Pro_Website_ID);

                if (sitio != null)
                    WebView_Source = string.Format(sitio.Pro_Model_URL, modelo.Pro_Model_Name);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al bajar de modelo: " + ex.Message);
            }
        }

    }
}
