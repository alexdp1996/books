using Infrastructure.Logic;
using System;
using System.Web.Mvc;
using ViewModels;
using ViewModels.Enums;

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
                var tableVM = bookDM.Get(model);
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
        public ActionResult Update(BookEditVM model)
        {
            using (var bookDM = Factory.GetService<IBookDM>())
            {
                if (ModelState.IsValid)
                {
                    bookDM.Update(model);
                    var alert = new AlertVM
                    {
                        Message = "Book was updated",
                        Type = AlertType.Success
                    };
                    return new JsonResult { Data = alert, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
            throw new ArgumentException("Model is not valid");
        }

        [HttpPost]
        public ActionResult Create(BookEditVM model)
        {
            using (var bookDM = Factory.GetService<IBookDM>())
            {
                if (ModelState.IsValid)
                {
                    bookDM.Create(model);
                    var alert = new AlertVM
                    {
                        Message = "Book was added",
                        Type = AlertType.Success
                    };
                    return new JsonResult { Data = alert, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
            throw new ArgumentException("Model is not valid");
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            using (var bookDM = Factory.GetService<IBookDM>())
            {
                bookDM.Delete(id);
                var alert = new AlertVM
                {
                    Message = "Book with id " + id + " was deleted",
                    Type = AlertType.Success
                };
                return new JsonResult { Data = alert, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
    }
}