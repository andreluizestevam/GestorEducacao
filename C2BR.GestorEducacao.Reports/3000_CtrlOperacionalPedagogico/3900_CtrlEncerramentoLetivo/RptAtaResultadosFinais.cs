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

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3900_CtrlEncerramentoLetivo
{
    public partial class RptAtaResultadosFinais : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptAtaResultadosFinais()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              string infos,
                              int CO_EMP,
                              string strP_CO_ANO_REFER,
                              int strP_CO_EMP,
                              int strP_CO_MODU_CUR,
                              int strP_CO_CUR,
                              int strP_CO_TUR,
                              DateTime DataEmiss,
                              string NO_RELATORIO
                             )
        {
            try
            {
                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                // Instancia o header do relatorio
                ReportHeader header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(CO_EMP);

                //Seta o título dinamicamente inserindo um * caso não exista título definido de forma que apenas emitindo o relatório é possível saber se é dinamico ou não
                this.lblTitulo.Text = (!string.IsNullOrEmpty(NO_RELATORIO) ? NO_RELATORIO : "ATA DE RESULTADOS FINAIS*");

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #region Query Colaborador Parametrizada
                decimal dtRef = decimal.Parse(strP_CO_ANO_REFER);
                int intAno = int.Parse(strP_CO_ANO_REFER);

                #region Retorna as disciplinas associadas ao curso em questão na grade

                var lstDisciplinas = (from tb43 in ctx.TB43_GRD_CURSO
                                      join mat in ctx.TB02_MATERIA on tb43.CO_MAT equals mat.CO_MAT
                                      join cadMat in ctx.TB107_CADMATERIAS on mat.ID_MATERIA equals cadMat.ID_MATERIA
                                      where tb43.TB44_MODULO.CO_MODU_CUR == strP_CO_MODU_CUR && tb43.CO_CUR == strP_CO_CUR
                                      && (tb43.CO_ANO_GRADE == strP_CO_ANO_REFER)
                                      && tb43.CO_EMP == strP_CO_EMP
                                      && tb43.ID_MATER_AGRUP == null
                                          //Listar as disciplinas diferentes da classificação "Não se Aplica"
                                      && cadMat.CO_CLASS_BOLETIM != 4
                                      select new RelDisciplinas
                                      {
                                          CO_MAT = mat.CO_MAT,
                                          SGL_MAT = cadMat.NO_SIGLA_MATERIA,
                                          NO_MAT = cadMat.NO_MATERIA,
                                          ORD_IMP = tb43.CO_ORDEM_IMPRE ?? 20,
                                      }).Distinct().OrderBy(w => w.ORD_IMP).ToList();

                if (lstDisciplinas == null || lstDisciplinas.Count() <= 0)
                    return -1;

                #endregion

                lblLegenda.Text = "Legenda: ";

                #region Retorna os dados dos alunos

                var lst = (from tb07 in ctx.TB07_ALUNO
                           join tb08 in ctx.TB08_MATRCUR on tb07.CO_ALU equals tb08.CO_ALU
                           join tb083 in ctx.TB83_PARAMETRO on tb08.CO_EMP equals tb083.CO_EMP
                                                      join tb03 in ctx.TB03_COLABOR on tb083.CO_DIR1 equals tb03.CO_COL
                           join tb01 in ctx.TB01_CURSO on tb08.CO_CUR equals tb01.CO_CUR
                           join tb06 in ctx.TB06_TURMAS on tb08.CO_TUR equals tb06.CO_TUR
                           join tb129 in ctx.TB129_CADTURMAS on tb06.CO_TUR equals tb129.CO_TUR
                           where tb08.CO_EMP == strP_CO_EMP
                           && tb08.TB44_MODULO.CO_MODU_CUR == strP_CO_MODU_CUR
                           && tb08.CO_CUR == strP_CO_CUR
                           && tb08.CO_ANO_MES_MAT == strP_CO_ANO_REFER
                           && tb08.CO_SIT_MAT != "C"
                           && tb08.CO_TUR == strP_CO_TUR

                           select new AlunosEnotas
                           {
                               noAlu = tb07.NO_ALU,
                               coAlu = tb07.CO_ALU,
                               nuNire = tb07.NU_NIRE,
                               coSituAlu = tb08.CO_SIT_MAT,
                               coAnoMesMat = tb08.CO_ANO_MES_MAT,
                               coCur = tb08.CO_CUR,
                               coModuCur = tb08.TB44_MODULO.CO_MODU_CUR,
                               coTur = tb08.CO_TUR,
                               Resultado = (tb08.CO_SIT_MAT == "X" ? "Transferido(a)" : tb08.CO_STA_APROV != null && tb08.CO_SIT_MAT == "F" ? (tb08.CO_STA_APROV == "A" && (tb08.CO_STA_APROV_FREQ == "A" || tb08.CO_STA_APROV_FREQ == null) ? "Aprovado(a)" : "Reprovado(a)") : "Cursando"),

                               //Dados secretário(a)
                               nomeSecr1 = tb083.TB03_COLABOR.NO_COL,
                               sexoSecr = tb083.TB03_COLABOR.CO_SEXO_COL,

                               DT_EMISSAO = DataEmiss,

                               //Dados diretor(a)
                                nomeDir1 = tb03.NO_COL,
                                sexoDirec = tb03.CO_SEXO_COL,
                           }).DistinctBy(ha => ha.coAlu).OrderBy(o => o.noAlu);

                #endregion

                var res = lst.ToList();

                int num = 1;
                int c = 1;
                foreach (AlunosEnotas dcf in res)
                {
                    dcf.Num = num;
                    foreach (RelDisciplinas rdf in lstDisciplinas)
                    {
                        //Seta a legenda apenas no primeiro aluno
                        if (num == 1)
                            lblLegenda.Text += rdf.SGL_MAT + " - (" + rdf.NO_MAT + ")  ";

                        #region Apresenta as notas das disciplinas dos alunos

                        //===> Retorna as notas para cada disciplina do aluno
                        var lstNotas = (from tb079 in ctx.TB079_HIST_ALUNO
                                        join tb43 in ctx.TB43_GRD_CURSO on tb079.CO_MAT equals tb43.CO_MAT
                                        where tb079.CO_MODU_CUR == dcf.coModuCur
                                          && tb43.CO_MAT == tb079.CO_MAT
                                         && tb43.CO_CUR == tb079.CO_CUR
                                         && tb43.CO_ANO_GRADE == tb079.CO_ANO_REF
                                         && tb43.ID_MATER_AGRUP == null
                                        && tb079.CO_CUR == dcf.coCur
                                        && tb079.CO_TUR == dcf.coTur
                                        && tb079.CO_ANO_REF == dcf.coAnoMesMat
                                        && tb079.CO_ALU == dcf.coAlu
                                        && tb079.CO_MAT == rdf.CO_MAT
                                        select new ListaNotas
                                        {
                                            NOTA = tb079.VL_MEDIA_FINAL,
                                            ORD_IMP = tb43.CO_ORDEM_IMPRE ?? 20,
                                        }).OrderBy(o => o.ORD_IMP).FirstOrDefault();

                        var v = lstNotas;

                        if (lstNotas != null)
                        {
                            switch (c)
                            {
                                case 1:
                                    dcf.nt01 = v.NOTA_V;
                                    dcf.nmDisc1 = rdf.SGL_MAT;
                                    c++;
                                    break;
                                case 2:
                                    dcf.nt02 = v.NOTA_V;
                                    dcf.nmDisc2 = rdf.SGL_MAT;
                                    c++;
                                    break;
                                case 3:
                                    dcf.nt03 = v.NOTA_V;
                                    dcf.nmDisc3 = rdf.SGL_MAT;
                                    c++;
                                    break;
                                case 4:
                                    dcf.nt04 = v.NOTA_V;
                                    dcf.nmDisc4 = rdf.SGL_MAT;
                                    c++;
                                    break;
                                case 5:
                                    dcf.nt05 = v.NOTA_V;
                                    dcf.nmDisc5 = rdf.SGL_MAT;
                                    c++;
                                    break;
                                case 6:
                                    dcf.nt06 = v.NOTA_V;
                                    dcf.nmDisc6 = rdf.SGL_MAT;
                                    c++;
                                    break;
                                case 7:
                                    dcf.nt07 = v.NOTA_V;
                                    dcf.nmDisc7 = rdf.SGL_MAT;
                                    c++;
                                    break;
                                case 8:
                                    dcf.nt08 = v.NOTA_V;
                                    dcf.nmDisc8 = rdf.SGL_MAT;
                                    c++;
                                    break;
                                case 9:
                                    dcf.nt09 = v.NOTA_V;
                                    dcf.nmDisc9 = rdf.SGL_MAT;
                                    c++;
                                    break;
                                case 10:
                                    dcf.nt010 = v.NOTA_V;
                                    dcf.nmDisc10 = rdf.SGL_MAT;
                                    c++;
                                    break;
                                case 11:
                                    dcf.nt011 = v.NOTA_V;
                                    dcf.nmDisc11 = rdf.SGL_MAT;
                                    c++;
                                    break;
                                case 12:
                                    dcf.nt012 = v.NOTA_V;
                                    dcf.nmDisc12 = rdf.SGL_MAT;
                                    c++;
                                    break;
                                case 13:
                                    dcf.nt013 = v.NOTA_V;
                                    dcf.nmDisc13 = rdf.SGL_MAT;
                                    c++;
                                    break;
                                case 14:
                                    dcf.nt014 = v.NOTA_V;
                                    dcf.nmDisc14 = rdf.SGL_MAT;
                                    c++;
                                    break;
                                case 15:
                                    dcf.nt015 = v.NOTA_V;
                                    dcf.nmDisc15 = rdf.SGL_MAT;
                                    c++;
                                    break;
                                case 16:
                                    dcf.nt016 = v.NOTA_V;
                                    dcf.nmDisc16 = rdf.SGL_MAT;
                                    c++;
                                    break;
                                case 17:
                                    dcf.nt017 = v.NOTA_V;
                                    dcf.nmDisc17 = rdf.SGL_MAT;
                                    c++;
                                    break;
                            }
                        }
                        #endregion
                    }
                    num++;
                    c = 1;
                }

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (AlunosEnotas at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

                #endregion

        #region Classe Lista Pauta Chamada Verso

        /*
         * Esta classe foi criada para receber as datas da consulta que retorna as datas que possuem lançamento de frequência
         * */
        public class RelDisciplinas
        {
            public string SGL_MAT { get; set; }
            public string NO_MAT { get; set; }
            public int CO_MAT { get; set; }
            public int ORD_IMP { get; set; }
        }

        public class AlunosEnotas
        {
            public string noTurma { get; set; }
            public string noAlu { get; set; }
            public int nuNire { get; set; }
            public string Nire
            {
                get
                {
                    int l = this.nuNire.ToString().Length;
                    string n = this.nuNire.ToString();
                    while (l < 7)
                    {
                        n = "0" + n;
                        l = n.Length;
                    }
                    return n;
                }
            }
            public string NomeAluno
            {
                get
                {
                    return this.Nire + " - " + (this.noAlu.Length > 30 ? this.noAlu.Substring(0, 30).ToUpper() + "..." : this.noAlu);
                }
            }
            public string coSituAlu { get; set; }
            public string ST
            {
                get
                {
                    //Verifica se existe algum registro de transferência interna entre turmas
                    bool res = (from tbtrans in TB_TRANSF_INTERNA.RetornaTodosRegistros()
                                where tbtrans.TB07_ALUNO.CO_ALU == this.coAlu
                                && tbtrans.CO_TURMA_ATUAL == this.coTur
                                select tbtrans).Any();
                    if (res == true)
                    {
                        return "TRI";
                    }
                    else
                    {
                        string r = "";
                        switch (this.coSituAlu)
                        {
                            case "A":
                                r = "MAT";
                                break;
                            case "T":
                                r = "TRA";
                                break;
                            case "F":
                                r = "FIN";
                                break;
                            case "C":
                                r = "CAN";
                                break;
                            case "X":
                                r = "TRE";
                                break;
                            default:
                                r = " - ";
                                break;
                        }
                        return r;
                    }
                }
            }
            public string Resultado { get; set; }

            //Parâmetros para fazer query e saber se o aulo foi transferido de turma
            public int? coTur { get; set; }
            public int coAlu { get; set; }
            public int coModuCur { get; set; }
            public int coCur { get; set; }
            public string coAnoMesMat { get; set; }
            public int Num { get; set; }

            #region Variáveis das 17 colunas de notas
            public string nt01 { get; set; }
            public string nt02 { get; set; }
            public string nt03 { get; set; }
            public string nt04 { get; set; }
            public string nt05 { get; set; }
            public string nt06 { get; set; }
            public string nt07 { get; set; }
            public string nt08 { get; set; }
            public string nt09 { get; set; }
            public string nt010 { get; set; }
            public string nt011 { get; set; }
            public string nt012 { get; set; }
            public string nt013 { get; set; }
            public string nt014 { get; set; }
            public string nt015 { get; set; }
            public string nt016 { get; set; }
            public string nt017 { get; set; }
            #endregion

            #region Variáveis das 17 colunas de Nomes de Disciplinas
            public string nmDisc1 { get; set; }
            public string nmDisc2 { get; set; }
            public string nmDisc3 { get; set; }
            public string nmDisc4 { get; set; }
            public string nmDisc5 { get; set; }
            public string nmDisc6 { get; set; }
            public string nmDisc7 { get; set; }
            public string nmDisc8 { get; set; }
            public string nmDisc9 { get; set; }
            public string nmDisc10 { get; set; }
            public string nmDisc11 { get; set; }
            public string nmDisc12 { get; set; }
            public string nmDisc13 { get; set; }
            public string nmDisc14 { get; set; }
            public string nmDisc15 { get; set; }
            public string nmDisc16 { get; set; }
            public string nmDisc17 { get; set; }
            #endregion

            //Dados do(a) Secretário(a)
            public string nomeSecr1 { get; set; }
            public string nomeSecretario
            {
                get
                {
                    string nome = (this.nomeSecr1 != null ? (this.nomeSecr1 != "") ? this.nomeSecr1.ToString() : "***" : "***");
                    return nome;
                }

            }
            public string sexoSecr {get; set; }

            //Dados do(a) Diretor(a)
            public string nomeDir1 { get; set; }
            public string nomeDiretor
            {
                get
                {
                    string nome = ((this.nomeDir1 != "") ? this.nomeDir1.ToString() : "***");
                   
                    return nome;
                }
            }
            public string sexoDirec {get; set; }

            public DateTime DT_EMISSAO { get; set; }
            public string DesAto
            {
                get
                {
                    string noTurma = TB129_CADTURMAS.RetornaPelaChavePrimaria(this.coTur.Value).NO_TURMA;
                    return "Aos " + this.DT_EMISSAO.Day.ToString().PadLeft(2, '0') + " dias do mês de " + Funcoes.GetMes(this.DT_EMISSAO.Month) + " do ano " + this.DT_EMISSAO.Year + ", terminou-se o processo de apuração das notas"
                     + "finais e nota global dos alunos da " + noTurma + " deste estabelecimento de ensino com os seguintes resultados: ";
                }
            }
            public string notaRodape
            {
                get
                {
                    return "E para constar, eu " + this.nomeSecretario + (this.sexoSecr == "M" ? ", secretário, " : ", secretária, ") + "lavrei a presente ata que vai assinada " + (this.sexoDirec == "M" ? "Diretor " : "Diretora ") + " do estabelecimento de ensino.";
                }
            }
        }

        public class ListaNotas
        {
            public decimal? NOTA { get; set; }
            public string NOTA_V
            {
                get
                {
                    return (this.NOTA.HasValue ? this.NOTA.Value.ToString("N1") : " - ");
                }
            }
            public int ORD_IMP { get; set; }
        }
        #endregion
        #endregion
    }
}
