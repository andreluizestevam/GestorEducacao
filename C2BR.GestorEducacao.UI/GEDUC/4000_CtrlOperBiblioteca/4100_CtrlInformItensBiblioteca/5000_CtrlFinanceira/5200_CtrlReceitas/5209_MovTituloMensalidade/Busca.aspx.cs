//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: COBRANÇA BANCÁRIA
// OBJETIVO: ESCOLHER BOLETOS PARA SEREM GERADOS NO ARQUIVO REMESSA
// DATA DE CRIAÇÃO: 14/03/2013
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//    DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// -----------+----------------------------+-------------------------------------
// 14/03/2013 | CAIO BARBOSA MENDONÇA      | CRIAÇÃO DA TELA
// 20/03/2013 | Caio Mendonça              | Adicionei a UF na tabela de remessa TB322
// 18/07/2013 | André Nobre Vinagre        | Implementada a lógica para pegar:
//                                         | Multa => como percentual
//                                         | Desconto => como valor
//                                         | Juros => como valor
//
//
//
//
//
// OBS.: Como não estava funcionando utilizar a master de busca em sua totalidade, 
//        achei melhor trazer a maioria das coisas para a própria página. 
//
//


//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC._5000_CtrlFinanceira._5200_CtrlReceitas._5209_MovTituloMensalidade
{
    public partial class Busca : System.Web.UI.Page
    {
        public Buscas CurrentPadraoBuscas { get { return ((Buscas)this.Master); } }
        RegistroLog registroLog = new RegistroLog();

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaUnidadeContrato();
                CarregaModalidades();
                CarregaSerieCurso();
                CarregaTurma();

                fldGrid.Visible = false;
            }
        }

        protected void PopulaGrid()
        {
            string strTipoFonte = ddlTipoFonte.SelectedValue;
            string strTipoPeriodo = ddlTipoPeriodo.SelectedValue;
            string strSituacao = "A";
            string strSituacaoPre = "R";
            int coUnidContr = ddlUnidadeContrato.SelectedValue != "" ? int.Parse(ddlUnidadeContrato.SelectedValue) : 0;

            DateTime dataInicio = DateTime.MinValue;
            DateTime dataFim = DateTime.MinValue;
            DateTime? dataVerifIni = null;
            DateTime? dataVerifFim = null;

            DateTime.TryParse(txtPeriodoDe.Text, out dataInicio);
            DateTime.TryParse(txtPeriodoAte.Text, out dataFim);

            dataVerifIni = dataInicio == DateTime.MinValue ? null : (DateTime?)dataInicio;
            dataVerifFim = dataFim == DateTime.MinValue ? null : (DateTime?)dataFim;

            int modalidade = Convert.ToInt32(ddlModalidade.SelectedValue);
            int serie = Convert.ToInt32(ddlSerieCurso.SelectedValue);
            int turma = Convert.ToInt32(ddlTurma.SelectedValue);

            // Aluno
            if (strTipoFonte == "A")//int co_emp, string nu_doc, int nu_par, DateTime dt_cad_doc
            {
                var resultado = (from tb47 in VW47_CTA_RECEB.RetornaTodosRegistros()
                                 join tb39 in TB39_HISTORICO.RetornaTodosRegistros() on tb47.CO_HISTORICO equals tb39.CO_HISTORICO into sr
                                 join al in TB07_ALUNO.RetornaTodosRegistros() on tb47.CO_ALU equals al.CO_ALU
                                 join ttb47 in TB47_CTA_RECEB.RetornaTodosRegistros() on new { tb47.CO_EMP, tb47.NU_DOC, tb47.NU_PAR, tb47.DT_CAD_DOC } equals new { ttb47.CO_EMP, ttb47.NU_DOC, ttb47.NU_PAR, ttb47.DT_CAD_DOC }
                                 from x in sr.DefaultIfEmpty()
                                 where (dataVerifIni == null || (strTipoPeriodo == "V" && tb47.DT_VEN_DOC >= dataVerifIni) ||
                                 (strTipoPeriodo == "C" && tb47.DT_CAD_DOC >= dataVerifIni) || (strTipoPeriodo == "E" && tb47.DT_EMISS_DOCTO >= dataVerifIni))
                                 && (dataVerifFim == null || (strTipoPeriodo == "V" && tb47.DT_VEN_DOC <= dataVerifFim) ||
                                 (strTipoPeriodo == "C" && tb47.DT_CAD_DOC <= dataVerifFim) || (strTipoPeriodo == "E" && tb47.DT_EMISS_DOCTO <= dataVerifFim))
                                 && tb47.TP_CLIENTE_DOC == "A" && tb47.CO_EMP == LoginAuxili.CO_EMP
                                 && (coUnidContr != 0 ? tb47.CO_EMP_UNID_CONT == coUnidContr : coUnidContr == 0)
                                 && (strSituacao != "" ? (tb47.IC_SIT_DOC == strSituacao || tb47.IC_SIT_DOC == strSituacaoPre) : 0 == 0)
                                 && ttb47.CO_NOS_NUM != null
                                 && (modalidade != 0 ? ttb47.CO_MODU_CUR == modalidade : modalidade == 0)
                                 && (serie != 0 ? ttb47.CO_CUR == serie : serie == 0)
                                 && (turma != 0 ? ttb47.CO_TUR == turma : turma == 0)
                                 && ttb47.FL_TIPO_PREV_RECEB == "B"
                                 select new
                                 {
                                     check = true,
                                     tb47.NU_DOC,
                                     tb47.NU_PAR,
                                     tb47.VR_PAR_DOC,
                                     tb47.DT_CAD_DOC,
                                     tb47.DT_VEN_DOC,
                                     tb47.DT_EMISS_DOCTO,
                                     HIST = x.DE_HISTORICO != null ? (x.DE_HISTORICO.Length > 60 ? x.DE_HISTORICO.Substring(0, 60) + "..." : x.DE_HISTORICO) : "",
                                     NU_NIRE = tb47.NU_NIRE,
                                     CPFCNPJ = al.TB108_RESPONSAVEL.NU_CPF_RESP,
                                     CO_EMP = tb47.CO_EMP,
                                     NOSSO_NUMERO = ttb47.CO_NOS_NUM,
                                     ENVIO_BANCO = ttb47.FLA_ENVIO_BANCO == "S" ? "Sim" : "Não",
                                     REMESSA = ttb47.FLA_ARQ_REMESSA == "S" ? "Sim" : "Não",
                                     AGENCIA = ttb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_AGENCIA,
                                     BANCO = ttb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.IDEBANCO,
                                     CONTA = ttb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_CONTA,
                                     CARTEIRA = ttb47.TB227_DADOS_BOLETO_BANCARIO.CO_CARTEIRA
                                 });

                if (strTipoPeriodo == "V" && (dataVerifIni != null || dataVerifFim != null))
                    grdBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderBy(c => c.DT_VEN_DOC) : null;
                else if (strTipoPeriodo == "E" && (dataVerifIni != null || dataVerifFim != null))

                    grdBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderBy(c => c.DT_EMISS_DOCTO) : null;
                else
                    grdBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderBy(c => c.DT_CAD_DOC) : null;
            }
            
            // Não é aluno
            else if (strTipoFonte == "O")
            {
                var resultado = (from tb47 in VW47_CTA_RECEB.RetornaTodosRegistros()
                                 join tb39 in TB39_HISTORICO.RetornaTodosRegistros() on tb47.CO_HISTORICO equals tb39.CO_HISTORICO into sr
                                 from x in sr.DefaultIfEmpty()
                                 join ttb47 in TB47_CTA_RECEB.RetornaTodosRegistros() on new { tb47.CO_EMP, tb47.NU_DOC, tb47.NU_PAR, tb47.DT_CAD_DOC } equals new { ttb47.CO_EMP, ttb47.NU_DOC, ttb47.NU_PAR, ttb47.DT_CAD_DOC }
                                 where (dataVerifIni == null || (strTipoPeriodo == "V" && tb47.DT_VEN_DOC >= dataVerifIni) ||
                                 (strTipoPeriodo == "C" && tb47.DT_CAD_DOC >= dataVerifIni) || (strTipoPeriodo == "E" && tb47.DT_EMISS_DOCTO >= dataVerifIni))
                                 && (dataVerifFim == null || (strTipoPeriodo == "V" && tb47.DT_VEN_DOC <= dataVerifFim) ||
                                 (strTipoPeriodo == "C" && tb47.DT_CAD_DOC <= dataVerifFim) || (strTipoPeriodo == "E" && tb47.DT_EMISS_DOCTO <= dataVerifFim))
                                 && tb47.TP_CLIENTE_DOC == "O" && tb47.CO_EMP == LoginAuxili.CO_EMP
                                 && (coUnidContr != 0 ? tb47.CO_EMP_UNID_CONT == coUnidContr : coUnidContr == 0)
                                 && (strSituacao != "" ? (tb47.IC_SIT_DOC == strSituacao || tb47.IC_SIT_DOC == strSituacaoPre) : 0 == 0)
                                 && ttb47.CO_NOS_NUM != null
                                 && ttb47.FL_TIPO_PREV_RECEB == "B"
                                 select new
                                 {
                                     check = true,
                                     tb47.NU_DOC,
                                     tb47.NU_PAR,
                                     tb47.VR_PAR_DOC,
                                     tb47.DT_CAD_DOC,
                                     tb47.DT_VEN_DOC,
                                     tb47.DT_EMISS_DOCTO,
                                     HIST = x.DE_HISTORICO != null ? (x.DE_HISTORICO.Length > 60 ? x.DE_HISTORICO.Substring(0, 60) + "..." : x.DE_HISTORICO) : "",
                                     NU_NIRE = tb47.CO_CPFCGC_CLI.Length == 14 ? tb47.CO_CPFCGC_CLI.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".") : tb47.CO_CPFCGC_CLI.Length == 11 ? tb47.CO_CPFCGC_CLI.Insert(9, "-").Insert(6, ".").Insert(3, ".") : tb47.CO_CPFCGC_CLI,
                                     CPFCNPJ = tb47.CO_CPFCGC_CLI,
                                     CO_EMP = tb47.CO_EMP,
                                     NOSSO_NUMERO = ttb47.CO_NOS_NUM,
                                     ENVIO_BANCO = ttb47.FLA_ENVIO_BANCO == "S" ? "Sim" : "Não",
                                     REMESSA = ttb47.FLA_ARQ_REMESSA == "S" ? "Sim" : "Não",
                                     AGENCIA = ttb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_AGENCIA,
                                     BANCO = ttb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.IDEBANCO,
                                     CONTA = ttb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_CONTA,
                                     CARTEIRA = ttb47.TB227_DADOS_BOLETO_BANCARIO.CO_CARTEIRA
                                 });

                if (strTipoPeriodo == "V" && (dataVerifIni != null || dataVerifFim != null))
                        grdBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderBy(c => c.DT_VEN_DOC) : null;
                else if (strTipoPeriodo == "E" && (dataVerifIni != null || dataVerifFim != null))
                        grdBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderBy(c => c.DT_EMISS_DOCTO) : null;
                else
                        grdBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderBy(c => c.DT_CAD_DOC) : null;
            }

            // Todos
            else
            {
                var resultado = (from tb47 in VW47_CTA_RECEB.RetornaTodosRegistros()
                                 join tb39 in TB39_HISTORICO.RetornaTodosRegistros() on tb47.CO_HISTORICO equals tb39.CO_HISTORICO into sr
                                 from x in sr.DefaultIfEmpty()
                                 join al in TB07_ALUNO.RetornaTodosRegistros() on tb47.CO_ALU equals al.CO_ALU into rs
                                 from y in rs.DefaultIfEmpty()
                                 join ttb47 in TB47_CTA_RECEB.RetornaTodosRegistros() on new { tb47.CO_EMP, tb47.NU_DOC, tb47.NU_PAR, tb47.DT_CAD_DOC } equals new { ttb47.CO_EMP, ttb47.NU_DOC, ttb47.NU_PAR, ttb47.DT_CAD_DOC }
                                 where (dataVerifIni == null || (strTipoPeriodo == "V" && tb47.DT_VEN_DOC >= dataVerifIni) ||
                                 (strTipoPeriodo == "C" && tb47.DT_CAD_DOC >= dataVerifIni) || (strTipoPeriodo == "E" && tb47.DT_EMISS_DOCTO >= dataVerifIni))
                                 && (dataVerifFim == null || (strTipoPeriodo == "V" && tb47.DT_VEN_DOC <= dataVerifFim) ||
                                 (strTipoPeriodo == "C" && tb47.DT_CAD_DOC <= dataVerifFim) || (strTipoPeriodo == "E" && tb47.DT_EMISS_DOCTO <= dataVerifFim))
                                 && tb47.CO_EMP == LoginAuxili.CO_EMP && (coUnidContr != 0 ? tb47.CO_EMP_UNID_CONT == coUnidContr : coUnidContr == 0)
                                 && (ddlTipoFonte.SelectedValue != "T" ? tb47.TP_CLIENTE_DOC == ddlTipoFonte.SelectedValue : ddlTipoFonte.SelectedValue == "T")
                                 && (strSituacao != "" ? (tb47.IC_SIT_DOC == strSituacao || tb47.IC_SIT_DOC == strSituacaoPre) : 0 == 0)
                                 && ttb47.CO_NOS_NUM != null
                                 && (modalidade != 0 ? ttb47.CO_MODU_CUR == modalidade : modalidade == 0)
                                 && (serie != 0 ? ttb47.CO_CUR == serie : serie == 0)
                                 && (turma != 0 ? ttb47.CO_TUR == turma : turma == 0)
                                 && ttb47.FL_TIPO_PREV_RECEB == "B"
                                 select new
                                 {
                                     check = true,
                                     tb47.NU_DOC,
                                     tb47.NU_PAR,
                                     tb47.VR_PAR_DOC,
                                     tb47.DT_CAD_DOC,
                                     tb47.DT_VEN_DOC,
                                     tb47.DT_EMISS_DOCTO,
                                     HIST = x.DE_HISTORICO != null ? (x.DE_HISTORICO.Length > 60 ? x.DE_HISTORICO.Substring(0, 60) + "..." : x.DE_HISTORICO) : "",
                                     NU_NIRE = tb47.TP_CLIENTE_DOC == "A" ? tb47.NU_NIRE :
                                         (tb47.CO_CPFCGC_CLI.Length == 14 ? tb47.CO_CPFCGC_CLI.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".") : tb47.CO_CPFCGC_CLI.Length == 11 ? tb47.CO_CPFCGC_CLI.Insert(9, "-").Insert(6, ".").Insert(3, ".") : tb47.CO_CPFCGC_CLI),
                                     CPFCNPJ = tb47.TP_CLIENTE_DOC == "A" ? y.TB108_RESPONSAVEL.NU_CPF_RESP :
                                         (tb47.CO_CPFCGC_CLI.Length == 14 ? tb47.CO_CPFCGC_CLI.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".") : tb47.CO_CPFCGC_CLI.Length == 11 ? tb47.CO_CPFCGC_CLI.Insert(9, "-").Insert(6, ".").Insert(3, ".") : tb47.CO_CPFCGC_CLI),
                                     CO_EMP = tb47.CO_EMP,
                                     NOSSO_NUMERO = ttb47.CO_NOS_NUM,
                                     ENVIO_BANCO = ttb47.FLA_ENVIO_BANCO == "S" ? "Sim" : "Não",
                                     REMESSA = ttb47.FLA_ARQ_REMESSA == "S" ? "Sim" : "Não",
                                     AGENCIA = ttb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_AGENCIA,
                                     BANCO = ttb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.IDEBANCO,
                                     CONTA = ttb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_CONTA,
                                     CARTEIRA = ttb47.TB227_DADOS_BOLETO_BANCARIO.CO_CARTEIRA
                                 });

                if (strTipoPeriodo == "V" && (dataVerifIni != null || dataVerifFim != null))
                    grdBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderBy(c => c.DT_VEN_DOC) : null;
                else if (strTipoPeriodo == "E" && (dataVerifIni != null || dataVerifFim != null))
                    grdBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderBy(c => c.DT_EMISS_DOCTO) : null;
                else
                    grdBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderBy(c => c.DT_CAD_DOC) : null;
            }


        }
        
        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>("doc", "NU_DOC"));
            queryStringKeys.Add(new KeyValuePair<string, string>("par", "NU_PAR"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion


        #region Eventos da Grid

        protected void grdBusca_DataBound(object sender, EventArgs e)
        {
            GridViewRow grdViewRow = grdBusca.BottomPagerRow;

            if (grdViewRow != null)
            {
                DropDownList ddlPaginaLista = (DropDownList)grdViewRow.Cells[0].FindControl("ddlGrdPaginas");

                if (ddlPaginaLista != null)
                    for (int i = 0; i < grdBusca.PageCount; i++)
                    {
                        int numeroPagina = i + 1;
                        ListItem lstItem = new ListItem(numeroPagina.ToString());

                        if (i == grdBusca.PageIndex)
                            lstItem.Selected = true;

                        ddlPaginaLista.Items.Add(lstItem);
                    }
            }
        }

        protected void ddlGrdPaginas_SelectedIndexChanged(Object sender, EventArgs e)
        {
            GridViewRow grdViewRow = grdBusca.BottomPagerRow;

            if (grdViewRow != null)
            {
                DropDownList ddlListaPagina = (DropDownList)grdViewRow.Cells[0].FindControl("ddlGrdPaginas");
                BindGrdBusca(ddlListaPagina.SelectedIndex);
            }
        }

        protected void grdBusca_PageIndexChanging(object sender, GridViewPageEventArgs e) { 
            BindGrdBusca(e.NewPageIndex); 
        }

        protected void AtualizaTodosCheck(object sender, EventArgs e)
        {

            CheckBox checkTot = (CheckBox)sender;

            foreach (GridViewRow linha in grdBusca.Rows)
            {
                if (grdBusca.Rows.Count > 0)
                {
                    ((CheckBox)linha.Cells[0].FindControl("check")).Checked = checkTot.Checked;
                }
            }
        }
        
        #endregion


        #region Carregamento

        /// <summary>
        /// Método que carrega o dropdown de Unidades de Contrato
        /// </summary>
        private void CarregaUnidadeContrato()
        {
            ddlUnidadeContrato.DataSource = from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                            where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                            select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP };

            ddlUnidadeContrato.DataTextField = "NO_FANTAS_EMP";
            ddlUnidadeContrato.DataValueField = "CO_EMP";
            ddlUnidadeContrato.DataBind();

            ddlUnidadeContrato.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }


        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Todas", "0"));
        }


        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "0" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string anoGrade = DateTime.Now.Year.ToString();

            if (modalidade != 0)
            {
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                            where tb01.CO_MODU_CUR == modalidade && tb01.CO_SITU == "A"
                                            join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb01.CO_CUR equals tb43.CO_CUR
                                            where tb43.CO_ANO_GRADE == anoGrade && tb43.TB44_MODULO.CO_MODU_CUR == modalidade
                                            select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy(c => c.NO_CUR);

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
            }
            else
            {
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                            where tb01.CO_SITU == "A"
                                            join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb01.CO_CUR equals tb43.CO_CUR
                                            where tb43.CO_ANO_GRADE == anoGrade
                                            select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy(c => c.NO_CUR);

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
            }

            ddlSerieCurso.Items.Insert(0, new ListItem("Todas", "0"));

            CarregaTurma();
        }


        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "0" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "0" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if ((modalidade != 0) && (serie != 0))
            {
                ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                       where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                                       select new
                                       {
                                           tb06.TB129_CADTURMAS.CO_SIGLA_TURMA,
                                           tb06.CO_TUR
                                       }).OrderBy(t => t.CO_SIGLA_TURMA);

                ddlTurma.DataTextField = "CO_SIGLA_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
            }
            else
            {
                ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                       where 
                                       (modalidade != 0 ? tb06.CO_MODU_CUR == modalidade : modalidade == 0)
                                       && (serie != 0 ? tb06.CO_CUR == serie : serie == 0)
                                       select new
                                       {
                                           tb06.TB129_CADTURMAS.CO_SIGLA_TURMA,
                                           tb06.CO_TUR
                                       }).OrderBy(t => t.CO_SIGLA_TURMA);

                ddlTurma.DataTextField = "CO_SIGLA_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
            }

            ddlTurma.Items.Insert(0, new ListItem("Todas", "0"));
        }

        #endregion


        #region Selected Changed

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            ddlSerieCurso.Enabled = true;
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            ddlTurma.Enabled = true;
        }

        #endregion


        private void BindGrdBusca(int newPageIndex)
        {
            PopulaGrid();

            fldGrid.Visible = true;
            btnBusca.Visible = true;

            grdBusca.PageIndex = newPageIndex;
            grdBusca.DataBind();
        }

        // Função do botão de buscar
        protected void btnBusca_Click(object sender, EventArgs e)
        {
            if (LoginAuxili.ORG_CODIGO_ORGAO == 0)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "LogOut", "javascript:parent.LogOut();", true);

            if (Page.IsValid)
            {
                registroLog.RegistroLOG(null, RegistroLog.ACAO_PESQUISA);
                BindGrdBusca(0);
            }
        }


        protected void ProcessaSelecionados(object sender, EventArgs e)
        {

            try
            {
                int cont = 0;

                int contt = 2;

                //if (TB322_ARQ_REM_BOLETO.RetornaTodosRegistros().Count() == 0)
                //{
                //    contt = 1;
                //}
                //else
                //{
                //    contt = TB322_ARQ_REM_BOLETO.RetornaTodosRegistros().OrderByDescending(x => x.NU_LOTE_ARQUI).FirstOrDefault().NU_LOTE_ARQUI;
                //    contt++;
                //}

                foreach (GridViewRow linha in grdBusca.Rows)
                {
                    if (grdBusca.Rows.Count > 0)
                    {
                        if (((CheckBox)linha.Cells[0].FindControl("check")).Checked == true)
                        {
                            int co_emp = Convert.ToInt32(linha.Cells[10].Text);
                            string nu_doc = linha.Cells[1].Text;
                            int nu_par = Convert.ToInt32(linha.Cells[2].Text);
                            DateTime dt_cad_doc = Convert.ToDateTime(linha.Cells[12].Text);
                            string nosso_numero = linha.Cells[11].Text;
                            int agencia = Convert.ToInt32(linha.Cells[15].Text);
                            string banco = linha.Cells[16].Text;
                            string conta = linha.Cells[17].Text;
                            string carteira = linha.Cells[18].Text;

                            TB322_ARQ_REM_BOLETO rem = RetornaRem(co_emp, nu_doc, nu_par, dt_cad_doc, nosso_numero, contt,carteira,banco,agencia,conta);

                            TB322_ARQ_REM_BOLETO.SaveOrUpdate(rem);
                            
                            cont++;
                            contt++;

                            TB47_CTA_RECEB tb47 = TB47_CTA_RECEB.RetornaPelaChavePrimaria(co_emp, nu_doc, nu_par, dt_cad_doc);
                            tb47.FLA_ARQ_REMESSA = "S";
                            TB47_CTA_RECEB.SaveOrUpdate(tb47);

                        }
                    }
                }
               
                TB322_ARQ_REM_BOLETO rm = new TB322_ARQ_REM_BOLETO();
                registroLog.RegistroLOG(rm, RegistroLog.ACAO_GRAVAR);

                fldGrid.Visible = false;

                AuxiliPagina.EnvioMensagemSucesso(this.Page, "Dados inseridos com sucesso.  " + cont.ToString() + " itens processados.");

            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, ex.ToString());
            }

        }


        protected TB322_ARQ_REM_BOLETO RetornaRem(int co_emp, string nu_doc, int nu_par, DateTime dt_cad_doc, string nosso_numero, int contt, string carteira, string banco, int agencia, string conta)
        {
            try
            {
                bool novo = true;

                TB322_ARQ_REM_BOLETO rem = TB322_ARQ_REM_BOLETO.RetornaPeloNossoNumero(nosso_numero,carteira,conta,banco,agencia);

                if (rem != null)
                {
                    novo = false;
                }
                else
                {
                    rem = new TB322_ARQ_REM_BOLETO();
                }

                TB47_CTA_RECEB tb47 = TB47_CTA_RECEB.RetornaPelaChavePrimaria(co_emp, nu_doc, nu_par, dt_cad_doc);

                tb47.TB227_DADOS_BOLETO_BANCARIOReference.Load();

                tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTEReference.Load();
                tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIAReference.Load();
                tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCOReference.Load();
                tb47.TB108_RESPONSAVELReference.Load();
                tb47.TB103_CLIENTEReference.Load();

                //--------> Recebe a Unidade
                TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(tb47.CO_EMP);

                // Busca id do cliente e do responsavel, se tiver
                int coResp = tb47.TB108_RESPONSAVEL != null ? tb47.TB108_RESPONSAVEL.CO_RESP : 0;
                int coCliente = tb47.TB103_CLIENTE != null ? tb47.TB103_CLIENTE.CO_CLIENTE : 0;

                // Busca dados do responsável
                var varResp = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                               where tb108.CO_RESP == coResp
                               join tb905 in TB905_BAIRRO.RetornaTodosRegistros() on tb108.CO_BAIRRO equals tb905.CO_BAIRRO
                               select new
                               {
                                   BAIRRO = tb905.NO_BAIRRO,
                                   CEP = tb108.CO_CEP_RESP,
                                   CIDADE = tb905.TB904_CIDADE.NO_CIDADE,
                                   CPFCNPJ = tb108.NU_CPF_RESP.Length >= 11 ? tb108.NU_CPF_RESP.Insert(9, "-").Insert(6, ".").Insert(3, ".") : tb108.NU_CPF_RESP,
                                   ENDERECO = tb108.DE_ENDE_RESP,
                                   NUMERO = tb108.NU_ENDE_RESP,
                                   COMPL = tb108.DE_COMP_RESP,
                                   NOME = tb108.NO_RESP,
                                   UF = tb905.CO_UF
                               }).FirstOrDefault();

                // Busca dados do cliente
                var varCliente = (from lTb103 in TB103_CLIENTE.RetornaTodosRegistros()
                                  where lTb103.CO_CLIENTE == coCliente
                                  select new
                                  {
                                      BAIRRO = lTb103.TB905_BAIRRO.NO_BAIRRO,
                                      CEP = lTb103.CO_CEP_CLI,
                                      CIDADE = lTb103.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE,
                                      CPFCNPJ = (lTb103.TP_CLIENTE == "F" && lTb103.CO_CPFCGC_CLI.Length >= 11) ? lTb103.CO_CPFCGC_CLI.Insert(9, "-").Insert(6, ".").Insert(3, ".") :
                                                ((lTb103.TP_CLIENTE == "J" && lTb103.CO_CPFCGC_CLI.Length >= 14) ? lTb103.CO_CPFCGC_CLI.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".") : lTb103.CO_CPFCGC_CLI),
                                      ENDERECO = lTb103.DE_END_CLI,
                                      NUMERO = lTb103.NU_END_CLI,
                                      COMPL = lTb103.DE_COM_CLI,
                                      NOME = lTb103.NO_FAN_CLI,
                                      UF = lTb103.CO_UF_CLI
                                  }).FirstOrDefault();

                // decide se vai ser utilizado cliente ou responsável
                var varSacado = coResp != 0 && coCliente == 0 ? varResp : coResp == 0 && coCliente != 0 ? varCliente : null;


                // Cedente para geração do arquivo de remessa, apenas um cedente por remessa
                rem.CO_CPFCGC_EMP = tb25.CO_CPFCGC_EMP.Trim(); // CPF
                rem.NO_RAZSOC_EMP = tb25.NO_RAZSOC_EMP.Trim(); // Razão Social
                rem.CO_AGENCIA = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.CO_AGENCIA; // Agência
                rem.DI_AGENCIA = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.DI_AGENCIA.Trim(); // Digito Agência
                rem.CO_CONTA = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_CONTA.Trim(); // Conta
                rem.CO_DIG_CONTA = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_DIG_CONTA.Trim(); // Digito conta

                // Boleto
                rem.DT_VENCIMENTO = tb47.DT_VEN_DOC; // vencimento
                rem.VL_TITULO = tb47.VR_PAR_DOC; // valor
                rem.CO_CARTEIRA = tb47.TB227_DADOS_BOLETO_BANCARIO.CO_CARTEIRA.Trim(); // carteira
                rem.NU_NOSSO_NUMERO = tb47.CO_NOS_NUM.Trim(); // nosso número

                
                //
                
                //Juros => Valor dia
                if (tb47.VR_JUR_DOC != null)
                {
                    if (tb47.CO_FLAG_TP_VALOR_JUR == "P")
                    {
                        rem.VL_JUROS = ((Convert.ToDecimal(tb47.VR_JUR_DOC) * tb47.VR_PAR_DOC) / 100);
                    }
                    else
                        rem.VL_JUROS = Convert.ToDecimal(tb47.VR_JUR_DOC);
                }

                //Multa => Percentual
                if (tb47.VR_MUL_DOC != null)
                {
                    if (tb47.CO_FLAG_TP_VALOR_MUL == "P")
                    {
                        rem.VL_MULTA = Convert.ToDecimal(tb47.VR_MUL_DOC); 
                    }
                    else
                        rem.VL_MULTA = ((Convert.ToDecimal(tb47.VR_MUL_DOC) * 100) / tb47.VR_PAR_DOC);
                }

                //Desconto => Valor
                decimal descto = 0;
                //Desconto especial
                if (tb47.VR_DES_DOC != null)
                {
                    if (tb47.CO_FLAG_TP_VALOR_DES == "P")
                    {
                        descto = descto + ((Convert.ToDecimal(tb47.VR_DES_DOC) * tb47.VR_PAR_DOC)/100);
                    }
                    else
                        descto = descto + Convert.ToDecimal(tb47.VR_DES_DOC);
                }                

                //Desconto Bolsa Aluno
                if (tb47.VL_DES_BOLSA_ALUNO != null)
                {
                    if (tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO == "P")
                    {
                        descto = descto + ((Convert.ToDecimal(tb47.VL_DES_BOLSA_ALUNO) * tb47.VR_PAR_DOC) / 100);
                    }
                    else
                        descto = descto + Convert.ToDecimal(tb47.VL_DES_BOLSA_ALUNO);
                }                
                //***********************

                rem.VL_DESCTO = descto;  // desconto
                rem.VL_OUTROS = Convert.ToDecimal(tb47.VR_OUT_DOC); // acrescimos, outros
                rem.VL_PAGO = Convert.ToDecimal(tb47.VR_PAG); // valor pago

                // Cria endereço para Sacado
                rem.NO_BAIRRO = varSacado.BAIRRO.Trim(); // Bairro
                rem.CO_CEP = varSacado.CEP.Trim(); // Cep
                rem.NO_CIDADE = varSacado.CIDADE.Trim(); // Cidade
                rem.DE_COMP = varSacado.COMPL; // Complemento
                rem.DE_ENDE = varSacado.ENDERECO; // Endereço
                rem.CO_UF = varSacado.UF; // UF

                // Sacado
                rem.NU_CPFCNPJ = varSacado.CPFCNPJ.Trim(); // CPF/CNPJ
                rem.NO_SACADO = varSacado.NOME.Trim(); // Nome

                // Número do convênio para remessa, apenas um convênio por remessa
                rem.NU_CONVENIO = tb47.TB227_DADOS_BOLETO_BANCARIO.NU_CONVENIO;

                rem.IDEBANCO = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCO.IDEBANCO;

                rem.CO_EMP_TB47 = tb47.CO_EMP;
                rem.DT_CAD_DOC_TB47 = tb47.DT_CAD_DOC;
                rem.NU_DOC_TB47 = tb47.NU_DOC;
                rem.NU_PAR_TB47 = tb47.NU_PAR;

                if (novo)
                {
                    rem.DT_CADASTRO = DateTime.Now;
                    rem.CO_COL_CADASTRO = LoginAuxili.CO_COL;
                    rem.CO_EMP_CADASTRO = LoginAuxili.CO_EMP;
                    rem.NR_IP_ACESS_CADASTRO = LoginAuxili.IP_USU;
                }

                rem.DT_ALTERACAO = DateTime.Now;
                rem.CO_EMP_ALTERACAO = LoginAuxili.CO_EMP;
                rem.CO_COL_ALTERACAO= LoginAuxili.CO_COL;
                rem.NR_IP_ACESS_ALTERACAO = LoginAuxili.IP_USU;
                rem.CO_SITU = "A";
                rem.FLA_ENVIO_BANCO = "N";
                string c = contt.ToString();

                // 000002
                for (int x = 0; x < (6 - c.Length); x++)
                {
                    c = "0" + c;
                }

                rem.NU_LOTE_ARQUI = c;


                return rem;

            }
            catch (Exception e)
            {
                return null;
            }

        }

    }




}