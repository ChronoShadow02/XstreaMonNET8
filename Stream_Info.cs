namespace XstreaMonNET8
{
    public class Stream_Info
    {
        internal int Bandwith { get; set; }
        internal string Resolution { get; set; }
        internal string Name { get; set; }
        internal string M3U8String { get; set; }

        internal Stream_Info(string Stream_String)
        {
            if (Stream_String.Length <= 0)
                return;

            // Intenta obtener el ancho de banda desde la etiqueta BANDWIDTH
            this.Bandwith = Value_Back.get_CInteger((object)VParse.HTML_Value(Stream_String.ToLower(), "bandwidth=", ","));

            // Si no se encontró bandwidth, intenta deducirlo desde la resolución
            if (this.Bandwith == 0)
                this.Bandwith = Conversions.ToInteger(VParse.HTML_Value(Stream_String.ToLower(), "resolution=", "x"));

            // Obtiene la resolución (por ejemplo: 1280x720)
            this.Resolution = VParse.HTML_Value(Stream_String.ToLower(), "resolution=", ",");

            // Obtiene el nombre del stream (si existe)
            this.Name = VParse.HTML_Value(Stream_String.ToLower(), "name=", "https");

            // Obtiene la URL HTTPS del stream
            this.M3U8String = "https:" + VParse.HTML_Value(Stream_String.ToLower(), "https:", "");
        }
    }
}
