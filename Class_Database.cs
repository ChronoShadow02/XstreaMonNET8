using System.Collections.Generic;

namespace XstreaMonNET8
{
    public class Class_Database
    {
        internal string Datatable_Name { get; set; }
        internal List<Class_Datatable_Field> Datatable_fields { get; set; }

        internal Class_Database(string Table_Name, List<Class_Datatable_Field> Fields)
        {
            Datatable_Name = Table_Name;
            Datatable_fields = Fields;
        }
    }
}
