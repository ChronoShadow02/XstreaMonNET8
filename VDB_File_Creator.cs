using System.ComponentModel;
using System.Timers;

namespace XstreaMonNET8
{
    public static class VDB_File_Creator
    {
        internal static List<BackgroundWorker> VDB_File_Worker = [];
        internal static bool Worker_Run;

        private static System.Timers.Timer _BGW_ListCheck;

        static VDB_File_Creator()
        {
            BGW_ListCheck = new System.Timers.Timer(1000);
            BGW_ListCheck.Elapsed += BGW_ListCheck_Elapsed;
            Worker_Run = false;
        }

        internal static System.Timers.Timer BGW_ListCheck
        {
            get => _BGW_ListCheck;
            set
            {
                if (_BGW_ListCheck != null)
                    _BGW_ListCheck.Elapsed -= BGW_ListCheck_Elapsed;

                _BGW_ListCheck = value;

                if (_BGW_ListCheck != null)
                    _BGW_ListCheck.Elapsed += BGW_ListCheck_Elapsed;
            }
        }

        internal static void WorkAdd(BackgroundWorker bgw)
        {
            try
            {
                VDB_File_Worker.Add(bgw);
                BGW_ListCheck.Start();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "VDB_File_Creator.WorkAdd");
            }
        }

        internal static void BgW_Run()
        {
            try
            {
                Worker_Run = true;
                List<BackgroundWorker> workersSnapshot = new(VDB_File_Worker);

                foreach (BackgroundWorker worker in workersSnapshot)
                {
                    worker.RunWorkerAsync();
                    while (worker.IsBusy)
                    {
                        Application.DoEvents();
                    }
                    VDB_File_Worker.Remove(worker);
                }

                Worker_Run = false;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "VDB_File_Creator.BgW_Run");
            }
        }

        internal static void BGW_ListCheck_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (VDB_File_Worker.Count == 0)
                {
                    BGW_ListCheck.Stop();
                }
                else
                {
                    if (!Worker_Run)
                        BgW_Run();
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "VDB_File_Creator.BGW_ListCheck_Elapsed");
            }
        }
    }
}
