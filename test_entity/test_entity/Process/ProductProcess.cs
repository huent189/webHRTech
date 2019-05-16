using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using test_entity.Models;

namespace test_entity.Repository
{
    class ProductProcess : CommonProcess
    {
        public void saveToDB(String filePath, int worksheetNumber)
        {
            List<string> data = readDataFromExcel(filePath, worksheetNumber);
            using(var sqlBulk = new SqlBulkCopy(_connectionString))
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Id", typeof(Guid));
                dt.Columns.Add("Name");
                foreach (string row in data){
                    dt.Rows.Add(Guid.NewGuid(), row);
                }
                sqlBulk.DestinationTableName = "Product";
                sqlBulk.WriteToServer(dt);
            }
        }
    }
}
