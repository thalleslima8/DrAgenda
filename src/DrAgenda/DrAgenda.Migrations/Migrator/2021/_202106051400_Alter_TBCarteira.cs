using System;
using System.Collections.Generic;
using System.Text;
using DrAgenda.Migrations.Helpers;
using FluentMigrator;

namespace DrAgenda.Migrations.Migrator._2021
{
    [Migration(202106051400, "Alter_TBCarteira")]
    public class _202106051400_Alter_TBCarteira : Migration
    {
        public override void Up()
        {
            #region TBCarteiras

            if (Schema.Table("TBCarteiras").Constraint("FK_TBCarteiras_Profissional_id").Exists())
            {
                Delete.ForeignKey("FK_TBCarteiras_Profissional_id").OnTable("TBCarteiras");
            }

            Delete.Column("Profissional_id").FromTable("TBCarteiras");

            #endregion
        }

        public override void Down()
        {
        }
    }
}
