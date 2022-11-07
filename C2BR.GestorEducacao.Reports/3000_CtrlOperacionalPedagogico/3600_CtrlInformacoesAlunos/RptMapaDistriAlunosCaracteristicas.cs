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
using System.Data;

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos
{
    public partial class RptMapaDistriAlunosCaracteristicas : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptMapaDistriAlunosCaracteristicas()
        {
            InitializeComponent();
        }

        public int InitReport(string parametros,
                              int coEmp,
                              string infos,
                              int strP_CO_EMP_REF,
                              string strP_CO_ANO_MES_MAT,
                              int strP_CO_MODU_CUR,
                              int strP_CO_CUR,
                              string strP_CO_TIPO,
                              string strP_UF,
                              int strP_CID
                              )
        {
            try
            {
                #region Setar o Header e as Labels
                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;
                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;
                // Setar o header do relatorio
                this.BaseInit(header);
                #endregion

                var ctx = GestorEntities.CurrentContext;
                var listaMatriculas = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                       where tb08.CO_ALU != null
                                       && tb08.TB07_ALUNO.CO_ESTA_ALU != null
                                       && tb08.CO_EMP_UNID_CONT == strP_CO_EMP_REF
                                       && tb08.CO_SIT_MAT == "A"
                                  && (strP_CO_MODU_CUR == 0 ? 0==0 : tb08.TB44_MODULO.CO_MODU_CUR == strP_CO_MODU_CUR)
                                  && (strP_CO_CUR == -1 || strP_CO_CUR == 0 ? 0 == 0 : tb08.CO_CUR == strP_CO_CUR)
                                  && (strP_CO_ANO_MES_MAT == "-1" || strP_CO_ANO_MES_MAT == "0" ? 0 == 0 : tb08.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT)
                                  && (strP_UF == "0" ? 0 == 0 : tb08.TB07_ALUNO.CO_ESTA_ALU == strP_UF)
                                  && (strP_CID == 0 ? 0 == 0 : tb08.TB07_ALUNO.TB905_BAIRRO.CO_CIDADE == strP_CID)
                                       select new 
                                    {
                                        coEmp = tb08.CO_EMP,
                                        coMod = tb08.TB44_MODULO.CO_MODU_CUR,
                                        coCur = tb08.CO_CUR,
                                        coAno = tb08.CO_ANO_MES_MAT,
                                        coTur = tb08.CO_TUR,
                                        coBairro = tb08.TB07_ALUNO.TB905_BAIRRO.CO_BAIRRO,
                                        coAlu = tb08.TB07_ALUNO.CO_ALU,
                                        nuLet = tb08.NU_SEM_LET,
                                        tipoReferência = strP_CO_TIPO,
                                        coUf = (tb08.TB07_ALUNO.CO_ESTA_ALU == null ? "0" : tb08.TB07_ALUNO.CO_ESTA_ALU),
                                        coCid = (tb08.TB07_ALUNO.TB905_BAIRRO.CO_CIDADE == null ? 0 : tb08.TB07_ALUNO.TB905_BAIRRO.CO_CIDADE)
                                    }
                                ).DistinctBy(d=>d.coAlu);

                if (listaMatriculas == null || listaMatriculas.Count() <= 0)
                    return -1;
                float larguraTurma = tabelaTurmaTopo.WidthF + 15;
                float larguraLinha = 402.5F;
                switch (strP_CO_TIPO)
                {
                    case "S":
                        listaMatriculas = listaMatriculas.DistinctBy(d => d.coMod);
                        tabelaTurmaTopo.Visible = tabelaTurmaLinha.Visible = true;
                        tabelaLinhaTopo.WidthF = tabelaLinhaLinha.WidthF = larguraLinha;
                        break;
                    case "B":
                        listaMatriculas = listaMatriculas.DistinctBy(d => d.coCid);
                        tabelaTurmaTopo.Visible = tabelaTurmaLinha.Visible = true;
                        tabelaLinhaTopo.WidthF = tabelaLinhaLinha.WidthF = larguraLinha;
                        break;
                    case "A":
                        listaMatriculas = listaMatriculas.DistinctBy(d => d.coAno);
                        tabelaTurmaTopo.Visible = tabelaTurmaLinha.Visible = false;
                        tabelaLinhaTopo.WidthF = tabelaLinhaLinha.WidthF = larguraLinha + larguraTurma;
                        break;
                }
                bsReport.Clear();
                foreach (var linha in listaMatriculas)
                {
                    listaDistribuicao distribuicao;
                    if (listaDistribuicaoValores(out distribuicao
                        , linha.coAlu
                        , linha.coAno
                        , linha.coBairro
                        , linha.coCur
                        , linha.coEmp
                        , linha.coMod
                        , linha.coTur
                        , linha.nuLet
                        , linha.tipoReferência
                        , linha.coUf
                        , (linha.coCid)))
                        bsReport.Add(distribuicao);
                }

                return 1;
            }
            catch(Exception e) 
            { 
                return 0; 
            }
        }

        public Boolean listaDistribuicaoValores(out listaDistribuicao lista
                        , int coAlu
                        , string coAno
                        , int coBairro
                        , int coCur
                        , int coEmp
                        , int coMod
                        , int? coTur
                        , string nuLet
                        , string tipoReferência
                        , string coUf
                        , int coCid
                        )
        {
            lista = new listaDistribuicao();
            if (coAlu != 0)
            {
                #region Valores iniciais
                lista.coAlu = coAlu;
                lista.coAno = coAno;
                lista.coBairro = coBairro;
                lista.coCur = lista.coCur;
                lista.coEmp = coEmp;
                lista.coMod = coMod;
                lista.coTur = coTur;
                lista.tipoReferência = tipoReferência;
                lista.nuLet = nuLet;
                
                #endregion
                #region Listagem
                var matriculas = (from f in TB08_MATRCUR.RetornaTodosRegistros()
                                  where f.CO_ALU != null
                                       && f.TB07_ALUNO.CO_ESTA_ALU != null
                                       && f.CO_EMP_UNID_CONT == coEmp
                                       && f.CO_SIT_MAT == "A"
                                       && (tipoReferência == "S" || tipoReferência == "A" ? (coAno == "-1" ? 0 == 0 : f.CO_ANO_MES_MAT == coAno) : 0 == 0)
                                       && (tipoReferência == "S" ? ((coMod == 0 ? 0 == 0 : f.TB44_MODULO.CO_MODU_CUR == coMod)
                                           && (coCur == -1 ? 0 == 0 : f.CO_CUR == coCur)) : 0 == 0) 
                                           && (tipoReferência == "B" ? ((coUf == "0" ? 0 == 0 : f.TB07_ALUNO.CO_ESTA_ALU == coUf)
                                  && (coCid == 0 ? 0 == 0 : f.TB07_ALUNO.TB905_BAIRRO.CO_CIDADE == coCid)) : 0 == 0)
                                  select f
                                  );
                
                #endregion
                if (matriculas != null && matriculas.Count() > 0)
                {
                    var matriculaUnica = matriculas.Where(f => f.CO_ALU == coAlu).FirstOrDefault();
                    #region Contagem
                    #region Colunas Quantidades
                    lista.qtdAluno = matriculas.Count();
                    lista.qtdPbe = matriculas.Where(f => f.TB07_ALUNO.FLA_BOLSA_ESCOLA != null && f.TB07_ALUNO.FLA_BOLSA_ESCOLA == true).Count();
                    #endregion
                    #region Colunas Sexo
                    lista.sexoF = matriculas.Where(f => f.TB07_ALUNO.CO_SEXO_ALU != null && f.TB07_ALUNO.CO_SEXO_ALU == "F").Count();
                    lista.sexoM = matriculas.Where(f => f.TB07_ALUNO.CO_SEXO_ALU != null && f.TB07_ALUNO.CO_SEXO_ALU == "M").Count();
                    #endregion
                    #region Colunas Turnos
                    var turnos = (from cont in matriculas
                                    join tb06 in TB06_TURMAS.RetornaTodosRegistros() on cont.CO_TUR equals tb06.CO_TUR
                                    select new
                                    {
                                        codigo = tb06.CO_TUR,
                                        periodo = tb06.CO_PERI_TUR
                                    }
                                        );
                    lista.turnoM = turnos.Where(f => f.periodo != null && f.periodo == "M").Count();
                    lista.turnoN = turnos.Where(f => f.periodo != null && f.periodo == "N").Count();
                    lista.turnoT = turnos.Where(f => f.periodo != null && f.periodo == "V").Count();
                    #endregion
                    #region Colunas Etnia
                    lista.etniaAm = matriculas.Where(f => f.TB07_ALUNO.TP_RACA == null && f.TB07_ALUNO.TP_RACA == "A").Count();
                    lista.etniaBr = matriculas.Where(f => f.TB07_ALUNO.TP_RACA == null && f.TB07_ALUNO.TP_RACA == "B").Count();
                    lista.etniaIn = matriculas.Where(f => f.TB07_ALUNO.TP_RACA == null && f.TB07_ALUNO.TP_RACA == "I").Count();
                    lista.etniaNe = matriculas.Where(f => f.TB07_ALUNO.TP_RACA == null && f.TB07_ALUNO.TP_RACA == "N").Count();
                    lista.etniaNi = matriculas.Where(f => f.TB07_ALUNO.TP_RACA == null || f.TB07_ALUNO.TP_RACA == "X").Count();
                    lista.etniaPa = matriculas.Where(f => f.TB07_ALUNO.TP_RACA == null && f.TB07_ALUNO.TP_RACA == "P").Count();
                    #endregion
                    #region Colunas Renda
                    lista.renda10 = matriculas.Where(f => f.TB07_ALUNO.RENDA_FAMILIAR != null && f.TB07_ALUNO.RENDA_FAMILIAR == "10").Count();
                    lista.renda13 = matriculas.Where(f => f.TB07_ALUNO.RENDA_FAMILIAR != null && (f.TB07_ALUNO.RENDA_FAMILIAR == "1" || f.TB07_ALUNO.RENDA_FAMILIAR == "2" || f.TB07_ALUNO.RENDA_FAMILIAR == "3")).Count();
                    lista.renda35 = matriculas.Where(f => f.TB07_ALUNO.RENDA_FAMILIAR != null && (f.TB07_ALUNO.RENDA_FAMILIAR == "3" || f.TB07_ALUNO.RENDA_FAMILIAR == "4" || f.TB07_ALUNO.RENDA_FAMILIAR == "5")).Count();
                    lista.renda5 = matriculas.Where(f => f.TB07_ALUNO.RENDA_FAMILIAR != null && (f.TB07_ALUNO.RENDA_FAMILIAR == "6" || f.TB07_ALUNO.RENDA_FAMILIAR == "7" || f.TB07_ALUNO.RENDA_FAMILIAR == "8" || f.TB07_ALUNO.RENDA_FAMILIAR == "9")).Count();
                    lista.rendaNi = matriculas.Where(f => f.TB07_ALUNO.RENDA_FAMILIAR == null || f.TB07_ALUNO.RENDA_FAMILIAR == "X").Count();
                    lista.rendaSr = matriculas.Where(f => f.TB07_ALUNO.RENDA_FAMILIAR != null && f.TB07_ALUNO.RENDA_FAMILIAR == "0").Count();
                    #endregion
                    #region Colunas Deficiência
                    lista.defiAu = matriculas.Where(f => f.TB07_ALUNO.TP_DEF != null && f.TB07_ALUNO.TP_DEF == "A").Count();
                    lista.defiFi = matriculas.Where(f => f.TB07_ALUNO.TP_DEF != null && f.TB07_ALUNO.TP_DEF == "F").Count();
                    lista.defiMe = matriculas.Where(f => f.TB07_ALUNO.TP_DEF != null && f.TB07_ALUNO.TP_DEF == "M").Count();
                    lista.defiMu = matriculas.Where(f => f.TB07_ALUNO.TP_DEF != null && f.TB07_ALUNO.TP_DEF == "P").Count();
                    lista.defiOu = matriculas.Where(f => f.TB07_ALUNO.TP_DEF != null && f.TB07_ALUNO.TP_DEF == "O").Count();
                    lista.defiSd = matriculas.Where(f => f.TB07_ALUNO.TP_DEF != null && f.TB07_ALUNO.TP_DEF == "N").Count();
                    lista.defiVi = matriculas.Where(f => f.TB07_ALUNO.TP_DEF != null && f.TB07_ALUNO.TP_DEF == "V").Count();
                    #endregion
                    #endregion
                    #region Personalização
                    switch (lista.tipoReferência)
                    {
                        case "S":
                            var serie = TB01_CURSO.RetornaTodosRegistros().Where(f => f.CO_CUR != null && f.CO_CUR == coCur).FirstOrDefault();
                            var turma = TB129_CADTURMAS.RetornaTodosRegistros().Where(f => f.CO_TUR != null && f.CO_TUR == coTur).FirstOrDefault();
                            lista.nomeLinha1 = "SÉRIE";
                            lista.nomeLinha2 = "TURMA";
                            lista.descLinha1 = serie != null ? serie.NO_CUR.ToString() : "";
                            lista.descLinha2 = turma != null ? turma.NO_TURMA.ToString() : "";
                            break;
                        case "B":
                            var bairro = TB905_BAIRRO.RetornaTodosRegistros().Where(f => f.CO_BAIRRO != null && f.CO_BAIRRO == coBairro).FirstOrDefault();
                            var cidade = TB904_CIDADE.RetornaTodosRegistros().Where(f => f.CO_CIDADE  != null && f.CO_CIDADE == coCid).FirstOrDefault();
                            lista.nomeLinha1 = "CIDADE";
                            lista.nomeLinha2 = "BAIRRO";
                            lista.descLinha1 = cidade == null ? "" : cidade.NO_CIDADE.ToString();
                            lista.descLinha2 = bairro == null ? "" : bairro.NO_BAIRRO.ToString();
                            break;
                        case "A":
                            lista.nomeLinha1 = "ANO";
                            lista.nomeLinha2 = "";
                            lista.descLinha1 = matriculaUnica == null ? "" : matriculaUnica.CO_ANO_MES_MAT.ToString();
                            lista.descLinha2 = "";
                            break;
                    }
                    #endregion
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public class listaDistribuicao
        {
            public string nomeLinha1 {get; set;}
            public string descLinha1 { get; set; }
            public string nomeLinha2 {get; set;}
            public string descLinha2 { get; set; }
            public int coEmp { get; set; }
            public int coMod { get; set; }
            public int coCur { get; set; }
            public int? coTur { get; set; }
            public string coAno { get; set; }
            public int? coBairro { get; set; }
            public int coAlu { get; set; }
            public string nuLet{ get; set; }
            public string tipoReferência { get; set; }
            public string coUf { get; set; }
            public int? coCid { get; set; }

            public int? qtdAluno {get; set;}
            public int? qtdPbe {get; set;}
            public int? sexoM {get; set;}
            public int? sexoF {get; set;}
            public int? turnoM {get; set;}
            public int? turnoT {get; set;}
            public int? turnoN {get; set;}
            public int? etniaBr {get; set;}
            public int? etniaNe {get; set;}
            public int? etniaAm {get; set;}
            public int? etniaPa {get; set;}
            public int? etniaIn {get; set;}
            public int? etniaNi {get; set;}
            public int? renda13 {get; set;}
            public int? renda35 { get; set; }
            public int? renda5 { get; set; }
            public int? renda10 { get; set; }
            public int? rendaSr { get; set; }
            public int? rendaNi { get; set; }
            public int? defiSd { get; set; }
            public int? defiAu { get; set; }
            public int? defiVi { get; set; }
            public int? defiFi { get; set; }
            public int? defiMe { get; set; }
            public int? defiMu { get; set; }
            public int? defiOu { get; set; }


            
        }
    }
}
