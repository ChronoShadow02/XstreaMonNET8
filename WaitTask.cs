using System.Diagnostics;
using Timer = System.Timers.Timer;

namespace XstreaMonNET8
{
    public class WaitTask
    {
        private readonly int Wait_Interval;
        private readonly int Prozess_ID;
        private readonly Timer Wait_Timer;
        private bool Pri_Task_Ready;

        public event Evt_Task_ReadyEventHandler Evt_Task_Ready;

        public bool Pro_Task_Ready
        {
            get => Pri_Task_Ready;
            set => Pri_Task_Ready = value;
        }

        public delegate void Evt_Task_ReadyEventHandler(int taskId);

        public WaitTask(int taskId, int intervall)
        {
            try
            {
                Prozess_ID = taskId;
                Wait_Interval = intervall;
                Wait_Timer = new Timer
                {
                    Interval = Wait_Interval
                };
                Wait_Timer.Elapsed += (s, e) => Prozess_Check();
                Wait_Timer.Start();
            }
            catch (Exception ex)
            {
                // Se omite el manejo personalizado de errores
                Console.WriteLine($"Error en WaitTask constructor: {ex.Message}");
            }
        }

        private void Prozess_Check()
        {
            try
            {
                Wait_Timer.Stop();

                if (Prozess_ID > 1)
                {
                    try
                    {
                        var process = Process.GetProcessById(Prozess_ID);
                        if (process != null && !process.HasExited)
                        {
                            Wait_Timer.Start();
                            return;
                        }
                    }
                    catch
                    {
                        // Ignorar excepciones si el proceso no existe
                    }
                }

                Pro_Task_Ready = true;
                Evt_Task_Ready?.Invoke(Prozess_ID);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Prozess_Check: {ex.Message}");
            }
        }
    }
}
