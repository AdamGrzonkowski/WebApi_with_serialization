using Domain.Services;
using Effort.Provider;
using Helper.Common.ConfigStrings;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace Test.Domain
{
    [Description("Tests for integration with external db.")]
    public class DatabaseConnectionTest
    {
        private static string ConnStringName => ConnectionStrings.LocalDbConnectionName;

        public DatabaseConnectionTest()
        {
            EffortProviderConfiguration.RegisterProvider(); 
        }

        [Fact]
        public void CanConnectToDatabase()
        {
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings[ConnStringName].ConnectionString))
            {
                connection.Open();
                Assert.Equal("Example_Api", connection.Database);
            }
        }

        [Fact]
        public void DatabaseSchemaIsUpToDateWithModel()
        {
            using (IDbContext dbContext = new ApplicationContext())
            {
                dbContext.Database.CompatibleWithModel(true); // throws exception if database is not compatible with model (if for example entity classes exist which do not have their tables created in db)
            }
        }
    }
}
