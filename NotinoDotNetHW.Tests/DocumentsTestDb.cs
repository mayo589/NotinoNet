using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NotinoDotNetHW.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotinoDotNetHW.Tests
{
    public class DocumentsTestDb : DocumentsDb
    {
        public DocumentsTestDb(DbContextOptions<DocumentsDb> options, IConfiguration configuration) : base(options, configuration)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseInMemoryDatabase(databaseName: "DocumentsTestDb");
        }
    }
}
