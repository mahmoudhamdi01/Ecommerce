using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Repository.Specification.Productspecs;
using Store.Service.Services.Product;
using Store.Service.Services.Product.Dtos;

namespace Store.Web.Controllers
{
    [Authorize]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("GetAllBrands")]
        public async Task<ActionResult<IReadOnlyList<BrandTypeDetailDto>>> GetAllBrands()
            =>Ok(await _productService.GetAllBrandsAsync());

        [HttpGet("GetAllTypes")]
        public async Task<ActionResult<IReadOnlyList<BrandTypeDetailDto>>> GetAllTypes()
          => Ok(await _productService.GetAllTypesAsync());

        [HttpGet] 
        public async Task<ActionResult<IReadOnlyList<ProductDetailsDto>>> GetAllProducts([FromQuery] ProductSpecification input)
         => Ok(await _productService.GetAllProductsAsync(input));

        [HttpGet("GetProductById")]
        public async Task<ActionResult<IReadOnlyList<BrandTypeDetailDto>>> GetProductById(int? Id)
                 => Ok(await _productService.GetProductByIdAsync(Id));
    }
}
