using System;
using System.Collections.Generic;
using System.Text;
using DrAgenda.Migrations.Helpers;
using FluentMigrator;

namespace DrAgenda.Migrations.Migrator._2021
{
    [Migration(202104032045, "Create_Main_Tables")]
    public class _202104032045_Create_Main_Tables : Migration
    {
        public override void Up()
        {
            #region TBEnderecos

            Create.Table("TBEnderecos")
                .WithIdColumn()
                .WithAudit()
                .WithColumn("Logradouro").AsString(50).NotNullable()
                .WithColumn("Complemento").AsString(100).NotNullable()
                .WithColumn("Numero").AsString(10).NotNullable()
                .WithColumn("Bairro").AsString(100).NotNullable()
                .WithColumn("Cep").AsString(10).NotNullable()
                .WithColumn("Municipio").AsString(50).NotNullable()
                .WithColumn("UF").AsString(2).NotNullable();

            #endregion

            #region TBMovimentos

            Create.Table("TBMovimentos")
                .WithIdColumn()
                .WithAudit()
                .WithColumn("Data").AsDate().NotNullable()
                .WithColumn("Valor").AsDecimal().NotNullable();

            #endregion

            #region TBCarteiras

            Create.Table("TBCarteiras")
                .WithIdColumn()
                .WithAudit()
                .WithColumn("Saldo").AsDecimal().NotNullable();

            #endregion

            #region TBPacientes

            Create.Table("TBPacientes")
                .WithIdColumn()
                .WithAudit()
                .WithColumn("Nome").AsString(50).NotNullable()
                .WithColumn("CPF").AsString(15).NotNullable()
                .WithColumn("Email").AsString(100).NotNullable()
                .WithColumn("Telefone").AsString().NotNullable()
                .WithColumn("Profissao").AsString(50).NotNullable()
                .WithColumn("Endereco_id").AsGuid().NotNullable().ForeignKey("FK_TBPacientes_Endereco_id", "TBEnderecos", "Id")
                .WithColumn("Status").AsString(200).NotNullable();

            #endregion

            #region TBProfissionais

            Create.Table("TBProfissionais")
                .WithIdColumn()
                .WithAudit()
                .WithColumn("Nome").AsString(50).NotNullable()
                .WithColumn("CPF").AsString(15).NotNullable()
                .WithColumn("Email").AsString(100).NotNullable()
                .WithColumn("Telefone").AsString().NotNullable()
                .WithColumn("Formacao").AsString(50).NotNullable()
                .WithColumn("Endereco_id").AsGuid().NotNullable().ForeignKey("FK_TBProfissionais_Endereco_id", "TBEnderecos", "Id")
                .WithColumn("Carteira_id").AsGuid().NotNullable().ForeignKey("FK_TBProfissionais_Carteira_id", "TBCarteiras", "Id");
                

            #endregion

            #region TBProntuarios

            Create.Table("TBProntuarios")
                .WithIdColumn()
                .WithAudit()
                .WithColumn("EvolucaoClinica").AsMaxString().NotNullable()
                .WithColumn("HipoteseDiagnostico").AsMaxString().NotNullable()
                .WithColumn("HistoricoClinico").AsMaxString().NotNullable()
                .WithColumn("Paciente_id").AsGuid().ForeignKey("FK_TBProntuarios_Paciente_id", "TBPacientes", "Id").NotNullable()
                .WithColumn("Profissional_id").AsGuid().ForeignKey("FK_TBProntuarios_Profissional_id", "TBProfissionais", "Id").NotNullable();

            #endregion

            #region TBConsultas

            Create.Table("TBConsultas")
                .WithIdColumn()
                .WithAudit()
                .WithColumn("Horario").AsDate().NotNullable()
                .WithColumn("Valor").AsDecimal().NotNullable()
                .WithColumn("Status").AsString(50).NotNullable()
                .WithColumn("Paciente_id").AsGuid().ForeignKey("FK_TBConsultas_Paciente_id", "TBPacientes", "Id").NotNullable()
                .WithColumn("Profissional_id").AsGuid().ForeignKey("FK_TBConsultas_Profissional_id", "TBProfissionais", "Id").NotNullable();

            #endregion
            
            #region Relacionamentos

            Alter.Table("TBMovimentos")
                .AddColumn("Profissional_id").AsGuid().ForeignKey("FK_TBMovimentos_Profissional_id", "TBProfissionais", "Id").NotNullable()
                .AddColumn("Paciente_id").AsGuid().ForeignKey("FK_TBMovimentos_Paciente_id", "TBPacientes", "Id").NotNullable()
                .AddColumn("Carteira_id").AsGuid().ForeignKey("FK_TBMovimentos_Carteira_id", "TBCarteiras", "Id").NotNullable();

            Alter.Table("TBCarteiras")
                .AddColumn("Profissional_id").AsGuid().ForeignKey("FK_TBCarteiras_Profissional_id", "TBProfissionais", "Id").NotNullable();

            #endregion
        }

        public override void Down()
        {
        }
    }
}
