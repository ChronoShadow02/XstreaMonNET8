namespace XstreaMonNET8
{
    public class TXT_Item
    {
        internal string Pro_Name { get; set; }

        internal string Pro_Description { get; set; }

        internal TXT_Item(string _Pro_Name, string _Pro_Description)
        {
            Pro_Name = _Pro_Name;
            Pro_Description = _Pro_Description;
        }
    }
}
