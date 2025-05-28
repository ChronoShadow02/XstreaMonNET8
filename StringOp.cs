using Newtonsoft.Json.Linq;

namespace XstreaMonNET8
{
    internal class StringOp
    {
        public static string UrlCombine(string baseUrl, params string[] segments)
        {
            string trimmedBase = baseUrl.TrimEnd('/');
            var allSegments = new[] { trimmedBase }.Concat(segments.Select(s => s.Trim('/')));
            return string.Join("/", allSegments);
        }

        public static string JsonReplaceKeyValue(string json, string key, Dictionary<string, string> values)
        {
            JObject jobject = JObject.Parse(json);
            string currentValue = jobject[key]?.ToString();

            if (currentValue != null && values.ContainsKey(currentValue))
                jobject[key] = values[currentValue];

            return jobject.ToString();
        }

        public static string JsonMerge(string json1, string json2, JsonMergeSettings mergeMethod)
        {
            JObject j1 = JObject.Parse(json1);
            JObject j2 = JObject.Parse(json2);
            j1.Merge(j2, mergeMethod);
            return j1.ToString();
        }

        public static bool JsonIsParsable(string json)
        {
            try
            {
                JObject.Parse(json);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
