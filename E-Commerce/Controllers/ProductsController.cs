using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Spacifications;
using E_Commerce.Controllers;
using E_Commerce.Errors;
using E_Commerce.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductsController : BaseApiController
{
    private readonly IGenericRepository<ProductBrand> brandRepo;
    private readonly IMapper mapper;
    private readonly IGenericRepository<Product> productRepo;
    private readonly IGenericRepository<ProductType> typeRepo;

    public ProductsController(IGenericRepository<Product> productRepo, IGenericRepository<ProductType> typeRepo,
        IGenericRepository<ProductBrand> brandRepo, IMapper mapper)
    {
        this.productRepo = productRepo;
        this.typeRepo = typeRepo;
        this.brandRepo = brandRepo;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetProducts([FromQuery] ProductParams productParams)
    {
        var spec = new ProductsWithTypesAndBrands(productParams);
        var specCount = new ProductWithFiltersForCountSpecification(productParams);
        var totalItems = await productRepo.CountAsync(specCount);

        var products = await productRepo.AllAsync(spec);
        if (products == null) return NotFound();

        var data = mapper.Map<List<Product>, List<ProductDto>>(products);

        return Ok(new Pagination<ProductDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var spec = new ProductsWithTypesAndBrands(id);
        var product = await productRepo.GetEntityWithSpec(spec);
        if (product == null) return NotFound(new ApiResponse(404));
        return Ok(mapper.Map<Product, ProductDto>(product));
    }

    [HttpGet("brands")]
    public async Task<ActionResult<List<ProductBrand>>> GetBrands()
    {
        return await brandRepo.GetAllAsync();
    }

    [HttpGet("types")]
    public async Task<ActionResult<List<ProductType>>> GetTypes()
    {
        return await typeRepo.GetAllAsync();
    }
}