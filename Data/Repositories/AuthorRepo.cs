using DataInfrastructure.Entities;
using DataInfrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class AuthorRepo : IAuthorRepo
    {
        private DataContext DataContext { get; }

        public AuthorRepo()
        {
            DataContext = new DataContext();
        }

        internal AuthorRepo(DataContext context)
        {
            DataContext = context;
        }

        public AuthorEM Get(long id)
        {
            return DataContext.Authors.Include("Books").FirstOrDefault(b => b.Id == id);
        }

        public IEnumerable<AuthorEM> Get(IEnumerable<long> Ids)
        {
            return DataContext.Authors.Where(a => Ids.Contains(a.Id.Value));
        }

        public IEnumerable<AuthorEM> Get(string term)
        {
            return DataContext.Authors.Where(a => (a.Name + " " + a.Surname).ToLower().Contains(term.ToLower()));
        }

        public DataTableResponseEM<AuthorEM> Get(DataTableRequestEM model)
        {
            Expression<Func<AuthorEM, object>> selector;

            var order = model.Order[0];
            var orderColumn = model.Columns[order.Column];
            switch (orderColumn.Name)
            {
                case "Surname": { selector = a => a.Surname; break; }
                case "CountOfBooks": { selector = a => a.Books.Count; break; }
                case "Name":
                default: { selector = a => a.Name; break; }
            }

            var response = new DataTableResponseEM<AuthorEM>();

            var authors = DataContext.Authors.Include("Books").AsQueryable();
            response.RecordsTotal = authors.Count();

            if (order.IsAcs)
            {
                authors = authors.OrderBy(selector);
            }
            else
            {
                authors = authors.OrderByDescending(selector);
            }

            response.RecordsFiltered = authors.Count();

            authors = authors.Skip(model.Start).Take(model.Length);

            response.Data = authors;

            return response;
        }

        public long Create(AuthorEM entity)
        {
            DataContext.Set<AuthorEM>().Add(entity);
            DataContext.SaveChanges();
            return entity.Id.Value;
        }

        public void Update(AuthorEM entity)
        {
            var entry = Get(entity.Id.Value);
            DataContext.Entry(entry).CurrentValues.SetValues(entity);
            DataContext.SaveChanges();
        }

        public void Delete(long id)
        {
            DataContext.Set<AuthorEM>().Remove(Get(id));
            DataContext.SaveChanges();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
