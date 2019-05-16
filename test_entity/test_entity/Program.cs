using System;
using test_entity.Models;
using test_entity.Process;
using test_entity.Repository;

namespace test_entity
{
    class Program
    {
        static void Main(string[] args)
        {
            test_consoleAppContext dbContext = new test_consoleAppContext();
            InventoryProcess process = new InventoryProcess();
            process.saveToDB("D:\\HRTech\\webHRTech\\test_entity\\Data.xlsx", 3);
            
        }
    }
}
