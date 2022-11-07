//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: DADOS OPERACIONAIS DE CONTRATO
// OBJETIVO: DEPARTAMENTO INSTITUCIONAL.
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
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0100_DadosOperContrato.F0103_DepartamentoInstitucional
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
            {
                //CarregaClassRisco();
                CarregaTipo();
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_DEPTO" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TIPO",
                HeaderText = "TIPO"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_DEPTO",
                HeaderText = "Departamento"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SIGLA_DEPTO",
                HeaderText = "Sigla"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CCUSTO",
                HeaderText = "Centro de Custo"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TEL",
                HeaderText = "Telefone"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "RAMAL",
                HeaderText = "Ramal"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SITUA",
                HeaderText = "Situação"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            try
            {

                //var risco = (ddlRisco.SelectedValue != "" ? int.Parse(ddlRisco.SelectedValue) : 0);
                var tipo = (ddlTipo.SelectedValue != "" ? int.Parse(ddlTipo.SelectedValue) : 0);

                var resultado = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                                 join tb174 in TB174_DEPTO_TIPO.RetornaTodosRegistros() on tb14.CO_TIPO_DEPTO equals tb174.ID_DEPTO_TIPO
                                 where (txtNomeDepto.Text != "" ? (tb14.NO_DEPTO.Contains(txtNomeDepto.Text) || tb14.CO_SIGLA_DEPTO.Contains(txtNomeDepto.Text)) : txtNomeDepto.Text == "")
                                 && tb14.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                 //&& (risco != 0 ? tb14.CO_CLASS_RISCO == risco : true)
                                 && (tipo != 0 ? tb14.CO_TIPO_DEPTO == tipo : true)
                                 && (ddlSitua.SelectedValue != "" ? tb14.CO_SITUA_DEPTO == ddlSitua.SelectedValue : true)
                                 select new
                                 {
                                     tb14.CO_DEPTO,
                                     tb14.CO_SIGLA_DEPTO,
                                     CO_SITUA = tb14.CO_SITUA_DEPTO.Equals("A") ? "Ativo" : tb14.CO_SITUA_DEPTO.Equals("M") ? "Manutenção" : tb14.CO_SITUA_DEPTO.Equals("X") ? "Interditado" : "Inativo",
                                     tb14.NO_DEPTO,
                                     PROTOCOLO = tb14.CO_CLASS_RISCO == null ? "-" : tb14.CO_CLASS_RISCO == 1 ? "Australiano" : tb14.CO_CLASS_RISCO == 2 ? "Canadense" : tb14.CO_CLASS_RISCO == 3 ? "Manchester" : tb14.CO_CLASS_RISCO == 4 ? "Americano" :
                                     tb14.CO_CLASS_RISCO == 5 ? "Pediatria" : tb14.CO_CLASS_RISCO == 6 ? "Obstetricia" : tb14.CO_CLASS_RISCO == 7 ? "Instituição" : "-",
                                     TEL = (String.IsNullOrEmpty(tb14.NU_TEL_DEPTO) ? "-" : tb14.NU_TEL_DEPTO),
                                     RAMAL = (String.IsNullOrEmpty(tb14.NU_RAMAL_DEPTO) ? "-" : tb14.NU_RAMAL_DEPTO),
                                     CCUSTO = "-",
                                     TIPO = tb174.NO_DEPTO_TIPO,
                                 }).OrderBy(d => d.NO_DEPTO);

                CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
            }
            catch (Exception e)
            {
            }
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_DEPTO"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        /// <summary>
        /// Método de carregamento dos tipos de departamento
        /// </summary>
        private void CarregaTipo()
        {
            ddlTipo.DataSource = (from tb174 in TB174_DEPTO_TIPO.RetornaTodosRegistros()
                                  where tb174.CO_SITU_TIPO.Equals("A")
                                  select new { tb174.ID_DEPTO_TIPO, tb174.NO_DEPTO_TIPO }).OrderBy(a => a.NO_DEPTO_TIPO);

            ddlTipo.DataTextField = "NO_DEPTO_TIPO";
            ddlTipo.DataValueField = "ID_DEPTO_TIPO";
            ddlTipo.DataBind();

            ddlTipo.Items.Insert(0, new ListItem("Todos", ""));
        }

    }
}
