using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("api/catalog")]
    public class CatalogController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CatalogController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetCatalog()
        //{
        //    var client = _httpClientFactory.CreateClient();
        //    var response = await client.GetAsync("http://localhost:5001/api/catalog");
        //    var content = await response.Content.ReadAsStringAsync();
        //    return Content(content, "application/json");
        //}
    }
}
