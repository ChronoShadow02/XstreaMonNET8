using System.ComponentModel;
using System.Runtime.CompilerServices;
using Timer = System.Windows.Forms.Timer;

namespace XstreaMonNET8
{
    public class Class_Model_Online_Check
    {
        private Guid Pri_Model_GUID;
        private int Pri_WebSite_ID;
        private string Pri_Model_Name;
        private bool Pri_Model_Online;
        internal int BGW_Result;
        private int BGW_Check;
        private int BGW_Last_Online_Status;

        private System.Windows.Forms.Timer _Pri_CheckTimer;
        private BackgroundWorker _Media_Worker;

        private System.Windows.Forms.Timer Pri_CheckTimer
        {
            get => _Pri_CheckTimer;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_Pri_CheckTimer != null)
                    _Pri_CheckTimer.Tick -= Pri_CheckTimer_Ticks;

                _Pri_CheckTimer = value;

                if (_Pri_CheckTimer != null)
                    _Pri_CheckTimer.Tick += Pri_CheckTimer_Ticks;
            }
        }

        private BackgroundWorker Media_Worker
        {
            get => _Media_Worker;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_Media_Worker != null)
                {
                    _Media_Worker.DoWork -= Check_Thread;
                    _Media_Worker.RunWorkerCompleted -= Check_Thread_Complete;
                }

                _Media_Worker = value;

                if (_Media_Worker != null)
                {
                    _Media_Worker.DoWork += Check_Thread;
                    _Media_Worker.RunWorkerCompleted += Check_Thread_Complete;
                }
            }
        }

        internal DateTime BGW_Last_Check { get; set; }

        internal bool get_Pro_Model_Online(bool Check = false)
        {
            return !(Check || BGW_Last_Check == DateTime.MinValue)
                ? Pri_Model_Online
                : Sites.Model_Online(Pri_Model_Name, Pri_WebSite_ID) != 0;
        }

        internal int Pro_Timer_Intervall
        {
            get => Pri_CheckTimer.Interval;
            set
            {
                if (value == 0)
                {
                    Pri_CheckTimer.Stop();
                }
                else
                {
                    if (Pri_CheckTimer.Enabled)
                        Pri_CheckTimer.Stop();

                    Pri_CheckTimer.Interval = value;

                    if (!Pri_CheckTimer.Enabled)
                        Pri_CheckTimer.Start();
                }
            }
        }

        public Class_Model_Online_Check(Guid Model_GUID, int Website_ID, string Model_Name, int TimerIntervall, bool instant_Check)
        {
            Pri_CheckTimer = new Timer();
            BGW_Last_Online_Status = 0;
            Media_Worker = new BackgroundWorker();

            try
            {
                Pri_Model_GUID = Model_GUID;
                Pri_WebSite_ID = Website_ID;
                Pri_Model_Name = Model_Name;
                Pri_CheckTimer.Interval = TimerIntervall;

                if (instant_Check)
                    Pri_CheckTimer_Ticks(null, EventArgs.Empty);
                else
                    Pri_CheckTimer.Start();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, $"Class_Model_Online_Check.New({Model_GUID})");
            }
        }

        private void Pri_CheckTimer_Ticks(object sender, EventArgs e)
        {
            try
            {
                if (Media_Worker.IsBusy)
                    return;

                Pri_CheckTimer.Stop();
                Media_Worker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Model_Online_Check.Pri_CheckTimer_Ticks");
            }
        }

        private void Check_Thread(object sender, DoWorkEventArgs e)
        {
            try
            {
                BGW_Check = Sites.Model_Online(Pri_Model_Name, Pri_WebSite_ID);

                if (BGW_Check != 1)
                    return;

                var result = Class_Model_List.Class_Model_Find(Pri_Model_GUID).Result;

                if (result == null || Parameter.URL_Response(result?.Pro_Model_M3U8).Result)
                    return;

                result.Model_Stream_Adressen_Load();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Model_Online_Check.Check_Thread");
            }
        }

        private void Check_Thread_Complete(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (!Pri_CheckTimer.Enabled)
                    Pri_CheckTimer.Start();

                if ((BGW_Check > 0) != get_Pro_Model_Online())
                {
                    Pri_Model_Online = BGW_Check > 0;
                    Online_Changed?.Invoke(get_Pro_Model_Online());
                }

                if (BGW_Check != BGW_Last_Online_Status)
                {
                    Online_Status_Change?.Invoke();
                    BGW_Last_Online_Status = BGW_Check;
                }

                Pri_CheckTimer.Start();
                BGW_Result = BGW_Check;
                BGW_Last_Check = DateTime.Now;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Model_Online_Check.Check_Thread_Complete");
            }
        }

        internal void Check_Run() => Pri_CheckTimer_Ticks(null, EventArgs.Empty);

        internal event Online_ChangedEventHandler Online_Changed;
        internal event Online_Status_ChangeEventHandler Online_Status_Change;

        internal delegate void Online_ChangedEventHandler(bool Online_Status);
        internal delegate void Online_Status_ChangeEventHandler();
    }
}
