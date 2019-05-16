using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using test_entity.Models;

namespace test_entity.Repository
{
    class CommonProcess
    {
        protected string _connectionString = "data source=localhost;initial catalog=test_consoleApp;persist security info=True;user id=sa;password=1234567@;multipleactiveresultsets=True;";
        public List<string> readDataFromExcel(String filePath, int worksheetNumber)
        {
            List<string> data = new List<string>();
            FileInfo readingFile = new FileInfo(filePath);
            using (ExcelPackage package = new ExcelPackage(readingFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[worksheetNumber];
                int totalRow = worksheet.Dimension.End.Row;
                for (int row = 2; row <= totalRow; row++)
                {
                    data.Add(worksheet.Cells[row, 1].Value.ToString());
                }
            }
            return data;
        }
    }
}
