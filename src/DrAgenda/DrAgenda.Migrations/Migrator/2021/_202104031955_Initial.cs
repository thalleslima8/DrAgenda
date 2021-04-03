using DrAgenda.Migrations.Helpers;
using FluentMigrator;

namespace DrAgenda.Migrations.Migrator._2021
{
    [Migration(202104031955, "Initial")]
    public class _202104031955_Initial : Migration
    {
        public override void Up()
        {
            #region TBUsuarios

            Create.Table("TBUsuarios")
                .WithIdColumn()
                .WithAudit()
                .WithColumn("Nome").AsString(100).NotNullable()
                .WithColumn("NomeUsuario").AsString(100).NotNullable().Unique("UN_TBUsuarios_NomeUsuario")
                .WithColumn("Email").AsString(150).NotNullable().Unique("UN_TBUsuarios_Email")
                .WithColumn("Senha").AsString(1000).NotNullable()
                .WithColumn("Token").AsString(50).Nullable()
                .WithColumn("DataExpiracaoToken").AsDateTime().Nullable()
                .WithColumn("Inativo").AsBoolean().NotNullable()
                .WithColumn("Admin").AsBoolean().NotNullable();

            #endregion

            #region TBPerfisAcesso

            Create.Table("TBPerfilAcessos")
                .WithIdColumn()
                .WithAudit()
                .WithColumn("Nome").AsString(100).NotNullable();

            #endregion

            #region TBUsuariosPerfisAcesso

            Create.Table("TBUsuariosPerfilAcessos")
                .WithColumn("Usuario_id").AsGuid().NotNullable()
                .WithColumn("PerfilAcesso_id").AsGuid().NotNullable();

            #endregion

            Create.ForeignKey("FK_TBUsuariosPerfilAcessos_Usuario_id")
                .FromTable("TBUsuariosPerfilAcessos").ForeignColumn("Usuario_id")
                .ToTable("TBUsuarios").PrimaryColumn("Id");

            Create.ForeignKey("FK_TBUsuariosPerfilAcessos_PerfilAcesso_id")
                .FromTable("TBUsuariosPerfilAcessos").ForeignColumn("PerfilAcesso_id")
                .ToTable("TBPerfilAcessos").PrimaryColumn("Id");
        }

        public override void Down()
        {
        }
    }
}
