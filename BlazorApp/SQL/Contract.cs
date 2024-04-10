using BlazorApp.Model;
using BlazorApp1.Shared;
using Dapper;
using Npgsql;
using System.Diagnostics;

namespace BlazorApp1.SQL
{
    public class Contract
    {
        // PostgreSQLの操作文を実行
        public static IEnumerable<ContractModel> SelectContract(IEnumerable<string> codeList)
        {
            const string sql =
                " SELECT " +
                "  id, " +
                "  code, " +
                "  value_string " +
                // "  value_date " +
                " FROM " +
                "  m_table_a "
                // " WHERE " +
                // "  code = ANY (@codeList) "
                ;

            var sqlParam = new
            {
                codeList = codeList.ToArray(),
            };

            using (var conn = new NpgsqlConnection(SharedData.ConnectionString))
            {
                return conn.Query<ContractModel>(sql, sqlParam);
            }
        }
    }

}
