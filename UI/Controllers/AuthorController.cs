using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace UI.Controllers
{
    public class AuthorController : Controller
    {
        private AuthorDM AuthorDM { get; } = new AuthorDM();

        [HttpGet]
        public ActionResult Index(string alert = null)
        {
            ViewBag.Alert = alert;
            return View();
        }

        [HttpPost]
        public ActionResult GetAuthors(DataTableVM model)
        {
            var tableVM = AuthorDM.GetAuthors(model);
            return new JsonResult
            {
                Data = tableVM,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpGet]
        public ActionResult Details(long id)
        {
            var model = AuthorDM.GetAuthor(id, true);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AuthorVM model)
        {
            if (ModelState.IsValid)
            {
                AuthorDM.AddAuthor(model);
                return RedirectToAction("Index", new { alert = "Author was created" });
            }
            ViewBag.Alert = "Failed to create author";
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var model = AuthorDM.GetAuthor(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(AuthorVM model)
        {
            if (ModelState.IsValid)
            {
                AuthorDM.UpdateAuthor(model);
                return RedirectToAction("Index", new { alert = "Author was edited" });
            }
            ViewBag.Alert = "Failed to edit author";
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            AuthorDM.DeleteAuthor(id);
            return new EmptyResult();
        }
    }
}