﻿using Dapper;
using DataDapper.Extensions;
using DataInfrastructure.Entities;
using DataInfrastructure.Interfaces;
using System;
using System.Data;
using System.Linq;

namespace DataDapper.Repositories
{
    public class BookRepo : BaseEntityRepo<BookEM>, IBookRepo
    {
        public void Delete(long id)
        {
            using (var con = Connection)
            {
                var SP = "USP_Book_Delete";
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

                var book = result.ReadSingleOrDefault<BookEM>();
                var authors = result.Read<AuthorEM>().AsList();

                if (book != null)
                {
                    book.Authors = authors;
                }

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
            return GetDataTable(model, "USPBookGetDataTableByNameDESC");
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
                    var authorIds = authorBook.Where(ab => ab.BookId == b.Id).Select(s => s.AuthorId).ToList();
                    var authorsToBook = authors.Where(a => authorIds.Contains(a.Id));
                    b.Authors.AddRange(authorsToBook);
                }

                datatableResponse.Data = books;

                return datatableResponse;
            }
        }
        #endregion

        public long Save(UpdatableBookEM book)
        {
            using (var con = Connection)
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Id", book.Id);
                queryParameters.Add("@Name", book.Name);
                queryParameters.Add("@Pages", book.Pages);
                queryParameters.Add("@Rate", book.Rate);
                queryParameters.Add("@Date", book.Date);
                queryParameters.Add("@AuthorIds", book.AuthorIds.AsParameter());

                var id = con.QuerySingle<long>("USPBookSave", queryParameters, commandType: CommandType.StoredProcedure);
                
                return id;
            }
        }
    }
}
