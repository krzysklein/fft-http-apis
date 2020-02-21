using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SDL.Client.Services
{
    public class CalcServiceClient : ICalcService
	{
		public string ApiUrl { get; }
		public CalcServiceClient(string apiUrl)
		{
			ApiUrl = apiUrl;
		}
		public async Task<int> SumAsync(int[] numbers)
		{
			using (var httpClient = new HttpClient())
			{
				var requestDto = new CalcService_Sum_RequestDto()
				{
					numbers = numbers
				};
				var json = JsonSerializer.Serialize(requestDto);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				var response = await httpClient.PostAsync(ApiUrl + "/api/CalcService/Sum", content);
				string jsonResponse = await response.Content.ReadAsStringAsync();
				var result = JsonSerializer.Deserialize<int>(jsonResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
				return result;
			}
		}

		public class CalcService_Sum_RequestDto
		{
			public int[] numbers { get; set; }
		}
	}
}
