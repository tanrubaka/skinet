using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using AutoMapper;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {

        [HttpGet]
        public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts(
            [FromServices] IGenericRepository<Product> productRepository,
            [FromServices] IMapper mapper) 
        { 
            var spec = new ProuctsWithTypesAndBrandsSpecification();
            var products = await productRepository.ListAsync(spec);
            return Ok(mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id,
            [FromServices] IGenericRepository<Product> productRepository,
            [FromServices] IMapper mapper) 
        { 
            var spec = new ProuctsWithTypesAndBrandsSpecification(id);
            var product = await productRepository.GetEntityWithSpec(spec);
            return Ok(mapper.Map<Product, ProductToReturnDto>(product));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands(
            [FromServices] IGenericRepository<ProductBrand> brandRepository
        ) => Ok(await brandRepository.ListAllAsync());

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes(
            [FromServices] IGenericRepository<ProductType> typeRepository
        ) => Ok(await typeRepository.ListAllAsync());
    }
}