using System;
using System.Text;
using DrAgenda.Migrations.Helpers;
using FluentMigrator;

namespace DrAgenda.Migrations.Migrator._2021
{
    [Migration(202104032020, "Create_Default_User")]
    public class _202104032020_Create_Default_User : Migration
    {
        public override void Up()
        {
            var hashPassword = SimpleHash.ComputeHash("admin", HashAlgorithmType.Md5, Encoding.UTF8.GetBytes("Admin"));

            Insert.IntoTable("TBUsuarios").Row(new
            {
                Id = Guid.NewGuid(),
                DataCriacao = DateTime.Now,
                Nome = "Administrador",
                Inativo = false,
                Email = "suporte@codout.com",
                NomeUsuario = "Admin",
                Senha = hashPassword,
                Admin = true
            });

        }

        public override void Down()
        {
        }
    }
}
