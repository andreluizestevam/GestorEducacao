using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.Reports
{
    public partial class RptColaboradorCidadeBairro : DevExpress.XtraReports.UI.XtraReport
    {
        public RptColaboradorCidadeBairro()
        {
            InitializeComponent();

            try
            {
                string qq = string.Format("Data: {0}", "12/08/2012");

                this.lblData.Text = string.Format(this.lblData.Text, DateTime.Today.ToString("dd/MM/yyyy"));
                this.lblHora.Text = string.Format(this.lblHora.Text, DateTime.Now.ToString("HH:mm"));

                GestorEntities ctx = new GestorEntities();

                var res = (from a in ctx.TB03_COLABOR.Include("TB25_Empresa")
                           from b in ctx.TB905_BAIRRO
                           where a.CO_BAIRRO == b.CO_BAIRRO

                           select new FuncionarioBairro
                           {
                               Bairro = b.NO_BAIRRO,
                               Celular = a.NU_TELE_CELU_COL,
                               Cidade = b.TB904_CIDADE.NO_CIDADE,
                               Deficiencia = a.TP_DEF,
                               Endereco = a.DE_ENDE_COL + ", " + a.NU_ENDE_COL,
                               DtNascimento = a.DT_NASC_COL

                           }).ToList();

                if (res.Count == 0)
                    return;
            }
            catch
            {
                throw;
            }
        }

        public class ReportFuncionarioBairro
        {
            public string Instituicao { get; set; }
            public string Endereco { get; set; }
            public string Unidade { get; set; }
            public byte[] Logo { get; set; }
            public string Parametros { get; set; }
            public List<FuncionarioBairro> Detalhes { get; set; }
        }

        public class FuncionarioBairro
        {
            public string Matricula { get; set; }
            public string Nome { get; set; }
            public string Sexo { get; set; }

            private string _deficiencia;
            public string Deficiencia
            {
                get
                {
                    switch (_deficiencia)
                    {
                        case "V": return "Visual";
                        case "M": return "Mental";
                        default: throw new ArgumentException("Deu pau");
                    }
                }
                set { this._deficiencia = value; }
            }

            public DateTime? DtNascimento { get; set; }
            public int Idade
            {
                get
                {
                    if (DtNascimento.HasValue)
                        return this.DtNascimento.Value.Subtract(DateTime.Now).Days;

                    return 0;
                }
            }
            public string Situacao { get; set; }
            public string Endereco { get; set; }
            public string SiglaUnid { get; set; }
            public string Celular { get; set; }
            public string Bairro { get; set; }
            public string Cidade { get; set; }
            public string UF { get; set; }
        }
    }
}
