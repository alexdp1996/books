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
                unit.Book.Delete(id);
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
                var bookEM = unit.Book.Get(id);
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

        public DataTableResponseVM Get(DataTableRequestVM model)
        {
            var result = new DataTableResponseVM();

            var dataTableEM = Mapper.Map<DataTableRequestEM>(model);

            var asc = model.Order[0].Dir == "asc";
            var column = (BookColumn) model.Order[0].Column;
            using (var unit = new UnitOfWork())
            {
                IEnumerable<BookEM> booksEM;
                int recordsTotal;
                int recordsFiltered;
                switch (column)
                {
                    default:
                    case BookColumn.Name:
                        {
                            if (asc)
                            {
                                booksEM = unit.Book.GetByNameAsc(dataTableEM, out recordsTotal, out recordsFiltered);
                            }
                            else
                            {
                                booksEM = unit.Book.GetByNameDesc(dataTableEM, out recordsTotal, out recordsFiltered);
                            }
                            break;
                        }
                    case BookColumn.Pages:
                        {
                            if (asc)
                            {
                                booksEM = unit.Book.GetByPagesAsc(dataTableEM, out recordsTotal, out recordsFiltered);
                            }
                            else
                            {
                                booksEM = unit.Book.GetByPagesDesc(dataTableEM, out recordsTotal, out recordsFiltered);
                            }
                            break;
                        }
                    case BookColumn.Rate:
                        {
                            if (asc)
                            {
                                booksEM = unit.Book.GetByRateAsc(dataTableEM, out recordsTotal, out recordsFiltered);
                            }
                            else
                            {
                                booksEM = unit.Book.GetByRateDesc(dataTableEM, out recordsTotal, out recordsFiltered);
                            }
                            break;
                        }
                    case BookColumn.Date:
                        {
                            if (asc)
                            {
                                booksEM = unit.Book.GetByDateAsc(dataTableEM, out recordsTotal, out recordsFiltered);
                            }
                            else
                            {
                                booksEM = unit.Book.GetByDateDesc(dataTableEM, out recordsTotal, out recordsFiltered);
                            }
                            break;
                        }
                }
                var booksVM = Mapper.Map<IEnumerable<BookVM>>(booksEM);

                result.data = booksVM;
                result.recordsFiltered = recordsFiltered;
                result.recordsTotal = recordsTotal;
            }
            result.draw = model.Draw;

            return result;
        }
    }
}
