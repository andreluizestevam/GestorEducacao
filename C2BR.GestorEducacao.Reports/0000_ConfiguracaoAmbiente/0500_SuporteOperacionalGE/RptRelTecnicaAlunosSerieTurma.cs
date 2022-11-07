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


namespace C2BR.GestorEducacao.Reports._0000_ConfiguracaoAmbiente._0500_SuporteOperacionalGE
{
    public partial class RptRelTecnicaAlunosSerieTurma : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptRelTecnicaAlunosSerieTurma()
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
                              int strP_CO_TUR)
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

                List<AlunosPorSerieTurma> listAlunos = new List<AlunosPorSerieTurma>();

                listAlunos =
                      (
                          from mat in ctx.TB08_MATRCUR
                          join alu in ctx.TB07_ALUNO on mat.CO_ALU equals alu.CO_ALU
                          join cid in ctx.TB904_CIDADE on alu.TB905_BAIRRO.CO_CIDADE equals cid.CO_CIDADE
                          join cur in ctx.TB01_CURSO on mat.CO_CUR equals cur.CO_CUR
                          join tur in ctx.TB06_TURMAS on mat.CO_TUR equals tur.CO_TUR
                          join ctur in ctx.TB129_CADTURMAS on tur.CO_TUR equals ctur.CO_TUR
                          join emp in ctx.TB25_EMPRESA on mat.CO_EMP_UNID_CONT equals emp.CO_EMP
                          join mod in ctx.TB44_MODULO on cur.CO_MODU_CUR equals mod.CO_MODU_CUR

                          where (strP_CO_EMP_REF != 0 ? strP_CO_EMP_REF == mat.CO_EMP_UNID_CONT : strP_CO_EMP_REF == 0) &&
                                (mat.CO_SIT_MAT == "A") &&
                                (mat.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT) && (mat.CO_ALU != null)
                                 && (strP_CO_MODU_CUR != 0 ? mod.CO_MODU_CUR == strP_CO_MODU_CUR : strP_CO_MODU_CUR == 0) &&
                                (strP_CO_CUR != 0 ? cur.CO_CUR == strP_CO_CUR : strP_CO_CUR == 0) &&
                                (strP_CO_TUR != 0 ? tur.CO_TUR == strP_CO_TUR : strP_CO_TUR == 0) && (mat.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT)
                                && (alu.CO_EMP == codInst)

                          select new AlunosPorSerieTurma
                          {
                              coEmp = emp.CO_EMP,
                              Unidade = emp.sigla,
                              Nire = alu.NU_NIRE,
                              Nome = alu.NO_ALU.ToUpper(),
                              Responsavel = alu.TB108_RESPONSAVEL.NO_RESP.ToUpper(),
                              CpfResp = alu.TB108_RESPONSAVEL.NU_CPF_RESP,
                              Modulo = mat.TB44_MODULO.DE_MODU_CUR,
                              Serie = cur.NO_CUR,
                              coSer = mat.CO_CUR,
                              Turma = ctur.CO_SIGLA_TURMA,
                              coTur = mat.CO_TUR,
                              DtMat = mat.DT_CAD_MAT,
                              coAlu = alu.CO_ALU,
                          }

                  ).OrderBy(a => a.Nome).Distinct().ToList();

                //var res = lst.ToList();



                #endregion

                // Erro: não encontrou registros
                if (listAlunos.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (var at in listAlunos)
                    bsReport.Add(at);

                return 1;
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region Classe AlunosPorSerieTurma

        public class AlunosPorSerieTurma
        {
            public int Ano { get; set; }
            public int coEmp { get; set; }
            public int? coSer { get; set; }
            public int? coTur { get; set; }
            public string Unidade { get; set; }
            public DateTime DtMat { get; set; }
            public int Nire { get; set; }
            public string Nome { get; set; }
            public int coAlu { get; set; }
            public string Responsavel { get; set; }
            public string CpfResp { get; set; }
            public string Modulo { get; set; }
            public string Serie { get; set; }
            public string Turma { get; set; }

            public String DtMatDesc {
                get
                {
                    string retorno;
                    if(this.DtMat != null)
                    {
                        retorno = this.DtMat.ToString("dd/MM/yyyy");
                    }else{
                        retorno = "-";
                    }
                    return retorno;
                }
            }

            public string CpfRespDesc
            {
                get
                {
                    string retorno;
                    if (this.CpfResp != null)
                    {
                        retorno = this.CpfResp.Insert(3, ".");
                        retorno = retorno.Insert(7, ".");
                        retorno = retorno.Insert(11, "-");
                    }
                    else
                    {
                        retorno = "-";

                    }
                    return retorno;
                }
            }

            public string parametros
            {
                get
                {
                    return "(Unidade: " + this.Unidade + " - Ano de Referência: " + this.Ano + ")";
                }
            }

            public string parametros2
            {
                get
                {
                    string retorno;
                    retorno = "Modalidade: " + this.Modulo + " - Série: " + this.Serie + " - " + " Turma: " + this.Turma;
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

            public string RespDesc
            {
                get
                {
                    if (this.Responsavel == null)
                    {
                        this.Responsavel = "";
                    }
                    if (this.Responsavel.Length > 37)
                    {
                        var NomeRetorno = this.Responsavel.Substring(0, 33) + "...";
                        return NomeRetorno;
                    }
                    else
                    {

                        return this.Responsavel;
                    }
                }
            }

        }

        #endregion

    }
}
