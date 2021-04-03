using DrAgenda.Migrations.Helpers;
using FluentMigrator;

namespace DrAgenda.Migrations.Migrator._2021
{
    [Migration(202104032035, "Create_Acesso_Periodo")]
    public class _202104032035_Create_Acesso_Periodo : Migration
    {
        public override void Up()
        {
            #region TBAcessosBloqueadosPeriodo

            Create.Table("TBAcessosBloqueadosPeriodo")
                .WithIdColumn()
                .WithAudit()
                .WithColumn("DataInicio").AsDateTime().NotNullable()
                .WithColumn("DataFim").AsDateTime().NotNullable()
                .WithColumn("Motivo").AsString(200).NotNullable()
                .WithColumn("Usuario_id").AsGuid().NotNullable();

            Create.ForeignKey("FK_TBAcessosBloqueadosPeriodo_Usuario_id")
                .FromTable("TBAcessosBloqueadosPeriodo").ForeignColumn("Usuario_id")
                .ToTable("TBUsuarios").PrimaryColumn("Id");

            #endregion

            #region TBHorariosAcessos

            Create.Table("TBHorariosAcessos")
                .WithIdColumn()
                .WithAudit()
                .WithColumn("DiaSemana").AsString(150).NotNullable()
                .WithColumn("HoraInicio").AsTime().NotNullable()
                .WithColumn("HoraFim").AsTime().NotNullable()
                .WithColumn("Usuario_id").AsGuid().NotNullable();

            Create.ForeignKey("FK_TBHorariosAcessos_Usuario_id")
                .FromTable("TBHorariosAcessos").ForeignColumn("Usuario_id")
                .ToTable("TBUsuarios").PrimaryColumn("Id");

            #endregion
        }

        public override void Down()
        {
        }
    }
}
