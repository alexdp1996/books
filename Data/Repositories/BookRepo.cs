using DataInfrastructure.Entities;
using DataInfrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public class BookRepo : BaseRepo<BookEM>, IBookRepo
    {

        public BookRepo(DataContext context) : base(context)
        {
        }

        public override BookEM Get(long id)
        {
            return DataContext.Books.Include("Authors").FirstOrDefault(b => b.Id == id);
        }

        public long Save(UpdatableBookEM book)
        {
            book.Authors.Clear();
            var id = base.Save(book);
            var returnedBook = Get(id);
            var authorRepo = new AuthorRepo(DataContext);
            var authors = authorRepo.Get(book.AuthorIds);
            returnedBook.Authors.AddRange(authors);
            DataContext.SaveChanges();
            return returnedBook.Id;
        }

        private DataTableResponseEM<BookEM> Get(DataTableRequestEM model, Func<IQueryable<BookEM>, IOrderedQueryable<BookEM>> orderFunc)
        {
            var response = new DataTableResponseEM<BookEM>();

            var books = DataContext.Books.Include("Authors").AsQueryable();
            response.RecordsTotal = books.Count();

            books = orderFunc(books);
            books = books.Skip(model.Start * model.Length).Take(model.Length);

            response.RecordsFiltered = books.Count();
            response.Data = books;

            return response;
        }

        public DataTableResponseEM<BookEM> GetByNameAsc(DataTableRequestEM model)
        {
            return Get(model, books => books.OrderBy(b => b.Name));
        }

        public DataTableResponseEM<BookEM> GetByNameDesc(DataTableRequestEM model)
        {
            return Get(model, books => books.OrderByDescending(b => b.Name));
        }

        public DataTableResponseEM<BookEM> GetByPagesAsc(DataTableRequestEM model)
        {
            return Get(model, books => books.OrderBy(b => b.Pages));
        }

        public DataTableResponseEM<BookEM> GetByPagesDesc(DataTableRequestEM model)
        {
            return Get(model, books => books.OrderByDescending(b => b.Pages));
        }

        public DataTableResponseEM<BookEM> GetByRateAsc(DataTableRequestEM model)
        {
            return Get(model, books => books.OrderBy(b => b.Rate));
        }

        public DataTableResponseEM<BookEM> GetByRateDesc(DataTableRequestEM model)
        {
            return Get(model, books => books.OrderByDescending(b => b.Rate));
        }

        public DataTableResponseEM<BookEM> GetByDateAsc(DataTableRequestEM model)
        {
            return Get(model, books => books.OrderBy(b => b.Date));
        }

        public DataTableResponseEM<BookEM> GetByDateDesc(DataTableRequestEM model)
        {
            return Get(model, books => books.OrderByDescending(b => b.Date));
        }
    }
}
