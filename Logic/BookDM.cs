using AutoMapper;
using DataInfrastructure.Entities;
using System.Collections.Generic;
using System.Linq;
using ViewModels;
using ViewModels.Enums;

namespace Logic
{
    public class BookDM
    {
        public void Delete(long id)
        {
            using (var unit = new UnitOfWork())
            {
                unit.BookDapper.Delete(id);
            }
        }

        public void Save(BookEditVM model)
        {
            var book = Mapper.Map<UpdatableBookEM>(model);
            using (var unit = new UnitOfWork())
            {
                var id = unit.Book.Save(book);
            }
        }

        public BookVM Get(long id)
        {
            using (var unit = new UnitOfWork())
            {
                var bookEM = unit.BookDapper.Get(id);
                var bookVM = Mapper.Map<BookVM>(bookEM);
                return bookVM;
            }
        }

        public BookVM Get(BookEditVM book)
        {
            var result = Mapper.Map<BookVM>(book);

            var authorDM = new AuthorDM();
            result.Authors = authorDM.Get(book.AuthorIds).ToList();

            return result;
        }

        public DataTableResponseVM<BookVM> Get(DataTableRequestVM model)
        {
            var dataTableEM = Mapper.Map<DataTableRequestEM>(model);

            var asc = model.Order[0].Dir == "asc";
            var column = (BookColumn) model.Order[0].Column;
            using (var unit = new UnitOfWork())
            {
                DataTableResponseEM<BookEM> responseEM;
                switch (column)
                {
                    default:
                    case BookColumn.Name:
                        {
                            if (asc)
                            {
                                responseEM = unit.Book.GetByNameAsc(dataTableEM);
                            }
                            else
                            {
                                responseEM = unit.Book.GetByNameDesc(dataTableEM);
                            }
                            break;
                        }
                    case BookColumn.Pages:
                        {
                            if (asc)
                            {
                                responseEM = unit.Book.GetByPagesAsc(dataTableEM);
                            }
                            else
                            {
                                responseEM = unit.Book.GetByPagesDesc(dataTableEM);
                            }
                            break;
                        }
                    case BookColumn.Rate:
                        {
                            if (asc)
                            {
                                responseEM = unit.Book.GetByRateAsc(dataTableEM);
                            }
                            else
                            {
                                responseEM = unit.Book.GetByRateDesc(dataTableEM);
                            }
                            break;
                        }
                    case BookColumn.Date:
                        {
                            if (asc)
                            {
                                responseEM = unit.Book.GetByDateAsc(dataTableEM);
                            }
                            else
                            {
                                responseEM = unit.Book.GetByDateDesc(dataTableEM);
                            }
                            break;
                        }
                }
                var responseVM = Mapper.Map<DataTableResponseVM<BookVM>>(responseEM);
                responseVM.draw = model.Draw;

                return responseVM;
            }
            
        }
    }
}
