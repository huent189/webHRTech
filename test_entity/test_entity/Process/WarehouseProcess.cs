using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using test_entity.Models;

namespace test_entity.Repository
{
    class WarehouseProcess
    {
        protected string _connectionString = "data source=localhost;initial catalog=test_consoleApp;persist security info=True;user id=sa;password=1234567@;multipleactiveresultsets=True;";
        public WarehouseProcess(test_consoleAppContext db)
        {
        }
        public DataTable dtWarehouse { get; set; }
        public void readDataFromExcel(String filePath, int worksheetNumber)
        {
            FileInfo readingFile = new FileInfo(filePath);
            using (ExcelPackage package = new ExcelPackage(readingFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[worksheetNumber];
                int totalRow = worksheet.Dimension.End.Row;
                dtWarehouse = new DataTable();
                dtWarehouse.Columns.Add("Id", typeof(Guid));
                dtWarehouse.Columns.Add("Name");
                for (int row = 2; row <= totalRow; row++)
                {
                    dtWarehouse.Rows.Add(Guid.NewGuid(), worksheet.Cells[row, 1].Value.ToString());
                }
            }
        }

        public void saveToDB()
        {
            using (var sqlBulk = new SqlBulkCopy(_connectionString))
            {
                sqlBulk.DestinationTableName = "Warehouse";
                SqlBulkCopyColumnMapping mapID =
                    new SqlBulkCopyColumnMapping("Id", "Id");
                sqlBulk.ColumnMappings.Add(mapID);
                SqlBulkCopyColumnMapping mapName =
                    new SqlBulkCopyColumnMapping("Name", "Name");
                sqlBulk.ColumnMappings.Add(mapName);
                sqlBulk.WriteToServer(dtWarehouse);
            }
        }

        public void run(String filePath, int worksheetNumber)
        {
            readDataFromExcel(filePath, worksheetNumber);
            saveToDB();
        }
    }
}
