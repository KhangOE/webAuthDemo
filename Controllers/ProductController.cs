using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_authentication.Data;
using web_authentication.Dto;
using web_authentication.entities;
using web_authentication.filter;
using web_authentication.Interfaces;

namespace web_authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IUnitOfWork _unitOfwork;
        private DataContext _dataContext;
        public ProductController(IUnitOfWork unitOfwork, DataContext dataContext)
        {
            _unitOfwork = unitOfwork;
            _dataContext = dataContext;
        }

        [UpperAgeRequire(20)]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var product = _dataContext.Products;
            return Ok(product);
        }


        [Authorize(Roles = "admin")]
        [HttpGet("checkadmin")]
        public async Task<IActionResult> checkAdmin()
        {
            var product = _dataContext.Products;
            return Ok(product);
        }



        [Authorize(Roles = "user")]
        [HttpGet("checkuser")]
        public async Task<IActionResult> checkUser()
        {
            var product = _dataContext.Products;
            return Ok(product);
        }

        [AuthorizationNeedAllFilter("admin,user")]
        [HttpGet("checkUserandAdmin")]
        public async Task<IActionResult> checkUserAndAdmin()
        {
            var product = _dataContext.Products;
            return Ok(product);
        }

        [Authorize(Roles = "user,admin")]
        [HttpGet("checkUserOrAdmin")]
        public async Task<IActionResult> checkUserOrAdmin()
        {
            var product = _dataContext.Products;
            return Ok(product);
        }


        //[Authorize]
        [ServiceFilter(typeof(ValidationParameterFilterAttrilbute))]
        [ServiceFilter(typeof(LoggingResultFilter))]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product,[FromQuery] List<int> categoryId)
        {
            await _unitOfwork.ProductRepository.Create(product);
            await _unitOfwork.SavechangesAsync();
            return Ok();
        }

        [ServiceFilter(typeof(ValidationExistEntityFilterAttribute<Product>))]
        [ServiceFilter(typeof(ValidationParameterFilterAttrilbute))]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            Console.WriteLine("id" + id);
            var entity = HttpContext.Items["entity"] as Product;
         
            await _unitOfwork.ProductRepository.Delete(entity);
            await _unitOfwork.SavechangesAsync();
      
            return Ok();
        }
    }
}
