using Dapper;
using Data.Extensions;
using Dommel;
using EntityModels;
using Infrastructure.Data;
using Shared.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Data.Repositories
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
                queryParameters.Add("@AuthorIds", authorIds.AsEnumerableParameter());

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

        public DataTableResponseEM<BookEM> GetList(DataTableRequestEM model)
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


                var books = reader.Read<BookEM>();
                var authorBook = reader.Read<AuthorBookEM>();
                var authors = reader.Read<AuthorEM>();
       
                datatableResponse.Data = MapWithAuthors(books, authorBook, authors);

                return datatableResponse;
            }
        }

        private IEnumerable<BookEM> MapWithAuthors(IEnumerable<BookEM> books, IEnumerable<AuthorBookEM> authorBook, IEnumerable<AuthorEM> authors)
        {
            foreach (var b in books)
            {
                var authorIds = authorBook.Where(ab => ab.BookId == b.Id.Value).Select(s => s.AuthorId).ToList();
                var authorsToBook = authors.Where(a => authorIds.Contains(a.Id.Value));
                b.Authors.AddRange(authorsToBook);
            }
            return books;
        }

        public IEnumerable<BookEM> GetALL()
        {
            using (var con = Connection)
            {
                var books = con.GetAll<BookEM>();

                var reader = con.QueryMultiple(@"SELECT AuthorID, BookID FROM AuthorBook
                                                 SELECT A.ID, A.Name, A.Surname FROM Author A WHERE EXISTS(SELECT 1 FROM AuthorBook AB WHERE AB.AuthorID = A.ID)");

                var authorBook = reader.Read<AuthorBookEM>();
                var authors = reader.Read<AuthorEM>();

                return MapWithAuthors(books, authorBook, authors);
            }        
        }
    }
}
