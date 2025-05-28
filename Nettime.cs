using System.Net.Sockets;
using System.Text;

namespace XstreaMonNET8
{
    public class Nettime
    {
        private const int THRESHOLD_SECONDS = 15;

        private static readonly string[] Servers =
        [
            "129.6.15.28",
            "129.6.15.29",
            "132.163.4.101",
            "132.163.4.102",
            "132.163.4.103",
            "128.138.140.44",
            "131.107.1.10",
            "216.200.93.8",
            "208.184.49.9",
            "207.126.98.204"
        ];

        public static string LastHost = "";
        public static DateTime LastSysTime;

        public static DateTime GetTime()
        {
            LastHost = "";
            DateTime t1 = DateTime.MinValue;

            foreach (var host in Servers)
            {
                t1 = GetNISTTime(host);
                if (t1 > DateTime.MinValue)
                {
                    LastHost = host;
                    break;
                }
            }

            return string.IsNullOrEmpty(LastHost) ? DateTime.UtcNow : t1;
        }

        public static int SecondsDifference(DateTime dt1, DateTime dt2)
        {
            TimeSpan timeSpan = dt1 - dt2;
            return timeSpan.Seconds + timeSpan.Minutes * 60 + timeSpan.Hours * 360;
        }

        public static bool WindowsClockIncorrect()
        {
            return Math.Abs(SecondsDifference(GetTime(), LastSysTime)) > THRESHOLD_SECONDS;
        }

        private static DateTime GetNISTTime(string host)
        {
            string responseText;

            try
            {
                using var tcpClient = new TcpClient(host, 13);
                using var stream = tcpClient.GetStream();
                using var reader = new StreamReader(stream, Encoding.ASCII);
                LastSysTime = DateTime.UtcNow;
                responseText = reader.ReadToEnd();
            }
            catch
            {
                return DateTime.Now;
            }

            try
            {
                if (responseText.Length < 50 || !responseText.Substring(38, 9).Equals("UTC(NIST)", StringComparison.OrdinalIgnoreCase))
                    return DateTime.Now;

                if (!responseText.Substring(30, 1).Equals("0"))
                    return DateTime.Now;

                int mjd = int.Parse(responseText.Substring(1, 5));
                int yearOffset = int.Parse(responseText.Substring(7, 2));
                int month = int.Parse(responseText.Substring(10, 2));
                int day = int.Parse(responseText.Substring(13, 2));
                int hour = int.Parse(responseText.Substring(16, 2));
                int minute = int.Parse(responseText.Substring(19, 2));
                int second = int.Parse(responseText.Substring(22, 2));

                if (mjd >= 15020)
                {
                    int year = (mjd <= 51544) ? (yearOffset + 1900) : (yearOffset + 2000);
                    return new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);
                }

                return DateTime.Now;
            }
            catch
            {
                return DateTime.Now;
            }
        }


        public struct SYSTEMTIME
        {
            public short wYear;
            public short wMonth;
            public short wDayOfWeek;
            public short wDay;
            public short wHour;
            public short wMinute;
            public short wSecond;
            public short wMilliseconds;
        }
    }
}
