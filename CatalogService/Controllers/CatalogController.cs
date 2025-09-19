using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace CatalogService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CatalogController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProducts()
        {
            // Token'ı oku ve doğrula
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            if (authHeader == null || !authHeader.StartsWith("Bearer "))
                return Unauthorized("JWT token eksik.");

            var token = authHeader.Substring("Bearer ".Length);
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                // Ekstra claim kontrolü yapılabilir
            }
            catch
            {
                return Unauthorized("JWT token geçersiz.");
            }

            var products = new[]
            {
                new { Id = 1, Name = "Laptop", Price = 1500 },
                new { Id = 2, Name = "Phone", Price = 800 }
            };
            return Ok(products);
        }
    }
}
