using Microsoft.AspNetCore.Mvc;

using courseModels;
using courseAnotherPartDataAccess;
using course.Repository;
using course.Repository.Irepository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace course.Properties
{
    [Area("Admin")]
    public class Comphanys : Controller
    {

        //  private readonly database _db;
        private readonly IUnitOfWork _db;


        public Comphanys(IUnitOfWork db)
        {
            _db = db;

        }
        //public CategoryController(ICategoryRespository db)
        //{
        //    _db = db;
        // }
        public IActionResult Index() // this to load  a page of if i press index we get index
        {
            IEnumerable<ComphanyPep> objCoverlist = (IEnumerable<ComphanyPep>)_db.Comphany.GetAll(); //to itrte over a collection data
            return View(objCoverlist);
        }
        //get
        public IActionResult Upsert(int? id)  //Update and insert 
        {
            ComphanyPep obj = new();

            if (id == null || id == 0) // is input is zero or null
            {

                return View(obj);
            }
            else
            {

                obj = _db.Comphany.GetFirst(u => u.Id == id);
                return View(obj);
            }

            //  return View(obj);

        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken] // mone hoy refresh na haor jonno dei
        public IActionResult Upsert(ComphanyPep obj) // to add somthing in db
        {

            if (ModelState.IsValid)
            {

                if (obj.Id == 0)
                {
                    _db.Comphany.Add(obj); //to update in databse
                    TempData["success"] = "Cover Created SuccesFully";
                }
                else
                {
                    _db.Comphany.Update(obj); //to update in databse
                    TempData["success"] = "Cover Edited SuccesFully";
                }
                //  _db.ProductType.Add(obj); //to update in databse
                //_db.Update(obj); //to update in databse
                _db.Save(); // to post the chnges in database

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
            var obj = _db.Comphany.GetFirst(u => u.Id == id);
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

            var obj = _db.Comphany.GetFirst(u => u.Id == id);
            //var obj = _db.GetFirst(u => u.Id == id);
            if (obj == null) // if we dont fidn the id
            {
                return Json(new { success = false, message = "Error While deleting" });
            }

            _db.Comphany.Remove(obj); //to update in databse
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
            var producList = _db.Comphany.GetAll();
            return Json(new { data = producList });
        }
        //post

        #endregion
    }
}
