using HackathonCCR.EDM.Programmability.Functions;
using HackathonCCR.EDM.Programmability.Stored_Procedures;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.IO;

namespace HackathonCCR.EDM.Context
{
    public partial class HackathonCCRContext : DbContext, IHackathonCCRContext
    {
        public Guid UserId;
        public const string ContextName = "HackathonCCRContext";

        public HackathonCCRContext(DbConnection connection) : base(connection, true)
        {
            StoredProcedures = new StoredProcedures(this);
            ScalarValuedFunctions = new ScalarValuedFunctions(this);
            TableValuedFunctions = new TableValuedFunctions(this);
        }

        public HackathonCCRContext() : base(GetConnectionString(ContextName))
        {
            StoredProcedures = new StoredProcedures(this);
            ScalarValuedFunctions = new ScalarValuedFunctions(this);
            TableValuedFunctions = new TableValuedFunctions(this);
        }

        public static string GetConnectionString(string connectionString)
        {
            var defaultConnection = ConfigurationManager.ConnectionStrings[connectionString]?.ConnectionString;
            var azureFunctionsConnection = Environment.GetEnvironmentVariable(connectionString);
            var apoloConnection = GetCoreConnectionString();

            var connection = defaultConnection ?? azureFunctionsConnection ?? apoloConnection ?? connectionString;

            return connection;
        }

        private static string GetCoreConnectionString()
        {
            var appSettingsPath = Directory.GetCurrentDirectory() + "/appsettings.json";

            if (File.Exists(appSettingsPath))
            {
                var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder().AddJsonFile(appSettingsPath);
                var configuration = builder.Build();
                return configuration.GetConnectionString("HackathonCCRContext");
            }

            return "";
        }
    }

}



