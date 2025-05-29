namespace XstreaMonNET8
{
    public class Class_Decoder_Item
    {
        internal int Decoder_ID { get; set; }
        internal string Decoder_Name { get; set; }
        internal string Decoder_Parameter { get; set; }
        internal string Decoder_Extension { get; set; }
        internal bool Decorder_Default_Select { get; set; }
        internal bool Decoder_CanConvert { get; set; }

        public Class_Decoder_Item(
            int ID,
            string Name,
            string Parameter,
            string Extension,
            bool Default_Select,
            bool CanConvert)
        {
            Decoder_ID = ID;
            Decoder_Name = Name;
            Decoder_Parameter = Parameter;
            Decoder_Extension = Extension;
            Decorder_Default_Select = Default_Select;
            Decoder_CanConvert = CanConvert;
        }
    }
}
