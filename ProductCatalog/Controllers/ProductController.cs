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
        public async Task<IActionResult> Index(int categoryId = 0)
        {
            var categoriesVm = _mapper.Map<List<CategoryVM>>(await _categoryRepository.GetAllAsync());
            if (categoryId != 0 && !categoriesVm.Any(x => x.Id == categoryId)) //Id Doesn't Exist
            {
                return RedirectToAction("Index", "Error", new ErrorVM { StatusCode = StatusCodes.Status400BadRequest, Message = "Bad Request" });
            }

            ViewBag.Categories = categoriesVm;
            IEnumerable<Product> products;
            if (User.IsInRole("Admin"))
            {
                products = await _productRepository.GetAllAsync(categoryId);
            }
            else
            {
                products = await _productRepository.GetActiveAsync(categoryId);
            }

            var productsVm = _mapper.Map<List<ProductVM>>(products);
            return View(productsVm);

            //return RedirectToAction("Index", "Error", new ErrorVM { StatusCode = StatusCodes.Status400BadRequest, Message = "Bad Request" });

        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add()
        {
            //ViewBag.Categories = await _categoryRepository.GetAllAsync();
            var categories = _mapper.Map<List<CategoryVM>>(await _categoryRepository.GetAllAsync());
            var productWithCategories = new ProductWithCategoriesVM { categories = categories };

            return View(productWithCategories);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct([FromForm] ProductWithCategoriesVM productWithCategoryVm)
        {
            if (ModelState.IsValid)
            {
                var category = await _categoryRepository.FindAsync(productWithCategoryVm.product.CategoryId);
                if (category is null)
                {
                    return RedirectToAction("Index", "Error", new ErrorVM { StatusCode = StatusCodes.Status400BadRequest, Message = "Bad Request" });
                }
                var product = _mapper.Map<Product>(productWithCategoryVm.product);
                product.Category = category!;
                await _productRepository.AddAsync(product);
                await _productRepository.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", "Error", new ErrorVM { StatusCode = StatusCodes.Status400BadRequest, Message = "Bad Request" });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product is null)
            {
                return View("Error", new ErrorVM { StatusCode = StatusCodes.Status400BadRequest, Message = "Bad Request" });
            }
            var productVm = _mapper.Map<ProductVM>(product);
            var categoriesVm = _mapper.Map<List<CategoryVM>>(await _categoryRepository.GetAllAsync());
            var productWithCategories = new ProductWithCategoriesVM { product = productVm, categories = categoriesVm };
            return View(productWithCategories);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditProduct([FromForm] ProductWithCategoriesVM productWithCategoryVm)
        {
            if (ModelState.IsValid)
            {
                var category = await _categoryRepository.FindAsync(productWithCategoryVm.product.CategoryId);
                if (category is null)
                {
                    return RedirectToAction("Index", "Error", new ErrorVM { StatusCode = StatusCodes.Status400BadRequest, Message = "Bad Request" });
                }
                var product = _mapper.Map<Product>(productWithCategoryVm.product);
                product.Category = category!;
                _productRepository.Update(product);
                await _productRepository.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", "Error", new ErrorVM { StatusCode = StatusCodes.Status400BadRequest, Message = "Bad Request" });
        }

        public async Task<IActionResult> Details(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product is null)
            {
                return View("Error", new ErrorVM { StatusCode = StatusCodes.Status400BadRequest, Message = "Bad Request" });
            }
            var productVm = _mapper.Map<ProductVM>(product);
            //var categoriesVm = _mapper.Map<List<CategoryVM>>(await _categoryRepository.GetAllAsync());
            //var productWithCategories = new ProductWithCategoriesVM { product = productVm, categories = categoriesVm };
            return View(productVm);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product is null)
            {
                return RedirectToAction("Index", "Error", new ErrorVM { StatusCode = StatusCodes.Status400BadRequest, Message = "Bad Request" });
            }
            product.IsDeleted = true;
            _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();
            var productsVm = _mapper.Map<List<ProductVM>>(await _productRepository.GetAllAsync());
            //return PartialView("_AllProductsTablePartial", productsVm);
            return RedirectToAction("Index");
        }
    }
}
