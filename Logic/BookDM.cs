using AutoMapper;
using Data;
using Data.Repositories;
using Entities;
using Entities.Enums;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

namespace Logic
{
    public class BookDM
    {
        private DataContext DataContext { get; } 
        private BookRepo BookRepo { get; }

        public BookDM()
        {
            DataContext = new DataContext();
            BookRepo = new BookRepo(DataContext);
        }

        public void Add(BookVM model)
        {
            var book = Mapper.Map<BookEM>(model);
            BookRepo.Add(book);
        }

        public void Delete(long bookId)
        {
            BookRepo.Delete(bookId);
        }

        public void Update(BookEditVM model)
        {
            var book = Mapper.Map<BookEM>(model);
            BookRepo.Update(book);
            BookRepo.UpdateAuthors(model.Id, model.AuthorIds);
        }

        public BookVM Get(long bookId)
        {
            return Mapper.Map<BookVM>(BookRepo.Get(bookId));
        }

        public DataTableDataVM Get(DataTableVM model)
        {
            var result = new DataTableDataVM();

            var dataTableEM = Mapper.Map<DataTableEM>(model);

            var books = BookRepo.Get(dataTableEM, out int recordsTotal, out int recordsFiltered);

            result.data = Mapper.Map<IEnumerable<BookVM>>(books);
            result.recordsFiltered = recordsFiltered;
            result.recordsTotal = recordsTotal;
            result.draw = model.Draw;

            return result;
        }
    }
}
