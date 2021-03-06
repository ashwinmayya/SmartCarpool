﻿//Copyright 2014 Microsoft

//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at

//       http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Util
{
	public class AzureSearchHelper
	{
		public const string ApiVersionStringPreview = "api-version=2014-07-31-Preview";
		public const string ApiVersionString = "api-version=2015-02-28";
		private static readonly Uri _serviceUri;
		private static HttpClient _httpClient;
		private static readonly JsonSerializerSettings _jsonSettings;

		static AzureSearchHelper()
		{
			_jsonSettings = new JsonSerializerSettings
			{
				Formatting = Formatting.Indented, // for readability, change to None for compactness
				ContractResolver = new CamelCasePropertyNamesContractResolver(),
				DateTimeZoneHandling = DateTimeZoneHandling.Utc
			};

			_jsonSettings.Converters.Add(new StringEnumConverter());

			_serviceUri = new Uri("https://" + ConfigurationManager.AppSettings["SearchServiceName"] + ".search.windows.net");
			_httpClient = new HttpClient();
			_httpClient.DefaultRequestHeaders.Add("api-key", ConfigurationManager.AppSettings["SearchServiceApiKey"]);
		}
		public static void IndexBatch(List<Dictionary<string, object>> changes)
		{
			var batch = new
			{
				value = changes
			};

			Uri uri = new Uri(_serviceUri, "/indexes/" + ConfigurationManager.AppSettings["IndexName"] + "/docs/index");
			string json = AzureSearchHelper.SerializeJson(batch);
			HttpResponseMessage response = AzureSearchHelper.SendSearchRequest(_httpClient, HttpMethod.Post, uri, json);
			response.EnsureSuccessStatusCode();
		}
		public static dynamic Search(double sLat, double sLon, double eLat, double eLon)
		{
			string search = "search=*";
			//string filter = "&$filter=geo.distance(location,geography'POINT(" + EscapeODataString(lon) + " " + EscapeODataString(lat) + ")')+le+" + EscapeODataString(distance.ToString());
			string filter = "&$filter=geo.intersects(startLocation, geography'POLYGON(("
			           + sLon + " " + sLat + ", "
			           + eLon + " " + sLat + ", "
			           + eLon + " " + eLat + ", "
			           + sLon + " " + eLat + ", "
			           + sLon + " " + sLat
			           + "))') and geo.intersects(endLocation, geography'POLYGON(("
			           + sLon + " " + sLat + ", "
			           + eLon + " " + sLat + ", "
			           + eLon + " " + eLat + ", "
			           + sLon + " " + eLat + ", "
			           + sLon + " " + sLat
			           + "))')";
			Uri uri = new Uri(_serviceUri, "/indexes/"+ ConfigurationManager.AppSettings["IndexName"] + "/docs?" + search + filter);
			HttpResponseMessage response = AzureSearchHelper.SendSearchRequest(_httpClient, HttpMethod.Get, uri);
			AzureSearchHelper.EnsureSuccessfulSearchResponse(response);

			return AzureSearchHelper.DeserializeJson<dynamic>(response.Content.ReadAsStringAsync().Result);
		}
		public static string SerializeJson(object value)
		{
			return JsonConvert.SerializeObject(value, _jsonSettings);
		}

		public static T DeserializeJson<T>(string json)
		{
			return JsonConvert.DeserializeObject<T>(json, _jsonSettings);
		}

		public static HttpResponseMessage SendSearchRequest(HttpClient client, HttpMethod method, Uri uri, string json = null)
		{
			UriBuilder builder = new UriBuilder(uri);
			string separator = string.IsNullOrWhiteSpace(builder.Query) ? string.Empty : "&";
			builder.Query = builder.Query.TrimStart('?') + separator + ApiVersionString;

			var request = new HttpRequestMessage(method, builder.Uri);

			if (json != null)
			{
				request.Content = new StringContent(json, Encoding.UTF8, "application/json");
			}

			return client.SendAsync(request).Result;
		}

		public static void EnsureSuccessfulSearchResponse(HttpResponseMessage response)
		{
			if (!response.IsSuccessStatusCode)
			{
				string error = response.Content == null ? null : response.Content.ReadAsStringAsync().Result;
				throw new Exception("Search request failed: " + error);
			}
		}
	}
}
