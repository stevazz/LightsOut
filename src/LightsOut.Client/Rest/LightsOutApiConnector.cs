using LightsOut.Client.Rest.Models.Binding;
using LightsOut.Client.Rest.Models.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOut.Client.Rest
{
    public static class LightsOutApiConnector
    {
        private static readonly HttpClient client;

        static LightsOutApiConnector()
        {
            client = new HttpClient();
        }

        public static async Task<LightsOutViewModel> IntializeAsync(InitializeBindingModel model)
        {
            var body = JsonConvert.SerializeObject(model);
            
            var response = await client.PostAsync("https://localhost:7110/LightsOut", new StringContent(body, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<LightsOutViewModel>(await response.Content.ReadAsStringAsync());
            }
            else
                return null;
        }

        public static async Task<LightsOutViewModel> ToggleAsync(ToggleBindingModel model)
        {
            var body = JsonConvert.SerializeObject(model);

            var response = await client.PatchAsync("https://localhost:7110/LightsOut", new StringContent(body, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<LightsOutViewModel>(await response.Content.ReadAsStringAsync());
            }
            else
                return null;
        }

        public static async Task<LightsOutViewModel> GetAsync(Guid Id)
        {
            var response = await client.GetAsync($"https://localhost:7110/LightsOut/{Id}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<LightsOutViewModel>(await response.Content.ReadAsStringAsync());
            }
            else
                return null;
        }

    }
}
