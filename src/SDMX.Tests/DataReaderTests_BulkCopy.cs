using NUnit.Framework;
using System.Data.SqlClient;
using System;
using System.Linq;
using System.Data;

namespace SDMX.Tests
{
    public partial class DataReaderTests
    {
        const string _connectionString = @"Server=.\SQLEXPRESS;Database=sdmx;Integrated Security=True";
        
        [Test]
        public void BulkCopy()
        {
            var structure = StructureMessage.Load(Utility.GetPath("lib\\StructureSample.xml"));
            string dataPath = Utility.GetPath("lib\\GenericSample.xml");
            var keyFamily = structure.KeyFamilies[0];

            using (var reader = DataReader.Create(dataPath, keyFamily))
            {
                reader.Cast("TIME", i => ((TimePeriod)i).DateTime);

                foreach (DataColumn col in ((IDataReader)reader).GetSchemaTable().Columns)
                    Console.WriteLine("{0}: {1}", col.ColumnName, col.DataType);

                CreateTable();
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(_connectionString))
                {
                    //bulkCopy.BatchSize = 500;
                    bulkCopy.ColumnMappings.Add("FREQ", "FREQ");
                    bulkCopy.ColumnMappings.Add("JD_TYPE", "JD_TYPE");
                    bulkCopy.ColumnMappings.Add("JD_CATEGORY", "JD_CATEGORY");
                    bulkCopy.ColumnMappings.Add("VIS_CTY", "VIS_CTY");
                    bulkCopy.ColumnMappings.Add("COLLECTION", "COLLECTION");
                    bulkCopy.ColumnMappings.Add("TIME_FORMAT", "TIME_FORMAT");
                    bulkCopy.ColumnMappings.Add("TIME", "TIME");
                    bulkCopy.ColumnMappings.Add("OBS_VALUE", "OBS_VALUE");
                    bulkCopy.ColumnMappings.Add("OBS_STATUS", "OBS_STATUS");
                    bulkCopy.ColumnMappings.Add("AVAILABILITY", "AVAILABILITY");
                    bulkCopy.ColumnMappings.Add("DECIMALS", "DECIMALS");
                    bulkCopy.ColumnMappings.Add("BIS_UNIT", "BIS_UNIT");
                    bulkCopy.ColumnMappings.Add("UNIT_MULT", "UNIT_MULT");
                    bulkCopy.ColumnMappings.Add("OBS_CONF", "OBS_CONF");
                    bulkCopy.ColumnMappings.Add("OBS_PRE_BREAK", "OBS_PRE_BREAK");

                    

                    //bulkCopy.NotifyAfter = 1;
                    bulkCopy.SqlRowsCopied += (a, e) => { Console.WriteLine(e.RowsCopied); };
                    bulkCopy.DestinationTableName = "Sample";
                    bulkCopy.WriteToServer(reader);
                }
            }

            int count = 0;
            ExecuteReader("select count(*) from Sample", r => count = (int)r[0]);
            Assert.AreNotEqual(0, count);
        }

        void ExecuteReader(string cmd, Action<SqlDataReader> action)
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

        void ExecuteNonQuery(string cmd)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var com = new SqlCommand(cmd, con))
            {
                con.Open();
                com.ExecuteNonQuery();
            }
        }

        void CreateTable()
        {
            string query = @"
IF  EXISTS (select * from sys.objects 
			where object_id = OBJECT_ID(N'[dbo].[Sample]') 
			AND type in (N'U'))
DROP TABLE [dbo].[Sample]

create table dbo.Sample (
	[FREQ] nvarchar(255) not null,
	[JD_TYPE] nvarchar(255) not null,
	[JD_CATEGORY] nvarchar(255) not null,
	[VIS_CTY] nvarchar(255) not null,
	[TIME] datetime not null,
	[OBS_VALUE] float not null,
	[AVAILABILITY] nvarchar(255) not null,
	[TIME_FORMAT] nvarchar(255) not null,
	[COLLECTION] nvarchar(255) not null,
	[DECIMALS] nvarchar(255) not null,
	[OBS_CONF] nvarchar(255) null,
	[OBS_STATUS] nvarchar(255) not null,
	[OBS_PRE_BREAK] nvarchar(255) null,
	[BIS_UNIT] nvarchar(255) not null,
	[UNIT_MULT] nvarchar(255) not null
)
";
            ExecuteNonQuery(query);
        }
    }
}
