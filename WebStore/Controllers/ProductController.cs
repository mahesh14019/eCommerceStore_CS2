using Microsoft.AspNetCore.Mvc;
using WebStore.DataAccess.Repository.IRepository;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _uow;
        public ProductController(IProductRepository productRepository, IUnitOfWork uow)
        {
            _productRepository = productRepository;
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
           // var products = new List<Product>();
            //var products = await _productRepository.GetAllAsync();
            return View();
            //var products = from mem in _productRepository.GetAll()
            //              select mem;
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    //products = products.Where(m => m.ProductName.Contains(searchString)
            //    //                       || m.Author.Contains(searchString)
            //    //                       || m.ISBN.Contains(searchString));
            //    products = products.Where(m => m.ProductName == searchString);
            //    //return RedirectToAction("Index");
            //}

            ////var memberList = Members.ToList();
            //return View(products);
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Insert(product);
                @TempData["msg"] = "Product added successfully!";
            }
            return View();
        }
        [HttpGet("Details/{ProductId}")]
        public IActionResult Details(int productId)
        {
            var products = _productRepository.FindAsync(productId);
            return View(products);
        }
        public IActionResult DeleteById(int productId)
        {

            _productRepository.DeleteById(productId);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int productId)
        {
            var products = _productRepository.FindAsync(productId);
            return View(products);

        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Update(product);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int productId)
        {
            if (productId == null || productId == 0)
            {
                return NotFound();
            }
            var products = _productRepository.FindAsync(productId);

            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int productId)
        {
            var products = _productRepository.FindAsync(productId);

            if (products == null)
            {
                return NotFound();
            }
            _productRepository.Delete(productId);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> GetAll()
        {
            //[FromBody] Product model
            //    var filteredData = _productRepository.GetAll()
            //.Where(p => p.ProductName.Contains(model.Search.Value) || p.Author.Contains(model.Search.Value))
            //.Skip(model.Start)
            //.Take(model.Length)
            //.ToList();

            //    return Json(new
            //    {
            //        draw = model.Draw,
            //        recordsTotal = _productRepository.GetAll().Count(),
            //        recordsFiltered = filteredData.Count(),
            //        data = filteredData
            //    });


            //var products = await _productRepository.GetAllAsync();
            //return Json(new { data=products});
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            IQueryable<Product> customerData = _uow.ProductData.GetAll(u=>u.IsActive==true);
            //Sorting  
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                switch (sortColumn.ToLower())
                {
                    case "isbn":
                        if (sortColumnDirection=="desc")
                        {
                            customerData = customerData.OrderByDescending(x => x.ISBN);
                        }
                        else
                        {
                            customerData = customerData.OrderBy(x => x.ISBN);
                        }
                        break;
                    case "productname":
                        if (sortColumnDirection == "desc")
                        {
                            customerData = customerData.OrderByDescending(x => x.ProductName);
                        }
                        else
                        {
                            customerData = customerData.OrderBy(x => x.ProductName);
                        }
                        break;
                    case "author":
                        if (sortColumnDirection == "desc")
                        {
                            customerData = customerData.OrderByDescending(x => x.Author);
                        }
                        else
                        {
                            customerData = customerData.OrderBy(x => x.Author);
                        }
                        break;
                    case "price":
                        if (sortColumnDirection == "desc")
                        {
                            customerData = customerData.OrderByDescending(x => x.Price);
                        }
                        else
                        {
                            customerData = customerData.OrderBy(x => x.Price);
                        }
                        break;
                    case "added date":
                        if (sortColumnDirection == "desc")
                        {
                            customerData = customerData.OrderByDescending(x => x.AddedDate);
                        }
                        else
                        {
                            customerData = customerData.OrderBy(x => x.AddedDate);
                        }
                        break;
                        
                }

            }
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                //   customerData = customerData.OrderBy(sortColumn + " " + sortColumnDirection);
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                customerData = customerData.Where(m => m.ProductName.Contains(searchValue)
                                            || m.ISBN.Contains(searchValue)
                                            || m.ISBN.Contains(searchValue)
                                            || m.Author.Contains(searchValue));
            }
            recordsTotal = customerData.Count();
            var data = customerData.Skip(skip).Take(pageSize).ToList();
            var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
            return Ok(jsonData);
        }
    }
}
