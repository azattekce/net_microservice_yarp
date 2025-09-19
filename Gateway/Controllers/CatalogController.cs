using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("api/catalog")]
    [Authorize]
    public class CatalogController : BaseController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CatalogController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }      

        [HttpGet("with-auth")]
        public async Task<IActionResult> GetCatalogWithAuth()
        {
            var client = _httpClientFactory.CreateClient();
            
            // Get token from current request
            string token = Request.Headers["Authorization"].ToString();
            
            // Create request with token
            var request = new HttpRequestMessage(HttpMethod.Get, "http://host.docker.internal:5001/api/catalog");
            request.Headers.Add("Authorization", token);            
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            
            return Content(content, "application/json");
        }
    }
}
