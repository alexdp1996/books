using Dapper;
using System.Collections.Generic;
using System.Data;
using static Dapper.SqlMapper;

namespace DataDapper.Extensions
{
    public static class ParamExtension
    {
        public static ICustomQueryParameter AsParameter<T>(this IEnumerable<T> elements)
        {
            var datatable = new DataTable();

            datatable.Columns.Add("Element");
            
            foreach(var elem in elements)
            {
                datatable.Rows.Add(elem);
            }

            return datatable.AsTableValuedParameter();
        }
    }
}
