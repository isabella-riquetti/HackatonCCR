using HackathonCCR.EDM.Models;
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
    public partial class BaseContext : DbContext, IBaseContext
    {
        public Guid UserId;
        public const string ContextName = "BaseContext";

        DbSet<User> IBaseContext.User { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public BaseContext(DbConnection connection) : base(connection, true)
        {
            StoredProcedures = new StoredProcedures(this);
            ScalarValuedFunctions = new ScalarValuedFunctions(this);
            TableValuedFunctions = new TableValuedFunctions(this);
        }

        public BaseContext() : base(GetConnectionString(ContextName))
        {
            StoredProcedures = new StoredProcedures(this);
            ScalarValuedFunctions = new ScalarValuedFunctions(this);
            TableValuedFunctions = new TableValuedFunctions(this);
        }

        public static string GetConnectionString(string connectionString)
        {
            var defaultConnection = ConfigurationManager.ConnectionStrings[connectionString]?.ConnectionString;
            var coreConnection = GetCoreConnectionString();
            var connection = defaultConnection ?? coreConnection ?? connectionString;

            return connection;
        }

        private static string GetCoreConnectionString()
        {
            var appSettingsPath = Directory.GetCurrentDirectory() + "/appsettings.json";

            if (File.Exists(appSettingsPath))
            {
                var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder().AddJsonFile(appSettingsPath);
                var configuration = builder.Build();
                return configuration.GetConnectionString("Sys10Context");
            }

            return "";
        }

        public int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}



