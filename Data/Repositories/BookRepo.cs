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
            var authorRepo = new AuthorRepo(DataContext);
            var authors = authorRepo.Get(book.AuthorIds);
            book.Authors.AddRange(authors);
            return base.Save(book);
        }

        private IEnumerable<BookEM> Get(DataTableRequestEM model, Func<IQueryable<BookEM>, IOrderedQueryable<BookEM>> orderFunc, out int recordsTotal, out int recordsFiltered)
        {
            var books = DataContext.Books.Include("Authors").AsQueryable();
            recordsTotal = books.Count();

            books = orderFunc(books);

            books = books.Skip(model.Start * model.Length).Take(model.Length);

            recordsFiltered = books.Count();

            return books;
        }

        public IEnumerable<BookEM> GetByNameAsc(DataTableRequestEM model, out int recordsTotal, out int recordsFiltered)
        {
            return Get(model, books => books.OrderBy(b => b.Name), out recordsTotal, out recordsFiltered);
        }

        public IEnumerable<BookEM> GetByNameDesc(DataTableRequestEM model, out int recordsTotal, out int recordsFiltered)
        {
            return Get(model, books => books.OrderByDescending(b => b.Name), out recordsTotal, out recordsFiltered);
        }

        public IEnumerable<BookEM> GetByPagesAsc(DataTableRequestEM model, out int recordsTotal, out int recordsFiltered)
        {
            return Get(model, books => books.OrderBy(b => b.Pages), out recordsTotal, out recordsFiltered);
        }

        public IEnumerable<BookEM> GetByPagesDesc(DataTableRequestEM model, out int recordsTotal, out int recordsFiltered)
        {
            return Get(model, books => books.OrderByDescending(b => b.Pages), out recordsTotal, out recordsFiltered);
        }

        public IEnumerable<BookEM> GetByRateAsc(DataTableRequestEM model, out int recordsTotal, out int recordsFiltered)
        {
            return Get(model, books => books.OrderBy(b => b.Rate), out recordsTotal, out recordsFiltered);
        }

        public IEnumerable<BookEM> GetByRateDesc(DataTableRequestEM model, out int recordsTotal, out int recordsFiltered)
        {
            return Get(model, books => books.OrderByDescending(b => b.Rate), out recordsTotal, out recordsFiltered);
        }

        public IEnumerable<BookEM> GetByDateAsc(DataTableRequestEM model, out int recordsTotal, out int recordsFiltered)
        {
            return Get(model, books => books.OrderBy(b => b.Date), out recordsTotal, out recordsFiltered);
        }

        public IEnumerable<BookEM> GetByDateDesc(DataTableRequestEM model, out int recordsTotal, out int recordsFiltered)
        {
            return Get(model, books => books.OrderByDescending(b => b.Date), out recordsTotal, out recordsFiltered);
        }
    }
}
