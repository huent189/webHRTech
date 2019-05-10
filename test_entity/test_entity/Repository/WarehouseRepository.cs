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
    class WarehouseRepository : CommonRepsitory
    {
        public WarehouseRepository(test_consoleAppContext db) : base(db)
        {
        }
        public void readDataFromExcel(String filePath, int worksheetNumber)
        {
            FileInfo readingFile = new FileInfo(filePath);
            using (ExcelPackage package = new ExcelPackage(readingFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[worksheetNumber];
                int totalRow = worksheet.Dimension.End.Row;
                //for(int row = 2; row <= 20; row++)
                //{
                //    Product p = new Product();
                //    p.Id = Guid.NewGuid();
                //    p.Name = worksheet.Cells[row, 1].Value.ToString();
                //    dbContext.Products.Add(p);
                //}
                //dbContext.SaveChanges();

                var dt = new DataTable();
                dt.Columns.Add("Id", typeof(Guid));
                dt.Columns.Add("Name");
                for (int row = 2; row <= totalRow; row++)
                {
                    dt.Rows.Add(Guid.NewGuid(), worksheet.Cells[row, 1].Value.ToString());
                }
                using (var sqlBulk = new SqlBulkCopy(_connectionString))
                {
                    sqlBulk.DestinationTableName = "Warehouse";
                    SqlBulkCopyColumnMapping mapID =
                        new SqlBulkCopyColumnMapping("Id", "Id");
                    sqlBulk.ColumnMappings.Add(mapID);
                    SqlBulkCopyColumnMapping mapName =
                        new SqlBulkCopyColumnMapping("Name", "Name");
                    sqlBulk.WriteToServer(dt);
                }
            }

        }
    }
}
