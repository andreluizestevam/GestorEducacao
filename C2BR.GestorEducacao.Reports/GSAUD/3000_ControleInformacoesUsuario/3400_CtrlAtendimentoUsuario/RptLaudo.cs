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
    public partial class RptLaudo : C2BR.GestorEducacao.Reports.RptRetratoCentro
    {
        public RptLaudo()
        {
            InitializeComponent();
        }

        public int InitReport(
              string titulo,
              string infos,
              int coEmp,
              int pac,
              string laudo,
              DateTime dataLaudo,
              int co_col
            )
        {
            try
            {
                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;

                lblTitulo.Text = !String.IsNullOrEmpty(titulo) ? titulo.ToUpper() : "LAUDO TÉCNICO";

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
                          select new Laudo
                          {
                              nomeMedico = tb03.NO_COL,
                              sexoMedico = tb03.CO_SEXO_COL,
                              SIGLA_ENT = tb03.CO_SIGLA_ENTID_PROFI,
                              NUMER_ENT = tb03.NU_ENTID_PROFI,
                              UF_ENT = tb03.CO_UF_ENTID_PROFI,

                              nomeCidade = tb904.NO_CIDADE,
                              nomeUF = tb904.CO_UF
                          }).ToList().FirstOrDefault();

                var tb07 = TB07_ALUNO.RetornaPeloCoAlu(pac);

                if (tb07 != null)
                {
                    at.Paciente = tb07.NO_ALU;
                    at.Nascimento = tb07.DT_NASC_ALU.HasValue ? tb07.DT_NASC_ALU.Value : new DateTime(1900, 1, 1);
                    at.SexoPac = !String.IsNullOrEmpty(tb07.CO_SEXO_ALU) ? tb07.CO_SEXO_ALU == "F" ? "Feminino" : "Masculino" : "-";
                }
                at.DesLaudo = laudo;

                CultureInfo culture = new CultureInfo("pt-BR");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                int dia = dataLaudo.Day;
                int ano = dataLaudo.Year;
                string mes = culture.TextInfo.ToTitleCase(dtfi.GetMonthName(dataLaudo.Month));
                string diasemana = culture.TextInfo.ToTitleCase(dtfi.GetDayName(dataLaudo.DayOfWeek));
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

        public class Laudo
        {
            public string Paciente { get; set; }
            public DateTime Nascimento { get; set; }
            public string NascPac
            {
                get
                {
                    return Nascimento.ToShortDateString();
                }
            }
            public string SexoPac { get; set; }
            public string idadePac
            {
                get
                {
                    return Funcoes.FormataDataNascimento(Nascimento);
                }
            }

            public string DesLaudo { get; set; }

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
