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
        //const string _baseUrl = ""http://localhost:63005";
        // const string _restUrl = "http://localhost:63005/api/tasks/";

        const string _baseUrl = "http://todoapi.azurewebsites.net/";
        const string _restUrl = "http://todoapi.azurewebsites.net/api/tasks/";
        public TaskAPiService()
        {
            Init();
        }
        private void Init()
        {
            _client.MaxResponseContentBufferSize = 256000;
            _client.BaseAddress = new Uri(_baseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<TaskDao> SaveToDoItemAsync(TaskDao item)
        {
            try
            {
                var uri = new Uri(string.Format(_restUrl, string.Empty));

                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync(uri, content);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var newContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TaskDao>(newContent);
                }

                return null;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async  Task<bool> DeleteToDoItemAsync(string id)
        {
            try
            {
                var uri = new Uri(string.Format(_restUrl + "{0}", id));

                HttpResponseMessage response = await _client.DeleteAsync(uri);
                response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TaskDao> FindByIdAsync(string id)
        {
            try
            {
                var uri = new Uri(string.Format(_restUrl + "{0}", id));

                HttpResponseMessage response = await _client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var newContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TaskDao>(newContent);
                }

                return null;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<TaskDao>> FindAllAsync()
        {
            try
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
            catch (Exception)
            {
                throw;
            }
        }

    }
}
