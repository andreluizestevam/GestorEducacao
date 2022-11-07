using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.Helper;
using System.Globalization;

namespace C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3400_CtrlAtendimentoUsuario
{
    public partial class RptAtestadoMedico2 : C2BR.GestorEducacao.Reports.RptRetratoCentro
    {
        public RptAtestadoMedico2()
        {
            InitializeComponent();
        }

        public int InitReport(
              string titulo,
              string infos,
              int coEmp,
              string nomePac,
              string dias,
              bool impCid,
              string cid,
              string rgPac,
              string data,
              string hora,
              int co_col
            )
        {
            try
            {
                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;

                lblTitulo.Text = !String.IsNullOrEmpty(titulo) ? titulo.ToUpper() : "-";

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                var at = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                          join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on coEmp equals tb25.CO_EMP
                          join tb904 in TB904_CIDADE.RetornaTodosRegistros() on tb25.CO_CIDADE equals tb904.CO_CIDADE
                          where tb03.CO_COL == co_col
                          select new Atestado
                          {
                              nomeMedico = tb03.NO_COL,
                              sexoMedico = tb03.CO_SEXO_COL,
                              SIGLA_ENT = tb03.CO_SIGLA_ENTID_PROFI,
                              NUMER_ENT = tb03.NU_ENTID_PROFI,
                              UF_ENT = tb03.CO_UF_ENTID_PROFI,

                              nomeCidade = tb904.NO_CIDADE,
                              nomeUF = tb904.CO_UF
                          }).ToList().FirstOrDefault();

                at.Paciente = nomePac;
                at.DataAgend = data;
                at.HoraAgend = hora;
                at.Dias = !String.IsNullOrEmpty(dias) ? dias : "0";
                at.ImpCid = impCid;
                at.Cid = impCid && !String.IsNullOrEmpty(cid) ? ", sendo portador da afecção CID " + cid : "";
                at.RG = rgPac;

                CultureInfo culture = new CultureInfo("pt-BR");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                int dia = DateTime.Now.Day;
                int ano = DateTime.Now.Year;
                string mes = culture.TextInfo.ToTitleCase(dtfi.GetMonthName(DateTime.Now.Month));
                string diasemana = culture.TextInfo.ToTitleCase(dtfi.GetDayName(DateTime.Now.DayOfWeek));
                string dataEmis = diasemana + ", " + dia + " de " + mes + " de " + ano;

                //Atribui algumas informações à label's no relatório
                this.lblInfosDia.Text = at.nomeCidade + "-" + at.nomeUF + ", " + dataEmis;

                //Adiciona ao DataSource do Relatório
                bsReport.Clear();
                bsReport.Add(at);

                return 1;

            }
            catch { return 0; }
        }

        public class Atestado
        {
            public string Paciente { get; set; }
            public string DataAgend { get; set; }
            public string HoraAgend { get; set; }
            public string Dias { get; set; }
            public bool ImpCid { get; set; }
            public string Cid { get; set; }
            public string RG { get; set; }

            //Informações do Médico
            public string nomeMedico { get; set; }
            public string sexoMedico { get; set; }
            public string MedicoValid
            {
                get
                {
                    return (this.sexoMedico == "M" ? "Dr. " : "Dra. ") + this.nomeMedico;
                }
            }
            public string SIGLA_ENT { get; set; }
            public string NUMER_ENT { get; set; }
            public string UF_ENT { get; set; }
            public string ENT_CONCAT
            {
                get
                {
                    string valor = this.SIGLA_ENT + " " + this.NUMER_ENT + " - " + this.UF_ENT;
                    if (valor != null && valor != "")
                        return valor;
                    else
                        return "-";
                }
            }

            public string nomeCidade { get; set; }
            public string nomeUF { get; set; }
        }
    }
}
