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
        private DataContext DataContext { get; } 

        public BookDM()
        {
            DataContext = new DataContext();
        }

        public void Add(BookVM model)
        {
            var book = Mapper.Map<BookEM>(model);
            using (var bookRepo = new BookRepo(DataContext))
            {
                bookRepo.Add(book);
            }
        }

        public void Delete(long bookId)
        {
            using (var bookRepo = new BookRepo(DataContext))
            {
                bookRepo.Delete(bookId);
            }
        }

        public void Update(BookEditVM model)
        {
            var book = Mapper.Map<BookEM>(model);
            using (var bookRepo = new BookRepo(DataContext))
            {
                bookRepo.Update(book);
                bookRepo.UpdateAuthors(model.Id, model.AuthorIds);
            }
        }

        public BookVM Get(long id)
        {
            using (var bookRepo = new BookRepo(DataContext))
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

            using (var bookRepo = new BookRepo(DataContext))
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
