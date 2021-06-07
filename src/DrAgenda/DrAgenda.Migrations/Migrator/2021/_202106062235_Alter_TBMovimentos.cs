using FluentMigrator;

namespace DrAgenda.Migrations.Migrator._2021
{
    [Migration(202106062235, "Alter_TBMovimentos")]
    public class _202106062235_Alter_TBMovimentos : Migration
    {
        public override void Up()
        {
            #region TBMovimentos
            
            Alter.Table("TBMovimentos").AddColumn("Consulta_id").AsGuid().NotNullable().ForeignKey("FK_TBMovimentos_Consulta_id", "TBConsultas", "Id");

            #endregion
        }

        public override void Down()
        {
        }
    }
}
