using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.DataTableColumns;

namespace Logic
{
    public class AuthorDM
    {
        private DataContext DataContext { get; } = new DataContext();

        public List<KeyValuePair<long, string>> GetAuthorsByTerm(string term)
        {
            var items = DataContext.Authors.Where(a => (a.Name + " " + a.Surname).ToLower().Contains(term.ToLower()));
            var result = new List<KeyValuePair<long, string>>();
            foreach (var i in items)
            {
                result.Add(new KeyValuePair<long, string>(i.Id, i.Name + " " + i.Surname));
            }
            return result;
        }

        public List<AuthorVM> GetByIds(IEnumerable<long> Ids)
        {
            var result = new List<AuthorVM>();
            foreach (var id in Ids)
            {
                result.Add(MapAuthor(Find(id)));
            }
            return result;
        }

        internal static AuthorVM MapAuthor(AuthorEM model, bool booksNeeded = false)
        {
            var author = new AuthorVM();
            author.Id = model.Id;
            author.Surname = model.Surname;
            author.Name = model.Name;

            if (booksNeeded)
            {
                foreach (var book in model.Books)
                {
                    author.Books.Add(BookDM.MapBook(book));
                }
            }

            author.CountOfBooks = model.Books.Count;

            return author;
        }

        private void MapAuthor(AuthorEM target, AuthorVM model)
        {
            target.Surname = model.Surname;
            target.Name = model.Name;
        }

        private AuthorEM Find(long Id)
        {
            return DataContext.Authors.Include("Books").FirstOrDefault(b => b.Id == Id);
        }

        public DatatableDataVM Get(DataTableVM model, bool booksNeeded = false)
        {
            var result = new DatatableDataVM();

            var list = new List<AuthorVM>();

            var authors = DataContext.Authors.Include("Books").AsQueryable();
            result.draw = model.draw;
            result.recordsTotal = authors.Count();

            switch ((AuthorColumn)model.order[0].column)
            {
                case AuthorColumn.Name:
                    {
                        authors = authors.OrderBy(b => b.Name);
                        break;
                    }
                case AuthorColumn.AmountOfBooks:
                    {
                        authors = authors.OrderBy(b => b.Books.Count);
                        break;
                    }
                case AuthorColumn.Surname:
                    {
                        authors = authors.OrderBy(b => b.Surname);
                        break;
                    }
            }

            foreach (var author in authors)
            {
                list.Add(MapAuthor(author, booksNeeded));
            }

            result.recordsFiltered = list.Count;
            result.data = list;

            return result;
        }

        public AuthorVM Get(long authorId, bool booksNeeded = false)
        {
            return MapAuthor(Find(authorId), booksNeeded);
        }

        public void Add(AuthorVM model)
        {
            var author = new AuthorEM();
            MapAuthor(author, model);
            DataContext.Authors.Add(author);
            DataContext.SaveChanges();
        }

        public void Delete(long authorId)
        {
            var author = Find(authorId);
            author.Books.Clear();
            DataContext.Authors.Remove(author);
            DataContext.SaveChanges();
        }

        public void Update(AuthorVM model)
        {
            var author = Find(model.Id);
            MapAuthor(author, model);
            DataContext.SaveChanges();
        }
    }
}
