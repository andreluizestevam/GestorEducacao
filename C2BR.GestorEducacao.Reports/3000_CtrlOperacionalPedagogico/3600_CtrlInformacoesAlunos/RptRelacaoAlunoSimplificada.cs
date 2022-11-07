using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using C2BR.GestorEducacao.Reports.Helper;
using System.Text.RegularExpressions;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos
{
    public partial class RptRelacaoAlunoSimplificada : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptRelacaoAlunoSimplificada()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              int codInst,
                              string infos,
                              int strP_CO_EMP_REF,
                              string strP_CO_ANO_MES_MAT,
                              int strP_CO_MODU_CUR,
                              int strP_CO_CUR,
                              int strP_CO_TUR,
                              string ordem)
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(codInst);

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query C

                List<AlunoSimplificada> listAlunos = new List<AlunoSimplificada>();
                listAlunos =
                      (
                          from mat in ctx.TB08_MATRCUR
                          join emp in ctx.TB25_EMPRESA on mat.CO_EMP equals emp.CO_EMP
                          join alu in ctx.TB07_ALUNO on mat.CO_ALU equals alu.CO_ALU
                          join cid in ctx.TB904_CIDADE on alu.TB905_BAIRRO.CO_CIDADE equals cid.CO_CIDADE
                          join cur in ctx.TB01_CURSO on mat.CO_CUR equals cur.CO_CUR
                          join tur in ctx.TB06_TURMAS on mat.CO_TUR equals tur.CO_TUR
                          join ctur in ctx.TB129_CADTURMAS on tur.CO_TUR equals ctur.CO_TUR
                          join empC in ctx.TB25_EMPRESA on mat.CO_EMP_UNID_CONT equals empC.CO_EMP
                          join mod in ctx.TB44_MODULO on cur.CO_MODU_CUR equals mod.CO_MODU_CUR

                          where (strP_CO_EMP_REF != 0 ? strP_CO_EMP_REF == mat.CO_EMP_UNID_CONT : strP_CO_EMP_REF == 0) &&
                                (mat.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT) && (mat.CO_ALU != null)
                                 && (strP_CO_MODU_CUR != 0 ? mod.CO_MODU_CUR == strP_CO_MODU_CUR : strP_CO_MODU_CUR == 0) &&
                                (strP_CO_CUR != 0 ? cur.CO_CUR == strP_CO_CUR : strP_CO_CUR == 0) &&
                                (strP_CO_TUR != 0 ? tur.CO_TUR == strP_CO_TUR : strP_CO_TUR == 0) && (mat.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT)
                                && (alu.CO_EMP == codInst)

                          select new AlunoSimplificada
                          {
                              Unidade = emp.NO_FANTAS_EMP,
                              Ano = strP_CO_ANO_MES_MAT,
                              Nire = alu.NU_NIRE,
                              Nome = alu.NO_ALU.ToUpper(),
                              Modulo = mat.TB44_MODULO.DE_MODU_CUR,
                              Serie = cur.NO_CUR,
                              Turma = ctur.CO_SIGLA_TURMA,
                              ModulosTodos = strP_CO_MODU_CUR,
                              SeriesTodos = strP_CO_CUR,
                              TurmasTodos = strP_CO_TUR,
                              UnidadeTodos = strP_CO_EMP_REF
                            
                             
                          }

                  ).OrderBy(a => a.Nome).Distinct().ToList();

                //var res = lst.ToList();



                #endregion

                // Erro: não encontrou registros
                if (listAlunos.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                List<AlunoSimplificada> lista = new List<AlunoSimplificada>();
                switch (ordem)
                {
                    case "nire":
                        lista = listAlunos.OrderBy(o => o.Nire).ToList();
                        break;
                    case "nome":
                    default:
                        lista = listAlunos.OrderBy(o => o.Nome).ToList();
                        break;
                }

                foreach (var at in lista)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }
        #endregion


        #region Classe Alunos Simplificada

        public class AlunoSimplificada
        {
            public string Ano { get; set; }
            public string Unidade { get; set; }
            public string UnidContr { get; set; }
            public int Nire { get; set; }
            public string Nome { get; set; }
            public string Modulo { get; set; }
            public string Serie { get; set; }
            public string Turma { get; set; }

            public int UnidadeTodos { get; set; }
            public string parametros
            {
                get
                {
                    String unidade = this.Unidade;
                    if (this.UnidadeTodos == 0)
                    {
                        unidade = "Todas";
                    }


                    return "(Unidade: " + unidade + " - Ano de Referência: " + this.Ano + ")";
                }
            }

            public int ModulosTodos { get; set; }
            public int SeriesTodos { get; set; }
            public int TurmasTodos { get; set; }

            public string parametros2
            {
                get
                {


                    String modulo = this.Modulo;
                    String serie = this.Serie;
                    String turma = this.Turma;
                    if (this.ModulosTodos == 0)
                    {
                         modulo = "Todas";
                    }
                     if (this.SeriesTodos == 0)
                    {
                        serie = "Todas";

                    }
                     if (this.TurmasTodos == 0)
                    {
                        turma = "Todas";
                    }


                    string retorno;

                    retorno = "Modalidade: " + modulo + " - Série: " + serie + " - " + " Turma: " + turma;
                    return retorno.ToUpper();
                }
            }




            public string nireDesc
            {
                get
                {
                    string descNire = this.Nire.ToString();
                    if (descNire.Length < 9)
                    {
                        while (descNire.Length < 9)
                        {
                            descNire = "0" + descNire;
                        }
                        return descNire;
                    }
                    else
                    {
                        return descNire;
                    }
                }
            }



            public string NomeDesc
            {
                get
                {
                    if (this.Nome.Length > 36)
                    {
                        var NomeRetorno = this.Nome.Substring(0, 33) + "...";
                        return NomeRetorno;
                    }
                    else
                    {

                        return this.Nome;
                    }
                }
            }



        }
        #endregion

    }

}
