using Data;
using Data.Repositories;
using DataInfrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class UnitOfWork : IDisposable
    {
        private DataContext DataContext { get; }
        public IAuthorRepo Author { get; }
        public IBookRepo Book { get; }

        public UnitOfWork()
        {
            DataContext = new DataContext();
            Author = new AuthorRepo(DataContext);
            Book = new BookRepo(DataContext);
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
