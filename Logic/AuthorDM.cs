using Data;
using Data.Repositories;
using Entities;
using Entities.Enums;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

namespace Logic
{
    public class AuthorDM
    {
        private DataContext DataContext { get; }
        private AuthorRepo AuthorRepo { get; }

        public AuthorDM()
        {
            DataContext = new DataContext();
            AuthorRepo  = new AuthorRepo(DataContext);
        }

        public IEnumerable<AuthorBaseVM> Get(string term)
        {
            var authors = AuthorRepo.Get(term);
            var result = new List<AuthorBaseVM>();
            foreach (var author in authors)
            {
                result.Add(new AuthorBaseVM
                {
                    Id = author.Id,
                    Name = author.Name,
                    Surname = author.Surname
                });
            }
            return result;
        }

        public IEnumerable<AuthorBaseVM> Get(IEnumerable<long> ids)
        {
            var result = new List<AuthorBaseVM>();
            foreach (var author in AuthorRepo.Get(ids))
            {
                result.Add(MapAuthor(author));
            }
            return result;
        }

        internal static AuthorVM MapAuthor(AuthorEM model, bool booksNeeded = false)
        {
            var author = new AuthorVM
            {
                Id = model.Id,
                Surname = model.Surname,
                Name = model.Name
            };

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

        public DataTableDataVM Get(DataTableVM model)
        {
            var result = new DataTableDataVM();

            var authors = AuthorRepo.Get(new DataTableEM
            {
                Start = model.start,
                Length = model.length,
                Order = new List<OrderEM>
                {
                    new OrderEM
                    {
                        Column = model.order[0].column,
                        Dir = model.order[0].dir
                    }
                }
            }, out int recordsTotal, out int recordsFiltered);

            var list = new List<AuthorBaseVM>();
            foreach (var author in authors)
            {
                list.Add(MapAuthor(author));
            }

            result.data = list;

            result.recordsFiltered = recordsFiltered;
            result.recordsTotal = recordsTotal;
            result.draw = model.draw;

            return result;
        }

        public AuthorVM Get(long authorId, bool booksNeeded = false)
        {
            return MapAuthor(AuthorRepo.Get(authorId), booksNeeded);
        }

        public void Add(AuthorVM model)
        {
            var author = new AuthorEM();
            MapAuthor(author, model);
            AuthorRepo.Add(author);
        }

        public void Delete(long authorId)
        {
            AuthorRepo.Delete(authorId);
        }

        public void Update(AuthorVM model)
        {
            var author = AuthorRepo.Get(model.Id);
            MapAuthor(author, model);
            AuthorRepo.Update(author);
        }
    }
}
