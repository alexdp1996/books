using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
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
            return GetDataTable(model, "USP_Author_Get_By_AmountOfBooks_ASC");
        }

        public DataTableResponseEM<AuthorEM> GetByAmountOfBooksDesc(DataTableRequestEM model)
        {
            return GetDataTable(model, "USP_Author_Get_By_AmountOfBooks_DESC");
        }

        public DataTableResponseEM<AuthorEM> GetByNameAsc(DataTableRequestEM model)
        {
            return GetDataTable(model, "USP_Author_Get_By_Name_ASC");
        }

        public DataTableResponseEM<AuthorEM> GetByNameDesc(DataTableRequestEM model)
        {
            return GetDataTable(model, "USP_Author_Get_By_Name_DESC");
        }

        public DataTableResponseEM<AuthorEM> GetBySurnameAsc(DataTableRequestEM model)
        {
            return GetDataTable(model, "USP_Author_Get_By_Surname_ASC");
        }

        public DataTableResponseEM<AuthorEM> GetBySurnameDesc(DataTableRequestEM model)
        {
            return GetDataTable(model, "USP_Author_Get_By_Surname_DESC");
        }

        private DataTableResponseEM<AuthorEM> GetDataTable(DataTableRequestEM model, string SPname)
        {
            using (var con = Connection)
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Start", model.Start);
                queryParameters.Add("@Lenght", model.Length);

                var reader = con.QueryMultiple(SPname, queryParameters, commandType: CommandType.StoredProcedure);
                var generalInfo = reader.ReadSingle<DataTableResponseGeneralEM>();

                var datatableResponse = new DataTableResponseEM<AuthorEM>();
                datatableResponse.RecordsFiltered = generalInfo.RecordsFiltered;
                datatableResponse.RecordsTotal = generalInfo.RecordsTotal;

                var authors = reader.Read<AuthorEM>().AsList();

                var authorBook = reader.Read<AuthorBookEM>().AsList();

                var books = reader.Read<BookEM>().AsList();

                foreach (var a in authors)
                {
                    var bookIds = authorBook.Where(ab => ab.AuthorId == a.Id).Select(s => s.BookId).ToList();
                    var booksToAdd = books.Where(b => bookIds.Contains(b.Id));
                    a.Books.AddRange(booksToAdd);
                }

                datatableResponse.Data = authors;

                return datatableResponse;
            }
        }

        public long Save(AuthorEM entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
