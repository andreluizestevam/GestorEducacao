using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Text.RegularExpressions;
using C2BR.GestorEducacao.Reports.Helper;

namespace C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5200_CtrlReceitas
{
    public partial class RptRelacaoAlunosMensalidades : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptRelacaoAlunosMensalidades()
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
                              int strP_CO_HISTORICO,
                              int strP_CO_TIPO_SOLI)
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

                List<AlunosMensalidades> listAlunos = new List<AlunosMensalidades>();


                listAlunos =
                      (
                          from mat in ctx.TB08_MATRCUR
                          join alu in ctx.TB07_ALUNO on mat.CO_ALU equals alu.CO_ALU
                          join cur in ctx.TB01_CURSO on mat.CO_CUR equals cur.CO_CUR
                          join tur in ctx.TB06_TURMAS on mat.CO_TUR equals tur.CO_TUR
                          join ctur in ctx.TB129_CADTURMAS on tur.CO_TUR equals ctur.CO_TUR
                          join emp in ctx.TB25_EMPRESA on mat.CO_EMP equals emp.CO_EMP
                          join mod in ctx.TB44_MODULO on cur.CO_MODU_CUR equals mod.CO_MODU_CUR
                          //join cta in ctx.TB47_CTA_RECEB on alu.CO_ALU equals cta.CO_ALU

                          where (strP_CO_EMP_REF != 0 ? strP_CO_EMP_REF == mat.CO_EMP_UNID_CONT : strP_CO_EMP_REF == 0)
                             && (mat.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT)
                             && (mat.CO_ALU != null)
                             && (strP_CO_MODU_CUR != 0 ? mod.CO_MODU_CUR == strP_CO_MODU_CUR : strP_CO_MODU_CUR == 0)
                             && (strP_CO_CUR != 0 ? cur.CO_CUR == strP_CO_CUR : strP_CO_CUR == 0)
                             && (strP_CO_TUR != 0 ? tur.CO_TUR == strP_CO_TUR : strP_CO_TUR == 0)
                             && (mat.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT)
                             && (alu.CO_EMP == codInst)

                          select new AlunosMensalidades
                          {
                              Codigo = alu.CO_ALU,
                              Unidade = emp.NO_FANTAS_EMP,
                              Ano = strP_CO_ANO_MES_MAT,
                              Nire = alu.NU_NIRE,
                              Nome = alu.NO_ALU.ToUpper(),
                              Modulo = mat.TB44_MODULO.DE_MODU_CUR,
                              Serie = cur.NO_CUR,
                              Turma = ctur.CO_SIGLA_TURMA,
                              Responsavel = alu.TB108_RESPONSAVEL.NO_RESP,
                              TelResp = alu.TB108_RESPONSAVEL.NU_TELE_RESI_RESP,
                              CelResp = alu.TB108_RESPONSAVEL.NU_TELE_CELU_RESP
                          }

                  ).OrderBy(a => a.Nome).Distinct().ToList();

                // Erro: não encontrou registros
                if (listAlunos.Count == 0)
                    return -1;

                List<AlunosMensalidades> ListaAlunosMensalidades = new List<AlunosMensalidades>();
                List<TB47_CTA_RECEB> listMensalidades = new List<TB47_CTA_RECEB>();
                List<VW47_CTA_RECEB> lstView47 = new List<VW47_CTA_RECEB>();

                var lst = listAlunos.Select(s => s.Codigo).ToList();

                if (strP_CO_HISTORICO != 0 && strP_CO_HISTORICO != -1 && (strP_CO_TIPO_SOLI == 0 || strP_CO_TIPO_SOLI == -1))
                {
                    listMensalidades = ctx.TB47_CTA_RECEB.Where(t => t.CO_ALU != null && lst.Contains(t.CO_ALU.Value) && t.TB39_HISTORICO.CO_HISTORICO == strP_CO_HISTORICO).ToList();
                }
                else if (strP_CO_TIPO_SOLI != 0 && strP_CO_TIPO_SOLI != -1 && (strP_CO_HISTORICO == 0 || strP_CO_HISTORICO == -1))
                {
                    var lstSolic = ctx.TB65_HIST_SOLICIT.Where(w => w.CO_TIPO_SOLI == strP_CO_TIPO_SOLI).Select(s => s.NU_DOC_RECEB_SOLIC).ToList();

                    listMensalidades = ctx.TB47_CTA_RECEB.Where(t => t.CO_ALU != null && lst.Contains(t.CO_ALU.Value) && lstSolic.Contains(t.NU_DOC)).ToList();
                }
                else
                {
                    listMensalidades = ctx.TB47_CTA_RECEB.Where(t => t.CO_ALU != null && lst.Contains(t.CO_ALU.Value)).ToList();
                }
                foreach (var item in listAlunos)
                {
                    AlunosMensalidades aluno = new AlunosMensalidades();
                    aluno.Codigo = item.Codigo;
                    aluno.Unidade = item.Unidade;
                    aluno.Ano = item.Ano;
                    aluno.Nire = item.Nire;
                    aluno.Nome = item.Nome;
                    aluno.Modulo = item.Modulo;
                    aluno.Serie = item.Serie;
                    aluno.Turma = item.Turma;
                    aluno.Responsavel = item.Responsavel;
                    aluno.TelResp = item.TelResp;
                    aluno.CelResp = item.CelResp;
                    aluno.NomeHistorico = (strP_CO_HISTORICO != 0 && strP_CO_HISTORICO != -1) ? TB39_HISTORICO.RetornaPelaChavePrimaria(strP_CO_HISTORICO).DE_HISTORICO : "Todos";

                    if (strP_CO_HISTORICO == -1)
                        aluno.NomeHistorico = "**";
                    aluno.NomeTipoSolicitacao = (strP_CO_TIPO_SOLI != 0 && strP_CO_TIPO_SOLI != -1) ? TB66_TIPO_SOLIC.RetornaPelaChavePrimaria(strP_CO_TIPO_SOLI).NO_TIPO_SOLI : "Todos";
                    
                    if (strP_CO_TIPO_SOLI == -1)
                        aluno.NomeTipoSolicitacao = "**";
                    
                    var mensalidades = listMensalidades.Where(f => f.CO_ALU == item.Codigo && f.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT).ToList();

                    if (mensalidades.Count() > 0)
                    {
                        aluno.ValorMensalidade = mensalidades.First(w => w.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT).VR_PAR_DOC;

                        aluno.ValorTotalPago = mensalidades.Where(w => w.VR_PAG.HasValue && w.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT).Sum(s => s.VR_PAG.Value);

                        var total = mensalidades.Where(w => w.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT).ToList();

                        decimal totalDec = new decimal(0);

                        if (total != null)
                            totalDec = total.Sum(s => s.VR_PAR_DOC);

                        aluno.ValorTotalAberto = totalDec - aluno.ValorTotalPago;

                        var janeiro = mensalidades.Where(f => f.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT && f.DT_VEN_DOC.Month == 1).ToList();
                        aluno.ValorJaneiro = janeiro != null ? janeiro.Sum(s => s.VR_PAG) : new Nullable<decimal>();

                        var fevereiro = mensalidades.Where(f => f.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT && f.DT_VEN_DOC.Month == 2).ToList();
                        aluno.ValorFevereiro = fevereiro != null ? fevereiro.Sum(s => s.VR_PAG) : new Nullable<decimal>();

                        var marco = mensalidades.Where(f => f.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT && f.DT_VEN_DOC.Month == 3).ToList();
                        aluno.ValorMarco = marco != null ? marco.Sum(s => s.VR_PAG) : new Nullable<decimal>();

                        var abril = mensalidades.Where(f => f.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT && f.DT_VEN_DOC.Month == 4).ToList();
                        aluno.ValorAbril = abril != null ? abril.Sum(s => s.VR_PAG) : new Nullable<decimal>();

                        var maio = mensalidades.Where(f => f.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT && f.DT_VEN_DOC.Month == 5).ToList();
                        aluno.ValorMaio = maio != null ? maio.Sum(s => s.VR_PAG) : new Nullable<decimal>();

                        var junho = mensalidades.Where(f => f.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT && f.DT_VEN_DOC.Month == 6).ToList();
                        aluno.ValorJunho = junho != null ? junho.Sum(s => s.VR_PAG) : new Nullable<decimal>();

                        var julho = mensalidades.Where(f => f.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT && f.DT_VEN_DOC.Month == 7).ToList();
                        aluno.ValorJulho = julho != null ? julho.Sum(s => s.VR_PAG) : new Nullable<decimal>();

                        var agosto = mensalidades.Where(f => f.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT && f.DT_VEN_DOC.Month == 8).ToList();
                        aluno.ValorAgosto = agosto != null ? agosto.Sum(s => s.VR_PAG) : new Nullable<decimal>();

                        var setembro = mensalidades.Where(f => f.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT && f.DT_VEN_DOC.Month == 9).ToList();
                        aluno.ValorSetembro = setembro != null ? setembro.Sum(s => s.VR_PAG) : new Nullable<decimal>();

                        var outubro = mensalidades.Where(f => f.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT && f.DT_VEN_DOC.Month == 10).ToList();
                        aluno.ValorOutubro = outubro != null ? outubro.Sum(s => s.VR_PAG) : new Nullable<decimal>();

                        var novembro = mensalidades.Where(f => f.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT && f.DT_VEN_DOC.Month == 11).ToList();
                        aluno.ValorNovembro = novembro != null ? novembro.Sum(s => s.VR_PAG) : new Nullable<decimal>();

                        var dezembro = mensalidades.Where(f => f.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT && f.DT_VEN_DOC.Month == 12).ToList();
                        aluno.ValorDezembro = dezembro != null ? dezembro.Sum(s => s.VR_PAG) : new Nullable<decimal>();

                    }

                    ListaAlunosMensalidades.Add(aluno);
                }

                #endregion

                // Erro: não encontrou registros
                if (ListaAlunosMensalidades.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (var at in ListaAlunosMensalidades)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe AlunosPorSerieTurma

        public class AlunosMensalidades
        {

            public string Ano { get; set; }
            public string Unidade { get; set; }
            public string UnidContr { get; set; }
            public DateTime DtNasc { get; set; }
            public int Nire { get; set; }
            public string Nome { get; set; }
            public int Codigo { get; set; }
            public string Sexo { get; set; }
            public string Deficiencia { get; set; }
            public string Cidade { get; set; }
            public string Bairro { get; set; }
            public string Responsavel { get; set; }
            public string CPFResp { get; set; }
            public string TipoResp { get; set; }
            public string TelResp { get; set; }
            public string CelResp { get; set; }
            public string Modulo { get; set; }
            public string Serie { get; set; }
            public string Turma { get; set; }
            public string Situacao { get; set; }
            public string NomeHistorico { get; set; }
            public string NomeTipoSolicitacao { get; set; }

            public string TelRespDesc
            {
                get
                {
                    if (!String.IsNullOrEmpty(this.TelResp))
                    {
                        var telRetorno = String.Format("({0}) {1}-{2}", this.TelResp.Substring(0, 2).ToString(),
                                                                       this.TelResp.Substring(2, 4).ToString(),
                                                                       this.TelResp.Substring(6, 4).ToString());
                        return telRetorno;
                    }
                    else
                    {
                        return "*";
                    }
                }
            }
            public string CelRespDesc
            {
                get
                {
                    if (!String.IsNullOrEmpty(this.CelResp))
                    {
                        var telRetorno = String.Format("({0}) {1}-{2}", this.CelResp.Substring(0, 2).ToString(),
                                                                       this.CelResp.Substring(2, 4).ToString(),
                                                                       this.CelResp.Substring(6, 4).ToString());
                        return telRetorno;
                    }
                    else
                    {
                        return "*";
                    }
                }
            }

            public string ResponsavelDesc
            {
                get
                {
                    string _responsavel = "*";

                    if (!string.IsNullOrEmpty(this.Responsavel))
                        _responsavel = this.Responsavel;

                    string responsavelDesc = "( " + _responsavel.ToUpper() + " - " + this.TelRespDesc + " / " + this.CelRespDesc + " )";
                    return responsavelDesc;
                }
            }
            public decimal? ValorTotalPago { get; set; }
            public string ValorTotalPagoDesc
            {
                get
                {
                    if (this.ValorTotalPago != null && this.ValorTotalPago > 0)
                    {
                        return this.ValorTotalPago.Value.ToString("C");
                    }
                    else
                    {
                        return " - ";
                    }
                }
            }
            public decimal? ValorTotalAberto { get; set; }
            public string ValorTotalAbertoDesc
            {
                get
                {
                    if (this.ValorTotalAberto != null)
                    {
                        return this.ValorTotalAberto.Value.ToString("C");
                    }
                    else
                    {
                        return " - ";
                    }
                }
            }
            public decimal? ValorMensalidade { get; set; }
            public string ValorMensalidadeDesc
            {
                get
                {
                    if (this.ValorMensalidade != null)
                    {
                        return this.ValorMensalidade.ToString();
                    }
                    else
                    {
                        return " - ";
                    }
                }
            }
            public decimal? ValorJaneiro { get; set; }
            public string ValorJaneiroDesc
            {
                get
                {
                    if (this.ValorJaneiro != null)
                    {
                        return this.ValorJaneiro.ToString();
                    }
                    else
                    {
                        return " - ";
                    }
                }
            }

            public decimal? ValorFevereiro { get; set; }
            public string ValorFevereiroDesc
            {
                get
                {
                    if (this.ValorFevereiro != null)
                    {
                        return this.ValorFevereiro.ToString();
                    }
                    else
                    {
                        return " - ";
                    }
                }
            }

            public decimal? ValorMarco { get; set; }
            public string ValorMarcoDesc
            {
                get
                {
                    if (this.ValorMarco != null)
                    {
                        return this.ValorMarco.ToString();
                    }
                    else
                    {
                        return " - ";
                    }
                }
            }

            public decimal? ValorAbril { get; set; }
            public string ValorAbrilDesc
            {
                get
                {
                    if (this.ValorAbril != null)
                    {
                        return this.ValorAbril.ToString();
                    }
                    else
                    {
                        return " - ";
                    }
                }
            }

            public decimal? ValorMaio { get; set; }
            public string ValorMaioDesc
            {
                get
                {
                    if (this.ValorMaio != null)
                    {
                        return this.ValorMaio.ToString();
                    }
                    else
                    {
                        return " - ";
                    }
                }
            }

            public decimal? ValorJunho { get; set; }
            public string ValorJunhoDesc
            {
                get
                {
                    if (this.ValorJunho != null)
                    {
                        return this.ValorJunho.ToString();
                    }
                    else
                    {
                        return " - ";
                    }
                }
            }

            public decimal? ValorJulho { get; set; }
            public string ValorJulhoDesc
            {
                get
                {
                    if (this.ValorJulho != null)
                    {
                        return this.ValorJulho.ToString();
                    }
                    else
                    {
                        return " - ";
                    }
                }
            }

            public decimal? ValorAgosto { get; set; }
            public string ValorAgostoDesc
            {
                get
                {
                    if (this.ValorAgosto != null)
                    {
                        return this.ValorAgosto.ToString();
                    }
                    else
                    {
                        return " - ";
                    }
                }
            }

            public decimal? ValorSetembro { get; set; }
            public string ValorSetembroDesc
            {
                get
                {
                    if (this.ValorSetembro != null)
                    {
                        return this.ValorSetembro.ToString();
                    }
                    else
                    {
                        return " - ";
                    }
                }
            }

            public decimal? ValorOutubro { get; set; }
            public string ValorOutubroDesc
            {
                get
                {
                    if (this.ValorOutubro != null)
                    {
                        return this.ValorOutubro.ToString();
                    }
                    else
                    {
                        return " - ";
                    }
                }
            }

            public decimal? ValorNovembro { get; set; }
            public string ValorNovembroDesc
            {
                get
                {
                    if (this.ValorNovembro != null)
                    {
                        return this.ValorNovembro.ToString();
                    }
                    else
                    {
                        return " - ";
                    }
                }
            }

            public decimal? ValorDezembro { get; set; }
            public string ValorDezembroDesc
            {
                get
                {
                    if (this.ValorDezembro != null)
                    {
                        return this.ValorDezembro.ToString();
                    }
                    else
                    {
                        return " - ";
                    }
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
                    retorno = "Modalidade: " + this.Modulo + " - Série: " + this.Serie + " - " + " Turma: " + this.Turma + " - Histórico:" + this.NomeHistorico + " - Tipo Solicitação:" + this.NomeTipoSolicitacao;
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
    }
}
        #endregion