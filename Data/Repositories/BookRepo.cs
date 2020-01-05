using DataInfrastructure.Entities;
using DataInfrastructure.Enums;
using DataInfrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class BookRepo : IBookRepo
    {
        private DataContext DataContext { get; }

        public BookRepo()
        {
            DataContext = new DataContext();
        }


        public long Create(BookEM entity)
        {
            DataContext.Set<BookEM>().Add(entity);
            DataContext.SaveChanges();
            return entity.Id.Value;
        }

        public void Update(BookEM entity)
        {
            var entry = Get(entity.Id.Value);
            DataContext.Entry(entry).CurrentValues.SetValues(entity);
            DataContext.SaveChanges();
        }

        public void Delete(long id)
        {
            DataContext.Set<BookEM>().Remove(Get(id));
            DataContext.SaveChanges();
        }

        public BookEM Get(long id)
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

        public DataTableResponseEM<BookEM> Get(DataTableRequestEM model)
        {
            Expression<Func<BookEM, object>> selector;

            var order = model.Order[0];
            switch (order.Column)
            {
                case (int)BookColumn.Date: { selector = b => b.CreatedDate; break; }
                case (int)BookColumn.Pages: { selector = b => b.Pages; break; }
                case (int)BookColumn.Rate: { selector = b => b.Rate; break; }
                case (int)BookColumn.Name:
                default: { selector = b => b.Name; break; }
            }

            var response = new DataTableResponseEM<BookEM>();

            var books = DataContext.Books.Include("Authors").AsQueryable();
            response.RecordsTotal = books.Count();

            if (order.IsAcs)
            {
                books = books.OrderBy(selector);
            }
            else
            {
                books = books.OrderByDescending(selector);
            }

            response.RecordsFiltered = books.Count();

            books = books.Skip(model.Start).Take(model.Length);

            response.Data = books;

            return response;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
