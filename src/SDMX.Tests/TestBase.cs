using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace SDMX.Tests
{
    public class TestBase
    {
        //const string _connectionString = @"Server=.\sqlexpress;Database=sdmx;Integrated Security=True";
        public const string _connectionString = @"Server=.;Database=sdmx;Integrated Security=False; User id=dev;Password=dev";

        public void ExecuteReader(string cmd, Action<SqlDataReader> action)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var com = new SqlCommand(cmd, con))
            {
                con.Open();
                using (var reader = com.ExecuteReader())
                {
                    while (reader.Read())
                        action(reader);
                }
            }
        }

        public void ExecuteNonQuery(string cmd)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var com = new SqlCommand(cmd, con))
            {
                con.Open();
                com.ExecuteNonQuery();
            }
        }

        public void CreateTable(DataTable table)
        {
            var builder = new StringBuilder();

            builder.AppendFormat(@"
IF  EXISTS (select * from sys.objects 
			where object_id = OBJECT_ID(N'[dbo].[{0}]') 
			AND type in (N'U'))
DROP TABLE [dbo].[{0}]

create table dbo.{0} (", table.TableName);

            foreach (DataColumn column in table.Columns)
            {
                builder.AppendFormat("[{0}] {1} {2} null,",
                    column.ColumnName,
                    GetColumnTypeName(column.ColumnName, column.DataType),
                    column.ColumnName != "IsValid" ? "" : "not");
            }

            builder.Remove(builder.Length - 1, 1);

            builder.Append(")");

            ExecuteNonQuery(builder.ToString());
        }

        public string GetColumnTypeName(string name, Type type)
        {
            if (name == "ErrorMessages")
            {
                return "nvarchar(4000)";
            }

            if (type == typeof(double))
                return "float";
            else if (type == typeof(int))
                return "int";
            else if (type == typeof(DateTime))
                return "datetime";
            else if (type == typeof(DateTimeOffset))
                return "datetime2";
            else if (type == typeof(bool))
                return "bit";
            else
                return "nvarchar(255)";

        }
    }
}
