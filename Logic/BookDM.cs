using AmazonIntegration;
using EntityModels;
using Infrastructure.Data;
using Infrastructure.Logic;
using Newtonsoft.Json;
using Shared.Interfaces;
using Shared.Services;
using System.Configuration;
using System.Linq;
using ViewModels;

namespace Logic
{
    public class BookDM : BaseDM, IBookDM
    {
        public BookDM(IFactory factory) : base(factory)
        {

        }

        public void Delete(long id)
        {
            using (var bookRepo = Factory.GetService<IBookRepo>())
            {
                bookRepo.Delete(id);
            }
        }

        public void Update(BookVM model)
        {
            var book = MapperService.Map<BookEM>(model);
            using (var bookRepo = Factory.GetService<IBookRepo>())
            using (var scope = new TransactionService())
            {
                long id;

                id = book.Id.Value;
                bookRepo.Update(book);
                var authorsIds = model.Authors.Select(a => a.Id.Value);
                bookRepo.UpdateAuthors(id, authorsIds);

                scope.Complete();
            }
        }

        public void Create(BookVM model)
        {
            var book = MapperService.Map<BookEM>(model);
            using (var bookRepo = Factory.GetService<IBookRepo>())
            using (var scope = new TransactionService())
            {
                long id;

                id = bookRepo.Create(book);
                var authorsIds = model.Authors.Select(a => a.Id.Value);
                bookRepo.UpdateAuthors(id, authorsIds);

                scope.Complete();
            }
        }

        public BookVM Get(long? id)
        {
            using (var bookRepo = Factory.GetService<IBookRepo>())
            {
                if (id.HasValue)
                {
                    var bookEM = bookRepo.Get(id.Value);
                    var bookVM = MapperService.Map<BookVM>(bookEM);
                    return bookVM;
                }
                return null;
            }
        }

        public DataTableResponseVM<BookVM> GetList(DataTableRequestVM model)
        {
            var dataTableEM = MapperService.Map<DataTableRequestEM>(model);

            using (var bookRepo = Factory.GetService<IBookRepo>())
            {
                var responseEM = bookRepo.GetList(dataTableEM);
                var responseVM = MapperService.Map<DataTableResponseVM<BookVM>>(responseEM);
                responseVM.draw = model.Draw;

                return responseVM;
            }
            
        }

        public string Publish(BookVM model)
        {
            var em = MapperService.Map<BookEM>(model);
            var json = JsonConvert.SerializeObject(em);
            var arn = ConfigurationManager.AppSettings["AWSSNSTopicARN"];
            var sns = new SNS();
            return sns.PublishEntity(arn, json, "Book", "Book");
        }
    }
}
