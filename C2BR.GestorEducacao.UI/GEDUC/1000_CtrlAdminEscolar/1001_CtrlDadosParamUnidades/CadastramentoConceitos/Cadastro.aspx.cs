//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: DADOS/PARAMETRIZAÇÃO UNIDADES DE ENSINO
// OBJETIVO: CADASTRAMENTO DE CONCEITOS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 15/04/2013| André Nobre Vinagre        | Colocada a condição da unidade ter tipo de
//           |                            | avaliação como "C"onceito
//           |                            | 

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Web.UI.WebControls;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1001_CtrlDadosParamUnidades.CadastramentoConceitos
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
                txtInstituicao.Text = LoginAuxili.ORG_NOME_ORGAO;
                CarregaUnidades();
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
//--------> Faz a verificação para que nota inicial seja menor que 10            
            if (Decimal.Parse(txtNotaIni.Text) > 10)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Nota Inicial não pode ser maior que 10.");
                return;
            }

//--------> Faz a verificação para que nota final seja menor que 10
            if (Decimal.Parse(txtNotaFim.Text) > 10)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Nota Final não pode ser maior que 10.");
                return;
            }

//--------> Faz a verificação para que nota inicial não seja maior que nota final
            if (Decimal.Parse(txtNotaIni.Text) > Decimal.Parse(txtNotaFim.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this, "Nota Inicial deve ser menor que Nota Final.");
                return;
            }

            var tb200 = RetornaEntidade();

            if (tb200 == null)
            {
                string siglaConce = txtSiglaConce.Text;

//------------> Faz a verificação para saber se já existe ocorrência da Instituição e sigla informada
                int ocorrConce = (from iTb200 in TB200_EQUIV_NOTA_CONCEITO.RetornaTodosRegistros()
                                  where iTb200.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO && iTb200.CO_SIGLA_CONCEITO == siglaConce
                                  select iTb200).Count();

                if (ocorrConce > 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Já existe ocorrência da sigla e instituição informada.");
                    return;
                }

                int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

                tb200 = new TB200_EQUIV_NOTA_CONCEITO();

                if (coEmp == 0)
                {
                    int contadConce = (from iTb200 in TB200_EQUIV_NOTA_CONCEITO.RetornaTodosRegistros()
                                       where iTb200.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO && iTb200.TB25_EMPRESA == null
                                       select iTb200).Count();

//----------------> Faz a verificação para saber se existem mais de 5 registros para a instituição informada
                    if (contadConce >= 5)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Registro de conceitos superior a cinco para instituição selecionada.");
                        return;
                    }
                }
                else
                {                    
                    int contadUnid = (from iTb200 in TB200_EQUIV_NOTA_CONCEITO.RetornaTodosRegistros()
                                      where iTb200.TB25_EMPRESA.CO_EMP == coEmp
                                      select iTb200).Count();

//----------------> Faz a verificação para saber se existem mais de 5 registros para a unidade informada
                    if (contadUnid >= 5)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Registro de conceitos superior a cinco para unidade selecionada.");
                        return;
                    }

                    if (ddlUnidade.SelectedValue != "")
                        tb200.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(int.Parse(ddlUnidade.SelectedValue));
                }

                tb200.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

                tb200.CO_SIGLA_CONCEITO = txtSiglaConce.Text.ToUpper();
            }
            else            
            {
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                {
                    string siglaConce = txtSiglaConce.Text;

//----------------> Faz a verificação para saber se já existe ocorrência da Instituição e sigla informada na alteração
                    int ocorrConce = (from iTb200 in TB200_EQUIV_NOTA_CONCEITO.RetornaTodosRegistros()
                                      where iTb200.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO && iTb200.CO_SIGLA_CONCEITO == siglaConce
                                      && iTb200.ORG_CODIGO_ORGAO != tb200.ORG_CODIGO_ORGAO && iTb200.CO_SIGLA_CONCEITO != tb200.CO_SIGLA_CONCEITO
                                      select iTb200).Count();

                    if (ocorrConce > 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Já existe ocorrência da sigla e instituição informada.");
                        return;
                    }
                }                
            }

            tb200.DE_CONCEITO = txtDescrConce.Text;            
            tb200.VL_NOTA_MIN = Decimal.Parse(txtNotaIni.Text);
            tb200.VL_NOTA_MAX = Decimal.Parse(txtNotaFim.Text);
            tb200.CO_SITU_CONC = ddlSituacao.SelectedValue;
            tb200.VL_NOTA_ABSOL = decimal.Parse(txtNotaAbsol.Text);

            CurrentPadraoCadastros.CurrentEntity = tb200;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            var tb200 = RetornaEntidade();

            if (tb200 != null)
            {
                tb200.TB25_EMPRESAReference.Load();

                if (tb200.TB25_EMPRESA != null)
                    ddlUnidade.SelectedValue = tb200.TB25_EMPRESA.CO_EMP.ToString();

                txtDescrConce.Text = tb200.DE_CONCEITO;
                txtSiglaConce.Text = tb200.CO_SIGLA_CONCEITO;
                txtSiglaConce.Enabled = false;
                txtNotaIni.Text = tb200.VL_NOTA_MIN.ToString();
                txtNotaFim.Text = tb200.VL_NOTA_MAX.ToString();
                ddlSituacao.SelectedValue = tb200.CO_SITU_CONC;
                txtNotaAbsol.Text = tb200.VL_NOTA_ABSOL.ToString();
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB200_EQUIV_NOTA_CONCEITO</returns>
        private TB200_EQUIV_NOTA_CONCEITO RetornaEntidade()
        {
            return TB200_EQUIV_NOTA_CONCEITO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave("idOrgao"), QueryStringAuxili.RetornaQueryStringPelaChave("sigla"));
        }
        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o dropdown de Unidades
        /// </summary>
        private void CarregaUnidades() 
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, false);
        }
        #endregion       
    }
}
