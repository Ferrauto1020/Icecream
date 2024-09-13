using System.Net.Http.Json;
using IcecreamApp.Shared.Dtos;

namespace IcecreamApp.Services
{

    public class TestAPi
    {
        private readonly IHttpClientFactory httpClient;

        public TestAPi(IHttpClientFactory httpClient)
        {
            this.httpClient = httpClient;
        }

private HttpClient HttpClient =>  this.httpClient.CreateClient("IcecreamCompany");
        public async Task<HttpResponseMessage> SignupAsync(SignupRequestDto dto)
        {
            var result = await  HttpClient.PostAsJsonAsync("/api/signup",dto);
            
            return result;
        }
    }
}