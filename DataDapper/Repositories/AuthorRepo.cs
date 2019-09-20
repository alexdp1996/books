using System;
using System.Collections.Generic;
using DataInfrastructure.Entities;
using DataInfrastructure.Interfaces;

namespace DataDapper.Repositories
{
    public class AuthorRepo : BaseRepo, IAuthorRepo
    {
        public void Delete(long id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<AuthorEM> Get(IEnumerable<long> Ids)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<AuthorEM> Get(string term)
        {
            throw new System.NotImplementedException();
        }

        public AuthorEM Get(long id)
        {
            throw new System.NotImplementedException();
        }

        public DataTableResponseEM<AuthorEM> GetByAmountOfBooksAsc(DataTableRequestEM model)
        {
            throw new System.NotImplementedException();
        }

        public DataTableResponseEM<AuthorEM> GetByAmountOfBooksDesc(DataTableRequestEM model)
        {
            throw new System.NotImplementedException();
        }

        public DataTableResponseEM<AuthorEM> GetByNameAsc(DataTableRequestEM model)
        {
            throw new System.NotImplementedException();
        }

        public DataTableResponseEM<AuthorEM> GetByNameDesc(DataTableRequestEM model)
        {
            throw new System.NotImplementedException();
        }

        public DataTableResponseEM<AuthorEM> GetBySurnameAsc(DataTableRequestEM model)
        {
            throw new System.NotImplementedException();
        }

        public DataTableResponseEM<AuthorEM> GetBySurnameDesc(DataTableRequestEM model)
        {
            throw new System.NotImplementedException();
        }

        public long Save(AuthorEM entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
