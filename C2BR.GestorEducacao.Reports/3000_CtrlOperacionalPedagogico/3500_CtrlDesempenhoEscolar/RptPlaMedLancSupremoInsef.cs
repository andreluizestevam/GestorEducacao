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
    public partial class RptPlaMedLancSupremoInsef : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptPlaMedLancSupremoInsef()
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
                                int strP_CO_MAT,
                                int coEmp,
                                bool formulario,
                                string AgrupadoPor,
                                int CO_ALU,
                                string NomeFuncionalidadeCadastrada,
                                bool isResponsavel = false
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

                string trimestre = "";

                switch (strP_CO_BIMESTRE)
                {
                    case "T1":
                        trimestre = " - 1° Trimestre";
                        break;
                    case "T2":
                        trimestre = " - 2° Trimestre";
                        break;
                    case "T3":
                        trimestre = " - 3° Trimestre";
                        break;
                }

                if (!IsEmptyValue(NomeFuncionalidadeCadastrada))
                    lblTitulo.Text = NomeFuncionalidadeCadastrada + trimestre;
                else
                    lblTitulo.Text = "PLANILHA DE LANÇAMENTO DE NOTAS" + trimestre;

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                #region Seta o secretário escolar e o Professor

                //Mostra as informações de professor apenas se o agrupamento for por Disciplina
                xrLabel3.Visible = lblProfessor.Visible = xrLabel4.Visible = xrLabel5.Visible = (AgrupadoPor == "A" ? false : true);

                //select TP_CTRLE_SECRE_ESCOL from TB149_PARAM_INSTI
                string noSecretario = "";
                string matSecretario = "";
                TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(strP_CO_EMP_REF != 0 ? strP_CO_EMP_REF : coEmp);
                tb25.TB000_INSTITUICAOReference.Load();

                int coOrg = tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO;
                TB149_PARAM_INSTI tb149 = TB149_PARAM_INSTI.RetornaPelaChavePrimaria(coOrg);

                if (tb149.TP_CTRLE_SECRE_ESCOL == "I")
                {
                    // Secretaria escolar controlada por instituição
                    if (tb149.TB03_COLABOR1 != null)
                    {
                        noSecretario = TB03_COLABOR.RetornaPelaChavePrimaria(strP_CO_EMP_REF, tb149.TB03_COLABOR1.CO_COL).NO_COL;
                        matSecretario = TB03_COLABOR.RetornaPelaChavePrimaria(strP_CO_EMP_REF, tb149.TB03_COLABOR1.CO_COL).CO_MAT_COL;
                    }
                }
                else
                {
                    // Secretaria escolar controlada por unidade
                    if (tb149.TB03_COLABOR != null)
                    {
                        noSecretario = TB03_COLABOR.RetornaPelaChavePrimaria(strP_CO_EMP_REF, tb149.TB03_COLABOR.CO_COL).NO_COL;
                        matSecretario = TB03_COLABOR.RetornaPelaChavePrimaria(strP_CO_EMP_REF, tb149.TB03_COLABOR.CO_COL).CO_MAT_COL;
                    }
                }

                if (matSecretario != "" && noSecretario != "")
                {
                    lblSecretario.Text = matSecretario + " - " + noSecretario;
                }
                else
                {
                    if (isResponsavel != true)
                    {
                        lblSecretario.Text = "Nome:";
                        lblSecretario.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                    }
                    else
                    {
                        lblSecretario.Text = "";
                        xrLabel6.Text = "";
                    }
                }

                string turmaUnica = "N";
                if (strP_CO_MODU_CUR != 0 && strP_CO_CUR != 0 && strP_CO_TUR != 0)
                {
                    // Recebe o conteúdo da coluna CO_FLAG_RESP_TURMA, da tabela TB06_TURMAS, que informa se a turma é única ou não
                    turmaUnica = TB06_TURMAS.RetornaPelaChavePrimaria(coEmp, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR).CO_FLAG_RESP_TURMA;
                }

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                string coFlagResp = (from coFlag in ctx.TB06_TURMAS
                                     where coFlag.CO_EMP == strP_CO_EMP_REF
                                           && coFlag.CO_MODU_CUR == strP_CO_MODU_CUR
                                           && coFlag.CO_CUR == strP_CO_CUR
                                           && coFlag.CO_TUR == strP_CO_TUR
                                     select new
                                     {
                                         coFlag.CO_FLAG_RESP_TURMA
                                     }).FirstOrDefault().CO_FLAG_RESP_TURMA;

                #endregion

                if (formulario == true)
                {
                    var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                               join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb07.CO_ALU equals tb08.CO_ALU
                               join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb43.CO_CUR
                               join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                               join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                               where tb08.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT
                               && tb43.CO_EMP == tb08.CO_EMP
                               && tb43.CO_ANO_GRADE == tb08.CO_ANO_MES_MAT
                               && tb43.CO_CUR == tb08.CO_CUR
                               && (strP_CO_EMP_REF != 0 ? tb08.CO_EMP == strP_CO_EMP_REF : 0 == 0)
                               && (strP_CO_MODU_CUR != 0 ? tb08.TB44_MODULO.CO_MODU_CUR == strP_CO_MODU_CUR : 0 == 0)
                               && (strP_CO_CUR != 0 ? tb08.CO_CUR == strP_CO_CUR : 0 == 0)
                               && (strP_CO_TUR != 0 ? tb08.CO_TUR == strP_CO_TUR : 0 == 0)
                               && (strP_CO_MAT != 0 ? tb02.CO_MAT == strP_CO_MAT : 0 == 0)
                               && (CO_ALU != 0 ? tb07.CO_ALU == CO_ALU : 0 == 0)
                               select new PlaMedLancSupremoInsef
                               {
                                   noAlu = tb07.NO_ALU,
                                   nuNir = tb07.NU_NIRE,
                                   noMateria = tb107.NO_MATERIA,
                                   agrupadoPor = AgrupadoPor == "A" ? tb07.NO_ALU : tb107.NO_MATERIA,
                                   DescricaoLista = (AgrupadoPor == "A" ? tb107.NO_MATERIA : tb07.NO_ALU),
                                   DiscAgrupadora = tb43.FL_DISCI_AGRUPA,
                                   STATUS_MATRICULA = tb08.CO_SIT_MAT,
                                   idMatAgrup = tb43.ID_MATER_AGRUP,
                                   OrdImp = tb43.CO_ORDEM_IMPRE,
                                   coCur = tb08.CO_CUR,
                                   coTur = tb08.CO_TUR,
                                   coAno = tb08.CO_ANO_MES_MAT,
                                   coMat = tb02.CO_MAT,
                                   coAlu = tb07.CO_ALU,
                                   vlMedia = tb02.VL_CALCU_MEDIA,
                                   AgrupadoPorSelect = AgrupadoPor,
                                   coModalidade = tb08.TB44_MODULO.CO_MODU_CUR,
                                   coFlagResp = coFlagResp,
                                   rltAa = "",
                                   rltAe = "",
                                   rltAg = "",
                                   rltAp = "",
                                   rltCon = "",
                                   rltPro = "",
                                   rltSim = "",
                                   rltTp1 = "",
                                   rltTp2 = "",
                                   rltTra = ""
                               }).Distinct().ToList();

                    res = res.OrderBy(w => w.agrupadoPor).ThenBy(o => o.OrdImp_Valid).ThenBy(l => l.OrdImpFilhas).ThenBy(o => o.noAlu).ToList();

                    if (res.Count == 0)
                        return -1;

                    // Seta os dados no DataSource do Relatorio
                    bsReport.Clear();

                    foreach (var at in res)
                    {
                        at.descAgrup = (AgrupadoPor == "A" ? at.nuNir + " - " + at.noAlu : at.DiscipProfes);

                        if (!string.IsNullOrEmpty(at.noProfe))
                        {
                            string noProfessor = at.noProfe;
                            lblProfessor.Text = noProfessor;
                        }
                        else
                        {
                            lblProfessor.Text = "Nome: ";
                            lblProfessor.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        }

                        //Alterna o nome apresentado na linha para TRAN quando o aluno estiver com status de transferido
                        if (at.STATUS_MATRICULA == "X")
                        {
                            string tr = "TR";
                            at.rltAa = at.rltAe = at.rltAg = at.rltAp = at.rltCon = at.rltPro = at.rltSim = at.rltTp1 = at.rltTp2 = at.rltTra = at.md = tr;
                        }

                        bsReport.Add(at);
                    }
                }
                else
                {
                    var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                               join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb07.CO_ALU equals tb08.CO_ALU
                               join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb43.CO_CUR
                               join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                               join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                               where tb08.CO_ANO_MES_MAT.Substring(0, 4) == strP_CO_ANO_MES_MAT.Substring(0, 4)
                                && tb43.CO_EMP == tb08.CO_EMP
                                && tb43.CO_ANO_GRADE.Substring(0, 4) == tb08.CO_ANO_MES_MAT.Substring(0, 4)
                                && tb43.CO_CUR == tb08.CO_CUR
                                && (strP_CO_EMP_REF != 0 ? tb08.CO_EMP == strP_CO_EMP_REF : 0 == 0)
                                && (strP_CO_MODU_CUR != 0 ? tb08.TB44_MODULO.CO_MODU_CUR == strP_CO_MODU_CUR : 0 == 0)
                                && (strP_CO_CUR != 0 ? tb08.CO_CUR == strP_CO_CUR : 0 == 0)
                                && (strP_CO_TUR != 0 ? tb08.CO_TUR == strP_CO_TUR : 0 == 0)
                                && (strP_CO_MAT != 0 ? tb02.CO_MAT == strP_CO_MAT : 0 == 0)
                                && (CO_ALU != 0 ? tb07.CO_ALU == CO_ALU : 0 == 0)
                               select new PlaMedLancSupremoInsef
                               {
                                   coAlu = tb07.CO_ALU,
                                   coModalidade = tb08.TB44_MODULO.CO_MODU_CUR,
                                   agrupadoPor = (AgrupadoPor == "A" ? tb07.NO_ALU : tb107.NO_MATERIA),
                                   DescricaoLista = (AgrupadoPor == "A" ? tb107.NO_MATERIA : tb07.NO_ALU),
                                   DiscAgrupadora = tb43.FL_DISCI_AGRUPA,
                                   coCur = tb08.CO_CUR,
                                   coTur = tb08.CO_TUR,
                                   coAno = tb08.CO_ANO_MES_MAT,
                                   coMat = tb02.CO_MAT,
                                   noAlu = tb07.NO_ALU,
                                   nuNir = tb07.NU_NIRE,
                                   noMateria = tb107.NO_MATERIA,
                                   idMat = tb107.ID_MATERIA,
                                   vlMedia = tb02.VL_CALCU_MEDIA,
                                   STATUS_MATRICULA = tb08.CO_SIT_MAT,
                                   idMatAgrup = tb43.ID_MATER_AGRUP,
                                   OrdImp = tb43.CO_ORDEM_IMPRE,
                                   AgrupadoPorSelect = AgrupadoPor,
                                   coFlagResp = coFlagResp,
                               }).Distinct().ToList();


                    res = res.OrderBy(w => w.agrupadoPor).ThenBy(o => o.OrdImp_Valid).ThenBy(l => l.OrdImpFilhas).ThenBy(o => o.noAlu).ToList();

                    if (res.Count == 0)
                        return -1;
                    // Seta os dados no DataSource do Relatorio
                    bsReport.Clear();

                    foreach (var at in res)
                    {
                        at.descAgrup = (AgrupadoPor == "A" ? at.nuNir.ToString().PadLeft(7, '0') + " - " + at.noAlu : at.DiscipProfes);

                        at.total = (AgrupadoPor == "A" ? "Disciplina(s)." : "Alunos(as).");

                        if (!string.IsNullOrEmpty(at.noProfe))
                        {
                            string noProfessor = at.noProfe;
                            lblProfessor.Text = noProfessor;
                        }
                        else
                        {
                            lblProfessor.Text = "Nome: ";
                            lblProfessor.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        }

                        //Parte responsável por coletar as notas
                        #region Nota do Trimestre
                        var nota = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                    join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb079.CO_CUR equals tb43.CO_CUR
                                    join alu in TB07_ALUNO.RetornaTodosRegistros() on tb079.CO_ALU equals alu.CO_ALU
                                    where tb079.CO_MODU_CUR == at.coModalidade && tb079.CO_CUR == at.coCur && tb079.CO_TUR == at.coTur
                                    && (tb079.CO_ANO_REF == at.coAno)
                                    && tb079.CO_ANO_REF == tb43.CO_ANO_GRADE && tb079.CO_MAT == tb43.CO_MAT
                                    && (at.coAlu != 0 ? tb079.CO_ALU == at.coAlu : 0 == 0)
                                    && tb43.CO_EMP == tb079.CO_EMP
                                    && tb079.CO_EMP == strP_CO_EMP_REF
                                    && tb079.CO_MAT == at.coMat
                                    select new
                                    {
                                        media = (strP_CO_BIMESTRE == "T1" ? tb079.VL_NOTA_BIM1_ORI : strP_CO_BIMESTRE == "T2" ? tb079.VL_NOTA_BIM2_ORI : tb079.VL_NOTA_BIM3_ORI),
                                        idagrup = tb43.ID_MATER_AGRUP,
                                    }).FirstOrDefault();

                        //Atribui a nota verificando se é nulo e se é disciplina filha
                        if (nota != null)
                            at.md = (nota.idagrup == null ? nota.media.HasValue ? nota.media.Value.ToString("N1") : "" : " - ");
                        else
                            at.md = "";

                        #endregion

                        #region Notas Atividades
                        int ano = int.Parse(at.coAno);
                        int idMat = at.idMat;
                        var result = (from tb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                                      join tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros() on tb49.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV equals tb273.ID_TIPO_ATIV
                                      where tb49.TB07_ALUNO.CO_ALU == at.coAlu
                                      && tb49.TB01_CURSO.CO_CUR == at.coCur
                                      && tb49.CO_ANO == ano
                                      && tb49.CO_BIMESTRE == strP_CO_BIMESTRE
                                      && tb49.TB107_CADMATERIAS.ID_MATERIA == idMat
                                      select new
                                      {
                                          tb49.ID_NOTA_ATIV,
                                          tb49.CO_TIPO_ATIV,
                                          tb273.CO_SIGLA_ATIV,
                                          tb49.VL_NOTA,
                                          tb49.CO_REFER_NOTA,
                                      }).ToList();
                        decimal aa = 0, ae = 0, ag = 0, ap = 0, con = 0, pro = 0, sim = 0, tra = 0, tp1 = 0, tp2 = 0;
                        foreach (var l in result)
                        {
                            if (l.CO_SIGLA_ATIV.Trim() == "AA")
                            {
                                at.rltAa = "  " + l.VL_NOTA.ToString("N1");
                                aa = l.VL_NOTA;
                            }
                            if (l.CO_SIGLA_ATIV.Trim() == "AE")
                            {
                                at.rltAe = "  " + l.VL_NOTA.ToString("N1");
                                ae = l.VL_NOTA;
                            }
                            if (l.CO_SIGLA_ATIV.Trim() == "AG")
                            {
                                at.rltAg = "  " + l.VL_NOTA.ToString("N1");
                                ag = l.VL_NOTA;
                            }
                            if (l.CO_SIGLA_ATIV.Trim() == "AP")
                            {
                                at.rltAp = "  " + l.VL_NOTA.ToString("N1");
                                ap = l.VL_NOTA;
                            }
                            if (l.CO_SIGLA_ATIV.Trim() == "CT")
                            {
                                at.rltCon = "  " + l.VL_NOTA.ToString("N1");
                                con = l.VL_NOTA;
                            }
                            if (l.CO_SIGLA_ATIV.Trim() == "PJ")
                            {
                                at.rltPro = "  " + l.VL_NOTA.ToString("N1");
                                pro = l.VL_NOTA;
                            }
                            if (l.CO_SIGLA_ATIV.Trim() == "SI")
                            {
                                at.rltSim = "  " + l.VL_NOTA.ToString("N1");
                                sim = l.VL_NOTA;
                            }
                            if (l.CO_SIGLA_ATIV.Trim() == "PR")
                            {
                                if (l.CO_TIPO_ATIV.Trim() == "1")
                                {
                                    at.rltTp1 = "  " + l.VL_NOTA.ToString("N1");
                                    tp1 = l.VL_NOTA;
                                }
                                if (l.CO_TIPO_ATIV.Trim() == "2")
                                {
                                    at.rltTp2 = "  " + l.VL_NOTA.ToString("N1");
                                    tp2 = l.VL_NOTA;
                                }
                            }
                            if (l.CO_SIGLA_ATIV.Trim() == "TB")
                            {
                                at.rltTra = "  " + l.VL_NOTA.ToString("N1");
                                tra = l.VL_NOTA;
                            }
                        }

                        if (aa == 0)
                            at.rltAa = "-";
                        if (ae == 0)
                            at.rltAe = "-";
                        if (ag == 0)
                            at.rltAg = "-";
                        if (ap == 0)
                            at.rltAp = "-";
                        if (con == 0)
                            at.rltCon = "-";
                        if (pro == 0)
                            at.rltPro = "-";
                        if (sim == 0)
                            at.rltSim = "-";
                        if (tp1 == 0)
                            at.rltTp1 = "-";
                        if (tp2 == 0)
                            at.rltTp2 = "-";
                        if (tra == 0)
                            at.rltTra = "-";

                        decimal? media = (at.vlMedia.HasValue ? at.vlMedia : 1);
                        at.MD = (aa + ae + ag + ap + con + pro + sim + tp1 + tp2 + tra) / media;
                        at.md = ((decimal)at.MD).ToString("N1");
                        #endregion

                        //Alterna o nome apresentado na linha para TRAN quando o aluno estiver com status de transferido
                        if (at.STATUS_MATRICULA == "X")
                        {
                            string tr = "TR";
                            at.rltAa = at.rltAe = at.rltAg = at.rltAp = at.rltCon = at.rltPro = at.rltSim = at.rltTp1 = at.rltTp2 = at.rltTra = at.md = tr;
                        }
                        bsReport.Add(at);
                    }
                }

                return 1;
            }
            catch { return 0; }
        }
        #endregion

        #region Classe Boletim Aluno
        public class PlaMedLancSupremoInsef
        {
            //Informações gerais
            public int idMat { get; set; }
            public string noMateria { get; set; }
            public decimal? vlMedia { get; set; }
            public decimal? MD { get; set; }
            public string noProfe { get; set; }
            public string DiscipProfes
            {
                get
                {
                    //Coleta o nome do professor responsável por uma determinada disciplina
                    string noCol = "";
                    if (this.AgrupadoPorSelect == "D")
                    {
                        int anoI = int.Parse(this.coAno);
                        #region Retorna o nome do professor
                        if ((from resp in TB_RESPON_MATERIA.RetornaTodosRegistros() where resp.CO_MODU_CUR == this.coModalidade && resp.CO_CUR == this.coCur && resp.CO_TUR == this.coTur && resp.CO_ANO_REF == anoI && coFlagResp != "S" ? resp.CO_MAT == this.coMat : 0 == 0 select resp).Any())
                        {
                            if (coFlagResp == "S")
                            {
                                if ((from resp in TB_RESPON_MATERIA.RetornaTodosRegistros() join tb03 in TB03_COLABOR.RetornaTodosRegistros() on resp.CO_COL_RESP equals tb03.CO_COL where resp.CO_MODU_CUR == this.coModalidade && resp.CO_CUR == this.coCur && resp.CO_TUR == this.coTur && resp.CO_ANO_REF == anoI select new { tb03.NO_COL }).Any())
                                {
                                    noCol = (from resp in TB_RESPON_MATERIA.RetornaTodosRegistros()
                                             join tb03 in TB03_COLABOR.RetornaTodosRegistros() on resp.CO_COL_RESP equals tb03.CO_COL
                                             where resp.CO_MODU_CUR == this.coModalidade
                                             && resp.CO_CUR == this.coCur
                                             && resp.CO_TUR == this.coTur
                                             && resp.CO_ANO_REF == anoI
                                             select new
                                             {
                                                 NO_COL = tb03.CO_MAT_COL + " - " + tb03.NO_COL
                                             }).FirstOrDefault().NO_COL;
                                }
                            }
                            else
                            {
                                if ((from resp in TB_RESPON_MATERIA.RetornaTodosRegistros() join tb03 in TB03_COLABOR.RetornaTodosRegistros() on resp.CO_COL_RESP equals tb03.CO_COL where resp.CO_MODU_CUR == this.coModalidade && resp.CO_CUR == this.coCur && resp.CO_TUR == this.coTur && resp.CO_ANO_REF == anoI && resp.CO_MAT == this.coMat select new { tb03.NO_COL }).Any())
                                {
                                    noCol = (from resp in TB_RESPON_MATERIA.RetornaTodosRegistros()
                                             join tb03 in TB03_COLABOR.RetornaTodosRegistros() on resp.CO_COL_RESP equals tb03.CO_COL
                                             where resp.CO_MODU_CUR == this.coModalidade
                                             && resp.CO_CUR == this.coCur
                                             && resp.CO_TUR == this.coTur
                                             && resp.CO_ANO_REF == anoI
                                             && (this.coMat != 0 ? resp.CO_MAT == this.coMat : resp.CO_MAT == null)
                                             select new
                                             {
                                                 NO_COL = tb03.CO_MAT_COL + " - " + tb03.NO_COL
                                             }).FirstOrDefault().NO_COL;
                                }
                            }
                        }
                        #endregion
                    }

                    return "Disciplina: " + this.noMateria.ToUpper() + " - " + "Professor: " + (!string.IsNullOrEmpty(noCol) ? noCol : " - ");
                }
            }
            public string noAlu { get; set; }
            public int nuNir { get; set; }
            public string nome
            {
                get
                {
                    return this.nuNir.ToString().PadLeft(7, '0') + " - " + (this.noAlu.Length > 28 ? this.noAlu.Substring(0, 28) + "..." : this.noAlu);
                }
            }
            public string descAgrup { get; set; }
            public string DescricaoLista { get; set; }
            public string agrupadoPor { get; set; }
            public string total { get; set; }
            public string STATUS_MATRICULA { get; set; }
            public string DiscAgrupadora { get; set; }
            public int? idMatAgrup { get; set; }
            public string coFlagResp { get; set; }

            /// <summary>
            /// Opção escolhida pelo usuário para agrupamento
            /// </summary>
            public string AgrupadoPorSelect { get; set; }


            //Códigos importantes
            public int coModalidade { get; set; }
            public int coCur { get; set; }
            public int? coTur { get; set; }
            public string coAno { get; set; }
            public int coAlu { get; set; }
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
            public string rltLeg1
            {
                get
                {
                    return "A.A (Atividade Avaliativa)  -  A.E (Atividade Específica)  -  " +
                    "A.G (Atividade Globalizada)  -  A.P (Atividade Prática)  -  CON (Conceito)  -  PRO (Projeto)  -  " +
                    "SIM (Simulado)  -  TP1 (Teste/Prova 1)  -  TP2 (Teste/Prova 2)  -  TRA (Trabalho) - MD (Média). ";

                }
            }

            //Nota dos tipos  de atividadespublic string rltAa { get { if (rltAa == "") return "-"; else return rltAa; } }       
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
