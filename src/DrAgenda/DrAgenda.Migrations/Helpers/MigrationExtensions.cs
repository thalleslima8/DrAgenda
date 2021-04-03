using FluentMigrator.Builders.Create.Table;

namespace DrAgenda.Migrations.Helpers
{
    internal static class MigrationExtensions
    {
        public static ICreateTableColumnOptionOrWithColumnSyntax WithIdColumn(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
        {
            return tableWithColumnSyntax
                .WithColumn("Id")
                .AsGuid()
                .PrimaryKey();
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax WithAudit(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
        {
            return tableWithColumnSyntax
                .WithColumn("DataCriacao").AsDateTime().Nullable()
                .WithColumn("DataAtualizacao").AsDateTime().Nullable()
                .WithColumn("CriadoPor").AsString(100).Nullable()
                .WithColumn("AtualizadoPor").AsString(100).Nullable();
        }
    }
}
