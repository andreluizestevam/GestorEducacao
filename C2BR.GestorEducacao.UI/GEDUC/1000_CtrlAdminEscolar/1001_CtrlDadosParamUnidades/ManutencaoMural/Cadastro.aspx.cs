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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1001_CtrlDadosParamUnidades.ManutencaoMural
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

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    string dataAtual = DateTime.Now.ToString("dd/MM/yyyy");

                    txtDtCadasMural.Text = txtDtStatusMural.Text = dataAtual;
                }
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
//--------> Faz a verificação para saber se usuário logado é funcionário
            if (LoginAuxili.CO_COL < 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Usuário logado não cadastrado na tabela de funcionários.");
                return;
            }

            DateTime data;

//--------> Faz a verificação para saber se data inicial do mural é válida
            if (!DateTime.TryParse(txtDataInicioMural.Text,out data))
            {
                AuxiliPagina.EnvioMensagemErro(this, "Data Inicial Inválida.");
                return;
            }

//--------> Faz a verificação para saber se data final do mural é válida
            if (!DateTime.TryParse(txtDataInicioMural.Text, out data))
            {
                AuxiliPagina.EnvioMensagemErro(this, "Data Final Inválida.");
                return;
            }

//--------> Faz a verificação para saber se data inicial do mural é menor que data final do mural
            if (DateTime.Parse(txtDataInicioMural.Text) > DateTime.Parse(txtDataFinalMural.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this, "Data Inicial maior que Data Final.");
                return;
            }

//--------> Faz a verificação para saber se foi informada a URL do mural quando o tipo de URL do mural é externa
            if (ddlTipoURLMural.SelectedValue == "E" && txtURLMural.Text == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "URL Externa do Mural deve ser informada.");
                return;
            }

//--------> Faz a verificação para saber se unidade foi informada
            if (ddlTipoMural.SelectedValue == "U" && ddlUnidade.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Undidade Escolar deve ser informada.");
                return;
            }

//--------> Faz a verificação para saber se turma foi informada
            if (ddlTipoMural.SelectedValue == "T" && ddlTurma.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Turma deve ser informada.");
                return;
            }

            var tb301 = RetornaEntidade();

            if (tb301 == null)
            {
                tb301 = new TB301_MURAL();

                tb301.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
//------------> Adiciona a data atual ao campo de data cadastro quando for um novo registro
                tb301.DT_CADAS_MURAL = DateTime.Now;
                tb301.FL_ATUAL_PORTAL = "N";
            }
            else
            {
                //if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                //{
                //    string siglaConce = txtSiglaConce.Text;

                //    int ocorrConce = (from iTb200 in TB200_EQUIV_NOTA_CONCEITO.RetornaTodosRegistros()
                //                      where iTb200.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO && iTb200.CO_SIGLA_CONCEITO == siglaConce
                //                      && iTb200.ORG_CODIGO_ORGAO != tb200.ORG_CODIGO_ORGAO && iTb200.CO_SIGLA_CONCEITO != tb200.CO_SIGLA_CONCEITO
                //                      select iTb200).Count();
                //    if (ocorrConce > 0)
                //    {
                //        AuxiliPagina.EnvioMensagemErro(this, "Já existe ocorrência da sigla e instituição informada.");
                //        return;
                //    }
                //}
            }

            tb301.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL);            

            tb301.NO_TITUL_MURAL = txtTitulMural.Text;
            tb301.DE_AVISO_MURAL = txtDescAvisoMural.Text;
            tb301.NU_POSIC_MURAL = int.Parse(ddlNumPosicMural.SelectedValue);
            tb301.DT_INICIO_MURAL = DateTime.Parse(txtDataInicioMural.Text);
            tb301.DT_FINAL_MURAL = DateTime.Parse(txtDataFinalMural.Text);
            tb301.TP_URL_MURAL = ddlTipoURLMural.SelectedValue;
            tb301.DE_URL_MURAL = ddlTipoURLMural.SelectedValue == "E" ? txtURLMural.Text : null;
            tb301.TP_MURAL = ddlTipoMural.SelectedValue;

