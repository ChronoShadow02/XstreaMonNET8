using System.ComponentModel;

namespace XstreaMonNET8
{
    public class LicenseRequestOutcome
    {
        public ProcessOutcome ProcessOutcome { get; set; } = ProcessOutcome.Indeterminate;

        public Exception? WebClientException { get; set; }

        public bool APIReturnedSuccess { get; set; }

        public string? APIJsonString { get; set; }

        public List<License> Licences { get; set; } = [];

        public override string ToString()
        {
            var result = $"Process outcome: {ProcessOutcome} | API Success: {APIReturnedSuccess} | Licenses count: {Licences.Count}";

            if (!APIReturnedSuccess || ProcessOutcome != ProcessOutcome.Indeterminate)
            {
                result += $"\r\nWebclient Exception: {WebClientException?.Message}\r\nAPI Json:\r\n{APIJsonString}";
            }

            return result;
        }
    }
}
