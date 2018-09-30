using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsParty.HelpersAndExtensions
{
    public static class RestRequestExtension
    {
        public static IRestRequest AddLowerCamelJsonBody(this IRestRequest request, object obj)
        {
            var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var jsonString = JsonConvert.SerializeObject(obj, Formatting.Indented, jsonSerializerSettings);
            request.AddParameter("application/json", jsonString, ParameterType.RequestBody);
            return request;
        }
    }
}
