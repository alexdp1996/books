using Dapper;
using DataDapper.Extensions;
using DataInfrastructure.Entities;
using DataInfrastructure.Interfaces;
using Shared.Services;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataDapper.Repositories
{
    public class BookRepo : BaseEntityRepo<BookEM>, IBookRepo
    {
        public void UpdateAuthors(long bookId, IEnumerable<long> authorIds)
        {
            using (var con = Connection)
            {
                var SP = "USPBookUpdateAuthors";
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@BookId", bookId);
                queryParameters.Add("@AuthorIds", authorIds.AsParameter());

                con.Query(SP, queryParameters, commandType: CommandType.StoredProcedure);
            }
        }

        public override void Delete(long id)
        {
            using (var con = Connection)
            {
                var SP = "USPBookDelete";
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Id", id);

                con.Query(SP, queryParameters, commandType: CommandType.StoredProcedure);
            }
        }

        public override BookEM Get(long id)
        {
            using (var con = Connection)
            {
                var SP = "USPBookGet";
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@id", id);

                var result = con.QueryMultiple(SP, queryParameters, commandType: CommandType.StoredProcedure);

                var mappingObject = new BookMappingObjectEM();

                mappingObject.Book = result.ReadSingleOrDefault<BookEM>();
                mappingObject.Authors = result.Read<AuthorEM>().AsList();

                var book = MapperService.Map<BookEM>(mappingObject);

                return book;
            }
        }

        #region DataTable
        public DataTableResponseEM<BookEM> GetByDateAsc(DataTableRequestEM model)
        {
            return GetDataTable(model, "USPBookGetDataTableByDateASC");
        }

        public DataTableResponseEM<BookEM> GetByDateDesc(DataTableRequestEM model)
        {
            return GetDataTable(model, "USPBookGetDataTableByDateDESC");
        }

        public DataTableResponseEM<BookEM> GetByNameAsc(DataTableRequestEM model)
        {
            return GetDataTable(model, "USPBookGetDataTableByNameASC");
        }

        public DataTableResponseEM<BookEM> GetByNameDesc(DataTableRequestEM model)
        {
            return GetDataTable(model, "USPBookGetDataTableByNameDESC");
        }

        public DataTableResponseEM<BookEM> GetByPagesAsc(DataTableRequestEM model)
        {
            return GetDataTable(model, "USPBookGetDataTableByPagesASC");
        }

        public DataTableResponseEM<BookEM> GetByPagesDesc(DataTableRequestEM model)
        {
            return GetDataTable(model, "USPBookGetDataTableByPagesDESC");
        }

        public DataTableResponseEM<BookEM> GetByRateAsc(DataTableRequestEM model)
        {
            return GetDataTable(model, "USPBookGetDataTableByRateASC");
        }

        public DataTableResponseEM<BookEM> GetByRateDesc(DataTableRequestEM model)
        {
            return GetDataTable(model, "USPBookGetDataTableByRateDESC");
        }

        private DataTableResponseEM<BookEM> GetDataTable(DataTableRequestEM model, string SPname)
        {
            using (var con = Connection)
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Start", model.Start);
                queryParameters.Add("@Lenght", model.Length);

                var reader = con.QueryMultiple(SPname, queryParameters, commandType: CommandType.StoredProcedure);
                var generalInfo = reader.ReadSingle<DataTableResponseGeneralEM>();

                var datatableResponse = new DataTableResponseEM<BookEM>();
                datatableResponse.RecordsFiltered = generalInfo.RecordsFiltered;
                datatableResponse.RecordsTotal = generalInfo.RecordsTotal;


                var books = reader.Read<BookEM>().AsList();

                var authorBook = reader.Read<AuthorBookEM>().AsList();

                var authors = reader.Read<AuthorEM>().AsList();

                foreach (var b in books)
                {
                    var authorIds = authorBook.Where(ab => ab.BookId == b.Id.Value).Select(s => s.AuthorId).ToList();
                    var authorsToBook = authors.Where(a => authorIds.Contains(a.Id.Value));
                    b.Authors.AddRange(authorsToBook);
                }

                datatableResponse.Data = books;

                return datatableResponse;
            }
        }
        #endregion
    }
}
