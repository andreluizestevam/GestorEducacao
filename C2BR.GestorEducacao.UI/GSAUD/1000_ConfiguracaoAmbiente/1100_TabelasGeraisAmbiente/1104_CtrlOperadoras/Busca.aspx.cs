//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: GERENCIAMENTO DE USUÁRIOS DO GE
// OBJETIVO: MANUTENÇÃO DE TIPO DE PERFIL DE ACESSO.
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
// 09/12/2014| Maxwell Almeida           | Criação da funcionalidade Busca por Operadoras

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1104_CtrlOperadoras
{
    public partial class Busca : System.Web.UI.Page
    {
        public PadraoBuscas CurrentPadraoBuscas { get { return ((PadraoBuscas)this.Master); } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            CurrentPadraoBuscas.OnAcaoBuscaDefineGridView += new PadraoBuscas.OnAcaoBuscaDefineGridViewHandler(CurrentPadraoBuscas_OnAcaoBuscaDefineGridView);
            CurrentPadraoBuscas.OnDefineColunasGridView += new PadraoBuscas.OnDefineColunasGridViewHandler(CurrentPadraoBuscas_OnDefineColunasGridView);
            CurrentPadraoBuscas.OnDefineQueryStringIds += new PadraoBuscas.OnDefineQueryStringIdsHandler(CurrentPadraoBuscas_OnDefineQueryStringIds);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //VerificaOperServiPublico();
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_OPER" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NU_CNPJ_OPER",
                HeaderText = "CNPJ",
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NOM_OPER",
                HeaderText = "Nome"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_SIGLA_OPER",
                HeaderText = "Sigla"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_OPER",
                HeaderText = "Código"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SITU",
                HeaderText = "Situação"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            string strCNPJ = txtCNPJ.Text.Replace(".", "").Replace("/", "").Replace("-", "");

            var resultado = (from tb250 in TB250_OPERA.RetornaTodosRegistros()
                             where (txtNomeOper.Text != "" ? tb250.NOM_OPER.Contains(txtNomeOper.Text) : txtNomeOper.Text == "")
                             && (txtCNPJ.Text != "" ? tb250.NU_CNPJ_OPER.Equals(strCNPJ) : txtCNPJ.Text == "")
                              && (txtSigla.Text != "" ? tb250.NM_SIGLA_OPER.Equals(txtSigla.Text) : txtSigla.Text == "")
                             && (ddlSitu.SelectedValue != "0" ? tb250.FL_SITU_OPER == ddlSitu.SelectedValue : ddlSitu.SelectedValue == "0")
                             select new
                             {
                                 tb250.ID_OPER,
                                 NU_CNPJ_OPER = tb250.NU_CNPJ_OPER.Length == 11 ? tb250.NU_CNPJ_OPER.Insert(9, "-").Insert(6, ".").Insert(3, ".") : tb250.NU_CNPJ_OPER.Insert(2, ".").Insert(6, ".").Insert(9, "/").Insert(15, "-"),
                                 NOM_OPER = tb250.NOM_OPER,
                                 NM_SIGLA_OPER = tb250.NM_SIGLA_OPER,
                                 CO_OPER = tb250.CO_OPER,
                                 CO_SITU = tb250.FL_SITU_OPER == "A" ? "Ativo" : tb250.FL_SITU_OPER == "I" ? "Inativo" : tb250.FL_SITU_OPER == "S" ? "Suspenso" : " - ",
                             }).OrderBy(e => e.NOM_OPER);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_OPER"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Verifica se existe a operadora de serviço publico e cadastra caso não exista
        /// </summary>
        private void VerificaOperServiPublico()
        {
            bool temServiPublico = TB250_OPERA.RetornaTodosRegistros().Where(w => w.CO_OPER == "999").Any();

            if (!temServiPublico)
            {
                TB250_OPERA tb250 = new TB250_OPERA();
                tb250.NOM_OPER = "Serviço Público";
                tb250.NU_CNPJ_OPER = "99999999999999";
                tb250.ST_OPER = "A";
                tb250.CO_COL_CADAS = LoginAuxili.CO_COL;
                tb250.DT_CADAS = DateTime.Now;
                tb250.IP_CADAS = Request.UserHostAddress;
                tb250.CO_COL_SITU = LoginAuxili.CO_COL;
                tb250.FL_SITU_OPER = "A";
                tb250.DT_SITU_OPER = DateTime.Now;
                tb250.IP_SITU_OPER = Request.UserHostAddress;
                tb250.CO_OPER = "999";
                tb250.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                tb250.CO_EMP_SITU = LoginAuxili.CO_EMP;
                tb250.FL_INSTI_OPERA = "S";
                TB250_OPERA.SaveOrUpdate(tb250, true);
            }
        }

        #endregion
    }
}