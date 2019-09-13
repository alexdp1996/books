﻿using DataInfrastructure.Entities;
using DataInfrastructure.Enums;
using DataInfrastructure.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public class BookRepo : BaseRepo<BookEM>, IBookRepo
    {

        public BookRepo(DataContext context) : base(context)
        {
        }

        public override BookEM Get(long id)
        {
            return DataContext.Books.Include("Authors").FirstOrDefault(b => b.Id == id);
        }

        public long Save(UpdatableBookEM book)
        {
            book.Authors.Clear();
            var authorRepo = new AuthorRepo(DataContext);
            var authors = authorRepo.Get(book.AuthorIds);
            book.Authors.AddRange(authors);
            return base.Save(book);
        }

        public IEnumerable<BookEM> Get(DataTableRequestEM model, out int recordsTotal, out int recordsFiltered)
        {
            var books = DataContext.Books.Include("Authors").AsQueryable();
            recordsTotal = books.Count();

            var asc = model.Order[0].Asc;

            switch ((BookColumn)model.Order[0].Column)
            {
                case BookColumn.Name:
                    {
                        if (asc)
                        {
                            books = books.OrderBy(b => b.Name);
                        }
                        else
                        {
                            books = books.OrderByDescending(b => b.Name);
                        }
                        break;
                    }
                case BookColumn.Pages:
                    {
                        if (asc)
                        {
                            books = books.OrderBy(b => b.Pages);
                        }
                        else
                        {
                            books = books.OrderByDescending(b => b.Pages);
                        }
                        break;
                    }
                case BookColumn.Rate:
                    {
                        if (asc)
                        {
                            books = books.OrderBy(b => b.Rate);
                        }
                        else
                        {
                            books = books.OrderByDescending(b => b.Rate);
                        }
                            
                        break;
                    }
                case BookColumn.Date:
                    {
                        if (asc)
                        {
                            books = books.OrderBy(b => b.Date);
                        }
                        else
                        {
                            books = books.OrderByDescending(b => b.Date);
                        }
                        break;
                    }
            }

            books = books.Skip(model.Start * model.Length).Take(model.Length);

            recordsFiltered = books.Count();

            return books;
        }
    }
}
