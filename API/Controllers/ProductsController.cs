using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using AutoMapper;
using API.Errors;
using API.Helpers;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
            [FromServices] IGenericRepository<Product> productRepository,
            [FromServices] IMapper mapper,
            [FromQuery]ProductSpecParams productParams)
        { 
            var spec = new ProuctsWithTypesAndBrandsSpecification(productParams);
            var countSpec = new ProductWithFiltersCountSpecification(productParams);
            var totalItems = await productRepository.CountAsync(countSpec);
            var products = await productRepository.ListAsync(spec);
            var data =mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products); 
            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id,
            [FromServices] IGenericRepository<Product> productRepository,
            [FromServices] IMapper mapper) 
        { 
            var spec = new ProuctsWithTypesAndBrandsSpecification(id);
            var product = await productRepository.GetEntityWithSpec(spec);
            if(product == null)
                return NotFound(new ApiResponse(404));
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