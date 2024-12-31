using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.DAL.Data.Interfaces;
using ProductCatalog.DAL.Data.Models;
using ProductCatalog.Models;
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
            IEnumerable<Product> products;

            if (User.IsInRole("Admin"))
            {
                products = await _productRepository.GetAllAsync();
            }
            else
            {
                products = await _productRepository.GetActiveAsync(); 
            }
            
            var productsVm = _mapper.Map<IEnumerable<ProductVM>>(products);
            return View(productsVm);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add()
        {
            ViewBag.Categories = await _categoryRepository.GetAllAsync();
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct([FromForm] ProductVM productVm)
        {
            if (ModelState.IsValid)
            {
                var category = await _categoryRepository.FindAsync(productVm.CategoryId);
                if (category is null)
                {
                    return RedirectToAction("Index", "Error", new ErrorVM { StatusCode = StatusCodes.Status400BadRequest, Message = "Bad Request"});
                }
                var product = _mapper.Map<Product>(productVm);
                product.Category = category!;
                await _productRepository.AddAsync(product);
                await _productRepository.SaveChangesAsync();
                return RedirectToAction("Index"); 
            }
            
            return RedirectToAction("Index", "Error", new ErrorVM { StatusCode = StatusCodes.Status400BadRequest, Message = "Bad Request" });
        }
    }
}
