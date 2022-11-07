//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO DE MOVIMENTAÇÃO FUNCIONAL
// OBJETIVO: REGISTRO DE MOVIMENTAÇÃO DE COLABORADORES (INTERNA)
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.App_Masters;
using System.Data.Sql;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1230_CrtlMovimentacaoFuncional.F1231_RegistroMovimentacaoColaborador
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

//--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                                              
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    txtRespMovi.Text = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL)).NO_COL;
                    CarregaUnidade();
                    CarregaColaborador();
                    CarregaDadosColaborador();
                    txtDtCad.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                }
                
                CarregaUnidDest();
                CarregaMotivAfast();
                CarregaUnidColMov();
                CarregaColDoctoMov();
                CarregaFuncao();
                CarregaDepto();
                ControleFuncDeptoMov();

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                    ddlUnidade.Enabled = ddlColaborador.Enabled = txtDataI.Enabled = false;
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coCol = ddlColaborador.SelectedValue != "" ? int.Parse(ddlColaborador.SelectedValue) : 0;
            int coFunOrigem = int.Parse(hdCodFun.Value);
            int coDeptoOrigem = int.Parse(hdCodDep.Value);

            TB286_MOVIM_TRANSF_FUNCI tb286 = RetornaEntidade();

            if (tb286 == null)
            {
                tb286 = new TB286_MOVIM_TRANSF_FUNCI();

                tb286.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                tb286.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(coCol);
                tb286.TB15_FUNCAO = TB15_FUNCAO.RetornaPelaChavePrimaria(coFunOrigem);
                tb286.TB14_DEPTO = TB14_DEPTO.RetornaPelaChavePrimaria(coDeptoOrigem);

                if ((ddlTpMov.SelectedValue == "ME") || (ddlTpMov.SelectedValue == "MI"))
                {
                    tb286.TB15_FUNCAO1 = ddlFuncao.SelectedValue != "" ? TB15_FUNCAO.RetornaPelaChavePrimaria(int.Parse(ddlFuncao.SelectedValue)) : null;
                    tb286.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(int.Parse(ddlUnidDestino.SelectedValue));
                    tb286.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(int.Parse(ddlUnidade.SelectedValue));
                    tb286.TB14_DEPTO1 = ddlDepto.SelectedValue != "" ? TB14_DEPTO.RetornaPelaChavePrimaria(int.Parse(ddlDepto.SelectedValue)) : null;
                }
                /*else if(ddlTpMov.SelectedValue == "MI")
                {
                    tb286.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(int.Parse(ddlUnidade.SelectedValue));
                    tb286.TB15_FUNCAO1 = ddlFuncao.SelectedValue != "" ? TB15_FUNCAO.RetornaPelaChavePrimaria(int.Parse(ddlFuncao.SelectedValue)) : null;
                    tb286.TB14_DEPTO1 = ddlDepto.SelectedValue != "" ? TB14_DEPTO.RetornaPelaChavePrimaria(int.Parse(ddlDepto.SelectedValue)) : null;
                }*/
                else
                {
                    tb286.TB285_INSTIT_TRANSF = TB285_INSTIT_TRANSF.RetornaPelaChavePrimaria(int.Parse(ddlInstitTransf.SelectedValue));
                    tb286.NO_FUNCAO_DESTIN = txtFunMov.Text != "" ? txtFunMov.Text : null;
                    tb286.NO_DEPTO_DESTIN = txtDepMov.Text != "" ? txtDepMov.Text : null;
                }

                tb286.CO_TIPO_MOVIM = ddlTpMov.SelectedValue;
                tb286.CO_MOTIVO_AFAST = ddlMotivAfast.SelectedValue;
                tb286.CO_TIPO_REMUN = ddlTpRemun.SelectedValue;
                tb286.CO_TIPO_PERIODO = ddlTipo.SelectedValue;
                tb286.TB03_COLABOR2 = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);

                tb286.DT_INI_MOVIM_TRANSF_FUNCI = Convert.ToDateTime(txtDataI.Text);
                if (txtDataT.Text != "")
                {
                    tb286.DT_FIM_MOVIM_TRANSF_FUNCI = Convert.ToDateTime(txtDataT.Text);
                }
                tb286.DT_CADAST = DateTime.Now;
            }

            tb286.CO_TIPO_DOCTO = ddlTpDocto.SelectedValue;

            if (txtDtEmissDocto.Text != "")
                tb286.DT_EMISS_DOCTO = DateTime.Parse(txtDtEmissDocto.Text);

            if (txtNumDocto.Text != "")
                tb286.NU_DOCTO_MOVIM_TRANSF_FUNCI = txtNumDocto.Text;

            tb286.CO_STATUS = ddlStatus.SelectedValue;
            tb286.DE_OBSER = txtObs.Text != "" ? txtObs.Text : null;

            if ((ddlUnidColMov.SelectedValue != "") && (ddlColDoctoMov.SelectedValue != ""))
                tb286.TB03_COLABOR1 = TB03_COLABOR.RetornaPeloCoCol(int.Parse(ddlColDoctoMov.SelectedValue));

            if (ddlTpMov.SelectedValue == "ME")
            {
                var tb03 = TB03_COLABOR.RetornaPeloCoCol(coCol);

                tb03.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(int.Parse(ddlUnidDestino.SelectedValue));

                TB03_COLABOR.SaveOrUpdate(tb03, true);
            }

            CurrentPadraoCadastros.CurrentEntity = tb286;
        }                                       
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB286_MOVIM_TRANSF_FUNCI tb286 = RetornaEntidade();

            if (tb286 != null)
            {
                /*
                var resultado = (from lTb286 in TB286_MOVIM_TRANSF_FUNCI.RetornaTodosRegistros()
                                 join tb03 in TB03_COLABOR.RetornaTodosRegistros() on lTb286.TB03_COLABOR.CO_COL equals tb03.CO_COL
                                 where lTb286.ID_MOVIM_TRANSF_FUNCI == tb286.ID_MOVIM_TRANSF_FUNCI
                                 select new { tb03.TB25_EMPRESA1.CO_EMP, tb03.CO_COL }).FirstOrDefault();
                */
                tb286.TB03_COLABORReference.Load();
                tb286.TB25_EMPRESAReference.Load();
                tb286.TB25_EMPRESA1Reference.Load();

                //CarregaUnidade();
                //ddlUnidade.SelectedValue = resultado.CO_EMP.ToString();                
                ddlUnidade.Items.Insert(0, new ListItem(tb286.TB25_EMPRESA1.NO_FANTAS_EMP,tb286.TB25_EMPRESA1.CO_EMP.ToString()));

                //CarregaColaborador();
                //ddlColaborador.SelectedValue = resultado.CO_COL.ToString();
                ddlColaborador.Items.Insert(0, new ListItem(tb286.TB03_COLABOR.NO_COL,tb286.TB03_COLABOR.CO_COL.ToString()));

                CarregaDadosColaborador();
                ddlTpMov.SelectedValue = tb286.CO_TIPO_MOVIM;

                tb286.TB15_FUNCAOReference.Load();
                txtFun.Text = tb286.TB15_FUNCAO != null ? tb286.TB15_FUNCAO.NO_FUN : "";

                tb286.TB14_DEPTOReference.Load();
                txtDep.Text = tb286.TB14_DEPTO != null ? tb286.TB14_DEPTO.NO_DEPTO : "";

                CarregaMotivAfast();
                ddlMotivAfast.SelectedValue = tb286.CO_MOTIVO_AFAST;

                CarregaUnidDest();
                CarregaInstitTransf();

                ControleFuncDeptoMov();

                if (ddlTpMov.SelectedValue == "TE")
                {
                    tb286.TB285_INSTIT_TRANSFReference.Load();
                    ddlInstitTransf.SelectedValue = tb286.TB285_INSTIT_TRANSF.ID_INSTIT_TRANSF.ToString();
                    txtFunMov.Text = tb286.NO_FUNCAO_DESTIN != null ? tb286.NO_FUNCAO_DESTIN : "";
                    txtDepMov.Text = tb286.NO_DEPTO_DESTIN != null ? tb286.NO_DEPTO_DESTIN : "";
                }
                else
                {
                    tb286.TB25_EMPRESAReference.Load();
                    tb286.TB14_DEPTO1Reference.Load();
                    tb286.TB15_FUNCAO1Reference.Load();

                    ddlUnidDestino.SelectedValue = tb286.TB25_EMPRESA.CO_EMP.ToString();
                    
                    if (tb286.TB14_DEPTO1 != null)
                    {
                        ddlDepto.SelectedValue = tb286.TB14_DEPTO1.CO_DEPTO.ToString();
                    }                    
                    
                    if (tb286.TB15_FUNCAO1 != null)
                    {
                        ddlFuncao.SelectedValue = tb286.TB15_FUNCAO1.CO_FUN.ToString();
                    }                    
                }

                ddlTpRemun.SelectedValue = tb286.CO_TIPO_REMUN;
                ddlTipo.SelectedValue = tb286.CO_TIPO_PERIODO;

                if (ddlTipo.SelectedValue == "F")
                    txtDataI.Enabled = txtDataT.Enabled = true;
                else
                    txtDataT.Enabled = false;

                DateTime? dtInicio = tb286.DT_INI_MOVIM_TRANSF_FUNCI;
                txtDataI.Text = dtInicio.Value.Date.ToString("dd/MM/yyyy");

                if (tb286.DT_FIM_MOVIM_TRANSF_FUNCI != null)
                {
                    DateTime? dtFim = tb286.DT_FIM_MOVIM_TRANSF_FUNCI;
                    txtDataT.Text = dtFim.Value.Date.ToString("dd/MM/yyyy");
                }

                ddlTpDocto.SelectedValue = tb286.CO_TIPO_DOCTO;

                txtNumDocto.Text = tb286.NU_DOCTO_MOVIM_TRANSF_FUNCI != null ? tb286.NU_DOCTO_MOVIM_TRANSF_FUNCI : "";

                if (tb286.DT_EMISS_DOCTO != null)
                {
                    DateTime? dtEmis = tb286.DT_EMISS_DOCTO;
                    txtDtEmissDocto.Text = dtEmis.Value.Date.ToString("dd/MM/yyyy");
                }

                tb286.TB03_COLABOR1Reference.Load();
                if (tb286.TB03_COLABOR1 != null)
                {
                    //CarregaUnidColMov();
                    tb286.TB03_COLABOR1.TB25_EMPRESA1Reference.Load();
                    ddlUnidColMov.SelectedValue = tb286.TB03_COLABOR1.TB25_EMPRESA1.CO_EMP.ToString();
                    CarregaColDoctoMov();
                    ddlColDoctoMov.SelectedValue = tb286.TB03_COLABOR1.CO_COL.ToString();
                }

                tb286.TB03_COLABOR2Reference.Load();
                txtRespMovi.Text = tb286.TB03_COLABOR2 != null ? tb286.TB03_COLABOR2.NO_COL : "";

                txtDtCad.Text = tb286.DT_CADAST.ToString("dd/MM/yyyy");
                ddlStatus.SelectedValue = tb286.CO_STATUS;
                txtObs.Text = tb286.DE_OBSER;  
            }                                                                   
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB286_MOVIM_TRANSF_FUNCI</returns>
        private TB286_MOVIM_TRANSF_FUNCI RetornaEntidade()
        {
            return TB286_MOVIM_TRANSF_FUNCI.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }
        #endregion

        #region Carregamento
        
        /// <summary>
        /// Método que carrega o dropdown de Instituição de Transferência
        /// </summary>
        private void CarregaInstitTransf()
        {
            ddlInstitTransf.DataSource = from tb285 in TB285_INSTIT_TRANSF.RetornaTodosRegistros()
                                         select new { tb285.ID_INSTIT_TRANSF, tb285.NO_INSTIT_TRANSF };

            ddlInstitTransf.DataTextField = "NO_INSTIT_TRANSF";
            ddlInstitTransf.DataValueField = "ID_INSTIT_TRANSF";
            ddlInstitTransf.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Função
        /// </summary>
        private void CarregaFuncao()
        {
            ddlFuncao.DataSource = (from tb15 in TB15_FUNCAO.RetornaTodosRegistros()
                                    select new { tb15.CO_FUN, tb15.NO_FUN });

            ddlFuncao.DataTextField = "NO_FUN";
            ddlFuncao.DataValueField = "CO_FUN";
            ddlFuncao.DataBind();
        }
        
        /// <summary>
        /// Método que carrega o dropdown de Departamento
        /// </summary>
        private void CarregaDepto()
        {
            int coEmp = ddlUnidDestino.SelectedValue != "" ? int.Parse(ddlUnidDestino.SelectedValue) : 0;

            ddlDepto.DataSource = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                                   where tb14.TB25_EMPRESA.CO_EMP == coEmp
                                   select new { tb14.CO_DEPTO, tb14.NO_DEPTO });

            ddlDepto.DataTextField = "NO_DEPTO";
            ddlDepto.DataValueField = "CO_DEPTO";
            ddlDepto.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidade de Movimentação do Colaborador
        /// </summary>
        private void CarregaUnidColMov()
        {
            ddlUnidColMov.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                        where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                        select new { tb25.sigla, tb25.CO_EMP }).OrderBy( e => e.sigla );

            ddlUnidColMov.DataTextField = "sigla";
            ddlUnidColMov.DataValueField = "CO_EMP";
            ddlUnidColMov.DataBind();

            ddlUnidColMov.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que faz o controle de visibilidade de acordo com o tipo de movimentação
        /// </summary>
        private void ControleFuncDeptoMov()
        {
            if (ddlTpMov.SelectedValue == "TE")
            {
                liddlDepto.Visible = liddlFun.Visible = liUnidDest.Visible = false;
                litxtDep.Visible = litxtFun.Visible = liInstitTransf.Visible = true;
            }
            else
            {
                txtFunMov.Text = txtDepMov.Text = "";
                liddlDepto.Visible = liddlFun.Visible = liUnidDest.Visible = true;
                litxtDep.Visible = litxtFun.Visible = liInstitTransf.Visible = false;
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Colaboradores
        /// </summary>
        private void CarregaColDoctoMov()
        {
            int coEmp = ddlUnidColMov.SelectedValue != "" ? int.Parse(ddlUnidColMov.SelectedValue) : 0;

            ddlColDoctoMov.Items.Clear();

            if (coEmp != 0)
            {
                ddlColDoctoMov.DataSource = from tb03 in TB03_COLABOR.RetornaPeloCoUnid(coEmp)
                                            select new { tb03.NO_COL, tb03.CO_COL };

                ddlColDoctoMov.DataTextField = "NO_COL";
                ddlColDoctoMov.DataValueField = "CO_COL";
                ddlColDoctoMov.DataBind();
            }

            ddlColDoctoMov.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidade de Destino
        /// </summary>
        private void CarregaUnidDest()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            if (ddlTpMov.SelectedValue == "ME")
            {
                ddlUnidDestino.DataSource = from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                            where tb25.CO_EMP != coEmp && tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                            select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP };

                ddlUnidDestino.DataTextField = "NO_FANTAS_EMP";
                ddlUnidDestino.DataValueField = "CO_EMP";
                ddlUnidDestino.DataBind();

                ddlUnidDestino.Enabled = true;
            }
            else if (ddlTpMov.SelectedValue == "MI")
            {
                ddlUnidDestino.DataSource = from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                            where tb25.CO_EMP == coEmp
                                            select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP };

                ddlUnidDestino.DataTextField = "NO_FANTAS_EMP";
                ddlUnidDestino.DataValueField = "CO_EMP";
                ddlUnidDestino.DataBind();

                ddlUnidDestino.Enabled = false;
            }
            else
            {
                ddlUnidDestino.DataSource = from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                            where tb25.CO_EMP != coEmp && tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                            select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP };

                ddlUnidDestino.DataTextField = "NO_FANTAS_EMP";
                ddlUnidDestino.DataValueField = "CO_EMP";
                ddlUnidDestino.DataBind();

                ddlUnidDestino.Enabled = true;
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Motivos de Afastamento
        /// </summary>
        private void CarregaMotivAfast()
        {
            ddlMotivAfast.Items.Clear();

            if (ddlTpMov.SelectedValue == "TE")
            {

                ddlMotivAfast.Items.Insert(0, new ListItem("Transferência Externa", "TEX"));
                ddlMotivAfast.Items.Insert(1, new ListItem("Disponibilidade", "DIS"));
                ddlMotivAfast.Items.Insert(2, new ListItem("Atividade Pontual", "APO"));
                ddlMotivAfast.Items.Insert(2, new ListItem("Outros", "OUT"));
            }
            else
            {
                ddlMotivAfast.Items.Insert(0, new ListItem("Promoção", "PRO"));
                ddlMotivAfast.Items.Insert(1, new ListItem("Transferência Interna", "TIN"));
                ddlMotivAfast.Items.Insert(2, new ListItem("Disponibilidade", "DIS"));
                    ddlMotivAfast.Items.Insert(3, new ListItem("Atividade Pontual", "APO"));
                ddlMotivAfast.Items.Insert(4, new ListItem("Férias", "FER"));
                ddlMotivAfast.Items.Insert(5, new ListItem("Licença Médica", "LME"));
                ddlMotivAfast.Items.Insert(6, new ListItem("Licença Maternidade", "LMA"));
                ddlMotivAfast.Items.Insert(7, new ListItem("Licença Paternidade", "LPA"));
                ddlMotivAfast.Items.Insert(8, new ListItem("Licença Prêmia", "LPR"));
                ddlMotivAfast.Items.Insert(9, new ListItem("Licença Funcional", "LFU"));
                ddlMotivAfast.Items.Insert(10, new ListItem("Outras Licenças", "OLI"));
                ddlMotivAfast.Items.Insert(11, new ListItem("Demissão", "DEM"));
                ddlMotivAfast.Items.Insert(12, new ListItem("Encerramento Contrato", "ECO"));
                ddlMotivAfast.Items.Insert(13, new ListItem("Afastamento", "AFA"));
                ddlMotivAfast.Items.Insert(14, new ListItem("Suspensão", "SUS"));
                ddlMotivAfast.Items.Insert(15, new ListItem("Capacitação", "CAP"));
                ddlMotivAfast.Items.Insert(16, new ListItem("Treinamento", "TRE"));
                ddlMotivAfast.Items.Insert(17, new ListItem("Motivos Outros", "MOU"));
                ddlMotivAfast.Items.Insert(18, new ListItem("Outros", "OUT"));
            }
        }

        /// <summary>
        /// Método que carrega informações do colaborador selecionado
        /// </summary>
        private void CarregaDadosColaborador()
        {
            int coCol = ddlColaborador.SelectedValue != "" ? int.Parse(ddlColaborador.SelectedValue) : 0;

            if (coCol != 0)
            {
                var colaborador = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                   join tb15 in TB15_FUNCAO.RetornaTodosRegistros() on tb03.CO_FUN equals tb15.CO_FUN
                                   join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tb03.CO_DEPTO equals tb14.CO_DEPTO
                                   where tb03.CO_COL == coCol
                                   select new { tb15.NO_FUN, tb15.CO_FUN, tb14.NO_DEPTO, tb14.CO_DEPTO }).FirstOrDefault();

//------------> Completa informações da função 
                txtFun.Text = colaborador.NO_FUN;
                hdCodFun.Value = colaborador.CO_FUN.ToString();

//------------> Completa informações do departamento
                txtDep.Text = colaborador.NO_DEPTO;
                hdCodDep.Value = colaborador.CO_DEPTO.ToString();
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidade Escolar
        /// </summary>
        private void CarregaUnidade()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        /// <summary>
        /// Método que carrega o dropdown de Colaborador
        /// </summary>
        private void CarregaColaborador()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlColaborador.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(coEmp)
                                         select new { tb03.CO_COL, tb03.NO_COL }).OrderBy( c => c.NO_COL );

            ddlColaborador.DataTextField = "NO_COL";
            ddlColaborador.DataValueField = "CO_COL";
            ddlColaborador.DataBind();

            ddlColaborador.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFun.Text = txtDep.Text = "";
            CarregaColaborador();
        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDataT.Text = "";

            if (ddlTipo.SelectedValue == "F")
                txtDataI.Enabled = txtDataT.Enabled = true;
            else
                txtDataT.Enabled = false;

        }

        protected void ddlTpMov_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaMotivAfast();
            CarregaUnidDest();
            CarregaInstitTransf();
            ControleFuncDeptoMov();
        }

        protected void ddlUnidColMov_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaColDoctoMov();
        }

        protected void ddlUnidDestino_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDepto();
        }

        protected void ddlColaborador_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDadosColaborador();
        }
    }
}
