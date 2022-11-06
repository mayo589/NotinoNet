using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using NotinoDotNetHW.Enums;
using NotinoDotNetHW.Models;

namespace NotinoDotNetHW.Data
{
    public class DocumentsDb: DbContext
    {
        private readonly IConfiguration configuration;

        public DocumentsDb (DbContextOptions<DocumentsDb> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;

            Database.EnsureCreated();
        }

        /// <summary>
        /// Method for DB configuration. Can be supplied with additional DbTypes
        /// </summary>
        /// <param name="options"></param>
        /// <exception cref="Exception"></exception>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var sDbType = configuration.GetValue<string>("DbType");
            var dbType =(DbType) Enum.Parse(typeof(DbType), sDbType, true);

            switch (dbType)
            {
                case DbType.InMemory:
                    {
                        options.UseInMemoryDatabase(databaseName: configuration.GetValue<string>("DbConnections:InMemory:DbName"));
                        break;
                    }
                case DbType.SqlLite:
                    {
                        var dbFileName = configuration.GetValue<string>("DbConnections:SqlLite:DbFileName");
                        var localAppFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                        var dbPath = Path.Combine(localAppFolder, dbFileName);
                        options.UseSqlite($"Data Source={dbPath}");
                        break;
                    }
                case DbType.SqlServer:
                    {
                        options.UseSqlServer(configuration.GetValue<string>("DbConnections:SqlServer:ConnectionString"));
                        break;
                    }
                case DbType.Cosmos:
                    {
                        options.UseCosmos(
                            configuration.GetValue<string>("DbConnections:AzureCosmos:Endpoint"),
                            configuration.GetValue<string>("DbConnections:AzureCosmos:Key"),
                            configuration.GetValue<string>("DbConnections:AzureCosmos:DbName")
                        );
                        break;
                    }
                default:
                    {
                        throw new Exception($"Unsupported DbType: " + sDbType);
                    }
            }
            
        }

        public DbSet<Document> Documents { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Document>().Property(d => d.Tags)
                        .HasConversion(
                            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)default),
                            v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)default));
            
            modelBuilder.Entity<Document>()
                      .Property(d => d.Data)
                      .HasConversion(
                          v => Newtonsoft.Json.JsonConvert.SerializeObject(v),
                          v => Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(v));
        }

    }
}
