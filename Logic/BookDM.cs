using Data;
using Data.Repositories;
using Entities;
using Entities.Enums;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

namespace Logic
{
    public class BookDM
    {
        private DataContext DataContext { get; } 
        private BookRepo BookRepo { get; }

        public BookDM()
        {
            DataContext = new DataContext();
            BookRepo = new BookRepo(DataContext);
        }

        static internal BookVM MapBook(BookEM model)
        {
            var book = new BookVM
            {
                Id = model.Id,
                Name = model.Name,
                Rate = model.Rate,
                Pages = model.Pages,
                Date = model.Date
            };

            foreach (var author in model.Authors)
            {
                book.Authors.Add(AuthorDM.MapAuthor(author));
            }

            return book;
        }

        private void MapBook(BookEM target, BookVM model)
        {
            target.Id = model.Id;
            target.Name = model.Name;
            target.Rate = model.Rate;
            target.Pages = model.Pages;
            target.Date = model.Date;
        }

        public void Add(BookVM model)
        {
            var book = new BookEM();
            MapBook(book, model);
            BookRepo.Add(book);
        }

        public void Delete(long bookId)
        {
            BookRepo.Delete(bookId);
        }

        public void Update(BookEditVM model)
        {
            var book = new BookEM();
            MapBook(book, model);
            BookRepo.Update(book);
            BookRepo.UpdateAuthors(model.Id, model.AuthorIds);
        }

        public BookVM Get(long bookId)
        {
            return MapBook(BookRepo.Get(bookId));
        }

        public DataTableDataVM Get(DataTableVM model)
        {
            var result = new DataTableDataVM();

            var books = BookRepo.Get(new DataTableEM
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

            var list = new List<BookVM>();
            foreach (var book in books)
            {
                list.Add(MapBook(book));
            }

            result.data = list;

            result.recordsFiltered = recordsFiltered;
            result.recordsTotal = recordsTotal;
            result.draw = model.draw;

            return result;
        }
    }
}
