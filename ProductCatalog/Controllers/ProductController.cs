using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.DAL.Data.Interfaces;
using ProductCatalog.DAL.Data.Models;
using ProductCatalog.PL.Models;

namespace ProductCatalog.PL.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Categories = await _categoryRepository.GetAllAsync();
            var products = await _productRepository.GetAllAsync();
            var productsVm = _mapper.Map<List<ProductVM>>(products);
            return View(productsVm);
        }

        public async Task<IActionResult> Add()
        {
            ViewBag.Categories = await _categoryRepository.GetAllAsync();
            return View();
        }

        public async Task<IActionResult> AddProduct([FromForm] ProductVM product)
        {
            var products = await _productRepository.GetAllAsync();
            var productsVM = _mapper.Map<List<ProductVM>>(products);
            return View("Index", productsVM);
        }
    }
}
