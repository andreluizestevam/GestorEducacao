//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO OPERACIONAL DE COLABORADORES
// OBJETIVO: ASSOCIAÇÃO DE FUNÇÕES/CARGOS FUNCIONAIS A COLABORADORES
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1204_RegistroFuncaoCargoColaborador
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
                CarregaUnidades();
                CarregaColaboradores();
                CarregaDadosColaborador();
                CarregaUnidadesDestino();
                CarregaFuncoes();
                CarregaDpto();
                txtResponsavel.Text = LoginAuxili.NOME_USU_LOGADO;

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                    ddlUnidadeDestino.Enabled = true;
                }
                else if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                    ddlUnidade.Enabled = ddlColaborador.Enabled = txtDataInicio.Enabled = false;
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }


//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int coCol = ddlColaborador.SelectedValue != "" ? int.Parse(ddlColaborador.SelectedValue) : 0;
            int coEmp = ddlUnidadeDestino.SelectedValue != "" ? int.Parse(ddlUnidadeDestino.SelectedValue) : 0;
            int coFun = ddlFuncaoDestino.SelectedValue != "" ? int.Parse(ddlFuncaoDestino.SelectedValue) : 0;
            int coDepto = ddlDeptoDestino.SelectedValue != "" ? int.Parse(ddlDeptoDestino.SelectedValue) : 0;

            TB59_GESTOR_UNIDAD tb59 = RetornaEntidade();

            if (tb59 == null)
            {
                tb59 = new TB59_GESTOR_UNIDAD();

                tb59.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                var tb03 = TB03_COLABOR.RetornaPeloCoCol(coCol);
                tb59.TB03_COLABOR = tb03;
                tb03.TB25_EMPRESA1Reference.Load();
                tb59.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(tb03.TB25_EMPRESA1.CO_EMP);
                tb59.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
                tb59.TB15_FUNCAO = TB15_FUNCAO.RetornaPelaChavePrimaria(coFun);                
                tb59.DT_INICIO_ATIVID = Convert.ToDateTime(txtDataInicio.Text);
            }

            if (ddlDeptoDestino.SelectedValue != "")
                tb59.TB14_DEPTO = TB14_DEPTO.RetornaPelaChavePrimaria(coDepto);
