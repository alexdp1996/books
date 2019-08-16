using AutoMapper;
using Data;
using Data.Repositories;
using Entities;
using System.Collections.Generic;
using ViewModels;

namespace Logic
{
    public class BookDM
    {
        public void Delete(long id)
        {
            using (var сontext = new DataContext())
            using (var bookRepo = new BookRepo(сontext))
            {
                bookRepo.Delete(id);
            }
        }

        public void Save(BookEditVM model)
        {
            var book = Mapper.Map<BookEM>(model);
            using (var сontext = new DataContext())
            using (var bookRepo = new BookRepo(сontext))
            {
                bookRepo.Save(book);
                bookRepo.UpdateAuthors(model.Id, model.AuthorIds);
            }
        }

        public BookVM Get(long id)
        {
            using (var сontext = new DataContext())
            using (var bookRepo = new BookRepo(сontext))
            {
                var bookEM = bookRepo.Get(id);
                var bookVM = Mapper.Map<BookVM>(bookEM);
                return bookVM;
            }
        }

        public DataTableDataVM Get(DataTableVM model)
        {
            var result = new DataTableDataVM();

            var dataTableEM = Mapper.Map<DataTableEM>(model);

            using (var сontext = new DataContext())
            using (var bookRepo = new BookRepo(сontext))
            {
                var booksEM = bookRepo.Get(dataTableEM, out int recordsTotal, out int recordsFiltered);
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
