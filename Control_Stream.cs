namespace XstreaMonNET8
{
    public class Control_Stream : UserControl
    {
        // Evento que se dispara al iniciar/terminar grabación
        internal delegate void StreamRecord_StartEventHandler(Control_Stream control);
        internal delegate void StreamRecord_StopEventHandler(Control_Stream control);

        // Eventos para la vista previa
        internal delegate void Vorschau_CloseEventHandler(Control_Stream sender);
        internal delegate void Vorschau_FocusedEventHandler(Control_Stream sender);

        internal event StreamRecord_StartEventHandler StreamRecord_Start;
        internal event StreamRecord_StopEventHandler StreamRecord_Stop;
        internal event Vorschau_CloseEventHandler Vorschau_CloseEvent;
        internal event Vorschau_FocusedEventHandler Vorschau_FocusedEvent;

        // Suponemos que existe este campo BACKING para el modelo
        private Class_Model Pri_Model_Class;

        // Aquí iría la inicialización de los menús/contextos etc.,
        // que defina CMI_Favoriten_Record, CMI_Stop_Off, CMI_StreamRefresh, PAN_Control, etc.

        private void CMI_Favoriten_Record_Click(object sender, EventArgs e)
        {
            Favoriten_Record();
        }

        private void CMI_Stop_Off_Click(object sender, EventArgs e)
        {
            try
            {
                Pri_Model_Class.Pro_Aufnahme_Stop_Off = !Pri_Model_Class.Pro_Aufnahme_Stop_Off;
                // Asumimos que CMI_Stop_Off es un ToolStripMenuItem
                CMI_Stop_Off.Checked = Pri_Model_Class.Pro_Aufnahme_Stop_Off;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Stream.CMI_Stop_Off");
            }
        }

        private void CMI_StreamRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                var result = Pri_Model_Class.Model_Stream_Adressen_Load_Back().Result;
                if (result == null)
                    return;
                MessageBox.Show(result);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Stream.CMI_StreamRefresh_Click");
            }
        }

        private void Control_Stream_ControlAdded(object sender, ControlEventArgs e)
        {
            // TODO: manejar control añadido si es necesario
        }

        private void Control_Stream_GotFocus(object sender, EventArgs e)
        {
            // TODO: manejar foco si es necesario
        }

        private void PAN_Control_Click(object sender, EventArgs e)
        {
            var handler = Vorschau_FocusedEvent;
            handler?.Invoke(this);
        }

        // Método de ejemplo que lanza el evento de favoritos
        private void Favoriten_Record()
        {
            var handler = StreamRecord_Start;
            handler?.Invoke(this);
        }

        // Llaves de los menús (definidas en el InitializeComponent)
        // Asegúrate de que estos campos existan en tu diseñador:
        private ToolStripMenuItem CMI_Stop_Off;
        private ToolStripMenuItem CMI_StreamRefresh;
    }
}
