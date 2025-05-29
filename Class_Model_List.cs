namespace XstreaMonNET8
{
    public class Class_Model_List
    {
        private static List<Class_Model> _model_List = [];

        internal static int Pro_Count
        {
            get
            {
                int proCount = 0;
                foreach (Class_Model model in Model_List)
                {
                    if (!model.Pro_Model_Promo)
                        proCount++;
                }
                return proCount;
            }
        }

        internal static int Pro_Records
        {
            get
            {
                int proRecords = 0;
                foreach (Class_Model model in Model_List)
                {
                    if (model.Pro_Model_Stream_Record != null && Parameter.Task_Runs(model.Pro_Model_Stream_Record.ProzessID))
                        proRecords++;
                }
                return proRecords;
            }
        }

        internal static int Pro_Online
        {
            get
            {
                int proOnline = 0;
                foreach (Class_Model model in Model_List)
                {
                    if (model.get_Pro_Model_Online())
                        proOnline++;
                }
                return proOnline;
            }
        }

        internal static List<Class_Model> Model_List
        {
            get => _model_List;
            set => _model_List = value;
        }

        public static void Model_Add(Class_Model New_Model)
        {
            _model_List.Add(New_Model);
        }

        public static void Model_Del(Class_Model Del_Model)
        {
            _model_List.Remove(Del_Model);
        }

        public static async Task<Class_Model> Class_Model_Find(Guid Model_GUID)
        {
            await Task.CompletedTask;
            try
            {
                foreach (Class_Model model in Model_List)
                {
                    if (model.Pro_Model_GUID == Model_GUID)
                        return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.Class_Model_Find");
                return null;
            }
        }

        public static async Task<Class_Model> Class_Model_Find(int website_ID, string Model_Name)
        {
            await Task.CompletedTask;
            try
            {
                return Model_List.Find(C_Model =>
                    string.Equals(C_Model.Pro_Model_Name, Model_Name, StringComparison.OrdinalIgnoreCase)
                    && C_Model.Pro_Website_ID == website_ID);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.Class_Model_Find");
                return null!;
            }
        }
    }
}
