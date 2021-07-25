using FluentMigrator;

namespace DrAgenda.Migrations.Migrator._2021
{
    [Migration(202107250840, "Alter_TBPacientes_TBProfissionais")]
    public class _202107250840_Alter_TBPacientes_TBProfissionais : Migration
    {
        public override void Up()
        {
            
            
            Alter.Table("TBPacientes")
                .AddColumn("DataNascimento").AsDateTime().NotNullable();

            Alter.Table("TBProfissionais")
                .AddColumn("DataNascimento").AsDateTime().NotNullable();

            
        }

        public override void Down()
        {
        }
    }
}
