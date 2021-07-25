using DrAgenda.Core.Dominio.Person;
using DrAgenda.Data.ORM.Base;
using FluentNHibernate.Mapping;

namespace DrAgenda.Data.ORM
{
    public class ProfissionalMap : AuditClassMapBase<Profissional>
    {
        public ProfissionalMap() : base("TBProfissionais")
        {
            Map(x => x.CPF).Unique().Not.Nullable();
            Map(x => x.Nome).Not.Nullable();
            Map(x => x.Email).Unique().Not.Nullable();
            Map(x => x.Telefone).Not.Nullable();
            Map(x => x.Formacao);
            Map(x => x.DataNascimento).Not.Nullable();

            References(x => x.Endereco);
            References(x => x.Carteira).Cascade.AllDeleteOrphan();

            HasMany(x => x.Consultas)
                .Access.CamelCaseField(Prefix.Underscore)
                .AsSet()
                .Cascade.AllDeleteOrphan();

            HasManyToMany(x => x.Pacientes)
                .Table("TBPacientesProfissionais")
                .ParentKeyColumn("Profissional_id")
                .ChildKeyColumn("Paciente_id")
                .Access.CamelCaseField(Prefix.Underscore)
                .Inverse()
                .AsSet();
        }
    }
}
