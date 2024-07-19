using CarRental.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace CarRental.Tests.IntegrationTests.Setup
{
    public class CarRentalContextFixture : IDisposable
    {
        public CarRentalContext Context { get; private set; }

        public CarRentalContextFixture()
        {
            var builder = new DbContextOptionsBuilder<CarRentalContext>();
            builder.UseSqlServer(
                "");
            Context = new CarRentalContext(builder.Options);
            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            DisableAllConstraints();
            DropForeignKeyConstraints();
            DropUniqueConstraints();
            DropAllIndexes();
            DropAllTables();
            Context.Dispose();
        }

        private void DisableAllConstraints()
        {
            const string disableConstraintsScript = @"
                DECLARE @sql NVARCHAR(MAX) = '';
                SELECT @sql += 'ALTER TABLE ' + QUOTENAME(s.name) + '.' + QUOTENAME(t.name) + ' NOCHECK CONSTRAINT ALL; '
                FROM sys.tables t
                JOIN sys.schemas s ON t.schema_id = s.schema_id;
                EXEC sp_executesql @sql;
            ";
            Context.Database.ExecuteSqlRaw(disableConstraintsScript);
        }

        private void DropForeignKeyConstraints()
        {
            const string dropForeignKeyConstraintsScript = @"
                DECLARE @sql NVARCHAR(MAX) = '';
                SELECT @sql += 'ALTER TABLE ' + QUOTENAME(s.name) + '.' + QUOTENAME(t.name) + ' DROP CONSTRAINT ' + QUOTENAME(f.name) + '; '
                FROM sys.foreign_keys f
                JOIN sys.tables t ON f.parent_object_id = t.object_id
                JOIN sys.schemas s ON t.schema_id = s.schema_id;
                EXEC sp_executesql @sql;
            ";
            Context.Database.ExecuteSqlRaw(dropForeignKeyConstraintsScript);
        }

        private void DropUniqueConstraints()
        {
            const string dropUniqueConstraintsScript = @"
                DECLARE @sql NVARCHAR(MAX) = '';
                SELECT @sql += 'ALTER TABLE ' + QUOTENAME(s.name) + '.' + QUOTENAME(t.name) + ' DROP CONSTRAINT ' + QUOTENAME(k.name) + '; '
                FROM sys.key_constraints k
                JOIN sys.tables t ON k.parent_object_id = t.object_id
                JOIN sys.schemas s ON t.schema_id = s.schema_id
                WHERE k.type = 'UQ';
                EXEC sp_executesql @sql;
            ";
            Context.Database.ExecuteSqlRaw(dropUniqueConstraintsScript);
        }

        private void DropAllIndexes()
        {
            const string dropIndexesScript = @"
                DECLARE @sql NVARCHAR(MAX) = '';
                SELECT @sql += 'DROP INDEX ' + QUOTENAME(i.name) + ' ON ' + QUOTENAME(s.name) + '.' + QUOTENAME(t.name) + '; '
                FROM sys.indexes i
                JOIN sys.tables t ON t.object_id = i.object_id
                JOIN sys.schemas s ON s.schema_id = t.schema_id
                WHERE i.type > 0 AND i.is_primary_key = 0;
                EXEC sp_executesql @sql;
            ";
            Context.Database.ExecuteSqlRaw(dropIndexesScript);
        }

        private void DropAllTables()
        {
            var tableNames = Context.Model.GetEntityTypes()
                .Select(entityType => entityType.GetTableName())
                .Distinct()
                .ToList();

            foreach (var table in tableNames)
            {
                var query = $"DROP TABLE IF EXISTS [{table}]";
                Context.Database.ExecuteSqlRaw(query);
            }
        }
    }
}