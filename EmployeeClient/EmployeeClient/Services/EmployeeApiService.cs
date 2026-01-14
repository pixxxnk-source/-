using EmployeeClient.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace EmployeeClient.Services
{
    public class EmployeeApiService
    {
        private readonly HttpClient _httpClient;

        public EmployeeApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7073/");
        }
        // 一覧取得
        public async Task<List<Employee>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Employee>>("api/Employee")
           ?? new List<Employee>();
        }
        // 追加
        public async Task AddAsync(Employee employee)
        {
            await _httpClient.PostAsJsonAsync("api/Employee", employee);
        }
        // 更新
        public async Task UpdateAsync(Employee employee)
        {
            await _httpClient.PutAsJsonAsync($"api/Employee/{employee.Id}", employee);
        }
        // 削除
        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/Employee/{id}");
        }
    }
}
