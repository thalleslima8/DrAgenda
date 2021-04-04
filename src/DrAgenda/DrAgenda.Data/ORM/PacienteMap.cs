using DrAgenda.Core.Dominio.Person;
using DrAgenda.Data.ORM.Base;
using FluentNHibernate.Mapping;

namespace DrAgenda.Data.ORM
{
    public class PacienteMap : AuditClassMapBase<Paciente>
    {
        public PacienteMap() : base("TBPacientes")
        {
            Map(x => x.CPF).Unique().Not.Nullable();
            Map(x => x.Nome).Not.Nullable();
            Map(x => x.Email).Unique().Not.Nullable();
            Map(x => x.Telefone).Not.Nullable();
            
            Map(x => x.Status);
            Map(x => x.Profissao);

            References(x => x.Endereco);
            
            HasMany(x => x.Consultas)
                .Access.CamelCaseField(Prefix.Underscore)
                .AsSet()
                .Cascade.AllDeleteOrphan();

            HasMany(x => x.Prontuarios)
                .Access.CamelCaseField(Prefix.Underscore)
                .AsSet()
                .Cascade.AllDeleteOrphan();

            HasManyToMany(x => x.Profissionais)
                .Table("TBPacientesProfissionais")
                .ParentKeyColumn("Paciente_id")
                .ChildKeyColumn("Profissional_id")
                .Access.CamelCaseField(Prefix.Underscore)
                .AsSet();
        }
    }
}
