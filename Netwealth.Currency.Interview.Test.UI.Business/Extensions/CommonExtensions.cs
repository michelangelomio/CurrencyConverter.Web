using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;

namespace Netwealth.Currency.Interview.Test.UI.Business.Extensions
{
    public static class CommonExtensions
    {
        public static bool IsValidJson(this string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return false;
            }

            try
            {
                JToken.Parse(content);
                return true;
            }
            catch (JsonReaderException value)
            {
                Trace.WriteLine(value);
                return false;
            }
            catch (Exception value2)
            {
                Trace.WriteLine(value2);
                return false;
            }
        }
    }
}
