using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DataDapper.Extensions;
using DataInfrastructure.Entities;
using DataInfrastructure.Interfaces;
using Shared.Services;

namespace DataDapper.Repositories
{
    public class AuthorRepo : BaseEntityRepo<AuthorEM>, IAuthorRepo
    {
        public override void Delete(long id)
        {
            using (var con = Connection)
            {
                var SP = "USPAuthorDelete";
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Id", id);

                con.Query(SP, queryParameters, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<AuthorEM> Get(IEnumerable<long> Ids)
        {
            using (var con = Connection)
            {
                var SP = "USPAuthorGetByIds";
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Ids", Ids.AsParameter());

                var authors = con.Query<AuthorEM>(SP, queryParameters, commandType: CommandType.StoredProcedure);

                return authors;
            }
        }

        public IEnumerable<AuthorEM> Get(string term)
        {
            using (var con = Connection)
            {
                var SP = "USPAuthorGetByTerm";
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Term", term);

                var authors = con.Query<AuthorEM>(SP, queryParameters, commandType: CommandType.StoredProcedure);

                return authors;
            }
        }

        public override AuthorEM Get(long id)
        {
            using (var con = Connection)
            {
                var SP = "USPAuthorGet";
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Id", id);

                var result = con.QueryMultiple(SP, queryParameters, commandType: CommandType.StoredProcedure);

                var mappingObject = new AuthorMappingObjectEM();

                mappingObject.Author = result.ReadSingleOrDefault<AuthorEM>();
                mappingObject.Books = result.Read<BookEM>().AsList();

                var author = MapperService.Map<AuthorEM>(mappingObject);

                return author;
            }
        }

        public DataTableResponseEM<AuthorEM> Get(DataTableRequestEM model)
        {
            using (var con = Connection)
            {
                var SPname = "USPAuthorGetList";

                var order = model.Order[0];
                var orderColumnName = model.Columns[order.Column].Name;

                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Start", model.Start);
                queryParameters.Add("@Lenght", model.Length);
                queryParameters.Add("@OrderColumnName", orderColumnName);
                queryParameters.Add("@IsAsc", order.IsAcs);

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
                    var bookIds = authorBook.Where(ab => ab.AuthorId == a.Id.Value).Select(s => s.BookId).ToList();
                    var booksToAdd = books.Where(b => bookIds.Contains(b.Id.Value));
                    a.Books.AddRange(booksToAdd);
                }

                datatableResponse.Data = authors;

                return datatableResponse;
            }
        }
    }
}
