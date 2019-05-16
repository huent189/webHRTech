using OfficeOpenXml;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using test_entity.Entities;
using test_entity.Models;

namespace test_entity.Process
{
    class InventoryProcess
    {
        protected string _connectionString = "data source=localhost;initial catalog=test_consoleApp;persist security info=True;user id=sa;password=1234567@;multipleactiveresultsets=True;";
        ConcurrentDictionary<string, Guid> categoryDict;
        ConcurrentDictionary<string, Guid> productDict;
        ConcurrentDictionary<string, Guid> warehousrDict;

        public List<InventoryEntity> readDataFromExcel(String filePath, int worksheetNumber)
        {
            List<InventoryEntity> entities = new List<InventoryEntity>();
            FileInfo readingFile = new FileInfo(filePath);
            using (ExcelPackage package = new ExcelPackage(readingFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[worksheetNumber];
                int totalRow = worksheet.Dimension.End.Row;
                for (int row = 2; row <= totalRow; row++)
                {
                    InventoryEntity inventory = new InventoryEntity();
                    inventory.Product = worksheet.Cells[row, 1].Value.ToString();
                    inventory.Warehouse = worksheet.Cells[row, 3].Value.ToString();
                    inventory.Quantity = worksheet.Cells[row, 4].GetValue<int>();
                    inventory.Category = worksheet.Cells[row, 2].Value.ToString().Split('.').Last();
                    entities.Add(inventory);
                }
            }
            return entities;
        }
        public List<InventoryEntity> mapID(List<InventoryEntity> data)
        {
            using (var dbContext = new test_consoleAppContext())
            {
                categoryDict = new ConcurrentDictionary<string, Guid>(dbContext.Categories.ToDictionary(c => c.Name, c => c.Id));
                productDict = new ConcurrentDictionary<string, Guid>(dbContext.Products.ToDictionary(p => p.Name, p => p.Id));
                warehousrDict = new ConcurrentDictionary<string, Guid>(dbContext.Warehouses.ToDictionary(w => w.Name, w => w.Id));

                Parallel.ForEach(data, i =>
                {
                    i.ProductId = productDict.GetValueOrDefault(i.Product);
                    i.WarehouseId = warehousrDict.GetValueOrDefault(i.Warehouse);
                    i.CategoryId = categoryDict.GetValueOrDefault(i.Category);
                });
            }
            return data;
        }

        public void saveToDB(String filePath, int worksheetNumber)
        {
            List<InventoryEntity> data = readDataFromExcel(filePath, worksheetNumber);
            data = mapID(data);

            using (var sqlBulk = new SqlBulkCopy(_connectionString))
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("CategoryId", typeof(Guid));
                dt.Columns.Add("ProductId", typeof(Guid));
                dt.Columns.Add("WarehouseId", typeof(Guid));
                dt.Columns.Add("Quantity", typeof(int));
                data.ForEach(i => dt.Rows.Add(i.CategoryId, i.ProductId, i.WarehouseId, i.Quantity));
                sqlBulk.DestinationTableName = "Inventory";
                sqlBulk.WriteToServer(dt);
            }
        }
    }
}