//--------> Só é permitida a alteraração dos campos "Status" e "Observação"
            tb59.DT_TERMIN_ATIVID = txtDataFim.Text != "" ? (DateTime?)Convert.ToDateTime(txtDataFim.Text) : null;
            tb59.CO_SITU_GEST = ddlStatus.SelectedValue;
            tb59.DE_OBS = txtObservacao.Text;
            tb59.DT_CADASTRO = DateTime.Now; 
            tb59.TB03_COLABOR1 = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);
                       
            CurrentPadraoCadastros.CurrentEntity = tb59;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB59_GESTOR_UNIDAD tb59 = RetornaEntidade();

            if (tb59 != null)
            {
                tb59.TB14_DEPTOReference.Load();

                txtDataCadastro.Text = tb59.DT_CADASTRO.ToString("dd/MM/yyyy");

                var resultado = (from m in TB59_GESTOR_UNIDAD.RetornaTodosRegistros()
                                 join c in TB03_COLABOR.RetornaTodosRegistros()
                                 on m.TB03_COLABOR.CO_COL equals c.CO_COL
                                 where m.IDE_GESTOR_UNIDAD.Equals(tb59.IDE_GESTOR_UNIDAD)
                                 && m.TB03_COLABOR.CO_EMP.Equals(c.CO_EMP)
                                 select new { c.CO_EMP, CO_UNID = c.TB25_EMPRESA1.CO_EMP, c.CO_COL }).FirstOrDefault();

                ddlUnidade.SelectedValue = resultado.CO_UNID.ToString();
                CarregaColaboradores();
                ddlColaborador.SelectedValue = resultado.CO_COL.ToString();
                CarregaDadosColaborador();
                DateTime? dataInicio = tb59.DT_INICIO_ATIVID;
                txtDataInicio.Text = dataInicio.Value.Date.ToString("dd/MM/yyyy");
                txtDataFim.Text = tb59.DT_TERMIN_ATIVID == null ? "" : ((DateTime)tb59.DT_TERMIN_ATIVID).ToString("dd/MM/yyyy");                

                txtObservacao.Text = tb59.DE_OBS;

                CarregaUnidadesDestino();
                tb59.TB25_EMPRESAReference.Load();
                ddlUnidadeDestino.SelectedValue = tb59.TB25_EMPRESA.CO_EMP.ToString();

                tb59.TB15_FUNCAOReference.Load();
                CarregaFuncoes();
                ddlFuncaoDestino.SelectedValue = tb59.TB15_FUNCAO.CO_FUN.ToString();

                CarregaDpto();
                tb59.TB14_DEPTOReference.Load();
                if (tb59.TB14_DEPTO != null)
                    ddlDeptoDestino.SelectedValue = tb59.TB14_DEPTO.CO_DEPTO.ToString();
            }                                        
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidae TB59_GESTOR_UNIDAD</returns>
        private TB59_GESTOR_UNIDAD RetornaEntidade()
        {
            return TB59_GESTOR_UNIDAD.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }

        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidades()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Colaboradores
        /// </summary>
        private void CarregaColaboradores()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlColaborador.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(coEmp)
                                         select new { tb03.NO_COL, tb03.CO_COL }).OrderBy( c => c.NO_COL );

            ddlColaborador.DataTextField = "NO_COL";
            ddlColaborador.DataValueField = "CO_COL";
            ddlColaborador.DataBind();

            ddlColaborador.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o informações do colaborador escolhido
        /// </summary>
        private void CarregaDadosColaborador()
        {
            int coCol = ddlColaborador.SelectedValue != "" ? int.Parse(ddlColaborador.SelectedValue) : 0;
            
            if (coCol == 0)
                return;

            var tb03 = (from lTb03 in TB03_COLABOR.RetornaTodosRegistros()
                       join tb15 in TB15_FUNCAO.RetornaTodosRegistros() on lTb03.CO_FUN equals tb15.CO_FUN
                        where lTb03.CO_COL == coCol
                       select new { tb15.NO_FUN, lTb03.CO_DEPTO }).FirstOrDefault();

            if (tb03 != null)
            {
                txtFuncao.Text = tb03.NO_FUN;
                txtDepartamento.Text = tb03.CO_DEPTO != null ? TB14_DEPTO.RetornaPelaChavePrimaria((int)tb03.CO_DEPTO).NO_DEPTO : "";
            }            
        }

        /// <summary>
        /// Método que carrega o dropdown de Função
        /// </summary>
        private void CarregaFuncoes()
        {
            int coCol = ddlColaborador.SelectedValue != "" ? int.Parse(ddlColaborador.SelectedValue) : 0;

            if (coCol == 0)
                return;

            int funcaoAtual = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                               where tb03.CO_COL == coCol
                               select new { tb03.CO_FUN, tb03.CO_DEPTO }).FirstOrDefault().CO_FUN;

            ddlFuncaoDestino.DataSource = (from tb15 in TB15_FUNCAO.RetornaTodosRegistros()
                                           where tb15.CO_FUN != funcaoAtual && ((bool)tb15.CO_FLAG_CLASSI_ADMINI)
                                           select new { tb15.CO_FUN, tb15.NO_FUN }).OrderBy( f => f.NO_FUN );

            ddlFuncaoDestino.DataTextField = "NO_FUN";
            ddlFuncaoDestino.DataValueField = "CO_FUN";
            ddlFuncaoDestino.DataBind();

            ddlFuncaoDestino.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares de Destino
        /// </summary>
        private void CarregaUnidadesDestino()
        {
            ddlUnidadeDestino.DataSource = ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                                                    join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                                                    where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                                                    select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidadeDestino.DataTextField = "NO_FANTAS_EMP";
            ddlUnidadeDestino.DataValueField = "CO_EMP";
            ddlUnidadeDestino.DataBind();

            ddlUnidadeDestino.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Departamento de Destino
        /// </summary>
        private void CarregaDpto()
        {
            int coEmp = ddlUnidadeDestino.SelectedValue != "" ? int.Parse(ddlUnidadeDestino.SelectedValue) : 0;

            ddlDeptoDestino.DataSource = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                                          where tb14.TB25_EMPRESA.CO_EMP == coEmp
                                          select new { tb14.NO_DEPTO, tb14.CO_DEPTO });

            ddlDeptoDestino.DataTextField = "NO_DEPTO";
            ddlDeptoDestino.DataValueField = "CO_DEPTO";
            ddlDeptoDestino.DataBind();

            ddlDeptoDestino.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion     
   
        #region Validadores

        protected void cvDataInicio_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if (txtDataInicio.Text != "" && txtDataFim.Text != "")
            {
                if (DateTime.Parse(txtDataInicio.Text) > DateTime.Parse(txtDataFim.Text))
                {
                    e.IsValid = false;
                    AuxiliPagina.EnvioMensagemErro(this, "");
                }
            }
        }

        protected void cvDataFim_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if (txtDataFim.Text != "" && txtDataInicio.Text != "")
            {
                if (DateTime.Parse(txtDataFim.Text) < DateTime.Parse(txtDataInicio.Text))
                {
                    e.IsValid = false;
                    AuxiliPagina.EnvioMensagemErro(this, "");
                }
            }
        }
        #endregion

        protected void ddlUnidadeDestino_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlFuncaoDestino.Enabled = ddlDeptoDestino.Enabled = true;

            CarregaFuncoes();
            CarregaDpto();
        }

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaColaboradores();
            CarregaUnidadesDestino();
        }

        protected void ddlColaborador_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDadosColaborador();
            CarregaFuncoes();
        }
    }
}