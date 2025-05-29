namespace XstreaMonNET8
{
    public class Control_Clear_Dispose
    {
        public static async void Clear(Control.ControlCollection controls)
        {
            await Task.CompletedTask;

            if (controls == null || controls.Count == 0)
                return;

            try
            {
                foreach (Control control in controls)
                {
                    control.Dispose();
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Clear_Dispose.Clear");
            }

            controls.Clear();
        }
    }
}
