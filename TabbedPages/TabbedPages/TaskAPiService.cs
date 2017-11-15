using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TabbedPages.Daos;

namespace TabbedPages
{
    public class TaskAPiService : ITaskAPiService
    {
        private HttpClient _client = new HttpClient();
         const string _restUrl = "http://todoapi.azurewebsites.net/api/tasks/";

        public TaskAPiService()
        {
            Init();
        }
        private void Init()
        {
            _client.MaxResponseContentBufferSize = 256000;
            _client.BaseAddress = new Uri("http://todoapi.azurewebsites.net/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<TaskDao> SaveToDoItemAsync(TaskDao item)
        {           
            var uri = new Uri(string.Format(_restUrl, string.Empty));

            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            if(response.IsSuccessStatusCode)
            {
                var newContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TaskDao>(newContent);
            }

            return null;
        }

        public async  Task<bool> DeleteToDoItemAsync(string id)
        {
            var uri = new Uri(string.Format(_restUrl + "{0}", id));

            HttpResponseMessage response = await _client.DeleteAsync(uri);
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<TaskDao>> FindAllAsync()
        {
            var uri = new Uri(string.Format(_restUrl, string.Empty));

            HttpResponseMessage response = await _client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var newContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<TaskDao>>(newContent);
            }

            return null;
        }

    }
}
