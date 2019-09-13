using AutoMapper;
using Data;
using Data.Repositories;
using DataInfrastructure.Entities;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

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

            using (var unit = new UnitOfWork())
            {
                var booksEM = unit.Book.Get(dataTableEM, out int recordsTotal, out int recordsFiltered);
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
