using Dapper;
using DataInfrastructure.Entities;
using DataInfrastructure.Interfaces;
using System;
using System.Data;

namespace DataDapper.Repositories
{
    public class BookRepo : BaseRepo, IBookRepo
    {
        public void Delete(long id)
        {
            using (var con = Connection)
            {
                var SP = "USP_Delete_Book";
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@id", id);

                con.Query(SP, queryParameters, commandType: CommandType.StoredProcedure);
            }
        }

        public BookEM Get(long id)
        {
            using (var con = Connection)
            {
                var SP = "USP_Get_Book";
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@id", id);

                var result = con.QueryMultiple(SP, queryParameters, commandType: CommandType.StoredProcedure);

                var book = result.ReadSingleOrDefault<BookEM>();
                var authors = result.Read<AuthorEM>().AsList();

                if (book != null)
                {
                    book.Authors = authors;
                }

                return book;
            }
        }

        public DataTableResponseEM<BookEM> GetByDateAsc(DataTableRequestEM model)
        {
            throw new NotImplementedException();
        }

        public DataTableResponseEM<BookEM> GetByDateDesc(DataTableRequestEM model)
        {
            throw new NotImplementedException();
        }

        public DataTableResponseEM<BookEM> GetByNameAsc(DataTableRequestEM model)
        {
            throw new NotImplementedException();
        }

        public DataTableResponseEM<BookEM> GetByNameDesc(DataTableRequestEM model)
        {
            throw new NotImplementedException();
        }

        public DataTableResponseEM<BookEM> GetByPagesAsc(DataTableRequestEM model)
        {
            throw new NotImplementedException();
        }

        public DataTableResponseEM<BookEM> GetByPagesDesc(DataTableRequestEM model)
        {
            throw new NotImplementedException();
        }

        public DataTableResponseEM<BookEM> GetByRateAsc(DataTableRequestEM model)
        {
            throw new NotImplementedException();
        }

        public DataTableResponseEM<BookEM> GetByRateDesc(DataTableRequestEM model)
        {
            throw new NotImplementedException();
        }

        public long Save(UpdatableBookEM book)
        {
            throw new NotImplementedException();
        }

        public long Save(BookEM entity)
        {
            throw new NotImplementedException();
        }
    }
}
