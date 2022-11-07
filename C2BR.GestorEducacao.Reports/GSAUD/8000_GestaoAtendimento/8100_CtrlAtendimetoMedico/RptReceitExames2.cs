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

namespace C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAtendimetoMedico
{
    public partial class RptReceitExames2 : C2BR.GestorEducacao.Reports.RptRetratoCentro
    {
        public RptReceitExames2()
        {
            InitializeComponent();
        }
        #region Init Report

        public int InitReport(
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

                // Instancia o header do relatorio
                ReportHeader header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;
                #region Query

                var res = (from tbs390 in TBS390_ATEND_AGEND.RetornaTodosRegistros()
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs390.TBS174_AGEND_HORAR.CO_ALU equals tb07.CO_ALU
                           join tb905 in TB905_BAIRRO.RetornaTodosRegistros() on tb07.TB905_BAIRRO.CO_BAIRRO equals tb905.CO_BAIRRO into l1
                           from ls in l1.DefaultIfEmpty()
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs390.CO_COL_ATEND equals tb03.CO_COL
                           join tbs398 in TBS398_ATEND_EXAMES.RetornaTodosRegistros() on tbs390.ID_ATEND_AGEND equals tbs398.TBS390_ATEND_AGEND.ID_ATEND_AGEND
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs398.CO_EMP_CADAS equals tb25.CO_EMP
                           join tb904 in TB904_CIDADE.RetornaTodosRegistros() on tb25.CO_CIDADE equals tb904.CO_CIDADE

                           //join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs218.TB25_EMPRESA.CO_EMP equals tb25.CO_EMP into l1
                           //from lemp in l1.DefaultIfEmpty()
                           //join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tbs218.TB14_DEPTO.CO_DEPTO equals tb14.CO_DEPTO into l2
                           //from ldep in l2.DefaultIfEmpty()
                           where (coAtendimento != 0 ? tbs390.ID_ATEND_AGEND == coAtendimento : 0 == 0)
                           select new GuiaExame
                           {
                               //Informações do Paciente
                               Paciente = tb07.NO_ALU,
                               Nascimento = tb07.DT_NASC_ALU.HasValue ? tb07.DT_NASC_ALU.Value : new DateTime(1900,1,1),
                               SexoPac = !String.IsNullOrEmpty(tb07.CO_SEXO_ALU) ? tb07.CO_SEXO_ALU == "F" ? "Feminino" : "Masculino" : "-",
                               NirePaci = tb07.NU_NIRE,
                               Bairro = ls.NO_BAIRRO ?? "",
                               Cidade = ls.TB904_CIDADE.NO_CIDADE ?? "",
                               Uf = tb07.CO_ESTA_ALU,
                               obs = !String.IsNullOrEmpty(tbs398.DE_OBSER) ? tbs398.DE_OBSER : " - ",

                               //Informações do Exame
                               nomeExame = tbs398.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI.ToUpper() ,
                               //NR_REGISTRO = tbs218.NU_REGIS_EXAME,
                               //FL_APROV_ENCAM = tbs218.TBS356_PROC_MEDIC_PROCE.FL_AUTO_PROC_MEDI,
                               dt_exame = tbs398.DT_CADAS,

                               //Informações do Médico
                               nomeMedico = tb03.NO_COL,
                               sexoMedico = tb03.CO_SEXO_COL,
                               SIGLA_ENT = tb03.CO_SIGLA_ENTID_PROFI,
                               NUMER_ENT = tb03.NU_ENTID_PROFI,
                               UF_ENT = tb03.CO_UF_ENTID_PROFI,
                               DT_ENT = tb03.DT_EMISS_ENTID_PROFI,

                               //Dados do atendimento 
                               dataAtend = tbs390.DT_CADAS,
                               coAtendimento = coAtendimento,
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
                foreach (GuiaExame at in res)
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
                    this.lblInfosDia.Text = at.nomeCidade + " - " + at.nomeUF + ", " + data;

                    //Contador que aparece no início de cada item de medicamento
                    count++;
                    at.contador = count;

                    this.lblCRM.Text = at.ENT_CONCAT;
                    this.Parametros = at.dadosAtend;

                    bsReport.Add(at);
                }

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class GuiaExame
        {
            public int coAtendimento { get; set; }
            public int contador { get; set; }
            //Informações do Paciente
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

            //Informações dos Exames
            public string obs { get; set; }
            public int ID_EXAME { get; set; }
            public string nomeExame { get; set; }
            public string NR_REGISTRO { get; set; }
            public string CO_TIPO_EXECU { get; set; }
            public string FL_APROV_ENCAM { get; set; }
            public string FL_APROV_ENCAM_V
            {
                get
                {
                    if (this.CO_TIPO_EXECU == "I")
                    {
                        string s = "";
                        switch (this.FL_APROV_ENCAM)
                        {
                            //Se requer autorização, verifica qual o status na central de regulação
                            case "S":

                                string CO_STATUS = (from tbs350 in TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros()
                                                    where tbs350.ID_ITEM_ENCAM == this.ID_EXAME
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
                                s = "APROVADO";
                                break;
                            default:
                                s = "APROVADO";
                                break;
                        }
                        return s;
                    }
                    else
                        return "APROVADO";
                }
            }
            public string exameValid
            {
                get
                {
                    return this.NR_REGISTRO.Insert(4, ".").Insert(8, ".") + " - " + this.nomeExame + "............................." + this.FL_APROV_ENCAM_V;
                }
            }
            public DateTime? dt_exame { get; set; }
            public string DT_EXAME_SOLI
            {
                get
                {
                    return "Solicitado em " + this.dt_exame.Value.ToString("dd/MM/yy") + " - " + this.dt_exame.Value.ToString("HH:mm");
                }
            }
            public string UNID_EXAME { get; set; }
            public string LOCAL_EXAME { get; set; }
            public string CONCAT_LOCAL_EXAME
            {
                get
                {
                    return "Local: " + this.UNID_EXAME + " - " + this.LOCAL_EXAME;
                }
            }
            public string DT_APROV_EXAME
            {
                get
                {
                    if (this.CO_TIPO_EXECU == "I")
                    {
                        if (this.FL_APROV_ENCAM == "S")
                        {
                            var res = (from tbs350 in TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros()
                                       where tbs350.ID_ITEM_ENCAM == this.ID_EXAME
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
                    return "";
                }
            }

            //Dados do atendimento
            public string dadosAtend
            {
                get
                {
                    string valor = "(ATENDIMENTO Nº " + this.coAtendimento + " - Em " + this.dataAtend.ToString("dd/MM/yy") + " às " + this.dataAtend.ToString("HH:mm") + ")";
                    return valor;
                }
            }
            public string codigoAtend { get; set; }
            public DateTime dataAtend { get; set; }

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
                    string valor = this.SIGLA_ENT + " " + this.NUMER_ENT + " - " + this.UF_ENT;
                    if (valor != null && valor != "")
                        return valor;
                    else
                        return "-";
                   // return //(!string.IsNullOrEmpty(this.SIGLA_ENT) ? this.SIGLA_ENT + " " + this.NUMER_ENT + " - " + this.UF_ENT : "");
                }
            }

            //Informações Gerais
            public string nomeCidade { get; set; }
            public string nomeUF { get; set; }
            public string unidEmiss { get; set; }
        }
    }
}

