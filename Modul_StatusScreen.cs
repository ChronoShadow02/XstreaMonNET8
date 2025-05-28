namespace XstreaMonNET8
{
    internal static class Modul_StatusScreen
    {
        internal static Form_Status? StatusForm;

        internal static void Status_Show(string Status_Text)
        {
            try
            {
                if (StatusForm == null)
                {
                    var formStatus = new Form_Status
                    {
                        StartPosition = FormStartPosition.CenterScreen,
                        TopMost = false
                    };
                    StatusForm = formStatus;
                    StatusForm.Show();
                }

                StatusForm.Pro_Status_Text = Status_Text;

                if (!string.IsNullOrEmpty(Status_Text)) return;

                StatusForm.Dispose();
                StatusForm = null;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Parameter.Status_Show");
                StatusForm?.Dispose();
            }
        }
    }
}
