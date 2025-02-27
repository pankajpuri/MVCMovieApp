using FluentMigrator.Runner;
using FluentMigrator.Runner.Exceptions;
using Microsoft.Data.SqlClient;

namespace MVCMovieApp.Migrations
{
    public class Migrations : IMigrations
    {
        private readonly ILogger<Migrations> _logger;
        private readonly IMigrationRunner _migrationRunner;

        public Migrations(ILogger<Migrations> logger, IMigrationRunner migrationRunner)
        {
            _logger = logger;
            _migrationRunner = migrationRunner;
        }

        public bool MigrationRunner()
        {
            try
            {
                _migrationRunner.MigrateUp();
            }
            catch (MissingMigrationsException)
            {
                _logger.LogInformation("Migrations not found!");
                return true;
            }
            catch(SqlException ex)
            {
                _logger.LogInformation("Migration error:"+ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("Migration error: "+ ex.Message);
            }
            
            return true;
        }
    }
}
