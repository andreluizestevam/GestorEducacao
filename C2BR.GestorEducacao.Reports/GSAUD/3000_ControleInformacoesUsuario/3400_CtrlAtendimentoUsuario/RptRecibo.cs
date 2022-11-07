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
    public partial class RptRecibo : C2BR.GestorEducacao.Reports.RptRetratoCentro
    {
        public RptRecibo()
        {
            InitializeComponent();
        }

        public int InitReport(
              int paciente,
              int idRecibo,
              string rap,
              DateTime dtAtend,
              string vlrNum,
              string vlrExt,
              string infos,
              int coEmp
            )
        {
            try
            {
                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                var res = (from tbs363 in TBS363_CONSUL_PAGTO.RetornaTodosRegistros()
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on paciente equals tb07.CO_ALU
                           join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on tb07.TB108_RESPONSAVEL.CO_RESP equals tb108.CO_RESP
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb07.CO_EMP equals tb25.CO_EMP
                           join tb904 in TB904_CIDADE.RetornaTodosRegistros() on tb25.CO_CIDADE equals tb904.CO_CIDADE
                           where tbs363.ID_CONSUL_PAGTO == idRecibo
                           select new Recibo
                           {
                               Paciente = tb07.NO_ALU,
                               CpfPac = tb07.NU_CPF_ALU,
                               Responsavel = tb108.NO_RESP,
                               CpfResp = tb108.NU_CPF_RESP,

                               NumRecibo = tbs363.NU_RECIBO.Value,
                               vlrDinheiro = tbs363.VL_DINHE.HasValue ? tbs363.VL_DINHE.Value : 0,
                               vlrCheque = tbs363.VL_CHEQUE.HasValue ? tbs363.VL_CHEQUE.Value : 0,
                               vlrDebito = tbs363.VL_CARTA_DEBI.HasValue ? tbs363.VL_CARTA_DEBI.Value : 0,
                               vlrCredito = tbs363.VL_CARTA_CRED.HasValue ? tbs363.VL_CARTA_CRED.Value : 0,
                               vlrTransferencia = tbs363.VL_TRANS.HasValue ? tbs363.VL_TRANS.Value : 0,
                               vlrDeposito = tbs363.VL_DEPOS.HasValue ? tbs363.VL_DEPOS.Value : 0,
                               vlrBoleto = tbs363.VL_BOLETO.HasValue ? tbs363.VL_BOLETO.Value : 0,
                               vlrOutros = tbs363.VL_OUTRO.HasValue ? tbs363.VL_OUTRO.Value : 0,

                               nomeCidade = tb904.NO_CIDADE,
                               nomeUF = tb904.CO_UF
                           }).ToList().FirstOrDefault();

                res.RAP = rap;
                res.DtAgend = dtAtend.ToShortDateString();
                res.Valor = vlrNum;
                res.ValorExtenso = vlrExt;

                var emp = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);

                res.CNPJUnid = emp.CO_CPFCGC_EMP;
                res.UnidNome = emp.NO_RAZSOC_EMP;
                res.UnidFantazia = emp.NO_FANTAS_EMP;

                this.lblTitulo.Text = "RECIBO Nº " + res.NumRecibo.ToString("D5");

                CultureInfo culture = new CultureInfo("pt-BR");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                int dia = DateTime.Now.Day;
                int ano = DateTime.Now.Year;
                string mes = culture.TextInfo.ToTitleCase(dtfi.GetMonthName(DateTime.Now.Month));
                string diasemana = culture.TextInfo.ToTitleCase(dtfi.GetDayName(DateTime.Now.DayOfWeek));
                string dataEmis = diasemana + ", " + dia + " de " + mes + " de " + ano;

                //Atribui algumas informações à label's no relatório
                this.lblInfosDia.Text = res.nomeCidade + " - " + res.nomeUF + ", " + dataEmis;

                //Adiciona ao DataSource do Relatório
                bsReport.Clear();
                bsReport.Add(res);

                return 1;

            }
            catch { return 0; }
        }

        public class Recibo
        {
            public int NumRecibo { get; set; }
            public string RAP { get; set; }
            public string DtAgend { get; set; }
            public string UnidNome { get; set; }
            public string UnidFantazia { get; set; }
            public string cnpjUnid;
            public string CNPJUnid
            {
                get
                {
                    return !String.IsNullOrEmpty(cnpjUnid) ? "CNPJ - " + cnpjUnid.Format(TipoFormat.CNPJ) : "";
                }
                set
                {
                    cnpjUnid = value;
                }
            }

            public string Paciente { get; set; }
            public string CpfPac_;
            public string CpfPac
            {
                get
                {
                    if (!String.IsNullOrEmpty(this.CpfPac_))
                        return " (CPF " + this.CpfPac_.Format(TipoFormat.CPF) + ")";
                    else
                        return "";
                }
                set { this.CpfPac_ = value; }
            }
            public string Responsavel { get; set; }
            public string CpfResp_;
            public string CpfResp
            {
                get
                {
                    if (!String.IsNullOrEmpty(this.CpfResp_))
                        return " (CPF " + this.CpfResp_.Format(TipoFormat.CPF) + ")";
                    else
                        return "";
                }
                set { this.CpfResp_ = value; }
            }

            public string Valor { get; set; }
            public string ValorExtenso { get; set; }
            public decimal vlrDinheiro { get; set; }
            public decimal vlrCheque { get; set; }
            public decimal vlrDebito { get; set; }
            public decimal vlrCredito { get; set; }
            public decimal vlrTransferencia { get; set; }
            public decimal vlrDeposito { get; set; }
            public decimal vlrBoleto { get; set; }
            public decimal vlrOutros { get; set; }

            public string nomeCidade { get; set; }
            public string nomeUF { get; set; }
        }
    }
}
