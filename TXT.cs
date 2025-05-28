using System.Text;

namespace XstreaMonNET8
{
    public class TXT
    {
        private static readonly List<TXT_Item> TXT_List = new();

        internal TXT(string languageFile)
        {
            if (!File.Exists(languageFile)) return;
            TXT_List.Clear();
            Language_Read(languageFile);
        }

        internal void Language_Read(string languageFile)
        {
            if (!File.Exists(languageFile)) return;

            string[] lines = File.ReadAllLines(languageFile, Encoding.UTF8);

            foreach (var line in lines)
            {
                var parts = line.Split('|');
                if (parts.Length > 1)
                {
                    var existing = TXT_List.Find(p => string.Equals(p.Pro_Name, parts[0], StringComparison.OrdinalIgnoreCase));
                    if (existing != null)
                    {
                        existing.Pro_Description = parts[1];
                    }
                    else
                    {
                        TXT_List.Add(new TXT_Item(parts[0], parts[1]));
                    }
                }
            }
        }

        internal static string TXT_Description(string proName)
        {
            var item = TXT_List.Find(p => string.Equals(p.Pro_Name, proName, StringComparison.OrdinalIgnoreCase));
            return item != null ? item.Pro_Description : proName;
        }

        public static void Control_Languages(Form form)
        {
            try
            {
                foreach (Control ctrl in form.Controls)
                {
                    UpdateControlText(ctrl);
                }
            }
            catch { }
        }

        public static void Control_Text_Change(Control container)
        {
            try
            {
                foreach (Control ctrl in container.Controls)
                {
                    UpdateControlText(ctrl);
                }
            }
            catch { }
        }

        private static void UpdateControlText(Control control)
        {
            if (control.HasChildren)
            {
                Control_Text_Change(control);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(control.Text))
                {
                    control.Text = TXT_Description(control.Text);
                }
            }

            // Aplicar a ciertos controles explícitamente si se desea
            if (control is TextBox || control is ComboBox || control is DateTimePicker || control is Label || control is Button)
            {
                if (!string.IsNullOrWhiteSpace(control.Text))
                {
                    control.Text = TXT_Description(control.Text);
                }
            }
        }
    }
}
