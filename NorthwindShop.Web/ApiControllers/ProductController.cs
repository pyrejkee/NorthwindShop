using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NorthwindShop.BLL.EntitiesDTO;
using NorthwindShop.BLL.Services.Interfaces;
using NorthwindShop.Web.ViewModels;

namespace NorthwindShop.Web.ApiControllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Get()
        {
            var productDtos = await _productService.Get();
            var products = _mapper.Map<List<ProductViewModel>>(productDtos);

            return products;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductViewModel>> Get(int id)
        {
            var productDto = await _productService.GetById(id);
            if (productDto == null)
            {
                return NotFound();
            }

            var product = _mapper.Map<ProductViewModel>(productDto);

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<ProductViewModel>> AddProduct(CreateProductViewModel product)
        {
            var productToRepo = _mapper.Map<ProductDTO>(product);
            var productDto = await _productService.Add(productToRepo);
            var createdProduct = _mapper.Map<ProductViewModel>(productDto);

            return CreatedAtAction(nameof(AddProduct), new {id = createdProduct.Id}, createdProduct);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductViewModel>> UpdateProduct(int id, EditProductViewModel product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            var productToRepo = _mapper.Map<ProductDTO>(product);
            var updatedProductDto = await _productService.Update(productToRepo);
            var updatedProduct = _mapper.Map<ProductViewModel>(updatedProductDto);

            return Ok(updatedProduct);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await _productService.Remove(id);

            return Ok();
        }
    }
}
