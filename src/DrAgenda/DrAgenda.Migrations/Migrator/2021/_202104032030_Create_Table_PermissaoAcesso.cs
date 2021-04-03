using DrAgenda.Migrations.Helpers;
using FluentMigrator;

namespace DrAgenda.Migrations.Migrator._2021
{
    [Migration(202104032030, "Create_Table_PermissaoAcesso")]
    public class _202104032030_Create_Table_PermissaoAcesso : Migration
    {
        public override void Up()
        {
              #region TBPermissoesAcesso
            Create.Table("TBPermissoesAcesso")
                .WithIdColumn()
                .WithColumn("Nome").AsString(100).NotNullable()
                .WithColumn("Acao").AsString(255).Nullable()
                .WithColumn("PermissaoAcessoPai_id").AsGuid().Nullable();

            Create.ForeignKey("FK_TBPermissoesAcesso_PermissaoAcessoPai_id")
                .FromTable("TBPermissoesAcesso").ForeignColumn("PermissaoAcessoPai_id")
                .ToTable("TBPermissoesAcesso").PrimaryColumn("Id");
            #endregion

            #region TBPerfilAcessosPermissoesAcesso
            Create.Table("TBPerfilAcessosPermissoesAcesso")
                .WithColumn("PerfilAcesso_id").AsGuid().PrimaryKey()
                .WithColumn("PermissaoAcesso_id").AsGuid().PrimaryKey();

            Create.ForeignKey("FK_TBPerfilAcessosPermissoesAcesso_PerfilAcesso_id")
                .FromTable("TBPerfilAcessosPermissoesAcesso").ForeignColumn("PerfilAcesso_id")
                .ToTable("TBPerfilAcessos").PrimaryColumn("Id");

            Create.ForeignKey("FK_TBPerfilAcessosPermissoesAcesso_PermissaoAcesso_id")
                .FromTable("TBPerfilAcessosPermissoesAcesso").ForeignColumn("PermissaoAcesso_id")
                .ToTable("TBPermissoesAcesso").PrimaryColumn("Id");
            #endregion
        }

        public override void Down()
        {
        }
    }
}
