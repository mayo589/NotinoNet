using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotinoDotNetHW.Data;
using NotinoDotNetHW.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotinoDotNetHW.Tests
{
    public class BaseTest
    {
        public readonly DocumentsRepository DocumentsRepository;
        
        public BaseTest()
        {
            DocumentsRepository = new DocumentsRepository(new DocumentsTestDb(new DbContextOptions<DocumentsDb>(), null));
        }
    }
}
