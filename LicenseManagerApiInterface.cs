using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace XstreaMonNET8
{
    public class LicenseManagerApiInterface
    {
        private Dictionary<int, string> _licenseStatusMap;
        private Dictionary<LicenseRequestType, string> _licenseEndpointsMap;
        private Dictionary<Type, Dictionary<string, string>> _licensePropertyToDatabaseMap;
        private JsonMergeSettings _jsonMergeSettings;
        private readonly string _baseSiteURL;
        private readonly string _consumerKey;
        private readonly string _consumerSecret;

        public Dictionary<LicenseRequestType, string> LicenseEndpointsMap
        {
            get => _licenseEndpointsMap;
            set => _licenseEndpointsMap = value;
        }

        public Dictionary<int, string> LicenseStatusMap
        {
            get => _licenseStatusMap;
            set => _licenseStatusMap = value;
        }

        public Dictionary<Type, Dictionary<string, string>> LicensePropertyToDatabaseMap
        {
            get => _licensePropertyToDatabaseMap;
            set => _licensePropertyToDatabaseMap = value;
        }

        public JsonMergeSettings JsonMergeSettings
        {
            get => _jsonMergeSettings;
            set => _jsonMergeSettings = value;
        }

        public int WebClientTimeout { get; set; }

        public LicenseManagerApiInterface(string BaseSiteURL, string ConsumerKey, string ConsumerSecret)
        {
            _licenseStatusMap = new Dictionary<int, string>
            {
                { 1, "sold" },
                { 2, "delivered" },
                { 3, "active" },
                { 4, "inactive" }
            };

            _licenseEndpointsMap = new Dictionary<LicenseRequestType, string>
            {
                { LicenseRequestType.List, "/wp-json/lmfwc/v2/licenses/" },
                { LicenseRequestType.Retrieve, "/wp-json/lmfwc/v2/licenses/" },
                { LicenseRequestType.Activate, "/wp-json/lmfwc/v2/licenses/activate/" },
                { LicenseRequestType.Deactivate, "/wp-json/lmfwc/v2/licenses/deactivate" },
                { LicenseRequestType.Validate, "/wp-json/lmfwc/v2/licenses/validate/" },
                { LicenseRequestType.Create, "/wp-json/lmfwc/v2/licenses" },
                { LicenseRequestType.Update, "/wp-json/lmfwc/v2/licenses/" }
            };

            _licensePropertyToDatabaseMap = new Dictionary<Type, Dictionary<string, string>>
            {
                {
                    typeof(License),
                    new Dictionary<string, string>
                    {
                        { "LicenseKey", "license_key" },
                        { "ValidFor", "valid_for" },
                        { "OrderID", "order_id" },
                        { "ProductID", "product_id" },
                        { "ExpiresAt", "expires_at" },
                        { "TimesActivated", "times_activated" },
                        { "TimesActivatedMax", "times_activated_max" },
                        { "CreatedAt", "created_at" },
                        { "CreatedBy", "created_by" },
                        { "UpdatedAt", "updated_at" },
                        { "UpdatedBy", "updated_by" },
                        { "Source", "source" },
                        { "Status", "status" },
                        { "ID", "id" }
                    }
                }
            };

            _jsonMergeSettings = new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Union
            };

            WebClientTimeout = 100000;
            _baseSiteURL = BaseSiteURL;
            _consumerKey = ConsumerKey;
            _consumerSecret = ConsumerSecret;
        }

        public LicenseRequestOutcome LicenseRequest(
            LicenseRequestType RequestType,
            string LicenseKey = null,
            License License = null,
            string AdditionalParametersJson = null)
        {
            var webClientProcessor = new WebClientProcessor(_consumerKey, _consumerSecret)
            {
                WebClientTimeout = WebClientTimeout
            };

            var licenseRequestOutcome = new LicenseRequestOutcome();
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DynamicMappingResolver(_licensePropertyToDatabaseMap)
            };

            switch (RequestType)
            {
                case LicenseRequestType.List:
                    var responseList = webClientProcessor.HttpAction(StringOp.UrlCombine(_baseSiteURL, _licenseEndpointsMap[RequestType]), HttpMethod.Get);
                    if (responseList.Success)
                    {
                        var parsed = JsonConvert.DeserializeObject<LicenseResponseCollection>(responseList.ReturnedString);
                        licenseRequestOutcome.ProcessOutcome = ProcessOutcome.Success;
                        licenseRequestOutcome.APIJsonString = responseList.ReturnedString;
                        licenseRequestOutcome.APIReturnedSuccess = parsed.Success;
                        licenseRequestOutcome.Licences = parsed.Data.ToList();
                    }
                    else
                    {
                        licenseRequestOutcome.ProcessOutcome = ProcessOutcome.WebClientError;
                        licenseRequestOutcome.WebClientException = responseList.Exception;
                    }
                    break;

                case LicenseRequestType.Retrieve:
                case LicenseRequestType.Activate:
                case LicenseRequestType.Deactivate:
                case LicenseRequestType.Validate:
                    if (string.IsNullOrEmpty(LicenseKey))
                    {
                        licenseRequestOutcome.ProcessOutcome = ProcessOutcome.LicenseKeyNotPassedError;
                    }
                    else
                    {
                        var response = webClientProcessor.HttpAction(StringOp.UrlCombine(_baseSiteURL, _licenseEndpointsMap[RequestType], LicenseKey), HttpMethod.Get);
                        licenseRequestOutcome = ProcessLicenseWebClientResponse(response);
                    }
                    break;

                case LicenseRequestType.Create:
                case LicenseRequestType.Update:
                    if (string.IsNullOrEmpty(LicenseKey) && RequestType == LicenseRequestType.Update)
                    {
                        licenseRequestOutcome.ProcessOutcome = ProcessOutcome.LicenseKeyNotPassedError;
                        break;
                    }

                    if (License == null)
                    {
                        licenseRequestOutcome.ProcessOutcome = ProcessOutcome.LicenceObjectRequiredError;
                        break;
                    }

                    if (AdditionalParametersJson != null && !StringOp.JsonIsParsable(AdditionalParametersJson))
                    {
                        licenseRequestOutcome.ProcessOutcome = ProcessOutcome.AdditionalParametersJsonNonParsable;
                        break;
                    }

                    string serialized = JsonConvert.SerializeObject(License, settings);
                    var statusMap = _licenseStatusMap.ToDictionary(x => x.Key.ToString(), x => x.Value);
                    string jsonFinal = StringOp.JsonReplaceKeyValue(serialized, "status", statusMap);

                    if (AdditionalParametersJson != null)
                        jsonFinal = StringOp.JsonMerge(jsonFinal, AdditionalParametersJson, _jsonMergeSettings);

                    var method = RequestType == LicenseRequestType.Create ? HttpMethod.Post : HttpMethod.Put;
                    string url = RequestType == LicenseRequestType.Create
                        ? StringOp.UrlCombine(_baseSiteURL, _licenseEndpointsMap[RequestType])
                        : StringOp.UrlCombine(_baseSiteURL, _licenseEndpointsMap[RequestType], LicenseKey);

                    var responseFinal = webClientProcessor.HttpAction(url, method, jsonFinal);
                    licenseRequestOutcome = ProcessLicenseWebClientResponse(responseFinal);
                    break;
            }

            return licenseRequestOutcome;
        }

        private LicenseRequestOutcome ProcessLicenseWebClientResponse(WebClientResponse wcr)
        {
            var licenseRequestOutcome = new LicenseRequestOutcome();

            if (wcr.Success)
            {
                var parsed = JsonConvert.DeserializeObject<LicenseResponseSingle>(wcr.ReturnedString);
                licenseRequestOutcome.ProcessOutcome = ProcessOutcome.Success;
                licenseRequestOutcome.APIJsonString = wcr.ReturnedString;
                licenseRequestOutcome.APIReturnedSuccess = parsed.Success;
                licenseRequestOutcome.Licences = new List<License> { parsed.Data };
            }
            else
            {
                licenseRequestOutcome.ProcessOutcome = ProcessOutcome.WebClientError;
                licenseRequestOutcome.APIJsonString = wcr.ReturnedString;
                licenseRequestOutcome.WebClientException = wcr.Exception;
            }

            return licenseRequestOutcome;
        }

        private class LicenseResponseSingle
        {
            [JsonProperty("success")]
            public bool Success { get; set; } = false;

            [JsonProperty("data")]
            public License Data { get; set; }
        }

        private class LicenseResponseCollection
        {
            [JsonProperty("success")]
            public bool Success { get; set; } = false;

            [JsonProperty("data")]
            public IList<License> Data { get; set; }
        }
    }
}
