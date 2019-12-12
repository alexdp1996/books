using DataInfrastructure.Entities;
using DataInfrastructure.Enums;
using DataInfrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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

        public DataTableResponseEM<AuthorEM> Get(DataTableRequestEM model)
        {
            Expression<Func<AuthorEM, object>> selector;

            var order = model.Order[0];
            switch (order.Column)
            {
                case (int)AuthorColumn.Surname: { selector = a => a.Surname; break; }
                case (int)AuthorColumn.AmountOfBooks: { selector = a => a.Books.Count; break; }
                case (int)AuthorColumn.Name:
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
    }
}
