using DataInfrastructure.Entities;
using DataInfrastructure.Interfaces;
using LogicInfastructure.Interfaces;
using Shared.Interfaces;
using Shared.Services;
using System.Linq;
using ViewModels;
using ViewModels.Enums;

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

        public void Save(BookEditVM model)
        {
            var book = MapperService.Map<UpdatableBookEM>(model);
            using (var bookRepo = Factory.GetService<IBookRepo>())
            {
                var id = bookRepo.Save(book);
            }
        }

        public BookVM Get(long id)
        {
            using (var bookRepo = Factory.GetService<IBookRepo>())
            {
                var bookEM = bookRepo.Get(id);
                var bookVM = MapperService.Map<BookVM>(bookEM);
                return bookVM;
            }
        }

        public BookVM Get(BookEditVM book)
        {
            var result = MapperService.Map<BookVM>(book);

            using (var authorDM = Factory.GetService<IAuthorDM>())
            {
                result.Authors = authorDM.Get(book.AuthorIds).ToList();
                return result;
            }
        }

        public DataTableResponseVM<BookVM> Get(DataTableRequestVM model)
        {
            var dataTableEM = MapperService.Map<DataTableRequestEM>(model);

            var asc = model.Order[0].Dir == "asc";
            var column = (BookColumn) model.Order[0].Column;
            using (var bookRepo = Factory.GetService<IBookRepo>())
            {
                DataTableResponseEM<BookEM> responseEM;
                switch (column)
                {
                    default:
                    case BookColumn.Name:
                        {
                            if (asc)
                            {
                                responseEM = bookRepo.GetByNameAsc(dataTableEM);
                            }
                            else
                            {
                                responseEM = bookRepo.GetByNameDesc(dataTableEM);
                            }
                            break;
                        }
                    case BookColumn.Pages:
                        {
                            if (asc)
                            {
                                responseEM = bookRepo.GetByPagesAsc(dataTableEM);
                            }
                            else
                            {
                                responseEM = bookRepo.GetByPagesDesc(dataTableEM);
                            }
                            break;
                        }
                    case BookColumn.Rate:
                        {
                            if (asc)
                            {
                                responseEM = bookRepo.GetByRateAsc(dataTableEM);
                            }
                            else
                            {
                                responseEM = bookRepo.GetByRateDesc(dataTableEM);
                            }
                            break;
                        }
                    case BookColumn.Date:
                        {
                            if (asc)
                            {
                                responseEM = bookRepo.GetByDateAsc(dataTableEM);
                            }
                            else
                            {
                                responseEM = bookRepo.GetByDateDesc(dataTableEM);
                            }
                            break;
                        }
                }
                var responseVM = MapperService.Map<DataTableResponseVM<BookVM>>(responseEM);
                responseVM.draw = model.Draw;

                return responseVM;
            }
            
        }
    }
}
