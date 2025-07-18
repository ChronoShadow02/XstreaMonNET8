namespace XstreaMonNET8
{
    public class Class_Record_Manual
    {
        static Class_Record_Manual()
        {
            Manual_Record_List = new List<Class_Manual_Record>();
        }

        public static event Evt_List_ChangedEventHandler Evt_List_Changed;

        public static DateTime Pro_Record_Start { get; set; }

        public static List<Class_Manual_Record> Manual_Record_List { get; set; }

        internal static void New_Record(string Channel_Name, int Channel_Site, Class_Stream_Record Channel_Stream)
        {
            try
            {
                Manual_Record_List.Add(new Class_Manual_Record(Channel_Name, Channel_Site, Channel_Stream));
                Pro_Record_Start = Channel_Stream.Pro_Record_Beginn;
                Evt_List_Changed?.Invoke();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Record_Manual.New_Record");
            }
        }

        internal static Class_Manual_Record Find_Record(string Channel_Name, int Channel_Site)
        {
            try
            {
                foreach (var manualRecord in Manual_Record_List)
                {
                    if (string.Equals(manualRecord.Pro_Channel_Name, Channel_Name, StringComparison.OrdinalIgnoreCase) &&
                        manualRecord.Pro_Channel_Site == Channel_Site)
                    {
                        return manualRecord;
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Record_Manual.Find_Record");
            }

            return null;
        }

        internal static void Stop_Record(string Channel_Name, int Channel_Site)
        {
            try
            {
                var record = Find_Record(Channel_Name, Channel_Site);
                if (record == null)
                    return;

                record.Pro_Channel_Stream?.Stream_Record_Stop();
                Manual_Record_List.Remove(record);
                Evt_List_Changed?.Invoke();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Record_Manual.Stop_Record");
            }
        }

        internal static void Stop_Record(Class_Manual_Record Manual_Record)
        {
            try
            {
                if (Manual_Record == null)
                    return;

                Manual_Record_List.Remove(Manual_Record);
                Manual_Record = null;
                Evt_List_Changed?.Invoke();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Record_Manual.Stop_Record");
            }
        }

        public delegate void Evt_List_ChangedEventHandler();
    }
}
