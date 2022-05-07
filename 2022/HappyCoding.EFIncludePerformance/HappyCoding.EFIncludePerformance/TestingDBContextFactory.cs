using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HappyCoding.EFIncludePerformance
{
    public class TestingDBContextFactory : IDesignTimeDbContextFactory<TestingDBContext>
    {
        public TestingDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TestingDBContext>();
            optionsBuilder.UseSqlServer();

            return new TestingDBContext(optionsBuilder.Options);
        }
    }
}
