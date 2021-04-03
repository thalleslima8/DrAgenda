using DrAgenda.Migrations.Helpers;
using FluentMigrator;

namespace DrAgenda.Migrations.Migrator._2021
{
    [Migration(202104032040, "Criar_Log_Acesso")]
    public class _202104032040_Criar_Log_Acesso : Migration
    {
        public override void Up()
        {
            #region TBLogsAcessos

            Create.Table("TBLogsAcessos")
                .WithIdColumn()
                .WithAudit()
                .WithColumn("DataHoraAcesso").AsDateTime().Nullable()
                .WithColumn("CaminhoUrl").AsString(2000).Nullable()
                .WithColumn("HostPublicoAcesso").AsString(50).Nullable()
                .WithColumn("HostLocalAcesso").AsString(50).Nullable()
                .WithColumn("UserAgent").AsString(2000).Nullable()
                .WithColumn("MethodRequest").AsString(50).Nullable()
                .WithColumn("BodyRequest").AsString(int.MaxValue).Nullable()
                .WithColumn("Usuario_id").AsGuid().NotNullable();

            Create.ForeignKey("FK_TBLogsAcessos_Usuario_id")
                .FromTable("TBLogsAcessos").ForeignColumn("Usuario_id")
                .ToTable("TBUsuarios").PrimaryColumn("Id");

            #endregion
        }

        public override void Down()
        {
        }
    }
}
