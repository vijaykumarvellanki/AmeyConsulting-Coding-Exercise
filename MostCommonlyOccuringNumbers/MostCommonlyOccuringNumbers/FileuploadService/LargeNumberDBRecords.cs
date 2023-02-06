using Microsoft.Data.SqlClient;
using System.Data;

namespace MostCommonlyOccuringNumbers.FileuploadService
{
    public class LargeNumberDBRecords
    {
        private readonly string _connectionString = "";
        public LargeNumberDBRecords() { }
        public void InsertBulkData()
        {
            var dtCustomer = new DataTable();
            dtCustomer.Columns.Add("Name");

            for (var i = 1; i < 1000000; i++)
                dtCustomer.Rows.Add("Name " + i + 1);

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var transaction = connection.BeginTransaction();
                using (var sqlBulk = new SqlBulkCopy(connection, SqlBulkCopyOptions.KeepIdentity, transaction))
                {
                    sqlBulk.BatchSize = 1000;
                    sqlBulk.DestinationTableName = "Customer";
                    sqlBulk.WriteToServer(dtCustomer);
                }
            }
        }
    }
}
