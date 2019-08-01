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
    public class BookDM
    {
        private DataContext DataContext { get; } = new DataContext();

        static internal BookVM MapBook(BookEM model)
        {
            var book = new BookVM();
            book.Id = model.Id;
            book.Name = model.Name;
            book.Rate = model.Rate;

            foreach (var author in model.Authors)
            {
                book.Authors.Add(AuthorDM.MapAuthor(author));
            }

            return book;
        }

        private void MapBook(BookEM target, BookVM model)
        {
            target.Name = model.Name;
            target.Rate = model.Rate;
        }

        private BookEM Find(long Id)
        {
            return DataContext.Books.FirstOrDefault(b => b.Id == Id);
        }

        public void AddBook(BookVM model, long authorId)
        {
            var book = new BookEM();
            MapBook(book, model);
            DataContext.SaveChanges();
        }

        public void DeleteBook(long bookId)
        {
            var book = Find(bookId);
            book.Authors.Clear();
            DataContext.Books.Remove(book);
            DataContext.SaveChanges();
        }

        public void Update(BookVM model)
        {
            var book = Find(model.Id);
            MapBook(book, model);
            DataContext.SaveChanges();
        }

        public BookVM GetBook(long bookId)
        {
            return MapBook(Find(bookId));
        }

        public IEnumerable<BookVM> GetBooks(DatatableDataVM model)
        {
            var result = new List<BookVM>();
            
            var books = DataContext.Books.AsQueryable();
            //add sorting/filtering here later

            foreach (var book in books)
            {
                result.Add(MapBook(book));
            }

            return result;
        }

        public void AddAuthorToBook(long bookId, long authorId)
        {
            var book = Find(bookId);
            book.Authors.Add(DataContext.Authors.FirstOrDefault(a => a.Id == authorId));
            DataContext.SaveChanges();
        }
    }
}
