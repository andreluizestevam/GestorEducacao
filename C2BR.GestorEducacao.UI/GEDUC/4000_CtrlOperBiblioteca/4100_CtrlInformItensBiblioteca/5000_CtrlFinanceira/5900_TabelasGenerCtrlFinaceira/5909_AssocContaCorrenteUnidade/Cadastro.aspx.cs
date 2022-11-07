//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: **********************
// SUBMÓDULO: *******************
// OBJETIVO: ********************
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5900_TabelasGenerCtrlFinaceira.F5909_AssocContaCorrenteUnidade
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
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CarregaUnidades();
                CarregaBancos();
                CarregaGridContas();
            }
        }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            GestorEntities.CurrentContext.SaveChanges();
            AuxiliPagina.RedirecionaParaPaginaSucesso("Associações atualizadas com sucesso!", HttpContext.Current.Request.Url.AbsoluteUri);
        }
        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o dropdown de Unidades
        /// </summary>
        private void CarregaUnidades()
        {           
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     && tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Bancos
        /// </summary>
        private void CarregaBancos()
        {
            ddlBanco.DataSource = (from tb29 in TB29_BANCO.RetornaTodosRegistros().AsEnumerable()
                                   select new { tb29.IDEBANCO, DESBANCO = string.Format("{0} - {1}", tb29.IDEBANCO, tb29.DESBANCO) }).OrderBy(b => b.DESBANCO);

            ddlBanco.DataValueField = "IDEBANCO";
            ddlBanco.DataTextField = "DESBANCO";
            ddlBanco.DataBind();

            ddlBanco.Items.Insert(0, new ListItem("Todos", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Contas
        /// </summary>
        private void CarregaGridContas()
        {
            string strIdeBanco = ddlBanco.SelectedValue;

            var resultado = (from tb224 in TB224_CONTA_CORRENTE.RetornaTodosRegistros().Include(string.Format("{0}.{1}", typeof(TB30_AGENCIA).Name, typeof(TB29_BANCO).Name)).Include(typeof(TB000_INSTITUICAO).Name).AsEnumerable()
                             where (strIdeBanco != "" ? tb224.TB30_AGENCIA.TB29_BANCO.IDEBANCO == strIdeBanco : strIdeBanco == "")
                             && (tb224.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO)
                             select new
                             {
                                tb224.TB30_AGENCIA.TB29_BANCO.IDEBANCO, tb224.TB30_AGENCIA.TB29_BANCO.DESBANCO, tb224.CO_AGENCIA,
                                tb224.TB30_AGENCIA.NO_AGENCIA, tb224.CO_CONTA, tb224.CO_DIG_CONTA,
                                CO_CONTA_DIG = string.Format("{0}-{1}", tb224.CO_CONTA.Trim(), tb224.CO_DIG_CONTA.Trim()),
                                CO_STATUS = tb224.CO_STATUS.Equals("A") ? "Ativa" : tb224.CO_STATUS.Equals("I") ? "Inativa" :
                                            tb224.CO_STATUS.Equals("B") ? "Bloqueada" : tb224.CO_STATUS.Equals("E") ? "Encerrada" : "",
                                FLAG_EMITE_BOLETO_BANC = tb224.FLAG_EMITE_BOLETO_BANC.Equals("S") ? "Sim" : "Não"
                             }).OrderBy( c => c.DESBANCO ).ThenBy( c => c.CO_AGENCIA ).ThenBy( c => c.CO_CONTA ).ToList();

            grdContas.DataSource = (resultado.Count() > 0) ? resultado : null;
            grdContas.DataBind();
        }
        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGridContas();
        }

        protected void ddlBanco_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGridContas();
        }

        protected void grdContas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox ckSelect = ((CheckBox)e.Row.FindControl("ckSelect"));

                ckSelect.Enabled = true;

                int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

                HiddenField hfBanco = ((HiddenField)e.Row.FindControl("hiddenBanco"));
                HiddenField hfAgencia = ((HiddenField)e.Row.FindControl("hiddenAgencia"));
                HiddenField hfConta = ((HiddenField)e.Row.FindControl("hiddenConta"));

                string strIdeBanco = hfBanco.Value;
                int coAgencia = int.Parse(hfAgencia.Value);
                string coConta = hfConta.Value;

//------------> Faz a verificação para saber se a Conta Corrente já está associada a Unidade
                TB225_CONTAS_UNIDADE tb225 = (from lTb225 in TB225_CONTAS_UNIDADE.RetornaTodosRegistros()
                                              where (lTb225.CO_EMP == coEmp && lTb225.IDEBANCO == strIdeBanco && lTb225.CO_AGENCIA == coAgencia
                                              && lTb225.CO_CONTA == coConta)
                                              select lTb225).FirstOrDefault();

                if (tb225 != null)
                    ckSelect.Checked = true;
                else
                    ckSelect.Checked = false;
            }
        }

        protected void ddlAgencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGridContas();
        }

        protected void ckSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            GridViewRow linha = (GridViewRow)checkBox.NamingContainer;

            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            HiddenField hfBanco = ((HiddenField)linha.Cells[0].FindControl("hiddenBanco"));
            HiddenField hfAgencia = ((HiddenField)linha.Cells[0].FindControl("hiddenAgencia"));
            HiddenField hfConta = ((HiddenField)linha.Cells[0].FindControl("hiddenConta"));

            string strIdeBanco = hfBanco.Value;
            int coAgencia = int.Parse(hfAgencia.Value);
            string coConta = hfConta.Value;

//------------> Faz a verificação para saber se a Conta Corrente já está associada a Unidade
            TB225_CONTAS_UNIDADE tb225 = (from lTb225 in TB225_CONTAS_UNIDADE.RetornaTodosRegistros()
                                          where (lTb225.CO_EMP == coEmp && lTb225.IDEBANCO == strIdeBanco
                                          && lTb225.CO_AGENCIA == coAgencia && lTb225.CO_CONTA == coConta)
                                          select lTb225).FirstOrDefault();

//--------> Faz a verificação para saber se a conta foi selecionada
            if (checkBox.Checked == true)
            {
//------------> Se a Conta não estiver associada a Unidade então cria a associação
                if (tb225 == null)
                {
                    tb225 = new TB225_CONTAS_UNIDADE();

                    tb225.CO_EMP = coEmp;
                    tb225.IDEBANCO = strIdeBanco;
                    tb225.CO_AGENCIA = coAgencia;
                    tb225.CO_CONTA = coConta;
                    tb225.TB224_CONTA_CORRENTE = TB224_CONTA_CORRENTE.RetornaPelaChavePrimaria(strIdeBanco, coAgencia, coConta);

                    TB225_CONTAS_UNIDADE.SaveOrUpdate(tb225, true);
                }
            }
            else
            {
//------------> Se existe uma associação, mas a conta não foi selecionada na grid. Então remove a associação
                if (tb225 != null)
                    TB225_CONTAS_UNIDADE.Delete(tb225, true);
            }
            CarregaGridContas();
        }
    }
}
