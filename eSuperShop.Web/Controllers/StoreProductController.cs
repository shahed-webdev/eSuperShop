using System.Linq;
using eSuperShop.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CloudStorage;
using eSuperShop.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace eSuperShop.Web.Controllers
{
    [Authorize(Roles = "Seller")]
    public class StoreProductController : Controller
    {
        private readonly IProductCore _product;
        private readonly ICloudStorage _cloudStorage;

        public StoreProductController(IProductCore product, ICloudStorage cloudStorage)
        {
            _product = product;
            _cloudStorage = cloudStorage;
        }

        //Find Brand
        public async Task<IActionResult> FindBrand(int catalogId, string name)
        {
            var data = await _product.SearchBrandAsync(catalogId, name);
            return Json(data);
        }

        //Find Attribute
        public async Task<IActionResult> FindAttribute(int catalogId, string name)
        {
            var data = await _product.SearchAttributeAsync(catalogId, name);
            return Json(data);
        }

        //Find Specification
        public async Task<IActionResult> FindSpecification(int catalogId, string name)
        {
            var data = await _product.SearchSpecificationAsync(catalogId, name);
            return Json(data);
        }


        public IActionResult ProductCategory()
        {
            var model = _product.VendorWiseCatalogList(User.Identity.Name);

            return View(model.Data);
        }

        //get category details
        public IActionResult CategoryDetails(int id)
        {
            var model = _product.CatalogDetails(id);
            return Json(model);
        }

        //Add product
        public IActionResult AddProduct(int? id)
        {
            if (!id.HasValue) return RedirectToAction("ProductCategory");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductAddModel model, IFormFile[] productImage)
        {
            if (!ModelState.IsValid) return View(model);
            
            if (productImage != null)
            {
                var count = 1;
                foreach (var img in productImage)
                {
                    var fileName = FileBuilder.FileNameImage("product-image", img.FileName);
                    var blob = new ProductBlobAddModel
                    {
                        DisplayOrder = count,
                        BlobUrl = await _cloudStorage.UploadFileAsync(img, fileName)
                    };
                    model.Blobs.Add(blob);
                    count++;
                }
            }

            foreach (var attribute in model.Attributes)
            {
                foreach (var value in attribute.Values)
                {
                    if (value.AttributeImage == null) continue;

                    var fileName = FileBuilder.FileNameImage("product-attribute-image", value.AttributeImage.FileName);
                    value.ImageUrl = await _cloudStorage.UploadFileAsync(value.AttributeImage, fileName);
                }
            }
            
            var response = _product.AddProduct(model, User.Identity.Name);
            if (!response.IsSuccess) return View(model);

            return RedirectToAction("ProductCategory");
        }
    }
}
