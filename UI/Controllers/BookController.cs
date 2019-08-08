using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace UI.Controllers
{
    public class BookController : Controller
    {
        private BookDM BookDM { get; } = new BookDM();

        [HttpGet]
        public ActionResult Index(string alert = null)
        {
            ViewBag.Alert = alert;
            return View();
        }

        [HttpPost]
        public ActionResult GetBooks(DataTableVM model)
        {
            var tableVM = BookDM.GetBooks(model);
            return new JsonResult
            {
                Data = tableVM,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BookVM model)
        {
            if (ModelState.IsValid)
            {
                BookDM.AddBook(model);
                return RedirectToAction("Index", new { alert = "Book was created" });
            }
            ViewBag.Alert = "Failed to create book";
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(long id)
        {
            var model = BookDM.GetBook(id);
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var model = BookDM.GetBook(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BookVM model)
        {
            if (ModelState.IsValid)
            {
                BookDM.Update(model);
                return RedirectToAction("Index", new { alert = "Book was edited" });
            }
            ViewBag.Alert = "Failed to edit book";
            var dm = new AuthorDM();
            model.Authors = dm.GetAuthorsByIds(model.AuthorIds);
            return View(model);
        }

        [HttpGet]
        public ActionResult GetAuthorsByTerm(string term)
        {
            var dm = new AuthorDM();
            var res = dm.GetAuthorsByTerm(term);
            return new JsonResult { Data = res, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            BookDM.DeleteBook(id);
            return new EmptyResult();
        }
    }
}