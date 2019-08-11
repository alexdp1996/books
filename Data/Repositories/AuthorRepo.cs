using Entities;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class AuthorRepo
    {
        private DataContext DataContext { get; }

        public AuthorRepo(DataContext context)
        {
            DataContext = context;
        }

        public AuthorEM Get(long id)
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

        public IEnumerable<AuthorEM> Get(DataTableEM model, out int recordsTotal, out int recordsFiltered)
        {
            var authors = DataContext.Authors.Include("Books").AsQueryable();
            recordsTotal = authors.Count();

            var asc = model.Order[0].Dir == "asc";

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

        public void Update(AuthorEM author)
        {
            DataContext.Entry(Get(author.Id)).CurrentValues.SetValues(author);
            DataContext.SaveChanges();
        }

        public void Add(AuthorEM author)
        {
            DataContext.Authors.Add(author);
            DataContext.SaveChanges();
        }

        public void Delete(long id)
        {
            var author = Get(id);
            DataContext.Authors.Remove(author);
            DataContext.SaveChanges();
        }
    }
}
