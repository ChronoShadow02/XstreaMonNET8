using System.ComponentModel;

namespace XstreaMonNET8
{
    public class License
    {
        public long? ID { get; set; }
        public long? OrderID { get; set; }
        public long? ProductID { get; set; }
        public string LicenseKey { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public int? ValidFor { get; set; }
        public string Source { get; set; }

        [Description("(1) Sold  (2) Delivered  (3) Active  (4) Inactive")]
        public LicenseStatus Status { get; set; }

        public int? TimesActivated { get; set; }
        public int? TimesActivatedMax { get; set; }

        [Description("Only populated during the License Validate request")]
        public int? RemainingActivations { get; set; }

        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public long? UpdatedBy { get; set; }

        public override string ToString()
        {
            string activated = TimesActivated?.ToString() ?? "null";
            string maxActivated = TimesActivatedMax?.ToString() ?? "null";
            string expires = ExpiresAt?.ToString("yyyy-MM-dd") ?? "null";

            return $"Key: {LicenseKey} | Status: {Status} | Activation Count: {activated}/{maxActivated} | Expires: {expires}";
        }
    }
}
