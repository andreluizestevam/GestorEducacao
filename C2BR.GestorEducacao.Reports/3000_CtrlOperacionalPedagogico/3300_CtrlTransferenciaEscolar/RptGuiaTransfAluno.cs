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

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3300_CtrlTransferenciaEscolar
{
    public partial class RptGuiaTransfAluno : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptGuiaTransfAluno()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              string infos,
                              int strP_CO_EMP,
                              int strP_CO_EMP_REF,
                              int strP_CO_MODU_CUR,
                              int strP_CO_CUR,
                              int strP_CO_TUR,
                              string strP_CO_ANO_REF,
                              int strP_CO_ALU)
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(strP_CO_EMP);
                this.VisibleNumeroPage = false;
                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion                

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;


                var tb83 = (from iTb25 in ctx.TB25_EMPRESA
                            join iTb83 in ctx.TB83_PARAMETRO on iTb25.CO_EMP equals iTb83.CO_EMP
                            join diretor in ctx.TB03_COLABOR on iTb83.CO_DIR1 equals diretor.CO_COL into di
                            from x in di.DefaultIfEmpty()
                            join iTb904 in ctx.TB904_CIDADE on iTb25.CO_CIDADE equals iTb904.CO_CIDADE
                            where iTb25.CO_EMP == strP_CO_EMP
                            select new
                            {
                                nomeDiretor = x != null ? x.NO_COL : "",
                                matriculaDiretor = x != null ? x.CO_MAT_COL : "",
                                nomeSecretario = iTb83.TB03_COLABOR != null ? iTb83.TB03_COLABOR.NO_COL : "",
                                matriculaSecretario = iTb83.TB03_COLABOR != null ? iTb83.TB03_COLABOR.CO_MAT_COL : "",
                                cidade = iTb904.NO_CIDADE, UF = iTb904.CO_UF
                            }).FirstOrDefault();

                if (tb83 != null)
                {
                    lblCidadeUFData.Text = tb83.cidade + " - " + tb83.UF + ", " + DateTime.Now.ToString("dd") + " de " + DateTime.Now.ToString("MMMM") + " de " + DateTime.Now.ToString("yyyy");
                    lblNomeDiretor.Text = tb83.nomeDiretor;
                    lblMatrDiretor.Text = "Matrícula: " + (tb83.matriculaDiretor != "" ? tb83.matriculaDiretor.Insert(2, ".").Insert(6, "-") : "XXXXX");
                    lblNomeCoordenador.Text = tb83.nomeSecretario;
                    lblMatrCoordenador.Text = "Matrícula: " + (tb83.matriculaSecretario != "" ? tb83.matriculaSecretario.Insert(2, ".").Insert(6, "-") : "XXXXX");
                }  

                #region Query Lista Médias

                var lst = (from tb08 in ctx.TB08_MATRCUR
                           join tb079 in ctx.TB079_HIST_ALUNO on tb08.CO_CUR equals tb079.CO_CUR
                           join tb01 in ctx.TB01_CURSO on tb08.CO_CUR equals tb01.CO_CUR
                           join tb129 in ctx.TB129_CADTURMAS on tb08.CO_TUR equals tb129.CO_TUR
                           join tb02 in ctx.TB02_MATERIA on tb079.CO_MAT equals tb02.CO_MAT
                           join tb107 in ctx.TB107_CADMATERIAS on tb02.ID_MATERIA equals tb107.ID_MATERIA
                           where tb08.CO_ANO_MES_MAT == tb079.CO_ANO_REF && tb08.CO_ALU == tb079.CO_ALU && tb08.CO_TUR == tb079.CO_TUR
                           && tb08.CO_EMP == strP_CO_EMP_REF
                           && tb08.CO_CUR == strP_CO_CUR
                           && tb08.CO_TUR == strP_CO_TUR
                           && tb08.TB44_MODULO.CO_MODU_CUR == strP_CO_MODU_CUR
                           && tb08.CO_ANO_MES_MAT == strP_CO_ANO_REF
                           && tb08.CO_ALU == strP_CO_ALU
                           select new RelListaMedias
                           {
                               Disciplina = tb107.NO_MATERIA,
                               CoEmp = tb08.CO_EMP,
                               noAlu = tb08.TB07_ALUNO.NO_ALU.ToUpper(),
                               NuNis = tb08.TB07_ALUNO.NU_NIS,                               
                               NuNire = tb08.TB07_ALUNO.NU_NIRE,
                               codigoMatricula = tb08.CO_ALU_CAD,
                               Modalidade = tb08.TB44_MODULO.DE_MODU_CUR,
                               Serie = tb01.NO_CUR,
                               Turma = tb129.CO_SIGLA_TURMA,
                               Ano = tb08.CO_ANO_MES_MAT,
                               DataNasctoAluno = tb08.TB07_ALUNO.DT_NASC_ALU,
                               Nacionalidade = tb08.TB07_ALUNO.CO_NACI_ALU == "B" ? "Brasileira" : "Estrangeira",
                               Naturalidade = tb08.TB07_ALUNO.DE_NATU_ALU != null ? tb08.TB07_ALUNO.DE_NATU_ALU : "XXXXX",
                               UFNaturalidade = tb08.TB07_ALUNO.CO_UF_NATU_ALU != null ? tb08.TB07_ALUNO.CO_UF_NATU_ALU : "XX",
                               Pai = tb08.TB07_ALUNO.NO_PAI_ALU != null ? tb08.TB07_ALUNO.NO_PAI_ALU : "XXXXX",
                               Mae = tb08.TB07_ALUNO.NO_MAE_ALU != null ? tb08.TB07_ALUNO.NO_MAE_ALU : "XXXXX",
                               MediaB1 = tb079.VL_NOTA_BIM1,
                               MediaB2 = tb079.VL_NOTA_BIM2,
                               MediaB3 = tb079.VL_NOTA_BIM3,
                               MediaB4 = tb079.VL_NOTA_BIM4,
                               FaltaB1 = tb079.QT_FALTA_BIM1,
                               FaltaB2 = tb079.QT_FALTA_BIM2,
                               FaltaB3 = tb079.QT_FALTA_BIM3,
                               FaltaB4 = tb079.QT_FALTA_BIM4,
                               MediaFinal = tb079.VL_MEDIA_FINAL,
                               ResultadoMater = tb079.VL_MEDIA_FINAL != null && tb079.CO_STA_APROV_MATERIA != null ? (tb079.CO_STA_APROV_MATERIA == "A" ? "APROVADO" : "REPROVADO") : "PENDENTE",
                               Resultado = tb08.CO_STA_APROV != null ? (tb08.CO_STA_APROV == "A" && (tb08.CO_STA_APROV_FREQ == "A" || tb08.CO_STA_APROV_FREQ == null) ? "APROVADO" : "REPROVADO") : "*****"
                           }).Distinct().OrderBy(r => r.Disciplina);

                // Erro: não encontrou registros
                if (lst.ToList().Count == 0)
                    return -1;

                var res = lst.ToList();

                #endregion

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (RelListaMedias at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Lista Médias

        public class RelListaMedias
        {
            public int CoEmp { get; set; }
            public string noAlu { get; set; }
            public decimal? NuNis { get; set; }
            public int NuNire { get; set; }
            public string codigoMatricula { get; set; }
            public string Modalidade { get; set; }
            public string Serie { get; set; }
            public string Turma { get; set; }
            public string Ano { get; set; }
            public DateTime? DataNasctoAluno { get; set; }
            public string Nacionalidade { get; set; }
            public string Naturalidade { get; set; }
            public string UFNaturalidade { get; set; }
            public string Pai { get; set; }
            public string Mae { get; set; }
            public string ResultadoMater { get; set; }
            public string Resultado { get; set; }
            public string Disciplina { get; set; }
            public decimal? MediaB1 { get; set; }
            public int? FaltaB1 { get; set; }
            public decimal? MediaB2 { get; set; }
            public int? FaltaB2 { get; set; }
            public decimal? MediaB3 { get; set; }
            public int? FaltaB3 { get; set; }
            public decimal? MediaB4 { get; set; }
            public int? FaltaB4 { get; set; }
            public decimal? MediaFinal { get; set; }
            public string Linha1
            {
                get
                {
                    return this.noAlu + " ( Matrícula: " + this.codigoMatricula.Insert(2, ".").Insert(6, ".") + " Nº NIRE: " + this.NuNire.ToString().PadLeft(9,'0') + " )";
                }
            }
            public string Linha2
            {
                get
                {
                    return "( Modalidade: " + this.Modalidade + " - Série: " + this.Serie + " - Turma: " + this.Turma + " - Ano Letivo: " + this.Ano.Trim() + " )";
                }
            }
            public string Linha3
            {
                get
                {
                    return "Data Nascimento: " + (this.DataNasctoAluno != null ? this.DataNasctoAluno.Value.ToString("dd/MM/yyyy") : "XX/XX/XXXX") 
                        + " - Nacionalidade: " + this.Nacionalidade + " - Naturalidade: " + this.Naturalidade + " - " + this.UFNaturalidade;
                }
            }
            public string Linha4
            {
                get
                {
                    return "Filiação: (Pai) " + this.Pai + " - (Mãe) " + this.Mae;
                }
            }
            public string Observacao
            {
                get
                {
                    if (this.NuNis != null)
	                {
		                var ctx = GestorEntities.CurrentContext;
                        var tbTransExter = (from tbTE in ctx.TB_TRANSF_EXTERNA
                                            where tbTE.CO_NIS_ALUNO == this.NuNis && tbTE.CO_UNIDA_ATUAL == this.CoEmp
                                            select tbTE).FirstOrDefault();

                        if (tbTransExter != null)
                        {
                            return "Unidade Destino: " + tbTransExter.NM_UNIDA_DESTI +
                            " (Código INEP: " + tbTransExter.NU_INEP_DESTI.ToString() + ")" + 
                            "Endereço: " + tbTransExter.DE_ENDER_DESTI + (tbTransExter.DE_COMPL_DESTI != null ? ", " + tbTransExter.DE_COMPL_DESTI : "")  +
                            " - Bairro " + tbTransExter.NO_BAIRR_DESTI +
                            "CEP: " + Funcoes.Format(tbTransExter.CO_CEP_DESTI, TipoFormat.CEP) + " - " +
                            tbTransExter.NO_CIDAD_DESTI + " - " + tbTransExter.CO_UF_DESTI + ")" + Environment.NewLine + "Motivo:" + tbTransExter.DE_MOTIVO_TRANSF;
                        }
                        else
                        {
                            return "";
                        }
	                }
                    else
                        return "";
                }
            }
            public string DescricaoGuia
            {
                get
                {
                    return "               A presente transferência é expedida ao Aluno " + this.noAlu +
                      " matriculado nesta Unidade Educacional, na Série " + this.Serie + ", neste Ano Letivo de " + this.Ano.Trim() +
                      ", nos termos da Lei Vigente e demais normas baixadas pelo Sistema de Ensino, tem direito a prosseguir os Estudos " +
                      "em Estabelecimento Fundamental ou correspondente indicado abaixo.";
                }
            }
            public int TotalFaltas
            {
                get
                {
                    return (this.FaltaB1 ?? 0) + (this.FaltaB2 ?? 0) + (this.FaltaB3 ?? 0) + (this.FaltaB4 ?? 0);
                }
            }
        }
        #endregion
    }
}
