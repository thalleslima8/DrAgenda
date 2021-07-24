namespace DrAgenda.Shared.Dto.Base
{
    public class ConfiguracaoDto
    {
        public virtual string CodigoEntidade { get; set; }

        public virtual string NomeEntidade { get; set; }

        public virtual string CodigoOrgaoAutuador { get; set; }

        public virtual string NomeOrgaoAutuador { get; set; }

        public virtual string LogoOrgaoUrl { get; set; }

        public virtual int? PrazoDiasTriagem { get; set; }

        public virtual bool PossuiAuditoria { get; set; }

        public virtual bool PossuiCodigoAgenteAuditarImagemValida { get; set; }

        public virtual string TipoIntegracaoExportacao { get; set;}

        public int? SequencialLoteExportacao { get; set;}

        public ArquivoDto LogoOrgaoArquivo { get; set; }

    }
}
