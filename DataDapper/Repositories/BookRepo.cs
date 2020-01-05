using Dapper;
using DataDapper.Extensions;
using DataInfrastructure.Entities;
using DataInfrastructure.Interfaces;
using Dommel;
using Shared.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataDapper.Repositories
{
    public class BookRepo : BaseRepo, IBookRepo
    {
        public long Create(BookEM entity)
        {
            using (var con = Connection)
            {
                var result = con.Insert(entity);
                var converted = (long)Convert.ChangeType(result, typeof(long));
                return converted;
            }
        }

        public void Update(BookEM entity)
        {
            using (var con = Connection)
            {
                con.Update(entity);
            }
        }

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

        public void Delete(long id)
        {
            using (var con = Connection)
            {
                var SP = "USPBookDelete";
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Id", id);

                con.Query(SP, queryParameters, commandType: CommandType.StoredProcedure);
            }
        }

        public BookEM Get(long id)
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

        public DataTableResponseEM<BookEM> Get(DataTableRequestEM model)
        {
            using (var con = Connection)
            {
                var SPname = "USPBookGetList";

                var order = model.Order[0];
                var orderColumnName = model.Columns[order.Column].Name;

                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Start", model.Start);
                queryParameters.Add("@Lenght", model.Length);
                queryParameters.Add("@OrderColumnName", orderColumnName);
                queryParameters.Add("@IsAsc", order.IsAcs);

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
    }
}
