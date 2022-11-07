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

namespace C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar
{
    public partial class RptAtaConselhoClasseFinal : C2BR.GestorEducacao.Reports.RptPaisagemOficio
    {
        public RptAtaConselhoClasseFinal()
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
                //this.lblTitulo.Text = (!string.IsNullOrEmpty(NO_RELATORIO) ? NO_RELATORIO : "ATA DE CONSELHO DE CLASSE FINAL");
                this.lblTitulo.Text = ("ATA DE CONSELHO DE CLASSE FINAL");

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
                                      //orderby cadMat.NO_SIGLA_MATERIA == "PORTUG" ? 1 : (cadMat.NO_SIGLA_MATERIA == "MATEMA" ? 2 : (cadMat.NO_SIGLA_MATERIA == "CIENCI" ? 3 : (cadMat.NO_SIGLA_MATERIA == "GEOGRA" ? 4 : (cadMat.NO_SIGLA_MATERIA == "HISTOR" ? 5 : (cadMat.NO_SIGLA_MATERIA == "ARTE" ? 6 : (cadMat.NO_SIGLA_MATERIA == "EDFISI" ? 7 : (cadMat.NO_SIGLA_MATERIA == "RELIGI" ? 8 : (cadMat.NO_SIGLA_MATERIA == "FILOSO" ? 9 : (cadMat.NO_SIGLA_MATERIA == "INGLES" ? 10 : 10)))))))))
                                      orderby cadMat.NO_SIGLA_MATERIA == "PORTUG" ? 1 : 
                                              cadMat.NO_SIGLA_MATERIA == "MATEMA" ? 2 : 
                                              cadMat.NO_SIGLA_MATERIA == "CIENCI" ? 3 : 
                                              cadMat.NO_SIGLA_MATERIA == "GEOGRA" ? 4 : 
                                              cadMat.NO_SIGLA_MATERIA == "HISTOR" ? 5 : 
                                              cadMat.NO_SIGLA_MATERIA == "ARTE" ? 6 : 
                                              cadMat.NO_SIGLA_MATERIA == "EDFISI" ? 7 :  
                                              cadMat.NO_SIGLA_MATERIA == "RELIGI" ? 8 :  
                                              cadMat.NO_SIGLA_MATERIA == "FILOSO" ? 9 : 
                                              cadMat.NO_SIGLA_MATERIA == "INGLES" ? 10 : 10 ascending
                                      select new RelDisciplinas
                                      {
                                          CO_MAT = mat.CO_MAT,
                                          SGL_MAT = cadMat.NO_SIGLA_MATERIA,
                                          NO_MAT = cadMat.NO_MATERIA,
                                      }).ToList();
                                        //.OrderBy(w => w.SGL_MAT == "PORTUG" ? 1 : (w.SGL_MAT == "MATEMA" ? 2 : (w.SGL_MAT == "CIENCI" ? 3 : (w.SGL_MAT == "GEOGRA" ? 4 : (w.SGL_MAT == "HISTOR" ? 5 : (w.SGL_MAT == "ARTE" ? 6 : (w.SGL_MAT == "EDFISI" ? 7 : (w.SGL_MAT == "RELIGI" ? 8 : (w.SGL_MAT == "FILOSO" ? 9 : (w.SGL_MAT == "INGLES" ? 10 : 10))))))))))
                if (lstDisciplinas == null || lstDisciplinas.Count() <= 0)
                    return -1;

                #endregion

                lblLegenda.Text = "*Parte Diversificada                         *Enriquecimento Curricular";

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
                               mediaAprovacao = tb083.VL_MEDIA_APROV_DIRETA,

                               //Dados secretário(a)
                               nomeSecr1 = tb083.TB03_COLABOR.NO_COL,
                               sexoSecr = tb083.TB03_COLABOR.CO_SEXO_COL,

