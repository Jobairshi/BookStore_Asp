using Microsoft.AspNetCore.Mvc;

using courseModels;
using courseAnotherPartDataAccess;
using course.Repository;
using course.Repository.Irepository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace course.Properties
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        //  private readonly database _db;
        private readonly IUnitOfWork _db;
        private readonly IWebHostEnvironment _HostEnvironment; //to add image in wwwroot folder

        public ProductController(IUnitOfWork db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _HostEnvironment = hostEnvironment;
        }
        //public CategoryController(ICategoryRespository db)
        //{
        //    _db = db;
        // }
        public IActionResult Index() // this to load  a page of if i press index we get index
        {
            IEnumerable<Product> objCoverlist = (IEnumerable<Product>)_db.ProductType.GetAll(); //to itrte over a collection data
            return View(objCoverlist);
        }
        //get
        public IActionResult Upsert(int? id)  //Update and insert 
        {
            Product obj = new();
            IEnumerable<SelectListItem> CategoryList;
            CategoryList = _db.Category.GetAll().Select(
                u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.Name
                });
            IEnumerable<SelectListItem> CoverList = _db.Cover.GetAll().Select(
              u => new SelectListItem
              {
                  Value = u.Id.ToString(),
                  Text = u.Name
              });
            if (id == null || id == 0) // is input is zero or null
            {
                //we want to create product
                ViewBag.CategoryList = CategoryList;
                ViewBag.CoverList = CoverList;
                return View(obj);
            }
            else
            {
                ViewBag.CategoryList = CategoryList;
                ViewBag.CoverList = CoverList;
                obj = _db.ProductType.GetFirst(u => u.Id == id);
                return View(obj);   
            }

          //  return View(obj);

        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken] // mone hoy refresh na haor jonno dei
        public IActionResult Upsert(Product obj, IFormFile file) // to add somthing in db
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _HostEnvironment.WebRootPath;
                if (file != null) // to upload new image
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"Images\Products");
                    var extension = Path.GetExtension(file.FileName);
                    if(obj.ImageUrl != null) // to remove old image if it exist
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.ImageUrl = @"\Images\Products\" + fileName + extension;
                }
                if(obj.Id == 0)
                {
                    _db.ProductType.Add(obj); //to update in databse
                }
                else
                {
                    _db.ProductType.Update(obj); //to update in databse
                }
              //  _db.ProductType.Add(obj); //to update in databse
                //_db.Update(obj); //to update in databse
                _db.Save(); // to post the chnges in database
                TempData["success"] = "Cover Edited SuccesFully";
                return RedirectToAction("Index");//to go to the index form
            }
            return View(obj);
        }
        public IActionResult Delete(int? id)  //to Edit in Database
        {
            if (id == null || id == 0) // is input is zero or null
            {
                return NotFound();
            }
            // var obj = _db.Categories.Find(id);
            var obj = _db.ProductType.GetFirst(u => u.Id == id);
            //var obj = _db.GetFirst(u => u.Id == id);
            if (obj == null) // if we dont fidn the id
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost, ActionName("Delete")] // action name used for if your upper fucntion have same name with the action name
        [ValidateAntiForgeryToken] // mone hoy refresh na haor jonno dei
        public IActionResult DeletePost(int? id) // to add somthing in db
        {

            var obj = _db.ProductType.GetFirst(u => u.Id == id);
            //var obj = _db.GetFirst(u => u.Id == id);
            if (obj == null) // if we dont fidn the id
            {
                return Json(new { success = false, message = "Error While deleting" });
            }
            var oldImagePath = Path.Combine(_HostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _db.ProductType.Remove(obj); //to update in databse
            //_db.Remove(obj); //to update in databse
            _db.Save(); // to post the chnges in database
                        //  TempData["success"] = "Cover Deleted SuccesFully";
            return RedirectToAction("Index");//to go to the index form
            // return RedirectToAction("Index");//to go to the index form
            //}
        }

        #region API CALLS

        [HttpGet]
        public IActionResult getAll() // to get all the data from database using this..to show table
        {
            var producList = _db.ProductType.GetAll();
            return Json(new { data = producList });
        }
        //post
       
        #endregion
    }
}
