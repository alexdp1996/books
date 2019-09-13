using DataInfrastructure.Entities;
using DataInfrastructure.Enums;
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

        public IEnumerable<AuthorEM> Get(DataTableRequestEM model, out int recordsTotal, out int recordsFiltered)
        {
            var authors = DataContext.Authors.Include("Books").AsQueryable();
            recordsTotal = authors.Count();

            var asc = model.Order[0].Asc;

            switch ((AuthorColumn)model.Order[0].Column)
            {
                case AuthorColumn.Name:
                    {
                        if (asc)
                        {
                            authors = authors.OrderBy(b => b.Name);
                        }
                        else
                        {
                            authors = authors.OrderByDescending(b => b.Name);
                        }
                        break;
                    }
                case AuthorColumn.AmountOfBooks:
                    {
                        if (asc)
                        {
                            authors = authors.OrderBy(b => b.Books.Count);
                        }
                        else
                        {
                            authors = authors.OrderByDescending(b => b.Books.Count);
                        }
                        break;
                    }
                case AuthorColumn.Surname:
                    {
                        if (asc)
                        {
                            authors = authors.OrderBy(b => b.Surname);
                        }
                        else
                        {
                            authors = authors.OrderByDescending(b => b.Surname);
                        }
                        break;
                    }
            }

            authors = authors.Skip(model.Start * model.Length).Take(model.Length);

            recordsFiltered = authors.Count();

            return authors;
        }
    }
}
