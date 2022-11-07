//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: CIDADE
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
// 03/03/14 | Maxwell Almeida            | Criação da funcionalidade para cadastro de Tipos de plantões


//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;

namespace C2BR.GestorEducacao.UI.GSAUD._7000_ControleOperRH._7100_CtrlPontoFuncional._7150_CadastroPlantoes
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }
        #region Eventos
        string salvaDTSitu;
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
                CarregaUnidade();
                CarregaEspec();
                CarregaDepartamentos();
                CarregaSituacoes();
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            string hri = txtHoraIni.Text.Substring(0, 2);
            string miI = txtHoraIni.Text.Substring(3, 2);
            int hrII = int.Parse(hri);
            int miII = int.Parse(miI);

            if (hrII >= 24)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "A Hora inserida é inválida.");
            }
            else
                if (miII >= 60)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O Minuto inserido é inválido.");
                }
                else
                {

                    TB153_TIPO_PLANT tb153 = RetornaEntidade();

                    tb153.NO_TIPO_PLANT = txtNome.Text;
                    tb153.CO_SIGLA_TIPO_PLANT = txtSig.Text;
                    tb153.QT_HORAS = int.Parse(txtQtHoras.Text);
                    tb153.HR_INI_TIPO_PLANT = txtHoraIni.Text;
                    tb153.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(int.Parse(ddlUnid.SelectedValue));
                    tb153.CO_ESPEC = ddlEspec.SelectedValue != "" ? int.Parse(ddlEspec.SelectedValue) : (int?)null;
                    if (ddlLocal.SelectedValue != "")
                        tb153.TB14_DEPTO = TB14_DEPTO.RetornaPelaChavePrimaria(int.Parse(ddlLocal.SelectedValue));

                    tb153.CO_SITUA_TIPO_PLANT = ddlSitu.SelectedValue;

                    if (txtSituAlter.Text != ddlSitu.SelectedValue)
					{
						tb153.DT_SITUA_TIPO_PLANT = DateTime.Now;
					}

                    if (txtIsEd.Text == "sim")
                    {
                        tb153.CO_COL_ALTER = LoginAuxili.CO_EMP;
                        tb153.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                    }
                    else
                    {
                        tb153.DT_CADAS = DateTime.Now;
                        tb153.CO_COL_ALTER = LoginAuxili.CO_EMP;
                        tb153.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                        tb153.CO_COL = LoginAuxili.CO_COL;
                        tb153.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                    }
                    
                    CurrentPadraoCadastros.CurrentEntity = tb153;
                }
        }
        #endregion
        #region Métodos
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB153_TIPO_PLANT tb153 = RetornaEntidade();
            

            if (tb153 != null)
            {

                if (tb153 != null)
                {
                    txtIsEd.Text = "sim";
                    txtSituAlter.Text = tb153.CO_SITUA_TIPO_PLANT;
                    txtNome.Text = tb153.NO_TIPO_PLANT;
                    txtSig.Text = tb153.CO_SIGLA_TIPO_PLANT;
                    txtHoraIni.Text = tb153.HR_INI_TIPO_PLANT;
                    txtQtHoras.Text = tb153.QT_HORAS.ToString();
                    ddlSitu.SelectedValue = tb153.CO_SITUA_TIPO_PLANT;
                    ddlEspec.SelectedValue = (tb153.CO_ESPEC != null ? tb153.CO_ESPEC.ToString() : "");
                    tb153.TB14_DEPTOReference.Load();
                    ddlLocal.SelectedValue = (tb153.TB14_DEPTO != null ? tb153.TB14_DEPTO.CO_DEPTO.ToString() : "");
                    tb153.TB25_EMPRESAReference.Load();
                    ddlUnid.SelectedValue = tb153.TB25_EMPRESA.CO_EMP.ToString();
                }
            }
        }

        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB904_CIDADE</returns>
        private TB153_TIPO_PLANT RetornaEntidade()
        {
            TB153_TIPO_PLANT tb153 = TB153_TIPO_PLANT.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb153 == null) ? new TB153_TIPO_PLANT() : tb153;
        }
        #endregion

        #region Carregamentows

        /// <summary>
        /// Carrega as Unidades
        /// </summary>
        private void CarregaUnidade()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnid, LoginAuxili.ORG_CODIGO_ORGAO, false );
        }

        /// <summary>
        /// Carrega as Especialidades
        /// </summary>
        private void CarregaEspec()
        {
            int coEmp = (ddlUnid.SelectedValue != "" ? int.Parse(ddlUnid.SelectedValue) : LoginAuxili.CO_EMP);
            AuxiliCarregamentos.CarregaEspeciacialidades(ddlEspec, coEmp, null, false, false);
            ddlEspec.Items.Insert(0, new ListItem("", ""));
        }


        /// <summary>
        /// Carrega os Departamentos
        /// </summary>
        private void CarregaDepartamentos()
        {
            int coEmp = (ddlUnid.SelectedValue != "" ? int.Parse(ddlUnid.SelectedValue) : LoginAuxili.CO_EMP);
            AuxiliCarregamentos.CarregaDepartamentos(ddlLocal, coEmp, false, false);
            ddlLocal.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Carrega as situações possíveis do tipo de plantão
        /// </summary>
        private void CarregaSituacoes()
        {
            AuxiliCarregamentos.CarregaSituacaoTipoPlantao(ddlSitu, false);
        }

        #endregion

        #region Funções de Campo

        protected void ddlUnid_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDepartamentos();
            CarregaEspec();
        }

        #endregion
    }
}