using DataInfrastructure.Entities;
using DataInfrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public class AuthorRepo : BaseEntityRepo<AuthorEM>, IAuthorRepo
    {
        public AuthorRepo()
        {

        }

        internal AuthorRepo(DataContext context) : base(context)
        {

        }

        public override AuthorEM Get(long id)
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

        private DataTableResponseEM<AuthorEM> Get(DataTableRequestEM model, Func<IQueryable<AuthorEM>, IOrderedQueryable<AuthorEM>> orderFunc)
        {
            var response = new DataTableResponseEM<AuthorEM>();

            var authors = DataContext.Authors.Include("Books").AsQueryable();
            response.RecordsTotal = authors.Count();

            authors = orderFunc(authors);

            response.RecordsFiltered = authors.Count();

            authors = authors.Skip(model.Start * model.Length).Take(model.Length);

            response.Data = authors;

            return response;
        }

        public DataTableResponseEM<AuthorEM> GetByAmountOfBooksAsc(DataTableRequestEM model)
        {
            return Get(model, authors => authors.OrderBy(a => a.Books.Count));
        }

        public DataTableResponseEM<AuthorEM> GetByAmountOfBooksDesc(DataTableRequestEM model)
        {
            return Get(model, authors => authors.OrderByDescending(a => a.Books.Count));
        }

        public DataTableResponseEM<AuthorEM> GetByNameAsc(DataTableRequestEM model)
        {
            return Get(model, authors => authors.OrderBy(a => a.Name));
        }

        public DataTableResponseEM<AuthorEM> GetByNameDesc(DataTableRequestEM model)
        {
            return Get(model, authors => authors.OrderByDescending(a => a.Name));
        }

        public DataTableResponseEM<AuthorEM> GetBySurnameAsc(DataTableRequestEM model)
        {
            return Get(model, authors => authors.OrderBy(a => a.Surname));
        }

        public DataTableResponseEM<AuthorEM> GetBySurnameDesc(DataTableRequestEM model)
        {
            return Get(model, authors => authors.OrderByDescending(b => b.Surname));
        }
    }
}
