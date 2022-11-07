//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: DADOS/PARAMETRIZAÇÃO UNIDADES DE ENSINO
// OBJETIVO: CADASTRAMENTO DE FREQUÊNCIA FUNCIONAL
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1001_CtrlDadosParamUnidades.CadastramentoFrequFuncional
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
//--------> Faz a verificação para saber se horas informadas são válidas
            if (!ValidaHorarios()) return;

            var tb300 = RetornaEntidade();

            if (tb300 == null)
            {
                int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

                tb300 = new TB300_QUADRO_HORAR_FUNCI();

                if (coEmp == 0)
                {
                    int contadHorar = (from iTb300 in TB300_QUADRO_HORAR_FUNCI.RetornaTodosRegistros()
                                       where iTb300.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                       && iTb300.TB25_EMPRESA == null
                                       select iTb300).Count();

//----------------> Faz a verificação para saber se existem mais de 5 registros para a instituição informada
                    if (contadHorar >= 5)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Registro de horas superior a cinco para instituição selecionada.");
                        return;
                    }                    
                }
                else
                {
                    int contadUnid = (from iTb300 in TB300_QUADRO_HORAR_FUNCI.RetornaTodosRegistros()
                                      where iTb300.TB25_EMPRESA.CO_EMP == coEmp
                                      select iTb300).Count();

//----------------> Faz a verificação para saber se existem mais de 5 registros para a unidade informada
                    if (contadUnid >= 5)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Registro de horas superior a cinco para unidade selecionada.");
                        return;
                    }

                    if (ddlUnidade.SelectedValue != "")
                        tb300.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(int.Parse(ddlUnidade.SelectedValue));
                }

                tb300.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
            }
            else
            {
            }

            tb300.HR_LIMIT_ENTRA = txtLimiteEntHTP1.Text != "" ? txtLimiteEntHTP1.Text.Replace(":","") : null;
            tb300.HR_ENTRA_TURNO1 = txtTurno1EntHTP1.Text != "" ? txtTurno1EntHTP1.Text.Replace(":", "") : null;
            tb300.HR_SAIDA_TURNO1 = txtTurno1SaiHTP1.Text != "" ? txtTurno1SaiHTP1.Text.Replace(":", "") : null;
            tb300.HR_ENTRA_INTER = txtInterEntHTP1.Text != "" ? txtInterEntHTP1.Text.Replace(":", "") : null;
            tb300.HR_SAIDA_INTER = txtInterSaiHTP1.Text != "" ? txtInterSaiHTP1.Text.Replace(":", "") : null;
            tb300.HR_ENTRA_TURNO2 = txtTurno2EntHTP1.Text != "" ? txtTurno2EntHTP1.Text.Replace(":", "") : null;
            tb300.HR_SAIDA_TURNO2 = txtTurno2SaiHTP1.Text != "" ? txtTurno2SaiHTP1.Text.Replace(":", "") : null;
            tb300.HR_LIMIT_SAIDA = txtLimiteSaiHTP1.Text != "" ? txtLimiteSaiHTP1.Text.Replace(":", "") : null;
            tb300.HR_ENTRA_EXTRA = txtExtraEntHTP1.Text != "" ? txtExtraEntHTP1.Text.Replace(":", "") : null;
            tb300.HR_SAIDA_EXTRA = txtExtraSaiHTP1.Text != "" ? txtExtraSaiHTP1.Text.Replace(":", "") : null;
            tb300.HR_LIMIT_SAIDA_EXTRA = txtLimiteExtraSaiHTP1.Text != "" ? txtLimiteExtraSaiHTP1.Text.Replace(":", "") : null;
            tb300.CO_SIGLA_TIPO_PONTO = txtSiglaTipoPonto.Text;

            CurrentPadraoCadastros.CurrentEntity = tb300;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            var tb300 = RetornaEntidade();

            if (tb300 != null)
            {
                tb300.TB000_INSTITUICAOReference.Load();
                tb300.TB25_EMPRESAReference.Load();                

                if (tb300.TB25_EMPRESA != null)
                    ddlUnidade.SelectedValue = tb300.TB25_EMPRESA.CO_EMP.ToString();

                txtLimiteEntHTP1.Text = tb300.HR_LIMIT_ENTRA != null ? tb300.HR_LIMIT_ENTRA : "";
                txtTurno1EntHTP1.Text = tb300.HR_ENTRA_TURNO1 != null ? tb300.HR_ENTRA_TURNO1 : "";
                txtTurno1SaiHTP1.Text = tb300.HR_SAIDA_TURNO1 != null ? tb300.HR_SAIDA_TURNO1 : "";
                txtInterEntHTP1.Text = tb300.HR_ENTRA_INTER != null ? tb300.HR_ENTRA_INTER : "";
                txtInterSaiHTP1.Text = tb300.HR_SAIDA_INTER != null ? tb300.HR_SAIDA_INTER : "";
                txtTurno2EntHTP1.Text = tb300.HR_ENTRA_TURNO2 != null ?tb300.HR_ENTRA_TURNO2 : "";
                txtTurno2SaiHTP1.Text = tb300.HR_SAIDA_TURNO2 != null ? tb300.HR_SAIDA_TURNO2 : "";
                txtLimiteSaiHTP1.Text = tb300.HR_LIMIT_SAIDA != null ? tb300.HR_LIMIT_SAIDA : "";
                txtExtraEntHTP1.Text = tb300.HR_ENTRA_EXTRA != null ? tb300.HR_ENTRA_EXTRA : "";
                txtExtraSaiHTP1.Text = tb300.HR_SAIDA_EXTRA != null ? tb300.HR_SAIDA_EXTRA : "";
                txtLimiteExtraSaiHTP1.Text = tb300.HR_LIMIT_SAIDA_EXTRA != null ? tb300.HR_LIMIT_SAIDA_EXTRA : "";
                txtSiglaTipoPonto.Text = tb300.CO_SIGLA_TIPO_PONTO;
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB300_QUADRO_HORAR_FUNCI</returns>
        private TB300_QUADRO_HORAR_FUNCI RetornaEntidade()
        {
            return TB300_QUADRO_HORAR_FUNCI.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }

        #endregion

        #region Validações

        /// <summary>
        /// Método que faz a validação das horas
        /// </summary>
        /// <returns>True ou false</returns>
        private bool ValidaHorarios()
        {
            DateTime hora;

            if (txtLimiteEntHTP1.Text != "")
            {
                if (!(DateTime.TryParse(txtLimiteEntHTP1.Text, out hora)))
	            {
                    AuxiliPagina.EnvioMensagemErro(this, "Horário entrada de limite não é válido.");
                    return false;
	            }
            }

            if (txtTurno1EntHTP1.Text != "")
            {
                if (!(DateTime.TryParse(txtTurno1EntHTP1.Text, out hora)))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Horário entrada de turno 1 não é válido.");
                    return false;
                }
            }

            if (txtTurno1SaiHTP1.Text != "")
            {
                if (!(DateTime.TryParse(txtTurno1SaiHTP1.Text, out hora)))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Horário saída de turno 1 não é válido.");
                    return false;
                }
            }

            if (txtInterEntHTP1.Text != "")
            {
                if (!(DateTime.TryParse(txtInterEntHTP1.Text, out hora)))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Horário entrada de intervalo não é válido.");
                    return false;
                }
            }

            if (txtInterSaiHTP1.Text != "")
            {
                if (!(DateTime.TryParse(txtInterSaiHTP1.Text, out hora)))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Horário saída de intervalo não é válido.");
                    return false;
                }
            }

            if (txtTurno2EntHTP1.Text != "")
            {
                if (!(DateTime.TryParse(txtTurno2EntHTP1.Text, out hora)))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Horário entrada de turno 2 não é válido.");
                    return false;
                }
            }

            if (txtTurno2SaiHTP1.Text != "")
            {
                if (!(DateTime.TryParse(txtTurno2SaiHTP1.Text, out hora)))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Horário saída de turno 2 não é válido.");
                    return false;
                }
            }

            if (txtLimiteSaiHTP1.Text != "")
            {
                if (!(DateTime.TryParse(txtLimiteSaiHTP1.Text, out hora)))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Horário saída de limite não é válido.");
                    return false;
                }
            }

            if (txtExtraEntHTP1.Text != "")
            {
                if (!(DateTime.TryParse(txtExtraEntHTP1.Text, out hora)))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Horário entrada de extra não é válido.");
                    return false;
                }
            }

            if (txtExtraSaiHTP1.Text != "")
            {
                if (!(DateTime.TryParse(txtExtraSaiHTP1.Text, out hora)))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Horário saída de extra não é válido.");
                    return false;
                }
            }

            if (txtLimiteExtraSaiHTP1.Text != "")
            {
                if (!(DateTime.TryParse(txtLimiteExtraSaiHTP1.Text, out hora)))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Horário limite extra saída não é válido.");
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades
        /// </summary>
        private void CarregaUnidades() 
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                     select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP }).OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.Items.Insert(0, new ListItem("", ""));
        }
        #endregion        
    }
}
