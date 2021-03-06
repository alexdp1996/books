﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Dommel;
using EntityModels;
using Infrastructure.Data;
using Shared.Services;

namespace Data.Repositories
{
    public class AuthorRepo : BaseRepo, IAuthorRepo
    {
        public long Create(AuthorEM entity)
        {
            using (var con = Connection)
            {
                var result = con.Insert(entity);
                var converted = (long)Convert.ChangeType(result, typeof(long));
                return converted;
            }
        }

        public void Update(AuthorEM entity)
        {
            using (var con = Connection)
            {
                con.Update(entity);
            }
        }

        public void Delete(long id)
        {
            using (var con = Connection)
            {
                var SP = "USPAuthorDelete";
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Id", id);

                con.Query(SP, queryParameters, commandType: CommandType.StoredProcedure);
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

        public AuthorEM Get(long id)
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

        public DataTableResponseEM<AuthorEM> GetList(DataTableRequestEM model)
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

                var authors = reader.Read<AuthorEM>();
                var authorBook = reader.Read<AuthorBookEM>();
                var books = reader.Read<BookEM>();

               
                datatableResponse.Data = MapWithBooks(authors, authorBook, books);

                return datatableResponse;
            }
        }

        public IEnumerable<AuthorEM> GetALL()
        {
            using (var con = Connection)
            {
                var authors = con.GetAll<AuthorEM>();

                var reader = con.QueryMultiple(@"SELECT AuthorID, BookID FROM AuthorBook
                                                 SELECT B.Id, B.[Name], B.[CreatedDate], B.Pages, B.Rate FROM Book B
                                                 WHERE EXISTS(SELECT 1 FROM AuthorBook AB WHERE AB.BookID = B.ID)");

                var authorBook = reader.Read<AuthorBookEM>();
                var books = reader.Read<BookEM>();

                return MapWithBooks(authors, authorBook, books);
            }
        }

        private IEnumerable<AuthorEM> MapWithBooks(IEnumerable<AuthorEM> authors, IEnumerable<AuthorBookEM> authorBook, IEnumerable<BookEM> books)
        {
            foreach (var a in authors)
            {
                var bookIds = authorBook.Where(ab => ab.AuthorId == a.Id.Value).Select(s => s.BookId).ToList();
                var booksToAdd = books.Where(b => bookIds.Contains(b.Id.Value));
                a.Books.AddRange(booksToAdd);
            }

            return authors;
        }
    }
}
