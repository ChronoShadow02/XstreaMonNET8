namespace XstreaMonNET8
{
    public class Class_Manual_Record
    {
        internal Class_Stream_Record Pro_Channel_Stream { get; set; }

        internal string Pro_Channel_Name { get; set; }

        internal int Pro_Channel_Site { get; set; }

        internal string Pro_Channel_URL { get; set; }

        internal Class_Manual_Record(string Channel_Name, int Channel_Site, Class_Stream_Record Channel_Stream)
        {
            this.Pro_Channel_Name = Channel_Name;
            this.Pro_Channel_Site = Channel_Site;
            this.Pro_Channel_Stream = Channel_Stream;

            Class_Website classWebsite = Sites.Website_Find(this.Pro_Channel_Site);
            if (classWebsite == null)
                return;

            this.Pro_Channel_URL = classWebsite.Pro_URL + Channel_Name.ToString();
        }
    }
}
