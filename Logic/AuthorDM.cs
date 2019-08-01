using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Logic
{
    public class AuthorDM
    {
        private DataContext DataContext { get; } = new DataContext();

        internal static AuthorVM MapAuthor(AuthorEM model, bool booksNeeded = false)
        {
            var author = new AuthorVM();
            author.Id = model.Id;
            author.Surname = model.Surname;

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
        }

        private AuthorEM Find(long Id)
        {
            return DataContext.Authors.FirstOrDefault(b => b.Id == Id);
        }

        public IEnumerable<AuthorVM> GetAuthors(DatatableDataVM vm, bool booksNeeded = false)
        {
            var result = new List<AuthorVM>();

            var authors = DataContext.Authors.AsQueryable();
            //add sorting/filtering here later

            foreach (var author in authors)
            {
                result.Add(MapAuthor(author, booksNeeded));
            }

            return result;
        }

        public AuthorVM GetAuthor(long authorId, bool booksNeeded = false)
        {
            return MapAuthor(Find(authorId), booksNeeded);
        }


        public void DeleteAuthor(long authorId)
        {
            var author = Find(authorId);
            author.Books.Clear();
            DataContext.SaveChanges();
        }

        public void UpdateAuthor(AuthorVM model)
        {
            var author = Find(model.Id);
            MapAuthor(author, model);
            DataContext.SaveChanges();
        }
    }
}
