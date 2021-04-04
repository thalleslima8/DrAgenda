using System;
using System.Collections.Generic;
using System.Text;
using DrAgenda.Migrations.Helpers;
using FluentMigrator;

namespace DrAgenda.Migrations.Migrator._2021
{
    [Migration(202104032125, "Create_TBPacientesProfissionais")]
    public class _202104032125_Create_TBPacientesProfissionais : Migration
    {
        public override void Up()
        {
            #region TBPacientesProfissionais

            Create.Table("TBPacientesProfissionais")
                .WithColumn("Paciente_id").AsGuid().PrimaryKey().ForeignKey("FK_TBPacientesProfissionais_Paciente_id", "TBPacientes", "Id").Nullable()
                .WithColumn("Profissional_id").AsGuid().PrimaryKey().ForeignKey("FK_TBPacientesProfissionais_Profissional_id", "TBProfissionais", "Id").Nullable();

            #endregion
        }

        public override void Down()
        {
        }
    }
}
