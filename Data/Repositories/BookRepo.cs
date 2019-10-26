using DataInfrastructure.Entities;
using DataInfrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public class BookRepo : BaseEntityRepo<BookEM>, IBookRepo
    {

        public override BookEM Get(long id)
        {
            return DataContext.Books.Include("Authors").FirstOrDefault(b => b.Id == id);
        }

        public void UpdateAuthors(long bookId, IEnumerable<long> authorIds)
        {
            var book = Get(bookId);
            var authorRepo = new AuthorRepo(DataContext);
            var authors = authorRepo.Get(authorIds);
            book.Authors.Clear();
            book.Authors.AddRange(authors);
            DataContext.SaveChanges();
        }

        private DataTableResponseEM<BookEM> Get(DataTableRequestEM model, Func<IQueryable<BookEM>, IOrderedQueryable<BookEM>> orderFunc)
        {
            var response = new DataTableResponseEM<BookEM>();

            var books = DataContext.Books.Include("Authors").AsQueryable();
            response.RecordsTotal = books.Count();

            books = orderFunc(books);

            response.RecordsFiltered = books.Count();

            books = books.Skip(model.Start * model.Length).Take(model.Length);

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
