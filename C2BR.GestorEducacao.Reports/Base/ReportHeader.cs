using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.Helper;

namespace C2BR.GestorEducacao.Reports
{
    public partial class ReportHeader
    {
        public string Instituicao { get; set; }
        public string Unidade { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public int? Numero { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CNPJ { get; set; }
        public string CEP { get; set; }
        public string WSite { get; set; }
        public string TipoUnidade { get; set; }
        public string Descricao { get; set; }
        public byte[] Logo { get; set; }

        public string CnpjT
        {
            get
            {
                if (string.IsNullOrEmpty(CNPJ))
                    return string.Empty;

                return (!string.IsNullOrEmpty(CNPJ) ? "CNPJ - " + CNPJ.Format(TipoFormat.CNPJ) : "");                    
            }
        }
        public string CEPT
        {
            get
            {
                if (string.IsNullOrEmpty(CEP))
                    return string.Empty;

                return (!string.IsNullOrEmpty(CEP) ? "CEP -" + CEP.Format(TipoFormat.CEP) : "");
            }
        }
        public string Endereco
        {
            get
            {
                if (string.IsNullOrEmpty(Logradouro))
                    return string.Empty;

                return Logradouro + (Numero.HasValue ? ", " + Numero.Value.ToString() : "")
                    + (!string.IsNullOrEmpty(Complemento) ? " - " + Complemento : "");
            }
        }

        public string CidadeEstado
        {
            get
            {
                if (string.IsNullOrEmpty(Cidade))
                    return string.Empty;

                return Bairro + (!string.IsNullOrEmpty(Cidade) ? " - " + Cidade : "")
                     + (!string.IsNullOrEmpty(UF) ? " - " + UF : "");

            }
        }

        public string Contato
        {
            get
            {
                if (string.IsNullOrEmpty(Telefone) && string.IsNullOrEmpty(Email))
                    return string.Empty;

                return (!string.IsNullOrEmpty(Telefone) ? "+55 " + Telefone.Format(TipoFormat.Telefone) : "")
                    + (!string.IsNullOrEmpty(Email) ? " - " + Email : "");
            }
        }
        
        public string EndCidadeBairroUf
        {
            get
            {
                return (this.Endereco + " , " + this.Bairro + " - " + this.Cidade + " - " + this.UF);
            }
        }

        public static ReportHeader GetHeaderFromEmpresa(int codEmp)
        {
            var ctx = GestorEntities.CurrentContext;

            return (from e in ctx.TB25_EMPRESA//.Include("Image")
                    from cid in ctx.TB904_CIDADE
                    from b in ctx.TB905_BAIRRO
                    where e.CO_EMP == codEmp && e.CO_CIDADE == cid.CO_CIDADE
                    && e.CO_BAIRRO == b.CO_BAIRRO
                    select new ReportHeader
                    {
                        Logradouro = e.DE_END_EMP,
                        Numero = e.NU_END_EMP,
                        Telefone = e.CO_TEL1_EMP,
                        Cidade = cid.NO_CIDADE,
                        UF = cid.CO_UF,
                        Bairro = b.NO_BAIRRO,
                        Email = e.NO_EMAIL_EMP,
                        Instituicao = e.TB000_INSTITUICAO.ORG_NOME_FANTAS_ORGAO,
                        Unidade = e.NO_FANTAS_EMP,
                        //Logo = e.Image.ImageStream,
                        Logo = e.TB000_INSTITUICAO.Image3.ImageStream,
                        CNPJ = e.CO_CPFCGC_EMP,
                        CEP = e.CO_CEP_EMP,
                        WSite = e.NO_WEB_EMP,
                        TipoUnidade = e.CO_TIPO_UNID,
                        Descricao = e.DE_OBS_EMP,
                    }).FirstOrDefault();
        }
    }
}
