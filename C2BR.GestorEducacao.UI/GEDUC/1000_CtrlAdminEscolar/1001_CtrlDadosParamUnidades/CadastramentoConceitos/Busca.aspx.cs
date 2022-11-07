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
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 15/04/2013| André Nobre Vinagre        | Colocada a condição da unidade ter tipo de
//           |                            | avaliação como "C"onceito
//           |                            | 

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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1001_CtrlDadosParamUnidades.CadastramentoConceitos
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
            if (!IsPostBack)
                CarregaUnidades();
        } 

        private void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ORG_CODIGO_ORGAO", "CO_SIGLA_CONCEITO" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_FANTAS_EMP",
                HeaderText = "Unidade"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_CONCEITO",
                HeaderText = "Nome"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SIGLA_CONCEITO",
                HeaderText = "Sigla"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "VL_NOTA_MIN",
                HeaderText = "Nt Min"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "VL_NOTA_MAX",
                HeaderText = "Nt Max"
            });
        }        

        private void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView() 
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            string des = txtNome.Text;
            string sgl = txtSgl.Text;
            string situ = ddlSituacao.SelectedValue;

            var resultado = (from tb200 in TB200_EQUIV_NOTA_CONCEITO.RetornaTodosRegistros()      
                             where (coEmp != 0 ? tb200.TB25_EMPRESA.CO_EMP.Equals(coEmp) : 0 == 0)
                             && tb200.ORG_CODIGO_ORGAO.Equals(LoginAuxili.ORG_CODIGO_ORGAO)
                             && (!string.IsNullOrEmpty(des) ? tb200.DE_CONCEITO.Contains(des) : 0 == 0)
                             && (!string.IsNullOrEmpty(sgl) ? tb200.CO_SIGLA_CONCEITO.Contains(sgl) : 0 == 0)
                             && tb200.CO_SITU_CONC == situ
                             select new 
                             {
                                tb200.ORG_CODIGO_ORGAO, tb200.CO_SIGLA_CONCEITO, tb200.TB25_EMPRESA.NO_FANTAS_EMP,
                                tb200.DE_CONCEITO, tb200.VL_NOTA_MIN, tb200.VL_NOTA_MAX,
                             }).Distinct().OrderBy( c => c.NO_FANTAS_EMP );

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        private void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>("idOrgao", "ORG_CODIGO_ORGAO"));
            queryStringKeys.Add(new KeyValuePair<string, string>("sigla", "CO_SIGLA_CONCEITO"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

//====> Método que carrega o dropdown de Unidades
        private void CarregaUnidades() 
        {
            //ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
            //                         where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
            //                         && tb25.CO_FORMA_AVALIACAO == "C"
            //                         select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP }).OrderBy( e => e.NO_FANTAS_EMP );

            //ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            //ddlUnidade.DataValueField = "CO_EMP";
            //ddlUnidade.DataBind();

            //ddlUnidade.Items.Insert(0, new ListItem("", ""));
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }
        #endregion
    }
}
