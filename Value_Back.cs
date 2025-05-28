using System.Drawing.Imaging;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace XstreaMonNET8
{
    internal static class ValueBack
    {
        public static double Get_CDouble(object Item_value)
        {
            try
            {
                if (Item_value == null || string.IsNullOrEmpty(Item_value.ToString()))
                    return 0.0;

                return double.TryParse(Item_value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double result)
                    ? Math.Abs(result)
                    : 0.0;
            }
            catch
            {
                return 0.0;
            }
        }

        public static string Get_Zahl_Extract_From_String(string Item_String)
        {
            if (string.IsNullOrEmpty(Item_String)) return string.Empty;

            var sb = new StringBuilder();
            foreach (char c in Item_String)
            {
                if (char.IsDigit(c) || c == '-' || c == ',' || c == '.' || c == 'X')
                    sb.Append(c);
            }
            return sb.ToString();
        }

        public static int Get_CInteger(object IntegerValue)
        {
            if (IntegerValue == null) return 0;

            string str = IntegerValue.ToString()!;
            return !string.IsNullOrEmpty(str) && str != "∞" && int.TryParse(str, out int result) ? result : 0;
        }

        public static bool Get_Numeric_Similar(double First_Numeric, double Second_Numeric, int Similar_Range)
        {
            double tolerance = Similar_Range / 100.0;
            double aMin = First_Numeric * (1 - tolerance);
            double aMax = First_Numeric * (1 + tolerance);
            double bMin = Second_Numeric * (1 - tolerance);
            double bMax = Second_Numeric * (1 + tolerance);

            return First_Numeric > bMin && First_Numeric < bMax || Second_Numeric > aMin && Second_Numeric < aMax;
        }

        public static int Get_Duration_To_Seconds(string Duration_String)
        {
            int durationToSeconds = 0;
            try
            {
                if (!string.IsNullOrEmpty(Duration_String))
                {
                    MatchCollection matches = Regex.Matches(Duration_String, "\\d+");
                    if (matches.Count == 2)
                    {
                        double first = double.Parse(matches[0].Value, CultureInfo.InvariantCulture);
                        double second = double.Parse(matches[1].Value, CultureInfo.InvariantCulture);
                        durationToSeconds = Duration_String.Contains("h")
                            ? (int)Math.Round(first * 3600.0 + second * 60.0)
                            : (int)Math.Round(first * 60.0 + second);
                    }
                    else if (matches.Count == 1)
                    {
                        durationToSeconds = int.Parse(matches[0].Value, CultureInfo.InvariantCulture);
                    }
                }
            }
            catch
            {
                // Se ignora porque el valor por defecto ya es 0
            }
            return durationToSeconds;
        }

        public static bool Get_CBoolean(object _Boolean)
        {
            try
            {
                return _Boolean != null && Convert.ToBoolean(_Boolean);
            }
            catch
            {
                return false;
            }
        }

        public static Guid Get_CUnique(object _GUID)
        {
            string str = _GUID?.ToString()!;
            return Guid.TryParse(str, out Guid guid) ? guid : Guid.Empty;
        }

        public static DateTime Get_CDatum(object Datestring)
        {
            string str = Datestring?.ToString()!;
            return DateTime.TryParse(str, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsed)
                ? DateTime.SpecifyKind(parsed, DateTimeKind.Unspecified)
                : DateTime.MinValue;
        }

        public static string Get_SQL_Date(DateTime Datestring)
        {
            return $"#{Datestring.Month}/{Datestring.Day}/{Datestring.Year}#";
        }

        public static string Get_Numeric2Bytes(double b)
        {
            string[] units = ["Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB"];
            for (int y = units.Length - 1; y >= 0; y--)
            {
                double factor = Math.Pow(1024.0, y);
                if (b >= factor)
                    return ThreeNonZeroDigits(b / factor) + " " + units[y];
            }
            return "0 Bytes";
        }

        public static float Get_String_Width(string String_Value, Font String_Font)
        {
            using var bmp = new Bitmap(10, 10);
            using var g = Graphics.FromImage(bmp);
            return g.MeasureString(String_Value, String_Font).Width;
        }

        private static string ThreeNonZeroDigits(double value)
        {
            if (value < 10.0)
                return value.ToString("0.00", CultureInfo.InvariantCulture);
            else if (value < 100.0)
                return value.ToString("0.0", CultureInfo.InvariantCulture);
            else
                return ((int)Math.Round(value)).ToString(CultureInfo.InvariantCulture);
        }

        public static string MinutesToTime(float min)
        {
            TimeSpan time = TimeSpan.FromMinutes(min);
            return $"{(int)time.TotalHours:D2}:{time.Minutes:D2}:{time.Seconds:D2}";
        }

        public static int Alter(DateTime Berechnungs_Datum)
        {
            if (Berechnungs_Datum == DateTime.MinValue) return 0;

            DateTime today = DateTime.Today;
            int age = today.Year - Berechnungs_Datum.Year;
            if (Berechnungs_Datum > today.AddYears(-age)) age--;

            return age;
        }

        public static string ExtractSubAndMainDomainFromURL(string URL)
        {
            if (string.IsNullOrEmpty(URL)) return string.Empty;

            string cleanedUrl = URL.Split('?')[0];
            return cleanedUrl
                .Split('/')
                .FirstOrDefault(part => part.Contains('.')) ?? string.Empty;
        }

        public static DateTime TimeStampToDateTime(string unixTimeStamp)
        {
            double num = Get_CDouble(unixTimeStamp);
            return Math.Abs(num) > 1e-6
                ? DateTime.SpecifyKind(DateTime.UnixEpoch.AddSeconds(num / 1000.0), DateTimeKind.Utc)
                : DateTime.MinValue;
        }

        public static byte[] Get_CImageToByte(Image _Image, ImageFormat Imageformat)
        {
            if (_Image == null) return Array.Empty<byte>();

            using var memoryStream = new MemoryStream();
            _Image.Save(memoryStream, Imageformat);
            return memoryStream.ToArray();
        }

        public static Image Get_CBytesToImage(byte[] _Bytes)
        {
            try
            {
                if (_Bytes == null || _Bytes.Length == 0) return null!;
                using var memoryStream = new MemoryStream(_Bytes);
                return Image.FromStream(memoryStream);
            }
            catch
            {
                // Retorna null si la imagen es inválida
                return null!;
            }
        }

        public static int Get_Int_From_String(string input)
        {
            var match = Regex.Match(input, @"\d+");
            return match.Success ? int.Parse(match.Value) : 0;
        }

        public static int Month_From_String(string input)
        {
            string[] months = CultureInfo.InvariantCulture.DateTimeFormat.MonthNames;

            for (int i = 0; i < months.Length; i++)
            {
                if (!string.IsNullOrEmpty(months[i]) && input.IndexOf(months[i], StringComparison.OrdinalIgnoreCase) >= 0)
                    return i + 1;
            }

            return 0; // Si no encontró el mes
        }
    }
}
