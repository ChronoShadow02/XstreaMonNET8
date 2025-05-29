namespace XstreaMonNET8
{
    public class Class_Speicherformate
    {
        internal int Pro_ID { get; set; }

        internal string Pro_Speicherformat_Name { get; set; }

        internal string Pro_Speicherformat_File_Ext { get; set; }

        internal string Pro_Speicherformat_Parameter { get; set; }

        public Class_Speicherformate(int ID, string Name, string File_Ext, string Parameter)
        {
            Pro_ID = ID;
            Pro_Speicherformat_Name = Name;
            Pro_Speicherformat_File_Ext = File_Ext;
            Pro_Speicherformat_Parameter = Parameter;
        }
    }
}
