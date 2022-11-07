using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Text.RegularExpressions;
using C2BR.GestorEducacao.Reports.Helper;
using System.Globalization;

namespace C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3400_CtrlAtendimentoUsuario
{
    public partial class RptGuiaReserMedic : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptGuiaReserMedic()
        {
            InitializeComponent();
        }
        #region Init Report

        public int InitReport(string parametros,
                        string infos,
                        int coEmp,
                        int coAtendimento
                        )
        {


            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;

                lblParametros.Text = parametros;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;
                #region Query

                var res = (from tbs219 in TBS219_ATEND_MEDIC.RetornaTodosRegistros()
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs219.CO_ALU equals tb07.CO_ALU
                           join tb905 in TB905_BAIRRO.RetornaTodosRegistros() on tb07.TB905_BAIRRO.CO_BAIRRO equals tb905.CO_BAIRRO into l1
                           from ls in l1.DefaultIfEmpty()
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs219.CO_COL equals tb03.CO_COL
                           join tb092 in TB092_RESER_MEDIC.RetornaTodosRegistros() on tbs219.ID_ATEND_MEDIC equals tb092.TBS219_ATEND_MEDIC.ID_ATEND_MEDIC
                           join tb094 in TB094_ITEM_RESER_MEDIC.RetornaTodosRegistros() on tb092.ID_RESER_MEDIC equals tb094.TB092_RESER_MEDIC.ID_RESER_MEDIC
                           join tb90 in TB90_PRODUTO.RetornaTodosRegistros() on tb094.TB90_PRODUTO.CO_PROD equals tb90.CO_PROD
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs219.CO_EMP_CADAS equals tb25.CO_EMP
                           join tb904 in TB904_CIDADE.RetornaTodosRegistros() on tb25.CO_CIDADE equals tb904.CO_CIDADE

                           join tb25UL in TB25_EMPRESA.RetornaTodosRegistros() on tb092.CO_EMP equals tb25UL.CO_EMP
                           where tbs219.ID_ATEND_MEDIC == coAtendimento
                           select new GuiaReserMedicamentos
                           {
                               //Informações do Paciente
                               Paciente = tb07.NO_ALU,
                               NirePaci = tb07.NU_NIRE,
                               Bairro = ls.NO_BAIRRO ?? "",
                               Cidade = ls.TB904_CIDADE.NO_CIDADE ?? "",
                               Uf = tb07.CO_ESTA_ALU,

                               //Informações do Exame
                               nomeProd = tb90.NO_PROD,
                               NR_REGISTRO = tb092.CO_RESER_MEDIC,
                               dt_reser = tb092.DT_RESER_MEDIC,

                               //Informações do Médico
                               nomeMedico = tb03.NO_COL,
                               sexoMedico = tb03.CO_SEXO_COL,
                               SIGLA_ENT = tb03.CO_SIGLA_ENTID_PROFI,
                               NUMER_ENT = tb03.NU_ENTID_PROFI,
                               UF_ENT = tb03.CO_UF_ENTID_PROFI,
                               DT_ENT = tb03.DT_EMISS_ENTID_PROFI,

                               //Informações gerais
                               nomeCidade = tb904.NO_CIDADE,
                               nomeUF = tb904.CO_UF,
                               unidEmiss = tb25.NO_FANTAS_EMP,
                           }).ToList();

                #endregion


                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                int count = 0;
                bsReport.Clear();
                foreach (GuiaReserMedicamentos at in res)
                {
                    //Trata a label de informações do dia
                    CultureInfo culture = new CultureInfo("pt-BR");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    int dia = DateTime.Now.Day;
                    int ano = DateTime.Now.Year;
                    string mes = culture.TextInfo.ToTitleCase(dtfi.GetMonthName(DateTime.Now.Month));
                    string diasemana = culture.TextInfo.ToTitleCase(dtfi.GetDayName(DateTime.Now.DayOfWeek));
                    string data = diasemana + ", " + dia + " de " + mes + " de " + ano;

                    //Atribui algumas informações à label's no relatório
                    this.lblDoutor.Text = at.MedicoValid;
                    this.lblInfosDia.Text = at.nomeCidade + "-" + at.nomeUF + ", " + data;

                    //Contador que aparece no início de cada item de medicamento
                    count++;
                    at.contador = count;

                    this.lblCRM.Text = at.ENT_CONCAT;

                    bsReport.Add(at);
                }

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class GuiaReserMedicamentos
        {
            public int contador { get; set; }
            //Informações do Paciente
            public string Paciente { get; set; }
            public int NirePaci { get; set; }
            public string PacienteNireValid
            {
                get
                {
                    return this.NirePaci + " - " + this.Paciente;
                }
            }
            public string Bairro { get; set; }
            public string Cidade { get; set; }
            public string Uf { get; set; }
            public string concatEndereco
            {
                get
                {
                    return (this.Bairro != "" ? this.Bairro + ", " + this.Cidade + " - " + this.Uf : " - ");
                }
            }
            public string RG { get; set; }
            public string orgRG { get; set; }
            public string ufRG { get; set; }
            public string concatRG
            {
                get
                {
                    return (!string.IsNullOrEmpty(this.RG) ? this.RG + " - " + this.orgRG + "/" + this.ufRG : " - ");
                }
            }

            //Informações dos Itens
            public string nomeProd { get; set; }
            public string NR_REGISTRO { get; set; }
            public string FL_APROV_ENCAM { get; set; }
            public string FL_APROV_ENCAM_V
            {
                get
                {
                    string s = "";
                    switch (this.FL_APROV_ENCAM)
                    {
                        case "S":
                            s = "PENDENTE";
                            break;
                        case "N":
                            s = "AUTORIZADO";
                            break;
                        default:
                            s = "AUTORIZADO";
                            break;
                    }
                    return s;
                }
            }
            public string prodValid
            {
                get
                {
                    return this.NR_REGISTRO.Insert(4, ".").Insert(8, ".") + " - " + this.nomeProd + "............................." + this.FL_APROV_ENCAM_V;
                }
            }
            public DateTime? dt_reser { get; set; }
            public string DT_RESER_SOLI
            {
                get
                {
                    return "Reservado em " + this.dt_reser.Value.ToString("dd/MM/yy") + " - " + this.dt_reser.Value.ToString("HH:mm");
                }
            }
            public string DE_PROD { get; set; }
            public string CONCAT_DE_PROD
            {
                get
                {
                    return "Descrição: " + this.DE_PROD;
                }
            }


            //Informações do Médico
            public string nomeMedico { get; set; }
            public string CRMMedico { get; set; }
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
            public DateTime? DT_ENT { get; set; }
            public string ENT_CONCAT
            {
                get
                {
                    return (!string.IsNullOrEmpty(this.SIGLA_ENT) ? this.SIGLA_ENT + " " + this.NUMER_ENT + " - " + this.UF_ENT : "");
                }
            }

            //Informações Gerais
            public string nomeCidade { get; set; }
            public string nomeUF { get; set; }
            public string unidEmiss { get; set; }
        }
    }
}

