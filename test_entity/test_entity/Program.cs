using System;
using test_entity.Models;
using test_entity.Repository;

namespace test_entity
{
    class Program
    {
        static void Main(string[] args)
        {
            test_consoleAppContext dbContext = new test_consoleAppContext();
            WarehouseRepository repo = new WarehouseRepository(dbContext);
            repo.readDataFromExcel("D:\\HRTech\\webHRTech\\Data.xlsx", 2);
        }
    }
}