//--------> Faz a verificação para saber o tipo de mural, de acordo com o mesmo, vai definir os campos salvos
            /*
             * Se tipo == "I", define o valor NULL para TB25_EMPRESA e para TB06_TURMAS
             * Se tipo == "U", define o valor do dropDown de Unidade selecionado para TB25_EMPRESA e o valor NULL para TB06_TURMAS
             * Se tipo == "T", define o valor do dropDown de Unidade selecionado para TB25_EMPRESA e o valor do dropDown de Turma selecionado para TB06_TURMAS
             */
            if (ddlTipoMural.SelectedValue == "I")
            {
                tb301.TB25_EMPRESA = null;
                tb301.TB06_TURMAS = null;
            }
            else if (ddlTipoMural.SelectedValue == "U")
            {
                tb301.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(int.Parse(ddlUnidade.SelectedValue));
                tb301.TB06_TURMAS = null;
            }
            else if (ddlTipoMural.SelectedValue == "T")
            {
                tb301.TB06_TURMAS = TB06_TURMAS.RetornaPelaChavePrimaria(int.Parse(ddlUnidade.SelectedValue), int.Parse(ddlModalidade.SelectedValue), int.Parse(ddlSerieCurso.SelectedValue), int.Parse(ddlTurma.SelectedValue));
            }

            tb301.DT_STATUS_MURAL = DateTime.Now;
            tb301.CO_STATUS_MURAL = ddlStatusMural.SelectedValue;

            CurrentPadraoCadastros.CurrentEntity = tb301;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            var tb301 = RetornaEntidade();

            if (tb301 != null)
            {
                tb301.TB000_INSTITUICAOReference.Load();
                tb301.TB25_EMPRESAReference.Load();
                tb301.TB06_TURMASReference.Load();

                if (tb301.TB25_EMPRESA != null)
                {
                    liUnidade.Visible = true;
                    CarregaUnidades();
                    ddlUnidade.SelectedValue = tb301.TB25_EMPRESA.CO_EMP.ToString();
                }

                if (tb301.TB06_TURMAS != null)
                {
                    liUnidade.Visible = liModalidade.Visible = liSerie.Visible = liTurma.Visible = true;
                    CarregaUnidades();
                    ddlUnidade.SelectedValue = tb301.TB06_TURMAS.CO_EMP.ToString();
                    CarregaModalidades();
                    ddlModalidade.SelectedValue = tb301.TB06_TURMAS.CO_MODU_CUR.ToString();
                    CarregaSerieCurso();
                    ddlSerieCurso.SelectedValue = tb301.TB06_TURMAS.CO_CUR.ToString();
                    CarregaTurma();
                    ddlTurma.SelectedValue = tb301.TB06_TURMAS.CO_TUR.ToString();
                }

                txtTitulMural.Text = tb301.NO_TITUL_MURAL;
                txtDescAvisoMural.Text = tb301.DE_AVISO_MURAL;
                ddlNumPosicMural.SelectedValue = tb301.NU_POSIC_MURAL.ToString();
                txtDataInicioMural.Text = tb301.DT_INICIO_MURAL.ToString("dd/MM/yyyy");
                txtDataFinalMural.Text = tb301.DT_FINAL_MURAL.ToString("dd/MM/yyyy");
                ddlTipoURLMural.SelectedValue = tb301.TP_URL_MURAL;
                liURLMural.Visible = ddlTipoURLMural.SelectedValue == "E";
                txtURLMural.Text = tb301.DE_URL_MURAL != null && ddlTipoURLMural.SelectedValue == "E" ? tb301.DE_URL_MURAL : "";
                ddlTipoMural.SelectedValue = tb301.TP_MURAL;
                txtDtCadasMural.Text = tb301.DT_CADAS_MURAL.ToString("dd/MM/yyyy");
                txtDtStatusMural.Text = tb301.DT_STATUS_MURAL.ToString("dd/MM/yyyy");
                ddlStatusMural.SelectedValue = tb301.CO_STATUS_MURAL;
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB301_MURAL</returns>
        private TB301_MURAL RetornaEntidade()
        {
            return TB301_MURAL.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidades()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                   where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie && tb06.CO_EMP == coEmp
                                   select new { tb06.TB129_CADTURMAS.NO_TURMA, tb06.CO_TUR });

            ddlTurma.DataTextField = "NO_TURMA";
            ddlTurma.DataValueField = "CO_TUR";
            ddlTurma.DataBind();            
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where( m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(coEmp)
                                        where tb01.CO_MODU_CUR == modalidade
                                        select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy( c => c.NO_CUR );

            ddlSerieCurso.DataTextField = "NO_CUR";
            ddlSerieCurso.DataValueField = "CO_CUR";
            ddlSerieCurso.DataBind();
        }
        #endregion       

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
        }

        protected void ddlTipoMural_SelectedIndexChanged(object sender, EventArgs e)
        {
//--------> Faz a verificação para saber o tipo de mural
            /*
             * Se tipo == "I", desabilita os campos de Unidade, Modalidade, Série e Turma
             * Se tipo == "U", habilita o campo de Unidade e desabilita os campos de Modalidade, Série e Turma
             * Se tipo == "T", habilita o campo de Unidade, Modalidade, Série e Turma
             */
            if (ddlTipoMural.SelectedValue == "I")
            {
                liUnidade.Visible = liModalidade.Visible = liSerie.Visible = liTurma.Visible = false;
            }
            else if (ddlTipoMural.SelectedValue == "U")
            {
                CarregaUnidades();
                liUnidade.Visible = true;
                liModalidade.Visible = liSerie.Visible = liTurma.Visible = false;
            }
            else if (ddlTipoMural.SelectedValue == "T")
            {
                CarregaUnidades();
                CarregaModalidades();
                CarregaSerieCurso();
                CarregaTurma();
                liUnidade.Visible = liModalidade.Visible = liSerie.Visible = liTurma.Visible = true;
            }
        }

        protected void ddlTipoURLMural_SelectedIndexChanged(object sender, EventArgs e)
        {
//--------> Faz a verificação para saber o tipo de URL do mural, se == "E" apresenta o campo de descrição da URL do mural
            liURLMural.Visible = ddlTipoURLMural.SelectedValue == "E";
        }
    }
}
