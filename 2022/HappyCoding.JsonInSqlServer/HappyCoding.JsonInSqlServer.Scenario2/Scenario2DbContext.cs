﻿using Microsoft.EntityFrameworkCore;

namespace HappyCoding.JsonInSqlServer.Scenario2
{
    public class Scenario2DbContext : DbContext
    {
        public DbSet<ModelWithJsonData2> TestingTable { get; set; }

        public Scenario2DbContext(DbContextOptions options)
            : base(options)
        {

        }
    }
}
