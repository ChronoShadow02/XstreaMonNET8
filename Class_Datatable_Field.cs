namespace XstreaMonNET8
{
    public class Class_Datatable_Field
    {
        internal string Pro_Field_Name { get; set; }
        internal string Pro_Field_Typ { get; set; }
        internal bool Pro_Field_Identifer { get; set; }

        internal Class_Datatable_Field(string Field_Name, string Field_Typ, bool Field_Identifer)
        {
            Pro_Field_Name = Field_Name;
            Pro_Field_Typ = Field_Typ;
            Pro_Field_Identifer = Field_Identifer;
        }
    }
}
