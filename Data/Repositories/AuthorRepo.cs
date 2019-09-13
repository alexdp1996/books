using DataInfrastructure.Entities;
using DataInfrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public class AuthorRepo : BaseRepo<AuthorEM>, IAuthorRepo
    {

        public AuthorRepo(DataContext context) : base(context)
        {
        }

        public override AuthorEM Get(long id)
        {
            return DataContext.Authors.Include("Books").FirstOrDefault(b => b.Id == id);
        }

        public IEnumerable<AuthorEM> Get(IEnumerable<long> Ids)
        {
            return DataContext.Authors.Where(a => Ids.Contains(a.Id));
        }

        public IEnumerable<AuthorEM> Get(string term)
        {
            return DataContext.Authors.Where(a => (a.Name + " " + a.Surname).ToLower().Contains(term.ToLower()));
        }

        private IEnumerable<AuthorEM> Get(DataTableRequestEM model, Func<IQueryable<AuthorEM>, IOrderedQueryable<AuthorEM>> orderFunc, out int recordsTotal, out int recordsFiltered)
        {
            var authors = DataContext.Authors.Include("Books").AsQueryable();
            recordsTotal = authors.Count();

            authors = orderFunc(authors);

            authors = authors.Skip(model.Start * model.Length).Take(model.Length);

            recordsFiltered = authors.Count();

            return authors;
        }

        public IEnumerable<AuthorEM> GetByAmountOfBooksAsc(DataTableRequestEM model, out int recordsTotal, out int recordsFiltered)
        {
            return Get(model, authors => authors.OrderBy(a => a.Books.Count), out recordsTotal, out recordsFiltered);
        }

        public IEnumerable<AuthorEM> GetByAmountOfBooksDesc(DataTableRequestEM model, out int recordsTotal, out int recordsFiltered)
        {
            return Get(model, authors => authors.OrderByDescending(a => a.Books.Count), out recordsTotal, out recordsFiltered);
        }

        public IEnumerable<AuthorEM> GetByNameAsc(DataTableRequestEM model, out int recordsTotal, out int recordsFiltered)
        {
            return Get(model, authors => authors.OrderBy(a => a.Name), out recordsTotal, out recordsFiltered);
        }

        public IEnumerable<AuthorEM> GetByNameDesc(DataTableRequestEM model, out int recordsTotal, out int recordsFiltered)
        {
            return Get(model, authors => authors.OrderByDescending(a => a.Name), out recordsTotal, out recordsFiltered);
        }

        public IEnumerable<AuthorEM> GetBySurnameAsc(DataTableRequestEM model, out int recordsTotal, out int recordsFiltered)
        {
            return Get(model, authors => authors.OrderBy(a => a.Surname), out recordsTotal, out recordsFiltered);
        }

        public IEnumerable<AuthorEM> GetBySurnameDesc(DataTableRequestEM model, out int recordsTotal, out int recordsFiltered)
        {
            return Get(model, authors => authors.OrderByDescending(b => b.Surname), out recordsTotal, out recordsFiltered);
        }
    }
}
