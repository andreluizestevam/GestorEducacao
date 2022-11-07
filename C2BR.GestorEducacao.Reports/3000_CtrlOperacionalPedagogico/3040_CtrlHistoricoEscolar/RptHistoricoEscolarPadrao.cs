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

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3040_CtrlHistoricoEscolar
{
    public partial class RptHistoricoEscolarPadrao : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptHistoricoEscolarPadrao()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              int codInst,
                              int strP_CO_EMP,
                              int strP_CO_EMP_REF,
                              int strP_CO_MODU_CUR,
                              int strP_CO_ALU,
                              string infos)
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(strP_CO_EMP);
                string mes = "";
                if (DateTime.Now.Month == 1)
                {
                    mes = "janeiro";
                }
                else if (DateTime.Now.Month == 2)
                {
                    mes = "fevereiro";
                }
                else if (DateTime.Now.Month == 3)
                {
                    mes = "março";
                }
                else if (DateTime.Now.Month == 4)
                {
                    mes = "abril";
                }
                else if (DateTime.Now.Month == 5)
                {
                    mes = "maio";
                }
                else if (DateTime.Now.Month == 6)
                {
                    mes = "junho";
                }
                else if (DateTime.Now.Month == 7)
                {
                    mes = "julho";
                }
                else if (DateTime.Now.Month == 8)
                {
                    mes = "agosto";
                }
                else if (DateTime.Now.Month == 9)
                {
                    mes = "setembro";
                }
                else if (DateTime.Now.Month == 10)
                {
                    mes = "outubro";
                }
                else if (DateTime.Now.Month == 11)
                {
                    mes = "novembro";
                }
                else
                {
                    mes = "dezembro";
                }

                lblDataFooter.Text = header.Cidade + " - " + header.UF + ", " + DateTime.Now.ToString("dd") + " de " + mes +
                    " de " + DateTime.Now.Year.ToString();                

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                var tb83 = (from iTb83 in ctx.TB83_PARAMETRO
                           join diretor in ctx.TB03_COLABOR on iTb83.CO_DIR1 equals diretor.CO_COL into di
                           from x in di.DefaultIfEmpty()
                           where iTb83.CO_EMP == strP_CO_EMP
                           select new
                           {
                               nomeDiretor = x != null ? x.NO_COL : "",
                               matriculaDiretor = x != null ? x.CO_MAT_COL : "",
                               nomeSecretario = iTb83.TB03_COLABOR != null ? iTb83.TB03_COLABOR.NO_COL : "",
                               matriculaSecretario = iTb83.TB03_COLABOR != null ? iTb83.TB03_COLABOR.CO_MAT_COL : ""
                           }).FirstOrDefault();

                if (tb83 != null)
                {
                    lblDiretor.Text = tb83.nomeDiretor;
                    lblMatrDiret.Text = "Registro SEDUC nº " + (tb83.matriculaDiretor != "" ? tb83.matriculaDiretor : "XXXXX");
                    lblSecretario.Text = tb83.nomeSecretario;
                    lblMatrSecre.Text = "Registro SEDUC nº " + (tb83.matriculaSecretario != "" ? tb83.matriculaSecretario : "XXXXX");
                }

                //lblDescCertificado.Text = string.Format(lblDescCertificado.Text, "Série escolhida");

                #region Query Historico Escolar

                int iTotal = 1;
                string anoRefer = "";
                string situAprov = "";
                string nomeInstit = "";
                string cidadeInstit = "";
                int[] lstCoCur = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                string[] lstAnoRefer = { "", "", "", "", "", "", "", "", "" };

                var series = (from tb01 in ctx.TB01_CURSO
                              where tb01.CO_EMP == strP_CO_EMP_REF && tb01.CO_MODU_CUR == strP_CO_MODU_CUR
                              select new { tb01.CO_CUR, tb01.CO_SIGL_CUR, tb01.SEQ_IMPRESSAO, tb01.QT_AULA_CUR }).ToList().OrderBy(p => p.SEQ_IMPRESSAO).Distinct();

                foreach (var iDisc in series)
                {
                    if (iTotal > 9)
                    {
                        break;
                    }
                    
                    var tb08 = (from iTb08 in ctx.TB08_MATRCUR
                                join tb25 in ctx.TB25_EMPRESA on iTb08.CO_EMP equals tb25.CO_EMP
                                join tb904 in ctx.TB904_CIDADE on tb25.CO_CIDADE equals tb904.CO_CIDADE
                                where iTb08.CO_ALU == strP_CO_ALU && (iTb08.CO_SIT_MAT == "A" || iTb08.CO_SIT_MAT == "F") && iTb08.CO_CUR == iDisc.CO_CUR
                                select new 
                                { 
                                    iTb08.CO_ANO_MES_MAT, iTb08.CO_STA_APROV, iTb08.CO_STA_APROV_FREQ, 
                                    tb25.NO_FANTAS_EMP, tb904.NO_CIDADE
                                }).ToList();

                    if (tb08.Count == 0)
                    {
                        var tb130 = (from iTb130 in ctx.TB130_HIST_EXT_ALUNO
                                     where iTb130.CO_ALU == strP_CO_ALU && iTb130.CO_CUR == iDisc.CO_CUR
                                     select new 
                                     { 
                                         iTb130.CO_ANO_REF, iTb130.CO_STA_APROV, iTb130.NO_INST, iTb130.NO_CIDADE_INST
                                     }).ToList();

                        if (tb130.Count > 0)
                        {
                            lstAnoRefer[iTotal - 1] = tb130.Last().CO_ANO_REF;
                            anoRefer = tb130.Last().CO_ANO_REF.Trim();
                            situAprov = tb130.Where(p => p.CO_STA_APROV == "R").Count() > 0 ? "R" : "A";
                            nomeInstit = tb130.Last().NO_INST;
                            cidadeInstit = tb130.Last().NO_CIDADE_INST;
                        }
                        else
                            anoRefer = "";
                    }
                    else
                    {
                        lstAnoRefer[iTotal - 1] = tb08.Last().CO_ANO_MES_MAT;
                        anoRefer = tb08.Last().CO_ANO_MES_MAT.Trim();
                        situAprov = tb08.Last().CO_STA_APROV != null && tb08.Last().CO_STA_APROV_FREQ != null ?
                            (tb08.Last().CO_STA_APROV == "A" && tb08.Last().CO_STA_APROV_FREQ == "A" ? "A" : "R") : "";
                        nomeInstit = tb08.Last().NO_FANTAS_EMP;
                        cidadeInstit = tb08.Last().NO_CIDADE;
                    }

                    lstCoCur[iTotal - 1] = iDisc.CO_CUR;
                    if (iTotal == 1)
                    {
                        lblS1.Text = iDisc.CO_SIGL_CUR;
                        lblGS1.Text = iDisc.CO_SIGL_CUR;
                        lblTDL1.Text = iDisc.QT_AULA_CUR != null ? iDisc.QT_AULA_CUR.ToString() : "***";
                        if (anoRefer != "")
                        {
                            lblA1.Text = anoRefer;
                            lblGA1.Text = anoRefer;
                            lblRF1.Text = situAprov != "" ? (situAprov == "A" ? "Aprovado" : "Reprovado") : "*****";
                            lblGE1.Text = nomeInstit != "" ? nomeInstit : "*****";
                            lblGC1.Text = cidadeInstit != "" ? cidadeInstit : "*****";
                        }
                    }
                    else if (iTotal == 2)
                    {
                        lblS2.Text = iDisc.CO_SIGL_CUR;
                        lblGS2.Text = iDisc.CO_SIGL_CUR;
                        lblTDL2.Text = iDisc.QT_AULA_CUR != null ? iDisc.QT_AULA_CUR.ToString() : "***";
                        if (anoRefer != "")
                        {
                            lblA2.Text = anoRefer;
                            lblGA2.Text = anoRefer;
                            lblRF2.Text = situAprov != "" ? (situAprov == "A" ? "Aprovado" : "Reprovado") : "*****";
                            lblGE2.Text = nomeInstit != "" ? nomeInstit : "*****";
                            lblGC2.Text = cidadeInstit != "" ? cidadeInstit : "*****";
                        }
                    }
                    else if (iTotal == 3)
                    {
                        lblS3.Text = iDisc.CO_SIGL_CUR;
                        lblGS3.Text = iDisc.CO_SIGL_CUR;
                        lblTDL3.Text = iDisc.QT_AULA_CUR != null ? iDisc.QT_AULA_CUR.ToString() : "***";
                        if (anoRefer != "")
                        {
                            lblA3.Text = anoRefer;
                            lblGA3.Text = anoRefer;
                            lblRF3.Text = situAprov != "" ? (situAprov == "A" ? "Aprovado" : "Reprovado") : "*****";
                            lblGE3.Text = nomeInstit != "" ? nomeInstit : "*****";
                            lblGC3.Text = cidadeInstit != "" ? cidadeInstit : "*****";
                        }
                    }
                    else if (iTotal == 4)
                    {
                        lblS4.Text = iDisc.CO_SIGL_CUR;
                        lblGS4.Text = iDisc.CO_SIGL_CUR;
                        lblTDL4.Text = iDisc.QT_AULA_CUR != null ? iDisc.QT_AULA_CUR.ToString() : "***";
                        if (anoRefer != "")
                        {
                            lblA4.Text = anoRefer;
                            lblGA4.Text = anoRefer;
                            lblRF4.Text = situAprov != "" ? (situAprov == "A" ? "Aprovado" : "Reprovado") : "*****";
                            lblGE4.Text = nomeInstit != "" ? nomeInstit : "*****";
                            lblGC4.Text = cidadeInstit != "" ? cidadeInstit : "*****";
                        }
                    }
                    else if (iTotal == 5)
                    {
                        lblS5.Text = iDisc.CO_SIGL_CUR;
                        lblGS5.Text = iDisc.CO_SIGL_CUR;
                        lblTDL5.Text = iDisc.QT_AULA_CUR != null ? iDisc.QT_AULA_CUR.ToString() : "***";
                        if (anoRefer != "")
                        {
                            lblA5.Text = anoRefer;
                            lblGA5.Text = anoRefer;
                            lblRF5.Text = situAprov != "" ? (situAprov == "A" ? "Aprovado" : "Reprovado") : "*****";
                            lblGE5.Text = nomeInstit != "" ? nomeInstit : "*****";
                            lblGC5.Text = cidadeInstit != "" ? cidadeInstit : "*****";
                        }
                    }
                    else if (iTotal == 6)
                    {
                        lblS6.Text = iDisc.CO_SIGL_CUR;
                        lblGS6.Text = iDisc.CO_SIGL_CUR;
                        lblTDL6.Text = iDisc.QT_AULA_CUR != null ? iDisc.QT_AULA_CUR.ToString() : "***";
                        if (anoRefer != "")
                        {
                            lblA6.Text = anoRefer;
                            lblGA6.Text = anoRefer;
                            lblRF6.Text = situAprov != "" ? (situAprov == "A" ? "Aprovado" : "Reprovado") : "*****";
                            lblGE6.Text = nomeInstit != "" ? nomeInstit : "*****";
                            lblGC6.Text = cidadeInstit != "" ? cidadeInstit : "*****";
                        }
                    }
                    else if (iTotal == 7)
                    {
                        lblS7.Text = iDisc.CO_SIGL_CUR;
                        lblGS7.Text = iDisc.CO_SIGL_CUR;
                        lblTDL7.Text = iDisc.QT_AULA_CUR != null ? iDisc.QT_AULA_CUR.ToString() : "***";
                        if (anoRefer != "")
                        {
                            lblA7.Text = anoRefer;
                            lblGA7.Text = anoRefer;
                            lblRF7.Text = situAprov != "" ? (situAprov == "A" ? "Aprovado" : "Reprovado") : "*****";
                            lblGE7.Text = nomeInstit != "" ? nomeInstit : "*****";
                            lblGC7.Text = cidadeInstit != "" ? cidadeInstit : "*****";
                        }
                    }
                    else if (iTotal == 8)
                    {
                        lblS8.Text = iDisc.CO_SIGL_CUR;
                        lblGS8.Text = iDisc.CO_SIGL_CUR;
                        lblTDL8.Text = iDisc.QT_AULA_CUR != null ? iDisc.QT_AULA_CUR.ToString() : "***";
                        if (anoRefer != "")
                        {
                            lblA8.Text = anoRefer;
                            lblGA8.Text = anoRefer;
                            lblRF8.Text = situAprov != "" ? (situAprov == "A" ? "Aprovado" : "Reprovado") : "*****";
                            lblGE8.Text = nomeInstit != "" ? nomeInstit : "*****";
                            lblGC8.Text = cidadeInstit != "" ? cidadeInstit : "*****";
                        }
                    }
                    else if (iTotal == 9)
                    {
                        lblS9.Text = iDisc.CO_SIGL_CUR;
                        lblGS9.Text = iDisc.CO_SIGL_CUR;
                        lblTDL9.Text = iDisc.QT_AULA_CUR != null ? iDisc.QT_AULA_CUR.ToString() : "***";
                        if (anoRefer != "")
                        {
                            lblA9.Text = anoRefer;
                            lblGA9.Text = anoRefer;
                            lblRF9.Text = situAprov != "" ? (situAprov == "A" ? "Aprovado" : "Reprovado") : "*****";
                            lblGE9.Text = nomeInstit != "" ? nomeInstit : "*****";
                            lblGC9.Text = cidadeInstit != "" ? cidadeInstit : "*****";
                        }
                    }
                    iTotal++;
                }

                var lst = (from tb08 in ctx.TB08_MATRCUR
                           where tb08.CO_EMP == strP_CO_EMP_REF
                           && tb08.TB44_MODULO.CO_MODU_CUR == strP_CO_MODU_CUR
                           && tb08.CO_ALU == strP_CO_ALU && (tb08.CO_SIT_MAT == "F" || tb08.CO_SIT_MAT == "A")
                           select new RelHistoricoPadraoEscolar
                           {
                               NomeAluno = tb08.TB07_ALUNO.NO_ALU,
                               NascimentoAluno = tb08.TB07_ALUNO.DT_NASC_ALU,
                               SexoAluno = tb08.TB07_ALUNO.CO_SEXO_ALU == "M" ? "Masculino" : "Feminino",
                               Nacionalidade = tb08.TB07_ALUNO.CO_NACI_ALU == "B" ? "Brasileira" : "Estrangeira",
                               NaturalidadeAluno = tb08.TB07_ALUNO.DE_NATU_ALU + " / " + (tb08.TB07_ALUNO.CO_NACI_ALU == "B" ? tb08.TB07_ALUNO.CO_UF_NATU_ALU : "**"),
                               RGAluno = tb08.TB07_ALUNO.CO_RG_ALU,
                               OrgaoEmissorAluno = tb08.TB07_ALUNO.CO_ORG_RG_ALU,
                               UFRGAluno = tb08.TB07_ALUNO.CO_ESTA_RG_ALU,
                               DtExpedicaoRGAluno = tb08.TB07_ALUNO.DT_EMIS_RG_ALU,
                               MaeAluno = (tb08.TB07_ALUNO.NO_MAE_ALU != null ? tb08.TB07_ALUNO.NO_MAE_ALU : "*****"),
                               PaiAluno = (tb08.TB07_ALUNO.NO_PAI_ALU != null ? tb08.TB07_ALUNO.NO_PAI_ALU : "*****"),
                               NIRE = tb08.TB07_ALUNO.NU_NIRE
                           });
                
                var res = lst.FirstOrDefault();

                #endregion

                // Erro: não encontrou registros
                if (res == null)
                    return -1;

                #region Disciplinas

                var resultado = (from tb107 in ctx.TB107_CADMATERIAS
                               join tb02 in ctx.TB02_MATERIA on tb107.ID_MATERIA equals tb02.ID_MATERIA
                               where tb02.CO_EMP == strP_CO_EMP_REF && tb02.CO_MODU_CUR == strP_CO_MODU_CUR
                               select new
                               {
                                   tb107.NO_MATERIA, tb107.ID_MATERIA, tb107.CO_CLASS_BOLETIM
                               }).ToList().Distinct().OrderBy(p => p.CO_CLASS_BOLETIM);

                var lstOcor = (from tb107 in resultado
                               select new DisciplinasSeries
                               {
                                   Disciplina = tb107.NO_MATERIA,
                                   IdDisciplina = tb107.ID_MATERIA,
                                   ClassificacaoDisciplina = tb107.CO_CLASS_BOLETIM
                               }).ToList();        
               
                #endregion
                res.DisciplinasSeries = lstOcor.ToList();

                int idSerie = 0;
                string iAnoRef = "";
                foreach (var iRes in res.DisciplinasSeries)
                {
                    for (int i = 0; i < lstCoCur.Length; i++)
                    {
                        if (lstCoCur[i] != 0 && lstAnoRefer[i] != "")
                        {
                            idSerie = lstCoCur[i];
                            iAnoRef = lstAnoRefer[i];

                            var tb079 = (from iTb079 in ctx.TB079_HIST_ALUNO
                                         join tb02 in ctx.TB02_MATERIA on iTb079.CO_MAT equals tb02.CO_MAT
                                         where tb02.ID_MATERIA == iRes.IdDisciplina && iTb079.CO_ALU == strP_CO_ALU && iTb079.CO_CUR == idSerie
                                         && iTb079.CO_ANO_REF == iAnoRef
                                         select new
                                         {
                                             iTb079.VL_MEDIA_FINAL,
                                             iTb079.QT_FALTA_BIM1,
                                             iTb079.QT_FALTA_BIM2,
                                             iTb079.QT_FALTA_BIM3,
                                             iTb079.QT_FALTA_BIM4,
                                             tb02.QT_CARG_HORA_MAT
                                         }).FirstOrDefault();

                            if (tb079 != null)
                            {
                                if (i == 0)
                                {
                                    iRes.N1 = tb079.VL_MEDIA_FINAL;
                                    iRes.CH1 = tb079.QT_CARG_HORA_MAT;
                                }
                                else if (i == 1)
                                {
                                    iRes.N2 = tb079.VL_MEDIA_FINAL;
                                    iRes.CH2 = tb079.QT_CARG_HORA_MAT;
                                }
                                else if (i == 2)
                                {
                                    iRes.N3 = tb079.VL_MEDIA_FINAL;
                                    iRes.CH3 = tb079.QT_CARG_HORA_MAT;
                                }
                                else if (i == 3)
                                {
                                    iRes.N4 = tb079.VL_MEDIA_FINAL;
                                    iRes.CH4 = tb079.QT_CARG_HORA_MAT;
                                }
                                else if (i == 4)
                                {
                                    iRes.N5 = tb079.VL_MEDIA_FINAL;
                                    iRes.CH5 = tb079.QT_CARG_HORA_MAT;
                                }
                                else if (i == 5)
                                {
                                    iRes.N6 = tb079.VL_MEDIA_FINAL;
                                    iRes.CH6 = tb079.QT_CARG_HORA_MAT;
                                }
                                else if (i == 6)
                                {
                                    iRes.N7 = tb079.VL_MEDIA_FINAL;
                                    iRes.CH7 = tb079.QT_CARG_HORA_MAT;
                                }
                                else if (i == 7)
                                {
                                    iRes.N8 = tb079.VL_MEDIA_FINAL;
                                    iRes.CH8 = tb079.QT_CARG_HORA_MAT;
                                }
                                else if (i == 8)
                                {
                                    iRes.N9 = tb079.VL_MEDIA_FINAL;
                                    iRes.CH9 = tb079.QT_CARG_HORA_MAT;
                                }
                            }
                            else
                            {
                                var tb130 = (from iTb130 in ctx.TB130_HIST_EXT_ALUNO
                                             join tb02 in ctx.TB02_MATERIA on iTb130.CO_MAT equals tb02.CO_MAT
                                             where tb02.ID_MATERIA == iRes.IdDisciplina && iTb130.CO_ALU == strP_CO_ALU && iTb130.CO_CUR == idSerie
                                             && iTb130.CO_ANO_REF == iAnoRef
                                             select new
                                             {
                                                 iTb130.VL_MEDIA_FINAL,
                                                 iTb130.QT_FALTA_FINAL,
                                                 iTb130.QT_CH_MAT
                                             }).FirstOrDefault();

                                if (tb079 != null)
                                {
                                    if (i == 0)
                                    {
                                        iRes.N1 = tb130.VL_MEDIA_FINAL;
                                        iRes.CH1 = tb130.QT_CH_MAT;
                                    }
                                    else if (i == 1)
                                    {
                                        iRes.N2 = tb130.VL_MEDIA_FINAL;
                                        iRes.CH2 = tb130.QT_CH_MAT;
                                    }
                                    else if (i == 2)
                                    {
                                        iRes.N3 = tb130.VL_MEDIA_FINAL;
                                        iRes.CH3 = tb130.QT_CH_MAT;
                                    }
                                    else if (i == 3)
                                    {
                                        iRes.N4 = tb130.VL_MEDIA_FINAL;
                                        iRes.CH4 = tb130.QT_CH_MAT;
                                    }
                                    else if (i == 4)
                                    {
                                        iRes.N5 = tb130.VL_MEDIA_FINAL;
                                        iRes.CH5 = tb130.QT_CH_MAT;
                                    }
                                    else if (i == 5)
                                    {
                                        iRes.N6 = tb130.VL_MEDIA_FINAL;
                                        iRes.CH6 = tb130.QT_CH_MAT;
                                    }
                                    else if (i == 6)
                                    {
                                        iRes.N7 = tb130.VL_MEDIA_FINAL;
                                        iRes.CH7 = tb130.QT_CH_MAT;
                                    }
                                    else if (i == 7)
                                    {
                                        iRes.N8 = tb130.VL_MEDIA_FINAL;
                                        iRes.CH8 = tb130.QT_CH_MAT;
                                    }
                                    else if (i == 8)
                                    {
                                        iRes.N9 = tb130.VL_MEDIA_FINAL;
                                        iRes.CH9 = tb130.QT_CH_MAT;
                                    }
                                }
                            }
                        }
                    }
                }
                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                bsReport.Add(res);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Histórico Escolar

        public class RelHistoricoPadraoEscolar
        {
            public RelHistoricoPadraoEscolar()
            {
                this.DisciplinasSeries = new List<DisciplinasSeries>();
            }
            public string NomeAluno { get; set; }
            public DateTime? NascimentoAluno { get; set; }
            public string SexoAluno { get; set; }
            public string Nacionalidade { get; set; }
            public string NaturalidadeAluno { get; set; }
            public string UFAluno { get; set; }
            public string RGAluno { get; set; }
            public string OrgaoEmissorAluno { get; set; }
            public string UFRGAluno { get; set; }
            public DateTime? DtExpedicaoRGAluno { get; set; }
            public string PaiAluno { get; set; }
            public string MaeAluno { get; set; }
            public List<DisciplinasSeries> DisciplinasSeries { get; set; }
            public int? NIRE { get; set; }
            public decimal? NIS { get; set; }

            public string DescAluno
            {
                get
                {
                    return this.NomeAluno.ToUpper();
                }
            }

            public string DescPaiAluno
            {
                get
                {
                    return "Pai: " + this.PaiAluno.ToUpper();
                }
            }

            public string DescMaeAluno
            {
                get
                {
                    return "Mãe: " + this.MaeAluno.ToUpper();
                }
            }

            public string DescNascimento
            {
                get
                {
                    return (this.NascimentoAluno != null ? "Nascimento: " + this.NascimentoAluno.Value.ToString("dd/MM/yyyy")
                        : "Nascimento: *****") +
                        " - Nacionalidade: " + (this.Nacionalidade != null ? this.Nacionalidade : "*****") +
                        " - Naturalidade: " + (this.NaturalidadeAluno != null ? this.NaturalidadeAluno : "*****");
                }
            }

            public string DescRG
            {
                get
                {
                    return (this.RGAluno != null && this.RGAluno != "" ? ("RG nº: " + this.RGAluno + " / " +
                        (this.OrgaoEmissorAluno != null && this.OrgaoEmissorAluno != "" ? this.OrgaoEmissorAluno : "*****") +
                        " / " + (this.UFRGAluno != null ? this.UFRGAluno : "**"))
                        : "RG nº: *****") +
                        " - Nº NIS (Registro MEC): " + (this.NIS != null ? this.NIS.ToString() : "*****") +
                        " - Nº NIRE: " + this.NIRE.ToString().PadLeft(9, '0');
                }
            }            
        }

        public class DisciplinasSeries
        {
            public string Disciplina { get; set; }
            public int IdDisciplina { get; set; }
            public decimal? ClassificacaoDisciplina { get; set; }
            public decimal? N1 { get; set; }
            public int? CH1 { get; set; }
            public decimal? N2 { get; set; }
            public int? CH2 { get; set; }
            public decimal? N3 { get; set; }
            public int? CH3 { get; set; }
            public decimal? N4 { get; set; }
            public int? CH4 { get; set; }
            public decimal? N5 { get; set; }
            public int? CH5 { get; set; }
            public decimal? N6 { get; set; }
            public int? CH6 { get; set; }
            public decimal? N7 { get; set; }
            public int? CH7 { get; set; }
            public decimal? N8 { get; set; }
            public int? CH8 { get; set; }
            public decimal? N9 { get; set; }
            public int? CH9 { get; set; }

            public string DescClassificacao
            {
                get
                {
                    return this.ClassificacaoDisciplina == 1 ? "*** BASE NACIONAL COMUM" : this.ClassificacaoDisciplina == 2 ? "*** PARTE DIVERSIFICADA" :
                        this.ClassificacaoDisciplina == 3 ? "*** EXTRA CURRICULAR" : "NÃO SE APLICA";
                }
            }
        }
        #endregion
    }
}
