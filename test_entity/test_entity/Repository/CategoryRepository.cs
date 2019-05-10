using System;
using System.Collections.Generic;
using System.Text;
using test_entity.Models;

namespace test_entity.Repository
{
    class CategoryRepository : CommonRepsitory
    {
        public CategoryRepository(test_consoleAppContext db) : base(db)
        {
        }
        public void readDataFromExcel(String filePath, int worksheetNumber)
        {

        }
    }
}