                               //Dados diretor(a)
                               nomeDir1 = tb03.NO_COL,
                               sexoDirec = tb03.CO_SEXO_COL
                           }).DistinctBy(ha => ha.coAlu).OrderBy(o => o.noAlu);

                #endregion

                var res = lst.ToList();

                int num = 1;
                int c = 1;
                int numMatRep = 0;
                foreach (AlunosEnotas dcf in res)
                {
                    dcf.Num = num;
                    foreach (RelDisciplinas rdf in lstDisciplinas)
                    {
                        ////Seta a legenda apenas no primeiro aluno
                        //if (num == 1)
                        //    lblLegenda.Text += rdf.SGL_MAT + " - (" + rdf.NO_MAT + ")  ";

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
                                            NOTA1 = tb079.VL_MEDIA_TRI1,
                                            NOTA2 = tb079.VL_MEDIA_TRI2,
                                            NOTA3 = tb079.VL_MEDIA_TRI3,
                                            NOTAF = tb079.VL_MEDIA_ANUAL,
                                            ORD_IMP = tb43.CO_ORDEM_IMPRE ?? 20,
                                        }).OrderBy(o => o.ORD_IMP).FirstOrDefault();

                        decimal coAno = Convert.ToDecimal(dcf.coAnoMesMat);

                        #region Retorna as faltas associadas a disciplina em questao na grade
                        var lstFaltas = (from tb132 in ctx.TB132_FREQ_ALU
                                         join tb43 in ctx.TB43_GRD_CURSO on tb132.CO_MAT equals tb43.CO_MAT
                                         where  tb132.TB01_CURSO.CO_CUR == dcf.coCur 
                                                && tb132.CO_MAT == rdf.CO_MAT
                                                && tb132.TB01_CURSO.CO_MODU_CUR == dcf.coModuCur 
                                                && tb132.CO_TUR == dcf.coTur
                                                && tb132.CO_ANO_REFER_FREQ_ALUNO == coAno
                                                && tb132.TB07_ALUNO.CO_ALU == dcf.coAlu
                                                && tb132.CO_FLAG_FREQ_ALUNO == "N"
                                         select new
                                         {
                                            tb132.CO_FLAG_FREQ_ALUNO
                                         });

                        #endregion

                        var v = lstNotas;
                        var f = lstFaltas;
                        



                        if (lstNotas != null)
                        {
                            switch (c)
                            {
                                case 1:
                                    dcf.nt01 = v.NOTA_V1;
                                    dcf.nt02 = v.NOTA_V2;
                                    dcf.nt03 = v.NOTA_V3;
                                    dcf.nmDisc1 = rdf.NO_MAT;
                                    dcf.ftDisc1 = f.Count();
                                    dcf.ntf1 = v.NOTA_VF;
                                    if (v.NOTAF < dcf.mediaAprovacao) {
                                        numMatRep++;
                                    }

                                    c++;
                                    break;
                                case 2:
                                    dcf.nt04 = v.NOTA_V1;
                                    dcf.nt05 = v.NOTA_V2;
                                    dcf.nt06 = v.NOTA_V3;
                                    dcf.nmDisc2 = rdf.NO_MAT;
                                    dcf.ftDisc2 = f.Count();
                                    dcf.ntf2 = v.NOTA_VF;
                                    if (v.NOTAF < dcf.mediaAprovacao)
                                    {
                                        numMatRep++;
                                    }
                                    c++;
                                    break;
                                case 3:
                                    dcf.nt07 = v.NOTA_V1;
                                    dcf.nt08 = v.NOTA_V2;
                                    dcf.nt09 = v.NOTA_V3;
                                    dcf.nmDisc3 = rdf.NO_MAT;
                                    dcf.ftDisc3 = f.Count();
                                    dcf.ntf3 = v.NOTA_VF;
                                    if (v.NOTAF < dcf.mediaAprovacao)
                                    {
                                        numMatRep++;
                                    }
                                    c++;
                                    break;
                                case 4:
                                    dcf.nt010 = v.NOTA_V1;
                                    dcf.nt011 = v.NOTA_V2;
                                    dcf.nt012 = v.NOTA_V3;
                                    dcf.nmDisc4 = rdf.NO_MAT;
                                    dcf.ftDisc4 = f.Count();
                                    dcf.ntf4 = v.NOTA_VF;
                                    if (v.NOTAF < dcf.mediaAprovacao)
                                    {
                                        numMatRep++;
                                    }
                                    c++;
                                    break;
                                case 5:
                                    dcf.nt013 = v.NOTA_V1;
                                    dcf.nt014 = v.NOTA_V2;
                                    dcf.nt015 = v.NOTA_V3;
                                    dcf.nmDisc5 = rdf.NO_MAT;
                                    dcf.ftDisc5 = f.Count();
                                    dcf.ntf5 = v.NOTA_VF;
                                    if (v.NOTAF < dcf.mediaAprovacao)
                                    {
                                        numMatRep++;
                                    }
                                    c++;
                                    break;
                                case 6:
                                    dcf.nt016 = v.NOTA_V1;
                                    dcf.nt017 = v.NOTA_V2;
                                    dcf.nt018 = v.NOTA_V3;
                                    dcf.nmDisc6 = rdf.NO_MAT;
                                    dcf.ftDisc6 = f.Count();
                                    dcf.ntf6 = v.NOTA_VF;
                                    if (v.NOTAF < dcf.mediaAprovacao)
                                    {
                                        numMatRep++;
                                    }
                                    c++;
                                    break;
                                case 7:
                                    dcf.nt019 = v.NOTA_V1;
                                    dcf.nt020 = v.NOTA_V2;
                                    dcf.nt021 = v.NOTA_V3;
                                    dcf.nmDisc7 = rdf.NO_MAT;
                                    dcf.ftDisc7 = f.Count();
                                    dcf.ntf7 = v.NOTA_VF;
                                    if (v.NOTAF < dcf.mediaAprovacao)
                                    {
                                        numMatRep++;
                                    }
                                    c++;
                                    break;
                                case 8:
                                    dcf.nt022 = v.NOTA_V1;
                                    dcf.nt023 = v.NOTA_V2;
                                    dcf.nt024 = v.NOTA_V3; ;
                                    dcf.nmDisc8 = rdf.NO_MAT;
                                    dcf.ftDisc8 = f.Count();
                                    dcf.ntf8 = v.NOTA_VF;
                                    if (v.NOTAF < dcf.mediaAprovacao)
                                    {
                                        numMatRep++;
                                    }
                                    c++;
                                    break;
                                case 9:
                                    dcf.nt025 = v.NOTA_V1;
                                    dcf.nt026 = v.NOTA_V2;
                                    dcf.nt027 = v.NOTA_V3;
                                    dcf.nmDisc9 = rdf.NO_MAT;
                                    dcf.ftDisc9 = f.Count();
                                    dcf.ntf9 = v.NOTA_VF;
                                    if (v.NOTAF < dcf.mediaAprovacao)
                                    {
                                        numMatRep++;
                                    }
                                    c++;
                                    break;
                                case 10:
                                    dcf.nt028 = v.NOTA_V1;
                                    dcf.nt029 = v.NOTA_V2;
                                    dcf.nt030 = v.NOTA_V3;
                                    dcf.nmDisc10 = rdf.NO_MAT;
                                    dcf.ftDisc10 = f.Count();
                                    dcf.ntf10 = v.NOTA_VF;
                                    if (v.NOTAF < dcf.mediaAprovacao)
                                    {
                                        numMatRep++;
                                    }
                                    c++;
                                    break;
                            }

                            if (!v.NOTA1.HasValue || !v.NOTA2.HasValue || !v.NOTA3.HasValue)
                            {
                                dcf.situAprovacao = "-";
                            }
                            else
                            {
                                if (v.NOTAF >= dcf.mediaAprovacao && numMatRep == 0)
                                {
                                    dcf.situAprovacao = "AP";
                                }
                                else if (v.NOTAF < dcf.mediaAprovacao && numMatRep <= 3)
                                {
                                    dcf.situAprovacao = "RC";
                                }
                                else if (v.NOTAF < dcf.mediaAprovacao && numMatRep >= 4)
                                {
                                    dcf.situAprovacao = "RP";
                                }
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
                {
                    bsReport.Add(at);
                }

                //if (Pages.Last.Index == Pages.Count)
                //{
                //    GroupHeader1.RepeatEveryPage = false;
                //}


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
            public string noTurma { get { return TB129_CADTURMAS.RetornaPelaChavePrimaria(this.coTur.Value).NO_TURMA; } }
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
                    return this.noAlu;
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

            public decimal? mediaAprovacao { get; set; }
            public string mediaAprovacao_V {
                get
                {
                    return(this.mediaAprovacao.HasValue ? this.mediaAprovacao.Value.ToString("N1") : "0");
                }
            }
            public string situAprovacao { get; set; }

            #region Variáveis das 30 colunas de notas
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
            public string nt018 { get; set; }
            public string nt019 { get; set; }
            public string nt020 { get; set; }
            public string nt021 { get; set; }
            public string nt022 { get; set; }
            public string nt023 { get; set; }
            public string nt024 { get; set; }
            public string nt025 { get; set; }
            public string nt026 { get; set; }
            public string nt027 { get; set; }
            public string nt028 { get; set; }
            public string nt029 { get; set; }
            public string nt030 { get; set; }
            #endregion

            #region Variáveis das 10 colunas de Nomes de Disciplinas
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
            #endregion

            #region Variaveis das faltas
            public int ftDisc1 { get; set; }
            public int ftDisc2 { get; set; }
            public int ftDisc3 { get; set; }
            public int ftDisc4 { get; set; }
            public int ftDisc5 { get; set; }
            public int ftDisc6 { get; set; }
            public int ftDisc7 { get; set; }
            public int ftDisc8 { get; set; }
            public int ftDisc9 { get; set; }
            public int ftDisc10 { get; set; }
            #endregion

            #region Variáveis das 10 colunas de nota final

            public string ntf1 { get; set; }
            public string ntf2 { get; set; }
            public string ntf3 { get; set; }
            public string ntf4 { get; set; }
            public string ntf5 { get; set; }
            public string ntf6 { get; set; }
            public string ntf7 { get; set; }
            public string ntf8 { get; set; }
            public string ntf9 { get; set; }
            public string ntf10 { get; set; }

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
            public string sexoSecr { get; set; }

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
            public string sexoDirec { get; set; }

            public DateTime DT_EMISSAO { get; set; }
            public string DesAto
            {
                get
                {
                    string noTurma = TB129_CADTURMAS.RetornaPelaChavePrimaria(this.coTur.Value).NO_TURMA;
                    return "Ao(s) (___)____________________ dias do mês de _________________ do ano de 20___ (dois mil e ___________), esteve reunida a Comissão de Professores do " + noTurma + " referente ao ___ Trimestre para verificar, analisar resultados e propor soluções quanto ao ensino/aprendizagem dos alunos.";
                }
            }
            public string notaRodape
            {
                get
                {
                    return ""; //"E para constar, eu " + this.nomeSecretario + (this.sexoSecr == "M" ? ", secretário, " : ", secretária, ") + "lavrei a presente ata que vai assinada " + (this.sexoDirec == "M" ? "Diretor " : "Diretora ") + " do estabelecimento de ensino.";
                }
            }
        }

        public class ListaNotas
        {
            public decimal? NOTAF { get; set; }
            public string NOTA_VF
            {
                get
                {
                    return (this.NOTAF.HasValue ? this.NOTAF.Value.ToString("N1") : " - ");
                }
            }

            public decimal? NOTA1 { get; set; }
            public string NOTA_V1
            {
                get
                {
                    return (this.NOTA1.HasValue ? this.NOTA1.Value.ToString("N1") : " - ");
                }
            }
            public decimal? NOTA2 { get; set; }
            public string NOTA_V2
            {
                get
                {
                    return (this.NOTA2.HasValue ? this.NOTA2.Value.ToString("N1") : " - ");
                }
            }
            public decimal? NOTA3 { get; set; }
            public string NOTA_V3
            {
                get
                {
                    return (this.NOTA3.HasValue ? this.NOTA3.Value.ToString("N1") : " - ");
                }
            }
            public int ORD_IMP { get; set; }
        }
        #endregion
        #endregion
    }
}
