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
    public partial class RptGuiaServAmbul : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptGuiaServAmbul()
        {
            InitializeComponent();
        }
        #region Init Report

        public int InitReport(string parametros,
                        string infos,
                        int coEmp,
                        int coAtendimento,
                        int ID_ATEND_SERV_AMBU,
                        string CO_TIPO_IMPRESSAO = "0" //Pode receber (E)xterna, (I)nterna e (0)Todos, para filtrar a emissão da guia com os itens de acordo com o recebido neste parâmetro
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
                           join tbs332 in TBS332_ATEND_SERV_AMBUL.RetornaTodosRegistros() on tbs219.ID_ATEND_MEDIC equals tbs332.ID_ATEND_MEDIC
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs219.CO_EMP_CADAS equals tb25.CO_EMP
                           join tb904 in TB904_CIDADE.RetornaTodosRegistros() on tb25.CO_CIDADE equals tb904.CO_CIDADE

                           join tb25UL in TB25_EMPRESA.RetornaTodosRegistros() on tbs332.TB25_EMPRESA.CO_EMP equals tb25UL.CO_EMP
                           join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tbs332.CO_DEPTO equals tb14.CO_DEPTO into ldep
                           from depart in ldep.DefaultIfEmpty()

                           join tb89 in TB89_UNIDADES.RetornaTodosRegistros() on tbs332.TB89_UNIDADES.CO_UNID_ITEM equals tb89.CO_UNID_ITEM into lu
                           from lum in lu.DefaultIfEmpty()
                           where (coAtendimento != 0 ? tbs219.ID_ATEND_MEDIC == coAtendimento : 0 == 0)
                           && (ID_ATEND_SERV_AMBU != 0 ? tbs332.ID_ATEND_SERV_AMBUL == ID_ATEND_SERV_AMBU : 0 == 0)
                           && (CO_TIPO_IMPRESSAO != "0" ? tbs332.CO_TIPO_EXECU == CO_TIPO_IMPRESSAO : 0 == 0)
                           select new GuiaServAmbul
                           {
                               //Informações do Paciente
                               Paciente = tb07.NO_ALU,
                               NirePaci = tb07.NU_NIRE,
                               Bairro = ls.NO_BAIRRO ?? "",
                               Cidade = ls.TB904_CIDADE.NO_CIDADE ?? "",
                               Uf = tb07.CO_ESTA_ALU,

                               //Informações dos Serviços Ambulatoriais
                               ID_ATEND_SERV_AMBU = tbs332.ID_ATEND_SERV_AMBUL,
                               nomServ = tbs332.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                               NR_REGISTRO = tbs332.NU_REGIS_SERVI_AMBUL,
                               FL_APROV_ENCAM = tbs332.TBS356_PROC_MEDIC_PROCE.FL_AUTO_PROC_MEDI,
                               dt_Serv = tbs332.DT_SERVI_AMBUL,
                               UNID_SERV = tb25UL.NO_FANTAS_EMP,
                               LOCAL_SERV = (depart != null ? depart.NO_DEPTO : " - "),
                               TP_SERV = tbs332.TP_SERVI,

                               //Aplicação
                               TP_APLICACAO = tbs332.TP_APLIC,
                               qtd_Aplicacao = tbs332.QT_APLIC,
                               NM_UNIDADE = lum.NO_UNID_ITEM,

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
                foreach (GuiaServAmbul at in res)
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

        public class GuiaServAmbul
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

            //Informações dos Serviços Ambulatoriais
            public int ID_ATEND_SERV_AMBU { get; set; }
            public string nomServ { get; set; }
            public string NR_REGISTRO { get; set; }
            public string FL_APROV_ENCAM { get; set; }
            public string FL_APROV_ENCAM_V
            {
                get
                {
                    string s = "";
                    switch (this.FL_APROV_ENCAM)
                    {
                        //Se requer autorização, verifica qual o status na central de regulação
                        case "S":
                            string CO_STATUS = (from tbs350 in TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros()
                                                where tbs350.ID_ITEM_ENCAM == this.ID_ATEND_SERV_AMBU
                                                select new { tbs350.FL_APROV_ENCAM }).FirstOrDefault().FL_APROV_ENCAM;

                            switch (CO_STATUS)
                            {
                                case "A":
                                    s = "EM ANÁLISE";
                                    break;

                                case "N":
                                    s = "NÃO APROVADO";
                                    break;

                                case "S":
                                    s = "APROVADO";
                                    break;

                                case "P":
                                    s = "PENDENTE";
                                    break;

                                default:
                                    s = " - ";
                                    break;
                            }
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
            public string ServAmbulValid
            {
                get
                {
                    return this.NR_REGISTRO.Insert(4, ".").Insert(8, ".") + " - " + this.nomServ + "............................." + this.FL_APROV_ENCAM_V;
                }
            }
            public DateTime? dt_Serv { get; set; }
            public string DT_SERV_SOLI
            {
                get
                {
                    return "Solicitado em " + this.dt_Serv.Value.ToString("dd/MM/yy") + " - " + this.dt_Serv.Value.ToString("HH:mm");
                }
            }
            public string UNID_SERV { get; set; }
            public string LOCAL_SERV { get; set; }
            public string CONCAT_LOCAL_SERV
            {
                get
                {
                    return "Local: " + this.UNID_SERV + " - " + this.LOCAL_SERV;
                }
            }
            public string TP_SERV { get; set; }
            public string TP_SERV_V
            {
                get
                {
                    string s = "";

                    switch (this.TP_SERV)
                    {
                        case "M":
                            s = "Medicação";
                            break;
                        case "A":
                            s = "Acompanhamento";
                            break;
                        case "C":
                            s = "Curativo";
                            break;
                        case "O":
                            s = "Outras";
                            break;
                    }

                    return "Serviço: " + s;
                }
            }
            public string DT_APROV_SERV_AMBU
            {
                get
                {
                    if (this.FL_APROV_ENCAM == "S")
                    {
                        var res = (from tbs350 in TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros()
                                   where tbs350.ID_ITEM_ENCAM == this.ID_ATEND_SERV_AMBU
                                   select new { tbs350.FL_APROV_ENCAM, tbs350.DT_ALTER_ENCAM, tbs350.DT_SOLIC_ENCAM }).FirstOrDefault();
                        string s = "";
                        switch (res.FL_APROV_ENCAM)
                        {
                            case "A":
                                s = "Em Análise";
                                break;

                            case "N":
                                s = "Não Aprovado";
                                break;

                            case "S":
                                s = "Aprovado";
                                break;

                            case "P":
                                s = "Pendente";
                                break;

                            default:
                                s = "";
                                break;
                        }

                        return s + " em " + (res.DT_ALTER_ENCAM.HasValue ? res.DT_ALTER_ENCAM.Value.ToString("dd/MM/yy") + " - " + res.DT_ALTER_ENCAM.Value.ToString("HH:mm") : res.DT_SOLIC_ENCAM.ToString("dd/MM/yy") + " - " + res.DT_SOLIC_ENCAM.ToString("HH:mm"));
                    }
                    else
                        return "";
                }
            }

            //Aplicação
            public string TP_APLICACAO { get; set; }
            public string TP_APLICACAO_V
            {
                get
                {
                    string s = "";
                    switch (this.TP_APLICACAO)
                    {
                        case "O":
                            s = " Oral";
                            break;
                        case "I":
                            s = " Intravenosa";
                            break;
                    }
                    return s;
                }
            }
            public string qtd_Aplicacao { get; set; }
            public string NM_UNIDADE { get; set; }
            public string CONCAT_APLICACAO
            {
                get
                {
                    return "Aplicação: " + (!string.IsNullOrEmpty(this.TP_APLICACAO) ? this.qtd_Aplicacao + " " + this.NM_UNIDADE + this.TP_APLICACAO_V : " - ");
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

