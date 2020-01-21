using EntityModels;
using Infrastructure.Data;
using Infrastructure.Logic;
using Shared.Interfaces;
using Shared.Services;
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

        public void Update(BookEditVM model)
        {
            var book = MapperService.Map<BookEM>(model);
            using (var bookRepo = Factory.GetService<IBookRepo>())
            using (var scope = new TransactionService())
            {
                long id;

                id = book.Id.Value;
                bookRepo.Update(book);
                bookRepo.UpdateAuthors(id, model.AuthorIds);

                scope.Complete();
            }
        }

        public void Create(BookEditVM model)
        {
            var book = MapperService.Map<BookEM>(model);
            using (var bookRepo = Factory.GetService<IBookRepo>())
            using (var scope = new TransactionService())
            {
                long id;

                id = bookRepo.Create(book);
                bookRepo.UpdateAuthors(id, model.AuthorIds);

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

        public DataTableResponseVM<BookVM> Get(DataTableRequestVM model)
        {
            var dataTableEM = MapperService.Map<DataTableRequestEM>(model);

            using (var bookRepo = Factory.GetService<IBookRepo>())
            {
                var responseEM = bookRepo.Get(dataTableEM);
                var responseVM = MapperService.Map<DataTableResponseVM<BookVM>>(responseEM);
                responseVM.draw = model.Draw;

                return responseVM;
            }
            
        }
    }
}
