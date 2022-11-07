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
    public partial class RptStatusLancNotaAtivSupremo : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptStatusLancNotaAtivSupremo()
        {
            InitializeComponent();
        }
        #region Init Report
        //parametros, infos, strP_CO_EMP_REF, strP_CO_ANO_MES_MAT, strP_CO_CUR, strP_CO_MODU_CUR, strP_CO_TUR
        public int InitReport(string parametros,
                                string infos,
                                int strP_CO_EMP_REF,
                                string strP_CO_ANO_MES_MAT,
                                int strP_CO_CUR,
                                int strP_CO_MODU_CUR,
                                int strP_CO_TUR,
                                string strP_CO_BIMESTRE,
                                int coEmp,
            string NomeFuncionalidadeCadastrada
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
                //int coEmp = strP_CO_EMP_REF;

                // Instancia o header do relatorio
                //ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                C2BR.GestorEducacao.Reports.ReportHeader header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(coEmp);

                string bimestre = "";

                switch (strP_CO_BIMESTRE)
                {
                    case "B1":
                        bimestre = "1° Bimestre";
                        break;
                    case "B2":
                        bimestre = "2° Bimestre";
                        break;
                    case "B3":
                        bimestre = "3° Bimestre";
                        break;
                    case "B4":
                        bimestre = "4° Bimestre";
                        break;
                }

                if (NomeFuncionalidadeCadastrada == "")
                {
                    lblTitulo.Text = "PLANILHA DE LANÇAMENTO DE NOTAS - " + bimestre;
                }
                else
                {
                    lblTitulo.Text = NomeFuncionalidadeCadastrada   + bimestre;
                }
                

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

                           select new PlaMedLancSupremo
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

                    ////Verifica se é disciplina agrupadora, caso seja, coloca todas as colunas, com exceção de Simulados, com ***
                    if (at.DiscAgrupadora == "S")
                        at.Av1 = at.Av2 = at.Av3 = at.Av4 = at.Av5 = "***";

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
                    #region Nota do Bimestre
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
                                       && (strP_CO_BIMESTRE == "B1" ? tb079.VL_NOTA_BIM1 != null : strP_CO_BIMESTRE == "B2" ? tb079.VL_NOTA_BIM2 != null : strP_CO_BIMESTRE == "B3" ? tb079.VL_NOTA_BIM3 != null : tb079.VL_NOTA_BIM4 != null)
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
                                         tb49.ID_NOTA_ATIV,
                                         tb273.CO_SIGLA_ATIV,
                                         tb49.CO_REFER_NOTA,

                                     }).ToList();

                    int Av1 = 0;
                    int Av2 = 0;
                    int Av3 = 0;
                    int Av4 = 0;
                    int Av5 = 0;

                    foreach (var l in qtAluAtiv)
                    {
                        if (l.CO_REFER_NOTA == "AV1")
                            Av1++;

                        if (l.CO_REFER_NOTA == "AV2")
                            Av2++;
                        
                        if (l.CO_REFER_NOTA == "AV3")
                            Av3++;
                        
                        if (l.CO_REFER_NOTA == "AV4")

                            Av4++;
                        if (l.CO_REFER_NOTA == "AV5")
                            Av5++;
                    }

                    //Trata os campos correspondentes
                    //Nota 1
                    at.Av1 = (at.DiscAgrupadora != "AV1" ? Av1 == 0 ? "SL" : (Av1 == qtTotAlunos ? "OK" : Av1 < qtTotAlunos ? "PL" : Av1 > qtTotAlunos ? "LD" : " - ") : "***");
                    //Nota 2
                    at.Av2 = (at.DiscAgrupadora != "AV2" ? (Av2 == 0 ? "SL" : Av2 == qtTotAlunos ? "OK" : Av2 < qtTotAlunos ? "PL" : Av2 > qtTotAlunos ? "LD" : " - ") : "***");
                    //Nota 3
                    at.Av3 = (at.DiscAgrupadora != "AV3" ? (Av3 == 0 ? "SL" : Av3 == qtTotAlunos ? "OK" : Av3 < qtTotAlunos ? "PL" : Av3 > qtTotAlunos ? "LD" : " - ") : "***");
                    //Nota 4
                    at.Av4 = (at.DiscAgrupadora != "AV4" ? (Av4 == 0 ? "SL" : Av4 == qtTotAlunos ? "OK" : Av4 < qtTotAlunos ? "PL" : Av4 > qtTotAlunos ? "LD" : " - ") : "***");
                    //Nota 5
                    at.Av5 = (at.DiscAgrupadora != "AV5" ? (Av5 == 0 ? "SL" : Av5 == qtTotAlunos ? "OK" : Av5 < qtTotAlunos ? "PL" : Av5 > qtTotAlunos ? "LD" : " - ") : "***");
                    //Média
                    at.md = (qtLancMedia == 0 ? "SL" : qtLancMedia == qtTotAlunos ? "OK" : qtLancMedia < qtTotAlunos ? "PL" : qtLancMedia > qtTotAlunos ? "LD" : " - ");

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
        public class PlaMedLancSupremo
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


            //Nota de Provas
            public string Av1 { get; set; }
            public string Av2 { get; set; }
            public string Av3 { get; set; }
            public string Av4 { get; set; }
            public string Av5 { get; set; }

            //Nota de Provas
            //public string np1 { get; set; }
            //public string np2 { get; set; }

            ////Nota de atividades
            //public string na1 { get; set; }

            ////Nota de simulados
            //public string ns1 { get; set; }

            public string md { get; set; }
        }
        #endregion
    }
}
