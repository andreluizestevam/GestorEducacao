using System;
using System.Linq;
using System.Drawing;
using C2BR.GestorEducacao.Reports.Helper;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Data.Objects;
using System.Text;
using C2BR.GestorEducacao.Reports;

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar
{
    public partial class RptStatusLancNotaAtivInsef : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptStatusLancNotaAtivInsef()
        {
            InitializeComponent();
        }
        #region Init Report

        public int InitReport(string parametros,
                                string infos,
                                int strP_CO_EMP_REF,
                                string strP_CO_ANO_MES_MAT,
                                int strP_CO_CUR,
                                int strP_CO_MODU_CUR,
                                int strP_CO_TUR,
                                string strP_CO_BIMESTRE,
                                int coEmp
            )
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;
                this.VisiblePageHeader = true;
                this.VisibleNumeroPage = true;
                this.VisibleDataHeader = true;
                this.VisibleHoraHeader = true;

                // Instancia o header do relatorio
                C2BR.GestorEducacao.Reports.ReportHeader header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(coEmp);

                string trimestre = "";

                switch (strP_CO_BIMESTRE)
                {
                    case "T1":
                        trimestre = "1° Trimestre";
                        break;
                    case "T2":
                        trimestre = "2° Trimestre";
                        break;
                    case "T3":
                        trimestre = "3° Trimestre";
                        break;
                }

                lblTitulo.Text = "RELATÓRIO ANALÍTICO DE LANÇAMENTO DE NOTAS/AVALIAÇÕES - " + trimestre;

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                var res = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                           join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb43.CO_CUR
                           join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                           join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                           join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb08.CO_TUR equals tb129.CO_TUR
                           join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb01.CO_CUR
                           join tb44 in TB44_MODULO.RetornaTodosRegistros() on tb08.TB44_MODULO.CO_MODU_CUR equals tb44.CO_MODU_CUR
                           where tb08.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT
                           && tb43.CO_EMP == tb08.CO_EMP
                           && tb43.CO_ANO_GRADE == tb08.CO_ANO_MES_MAT
                           && tb43.CO_CUR == tb08.CO_CUR
                           && tb08.CO_SIT_MAT == "A"
                           && (strP_CO_EMP_REF != 0 ? tb08.CO_EMP == strP_CO_EMP_REF : 0 == 0)
                           && (strP_CO_MODU_CUR != 0 ? tb08.TB44_MODULO.CO_MODU_CUR == strP_CO_MODU_CUR : 0 == 0)
                           && (strP_CO_CUR != 0 ? tb08.CO_CUR == strP_CO_CUR : 0 == 0)
                           && (strP_CO_TUR != 0 ? tb08.CO_TUR == strP_CO_TUR : 0 == 0)

                           select new PlaMedLanc
                           {
                               //Coleta os Códigos necessários
                               coModalidade = tb08.TB44_MODULO.CO_MODU_CUR,
                               coCur = tb08.CO_CUR,
                               coTur = tb08.CO_TUR,
                               coAno = tb08.CO_ANO_MES_MAT,
                               coMat = tb02.CO_MAT,
                               idMat = tb107.ID_MATERIA,
                               OrdImp = tb43.CO_ORDEM_IMPRE,
                               coEmp = tb08.CO_EMP,
                               DiscAgrupadora = tb43.FL_DISCI_AGRUPA,
                               idMatAgrup = tb43.ID_MATER_AGRUP,

                               //Coleta os nomes
                               noMateria = tb107.NO_MATERIA,
                               noTurma = tb129.NO_TURMA,
                               noSerie = tb01.NO_CUR,
                               noModalidade = tb44.DE_MODU_CUR,

                           }).Distinct().ToList();

                res = res.OrderBy(w => w.noModalidade).ThenBy(p => p.noSerie).ThenBy(e => e.noTurma).ThenBy(o => o.OrdImp_Valid).ThenBy(l => l.OrdImpFilhas).ToList();

                if (res.Count == 0)
                    return -1;
                // Seta os dados no DataSource do Relatorio
                bsReport.Clear();

                foreach (var at in res)
                {

                    //Verifica se é disciplina agrupadora, caso seja, coloca todas as colunas, com exceção de Simulados, com ***
                    if (at.DiscAgrupadora == "S")
                    {
                        at.rltAa = at.rltAe = at.rltAg = at.rltAp = at.rltCon = at.rltPro = at.rltSim = at.rltTp1 = at.rltTp2 = at.rltTra = "***";
                    }

                    //Parte responsável por verificar se as notas foram lançadas
                    #region e

                    int anoI = int.Parse(at.coAno);

                    //Quantidade total de alunos na turma
                    int qtTotAlunos = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                       where tb08.CO_EMP == at.coEmp
                                       && tb08.TB44_MODULO.CO_MODU_CUR == at.coModalidade
                                       && tb08.CO_CUR == at.coCur
                                       && tb08.CO_TUR == at.coTur
                                       && tb08.CO_ANO_MES_MAT == at.coAno
                                       && tb08.CO_SIT_MAT == "A"
                                       select new { tb08.CO_ALU, tb08.CO_CUR, tb08.CO_ANO_MES_MAT }).Count();

                    //Verifica as notas bimestrais para a turma em contexto
                    #region Nota do Trimestre
                    int qtLancMedia = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                       join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb079.CO_ALU equals tb08.CO_ALU
                                       where tb079.CO_MODU_CUR == at.coModalidade && tb079.CO_CUR == at.coCur && tb079.CO_TUR == at.coTur
                                       && tb08.CO_EMP == tb079.CO_EMP && tb08.CO_CUR == tb079.CO_CUR && tb08.CO_TUR == tb079.CO_TUR
                                       && tb079.CO_ANO_REF == at.coAno
                                       && tb079.CO_EMP == at.coEmp
                                       && tb079.CO_MODU_CUR == at.coModalidade
                                       && tb079.CO_CUR == at.coCur
                                       && tb079.CO_MAT == at.coMat
                                       && tb079.CO_TUR == at.coTur
                                       && tb08.CO_SIT_MAT == "A"
                                       && (strP_CO_BIMESTRE == "T1" ? tb079.VL_NOTA_TRI1 != null : strP_CO_BIMESTRE == "T2" ? tb079.VL_NOTA_TRI2 != null : tb079.VL_NOTA_TRI3 != null)
                                       select new
                                       {
                                           tb079.CO_ALU,
                                           tb079.CO_CUR,
                                           tb079.CO_ANO_REF,
                                       }).Count();

                    //Faz uma lista ampla de atividades
                    var qtAluAtiv = (from tb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                                     join tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros() on tb49.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV equals tb273.ID_TIPO_ATIV
                                     join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb49.TB07_ALUNO.CO_ALU equals tb08.CO_ALU
                                     where tb49.TB01_CURSO.CO_CUR == at.coCur
                                     && tb08.CO_EMP == at.coEmp
                                     && tb08.TB44_MODULO.CO_MODU_CUR == at.coModalidade
                                     && tb08.CO_CUR == at.coCur
                                     && tb08.CO_TUR == at.coTur
                                     && tb08.CO_ANO_MES_MAT == at.coAno
                                     && tb08.CO_SIT_MAT == "A"
                                     && tb49.CO_ANO == anoI
                                     && tb49.CO_BIMESTRE == strP_CO_BIMESTRE
                                     && tb49.TB107_CADMATERIAS.ID_MATERIA == at.idMat
                                     select new
                                     {
                                         tb49.CO_TIPO_ATIV,
                                         tb49.ID_NOTA_ATIV,
                                         tb273.CO_SIGLA_ATIV,
                                     }).ToList();

                    int aa = 0;
                    int ae = 0;
                    int ag = 0;
                    int ap = 0;
                    int con = 0;
                    int pro = 0;
                    int sim = 0;
                    int tp1 = 0;
                    int tp2 = 0;
                    int tra = 0;

                    foreach (var l in qtAluAtiv)
                    {
                        //Incrementa os tipos de avaliação no contador
                        if (l.CO_SIGLA_ATIV == "AA") { aa++; }
                        if (l.CO_SIGLA_ATIV == "AE") { ae++; }
                        if (l.CO_SIGLA_ATIV == "AG") { ag++; }
                        if (l.CO_SIGLA_ATIV == "AP") { ap++; }
                        if (l.CO_SIGLA_ATIV == "CT") { con++; }
                        if (l.CO_SIGLA_ATIV == "PJ") { pro++; }
                        if (l.CO_SIGLA_ATIV == "SI") { sim++; }
                        if (l.CO_SIGLA_ATIV == "TB") { tra++; }

                        //Se for prova, verifica se é nota 1 ou 2 e incrementa o contador
                        if (l.CO_SIGLA_ATIV == "PR")
                        {
                            if (l.CO_TIPO_ATIV == "1")
                            {
                                tp1++;
                            }
                            if (l.CO_TIPO_ATIV == "2")
                            {
                                tp2++;
                            }
                        }
                    }

                    ////Trata os campos correspondentes
                    at.rltAa = (at.DiscAgrupadora != "S" ? (aa == 0 ? "SL" : aa == qtTotAlunos ? "OK" : aa < qtTotAlunos ? "PL" : " - ") : "***");
                    at.rltAe = (at.DiscAgrupadora != "S" ? (ae == 0 ? "SL" : ae == qtTotAlunos ? "OK" : ae < qtTotAlunos ? "PL" : " - ") : "***");
                    at.rltAg = (at.DiscAgrupadora != "S" ? (ag == 0 ? "SL" : ag == qtTotAlunos ? "OK" : ag < qtTotAlunos ? "PL" : " - ") : "***");
                    at.rltAp = (at.DiscAgrupadora != "S" ? (ap == 0 ? "SL" : ap == qtTotAlunos ? "OK" : ap < qtTotAlunos ? "PL" : " - ") : "***");
                    at.rltCon = (at.DiscAgrupadora != "S" ? (con == 0 ? "SL" : con == qtTotAlunos ? "OK" : con < qtTotAlunos ? "PL" : " - ") : "***");
                    at.rltPro = (at.DiscAgrupadora != "S" ? (pro == 0 ? "SL" : pro == qtTotAlunos ? "OK" : pro < qtTotAlunos ? "PL" : " - ") : "***");
                    at.rltSim = (at.DiscAgrupadora != "S" ? (sim == 0 ? "SL" : sim == qtTotAlunos ? "OK" : sim < qtTotAlunos ? "PL" : " - ") : "***");
                    at.rltTp1 = (at.DiscAgrupadora != "S" ? (tp1 == 0 ? "SL" : tp1 == qtTotAlunos ? "OK" : tp1 < qtTotAlunos ? "PL" : " - ") : "***");
                    at.rltTp2 = (at.DiscAgrupadora != "S" ? (tp2 == 0 ? "SL" : tp2 == qtTotAlunos ? "OK" : tp2 < qtTotAlunos ? "PL" : " - ") : "***");
                    at.rltTra = (at.DiscAgrupadora != "S" ? (tra == 0 ? "SL" : tra == qtTotAlunos ? "OK" : tra < qtTotAlunos ? "PL" : " - ") : "***");
                    at.md = (qtLancMedia == 0 ? "SL" : qtLancMedia == qtTotAlunos ? "OK" : qtLancMedia < qtTotAlunos ? "PL" : " - ");

                    #endregion

                    #endregion

                    bsReport.Add(at);
                }

                return 1;

            }

            catch { return 0; }
        }

        #endregion

        #region Classe Boletim Aluno
        public class PlaMedLanc
        {
            //Informações gerais
            public string noMateria { get; set; }
            public string noTurma { get; set; }
            public string noSerie { get; set; }
            public string noModalidade { get; set; }
            public string concatAgrup
            {
                get
                {
                    return this.noSerie.ToUpper() + " - " + this.noTurma.ToUpper();
                }
            }

            //Códigos importantes
            public int coEmp { get; set; }
            public int coModalidade { get; set; }
            public int coCur { get; set; }
            public int? coTur { get; set; }
            public string coAno { get; set; }
            public int idMat { get; set; }
            public int? idMatAgrup { get; set; }
            public int coMat { get; set; }
            public int? OrdImp { get; set; }
            public int OrdImp_Valid
            {
                get
                {
                    if (this.idMatAgrup.HasValue)
                    {
                        int? ordIm = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                                      where tb43.CO_ANO_GRADE == this.coAno
                                      && tb43.TB44_MODULO.CO_MODU_CUR == this.coModalidade
                                      && tb43.CO_CUR == this.coCur
                                      && tb43.CO_MAT == this.idMatAgrup
                                      select new { tb43.CO_ORDEM_IMPRE }).FirstOrDefault().CO_ORDEM_IMPRE;

                        return ordIm ?? 40;
                    }
                    else
                    {
                        return this.OrdImp ?? 40;
                    }
                }
            }
            public int OrdImpFilhas
            {
                get
                {
                    //Serve para ordenar as disciplinas filhas das agrupadores em ordem alfabética sem perder a ordem das demais
                    if (this.idMatAgrup.HasValue)
                    {
                        var res = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                                   join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                                   join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                   where tb43.TB44_MODULO.CO_MODU_CUR == this.coModalidade
                                   && tb43.CO_CUR == this.coCur
                                   && tb43.ID_MATER_AGRUP == this.idMatAgrup.Value
                                   && tb43.CO_ANO_GRADE == this.coAno
                                   select new
                                   {
                                       tb107.NO_MATERIA,
                                       tb43.CO_MAT,
                                   }).OrderBy(o => o.NO_MATERIA).ToList();

                        int posicao = 0;
                        foreach (var li in res)
                        {
                            posicao++;
                            if (li.CO_MAT == this.coMat)
                                break;
                        }

                        return posicao;
                    }
                    else
                    {
                        if (this.DiscAgrupadora == "S")
                            return 0;
                        else
                            return this.OrdImp_Valid;
                    }
                }
            }
            public string DiscAgrupadora { get; set; }
            public string rltLeg1
            {
                get
                {
                    return "A.A (Atividade Avaliativa)  -  A.E (Atividade Específica)  -  " +
                    "A.G (Atividade Globalizada)  -  A.P (Atividade Prática)  -  CON (Conceito)  -  PRO (Projeto)  -  " +
                    "SIM (Simulado)  -  TP1 (Teste/Prova 1)  -  TP2 (Teste/Prova 2)  -  TRA (Trabalho) - MD (Média). \n";

                }
            }
            public string rltLeg2
            {
                get
                {
                    return "SL (Sem Lançamento)  -  PL (Pendente de Lançamento)  -  OK (Todos os Alunos Lançados).";

                }
            }


            //Nota de avaliações
            public string rltAa { get; set; }
            public string rltAe { get; set; }
            public string rltAg { get; set; }
            public string rltAp { get; set; }
            public string rltCon { get; set; }
            public string rltPro { get; set; }
            public string rltSim { get; set; }
            public string rltTp1 { get; set; }
            public string rltTp2 { get; set; }
            public string rltTra { get; set; }
            public string md { get; set; }
        }

        #endregion
    }
}
