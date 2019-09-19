using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WindowsParty.Constants;

namespace WindowsParty.Handlers
{
	public abstract class HandlerBase
	{
		#region Properties

		protected SimpleContainer Container { get; private set; }
		private HttpClient client = new HttpClient();

		#endregion

		#region Abstract Properties

		protected abstract string Endpoint { get; }

		#endregion

		#region Constructors

		public HandlerBase(SimpleContainer container)
		{
			Container = container;
			Initialize();
		}

		#endregion

		#region Methods

		private void Initialize()
		{
			client.BaseAddress = new Uri(ConfigurationManager.AppSettings["API_URL"]);
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			if (App.Current.Properties.Contains(DefaultValues.TOKEN))
				client.DefaultRequestHeaders.Add("Authorization", $"Bearer {App.Current.Properties[DefaultValues.TOKEN]}");
		}

		protected async Task<T> Post<T>(object body) where T : class, new()
		{
			var response = client.PostAsJsonAsync(Endpoint, body).Result;
			response.EnsureSuccessStatusCode();
			return await response.Content.ReadAsAsync<T>();
		}

		protected async Task<IEnumerable<T>> Get<T>() where T : class, new()
		{
			var response = client.GetAsync(Endpoint).Result;
			response.EnsureSuccessStatusCode();
			return await response.Content.ReadAsAsync<IEnumerable<T>>();
		}

		#endregion
	}
}
