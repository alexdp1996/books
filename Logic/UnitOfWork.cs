using Data;
using DataInfrastructure.Interfaces;
using System;

namespace Logic
{
    public class UnitOfWork : IDisposable
    {
        public IBookRepo Book { get; }
        public IAuthorRepo Author { get; }

        public UnitOfWork()
        {
            //var context = new DataContext();
            //Book = new Data.Repositories.BookRepo(context);
            //Author = new Data.Repositories.AuthorRepo(context);

            Book = new DataDapper.Repositories.BookRepo();
            Author = new DataDapper.Repositories.AuthorRepo();
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
