﻿using Microsoft.AspNetCore.Mvc;

using courseModels;
using courseAnotherPartDataAccess;
using course.Repository;
using course.Repository.Irepository;

namespace course.Properties
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
       
        //  private readonly database _db;
         private readonly IUnitOfWorkCover _db;
        //private readonly ICategoryRespository _db;
        public CoverTypeController(IUnitOfWorkCover db)
        {
            _db = db;
        }
        //public CategoryController(ICategoryRespository db)
        //{
        //    _db = db;
       // }
        public IActionResult Index() // this to load  a page of if i press index we get index
        {
             IEnumerable<CoverType> objCoverlist = (IEnumerable<CoverType>)_db.CoverType.GetAll(); //to itrte over a collection data
           // IEnumerable<Category> objCategory = _db.GetAll(); //to itrte over a collection data
            return View(objCoverlist);
        }
        public IActionResult Create()
        {

            return View();
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken] // mone hoy refresh na haor jonno dei
        public IActionResult Create(CoverType obj) // to add somthing in db
        {
         
            if (ModelState.IsValid)
            {
                _db.CoverType.Add(obj);
               // _db.Add(obj);
                _db.Save(); // to post the chnges in database
                TempData["success"] = "Cover Created SuccesFully"; //to show some for a one time after  succes full dltion or edit or create
                return RedirectToAction("Index");//to go to the index form
            }
            return View(obj);
        }
        public IActionResult Edit(int ? id)  //to Edit in Database
        {
            if(id == null || id == 0) // is input is zero or null
            {
                return NotFound();
            }
            // var obj = _db.Categories.Find(id); 
             var obj = _db.CoverType.GetFirst(u=>u.Id == id);
           // var obj = _db.GetFirst(u => u.Id == id);
            if (obj == null) // if we dont fidn the id
            {
                return NotFound();
            }
            return View(obj);
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken] // mone hoy refresh na haor jonno dei
        public IActionResult Edit(CoverType obj) // to add somthing in db
        {
          
            if (ModelState.IsValid)
            {
                   _db.CoverType.Update(obj); //to update in databse
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
              var obj = _db.CoverType.GetFirst(u=>u.Id == id);
            //var obj = _db.GetFirst(u => u.Id == id);
            if (obj == null) // if we dont fidn the id
            {
                return NotFound();
            }
            return View(obj);
        }
        //post
        [HttpPost,ActionName("Delete")] // action name used for if your upper fucntion have same name with the action name
        [ValidateAntiForgeryToken] // mone hoy refresh na haor jonno dei
        public IActionResult DeletePost(int? id) // to add somthing in db
        {
            //if (obj.Name == obj.DisplayOrder.ToString()) // to show error from here
            //    if both textbox have same name
            //{
            //    ModelState.AddModelError("CustomError", "Both Input Cannot Be Same");
            //    ModelState.AddModelError("Name", "Both Input Cannot Be Same"); TO display down the TextBox
            //}
            //if (ModelState.IsValid)
            //{
            //var obj = _db.Categories.Find(id);
              var obj = _db.CoverType.GetFirst(u=>u.Id == id);
            //var obj = _db.GetFirst(u => u.Id == id);
            if (obj == null) // if we dont fidn the id
            {
                return NotFound();
            }
            _db.CoverType.Remove(obj); //to update in databse
            //_db.Remove(obj); //to update in databse
            _db.Save(); // to post the chnges in database
            TempData["success"] = "Cover Deleted SuccesFully";
            return RedirectToAction("Index");//to go to the index form
            //}
        }
    }
}
