﻿using Data;
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
            book.Pages = model.Pages;

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
            target.Pages = model.Pages;
            target.Authors.RemoveAll(a => !model.AuthorIds.Contains(a.Id));
            var leftIds = target.Authors.Select(a => a.Id).ToList();
            var newIds = model.AuthorIds.Where(id => !leftIds.Contains(id));
            foreach (var authorId in newIds)
            {
                target.Authors.Add(DataContext.Authors.FirstOrDefault(a => a.Id == authorId));
            }
        }

        private BookEM Find(long Id)
        {
            return DataContext.Books.FirstOrDefault(b => b.Id == Id);
        }

        public void AddBook(BookVM model)
        {
            var book = new BookEM();
            MapBook(book, model);
            DataContext.Books.Add(book);
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

        public DatatableDataVM GetBooks(DataTableVM model)
        {
            var result = new DatatableDataVM();

            var list = new List<BookVM>();
            
            var books = DataContext.Books.AsQueryable();
            result.draw = model.draw;
            result.recordsTotal = books.Count();

            //add sorting/filtering here later

            foreach (var book in books)
            {
                list.Add(MapBook(book));
            }
            result.recordsFiltered = list.Count;
            result.data = list;

            return result;
        }
    }
}
