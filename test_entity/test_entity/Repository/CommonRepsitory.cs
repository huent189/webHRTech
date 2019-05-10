using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using test_entity.Models;

namespace test_entity.Repository
{
    class CommonRepsitory
    {
        protected readonly test_consoleAppContext dbContext;
        protected string _connectionString = "data source=localhost;initial catalog=test_consoleApp;persist security info=True;user id=sa;password=1234567@;multipleactiveresultsets=True;";
        public CommonRepsitory(test_consoleAppContext db)
        {
            dbContext = db;
        }

    }
}
