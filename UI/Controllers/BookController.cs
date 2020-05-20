using Infrastructure.Logic;
using System;
using System.Web.Mvc;
using ViewModels;

namespace UI.Controllers
{
    public class BookController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetList(DataTableRequestVM model)
        {
            using (var bookDM = Factory.GetService<IBookDM>())
            {
                var tableVM = bookDM.GetList(model);
                return new JsonResult
                {
                    Data = tableVM,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        [HttpGet]
        public ActionResult Get(long? id)
        {
            using (var bookDM = Factory.GetService<IBookDM>())
            {
                var model = bookDM.Get(id);
                return PartialView("~/Views/Book/Get.cshtml", model);
            }
        }

        [HttpPost]
        public ActionResult Update(BookVM model)
        {
            using (var bookDM = Factory.GetService<IBookDM>())
            {
                if (ModelState.IsValid)
                {
                    bookDM.Update(model);
                    return new EmptyResult();
                }
            }
            throw new ArgumentException("Model is not valid");
        }

        [HttpPost]
        public ActionResult Create(BookVM model)
        {
            using (var bookDM = Factory.GetService<IBookDM>())
            {
                if (ModelState.IsValid)
                {
                    bookDM.Create(model);
                    return new EmptyResult();
                }
            }
            throw new ArgumentException("Model is not valid");
        }

        [HttpPost]
        public ActionResult Publish(BookVM model)
        {
            if (ModelState.IsValid)
            {
                using (var bookDM = Factory.GetService<IBookDM>())
                {
                    var messageId = bookDM.Publish(model);
                    return new JsonResult { Data = messageId, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
            throw new ArgumentException("Model is not valid");
        }

        [HttpGet]
        public ActionResult Publish()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            using (var bookDM = Factory.GetService<IBookDM>())
            {
                bookDM.Delete(id);
                return new EmptyResult();
            }
        }
    }
}