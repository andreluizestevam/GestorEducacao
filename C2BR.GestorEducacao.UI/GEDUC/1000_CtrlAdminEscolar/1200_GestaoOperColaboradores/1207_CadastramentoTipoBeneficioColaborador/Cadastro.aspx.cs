//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO OPERACIONAL DE COLABORADORES
// OBJETIVO: CADASTRAMENTO DE TIPOS DE BENEFÍCIOS INSTITUCIONAIS A COLABORADORES
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1207_CadastramentoTipoBeneficioColaborador
{
    public partial class Cadastro : System.Web.UI.Page 
    {
        public PadraoCadastros CurrentCadastroMasterPage { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e) 
        {
            base.OnPreInit(e);

//--------> Criação das instâncias utilizadas
            CurrentCadastroMasterPage.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentCadastroMasterPage_OnAcaoBarraCadastro);
            CurrentCadastroMasterPage.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentCadastroMasterPage_OnCarregaFormulario);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaUnidades();
                CarregaFuncionarios();
                CarregaBeneficios();
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        private void CurrentCadastroMasterPage_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentCadastroMasterPage_OnAcaoBarraCadastro() 
        {
            foreach (ListItem lstChTpBenef in chklTpBeneficio.Items) 
            {
                if (lstChTpBenef.Selected)
                {
                    int idBeneficio = int.Parse(lstChTpBenef.Value);
                    int coEmp = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoEmp);
                    int coCol = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCol);

                    TB287_COLABOR_BENEF tb287 = (from lTb287 in TB287_COLABOR_BENEF.RetornaTodosRegistros()
                                                 where lTb287.TB286_TIPO_BENECIF.ID_BENEFICIO == idBeneficio 
                                                 && lTb287.TB03_COLABOR.CO_COL == coCol && lTb287.TB03_COLABOR.CO_EMP == coEmp
                                                 select lTb287).FirstOrDefault();
                    if (tb287 == null)
                    {
                        tb287 = new TB287_COLABOR_BENEF();
                        tb287.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(int.Parse(ddlColaborador.SelectedValue));
                        tb287.DT_CADASTRO = DateTime.Now;
                    }

                    tb287.TB286_TIPO_BENECIF = TB286_TIPO_BENECIF.RetornaPelaChavePrimaria(int.Parse(lstChTpBenef.Value));
                    tb287.CO_SITUACAO = "A";

                    if (tb287.EntityState != System.Data.EntityState.Unchanged && GestorEntities.SaveOrUpdate(tb287) < 1)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao salvar os itens");
                        return;
                    }
                } 
                else 
                {
                    int idBeneficio = int.Parse(lstChTpBenef.Value);
                    int coEmp = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoEmp);
                    int coCol = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCol);

                    TB287_COLABOR_BENEF tb287 = (from lTb287 in TB287_COLABOR_BENEF.RetornaTodosRegistros()
                                                 where lTb287.TB286_TIPO_BENECIF.ID_BENEFICIO == idBeneficio 
                                                 && lTb287.TB03_COLABOR.CO_COL == coCol && lTb287.TB03_COLABOR.CO_EMP == coEmp
                                                 select lTb287).FirstOrDefault();

                    if (tb287 != null && GestorEntities.Delete(tb287) < 1)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao salvar os registros");
                        return;
                    }
                }        
            }

            AuxiliPagina.RedirecionaParaPaginaSucesso("Registro editado com sucesso.", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
        }
        #endregion

        #region Carregamento Informações

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario() 
        {
            int coCol = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCol);
            int coEmp = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoEmp);

            var resultado  = from tb287 in TB287_COLABOR_BENEF.RetornaTodosRegistros()
                             where tb287.TB03_COLABOR.CO_COL == coCol && tb287.TB03_COLABOR.TB25_EMPRESA.CO_EMP == coEmp
                             select tb287.TB286_TIPO_BENECIF.ID_BENEFICIO;

            foreach (int i in resultado)
            {
                chklTpBeneficio.Items.FindByValue(i.ToString()).Selected = true;        
            }

            ddlUnidade.SelectedValue = coEmp.ToString();
            ddlColaborador.SelectedValue = coCol.ToString();
        }

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

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        /// <summary>
        /// Método que carrega o dropdown de Funcionários
        /// </summary>
        private void CarregaFuncionarios() 
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlColaborador.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(coEmp)
                                         select new { tb03.NO_COL, tb03.CO_COL }).OrderBy( c => c.NO_COL );

            ddlColaborador.DataTextField = "NO_COL";
            ddlColaborador.DataValueField = "CO_COL";
            ddlColaborador.DataBind();
        }

        /// <summary>
        /// Método que carrega o CheckBoxList de Tipos de Benefícios
        /// </summary>
        private void CarregaBeneficios() 
        {
            chklTpBeneficio.DataSource = (from tb286 in TB286_TIPO_BENECIF.RetornaTodosRegistros()
                                          where tb286.CO_SITUACAO == "A" && tb286.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                          select new { tb286.ID_BENEFICIO, tb286.NO_BENEFICIO }).OrderBy( t => t.NO_BENEFICIO );

            chklTpBeneficio.DataTextField = "NO_BENEFICIO";
            chklTpBeneficio.DataValueField = "ID_BENEFICIO";
            chklTpBeneficio.DataBind();      
        }
        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e) 
        {
            CarregaFuncionarios();
        }
    }
}
