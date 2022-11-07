//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE PATRIMONIO
// SUBMÓDULO: REGISTRO DE ITENS DE PATRIMÔNIO
// OBJETIVO: REGISTRO DE ITENS DE PATRIMÔNIO
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
namespace C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6300_CtrlOperPatrimonio.F6310_CtrlManutencaoItensPatrimonio.F6311_RegistroItemPatrimonio
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
                CarregaGrupo();
                CarregaSubGrupo();
                CarregarUnidades();
                CarregaTipoPatrimonio();
                CarregarDepartamentos(LoginAuxili.CO_EMP);
            }
        }                      

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "COD_PATR" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "COD_PATR",
                HeaderText = "Código",
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField 
            {
              DataField = "NOM_PATR",
              HeaderText = "Nome",
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TP_PATR",
                HeaderText = "Tipo"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField 
            {
              DataField = "NOM_GRUPO",
              HeaderText = "Grupo"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField 
            {
              DataField = "NOM_SUBGRUPO",
              HeaderText = "SubGrupo"
            });
            
            BoundField vl = new BoundField();
            vl.DataField = "VL_AQUIS";
            vl.HeaderText = "Valor";
            vl.ItemStyle.CssClass = "numeroCol";
            CurrentPadraoBuscas.GridBusca.Columns.Add(vl);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_DEPTO",
                HeaderText = "Departamento",
            });            
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            decimal codPatr = txtCodPatrimonio.Text != "" ? decimal.Parse(txtCodPatrimonio.Text) : 0;
            int coEmp = ddlUnidadePatrimonio.SelectedValue != "" ? int.Parse(ddlUnidadePatrimonio.SelectedValue) : 0;
            int coDepto = ddlDeptoAtual.SelectedValue != "" ? int.Parse(ddlDeptoAtual.SelectedValue) : 0;
            int idGrupo = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : 0;
            int idSubGrupo = ddlSubGrupo.SelectedValue != "" ? int.Parse(ddlSubGrupo.SelectedValue) : 0;

            var resultado = (from tb212 in TB212_ITENS_PATRIMONIO.RetornaTodosRegistros().Include(typeof(TB14_DEPTO).Name).AsEnumerable()
                             where (codPatr != 0 ? tb212.COD_PATR == codPatr : codPatr == 0)
                             && (ddlTipoPatrimonio.SelectedValue != "" ? tb212.TP_PATR == ddlTipoPatrimonio.SelectedValue : ddlTipoPatrimonio.SelectedValue == "")
                             && (coEmp != 0 ? tb212.CO_EMP == coEmp : coEmp == 0)
                             && (coDepto != 0 ? tb212.TB14_DEPTO.CO_DEPTO == coDepto : coDepto == 0)
                             && (idGrupo != 0 ? tb212.TB261_SUBGRUPO.TB260_GRUPO.ID_GRUPO == idGrupo : idGrupo == 0)
                             && (idSubGrupo != 0 ? tb212.TB261_SUBGRUPO.ID_SUBGRUPO == idSubGrupo : idSubGrupo == 0)
                             select new
                             {
                                 tb212.COD_PATR, TP_PATR = tb212.TP_PATR == "1" ? "Móvel" : "Imóvel", tb212.VL_AQUIS, tb212.TB14_DEPTO.NO_DEPTO,
                                 tb212.NOM_PATR, tb212.TB261_SUBGRUPO.TB260_GRUPO.NOM_GRUPO, tb212.TB261_SUBGRUPO.NOM_SUBGRUPO
                             }).ToList();

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "COD_PATR"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Unidades Escolares
        private void CarregarUnidades()
        {
            ddlUnidadePatrimonio.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                               select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP });

            ddlUnidadePatrimonio.DataValueField = "CO_EMP";
            ddlUnidadePatrimonio.DataTextField = "NO_FANTAS_EMP";
            ddlUnidadePatrimonio.DataBind();

            ddlUnidadePatrimonio.Items.Insert(0, new ListItem("Todos", ""));
            ddlUnidadePatrimonio.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

//====> Método que carrega o DropDown de Departamentos
        private void CarregarDepartamentos(int coEmp)
        {
            ddlDeptoAtual.Enabled = true;

            ddlDeptoAtual.DataSource = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                                        where tb14.TB25_EMPRESA.CO_EMP == coEmp
                                        select new { tb14.CO_DEPTO, tb14.CO_SIGLA_DEPTO, }).OrderBy( d => d.CO_SIGLA_DEPTO );

            ddlDeptoAtual.DataValueField = "CO_DEPTO";
            ddlDeptoAtual.DataTextField = "CO_SIGLA_DEPTO";
            ddlDeptoAtual.DataBind();

            if (ddlDeptoAtual.Items.Count > 0)
            {
                ddlDeptoAtual.Items.Insert(0, new ListItem("Todos", ""));
                ddlDeptoAtual.Enabled = true;
            }
            else
                ddlDeptoAtual.Enabled = true;            
        }

//====> Método que carrega o DropDown de Tipos de Patrimônio
        private void CarregaTipoPatrimonio()
        {
            ddlTipoPatrimonio.DataSource = (from tb291 in TB291_TIPO_PATRIM.RetornaTodosRegistros()
                                           select new { tb291.CO_TIPO_PATRIM, tb291.NO_TIPO_PATRIM });

            ddlTipoPatrimonio.DataTextField = "NO_TIPO_PATRIM";
            ddlTipoPatrimonio.DataValueField = "CO_TIPO_PATRIM";
            ddlTipoPatrimonio.DataBind();          
        }

//====> Método que carrega o DropDown de Grupos
        private void CarregaGrupo() 
        {
            ddlGrupo.DataSource = (from tb260 in TB260_GRUPO.RetornaTodosRegistros()
                                   where tb260.TP_GRUPO == "P"
                                   select new { tb260.ID_GRUPO, tb260.NOM_GRUPO });

            ddlGrupo.DataValueField = "ID_GRUPO";
            ddlGrupo.DataTextField = "NOM_GRUPO";
            ddlGrupo.DataBind();

            ddlGrupo.Items.Insert(0, new ListItem("Todos", ""));
        }

//====> Método que carrega o DropDown de SubGrupos
        private void CarregaSubGrupo() 
        {
            int idGrupo = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : 0;

            ddlSubGrupo.DataSource = (from tb261 in TB261_SUBGRUPO.RetornaTodosRegistros()
                                      where tb261.TB260_GRUPO.ID_GRUPO == idGrupo
                                      select new { tb261.ID_SUBGRUPO, tb261.NOM_SUBGRUPO });

            ddlSubGrupo.DataValueField = "ID_SUBGRUPO";
            ddlSubGrupo.DataTextField = "NOM_SUBGRUPO";
            ddlSubGrupo.DataBind();

            ddlSubGrupo.Items.Insert(0, new ListItem("Todos", ""));
        }
        #endregion

        protected void ddlTipoPatrimonio_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrupo();
        }

        protected void ddlUnidadePatrimonio_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarDepartamentos(int.Parse(ddlUnidadePatrimonio.SelectedValue));
        } 

        protected void ddlGrupo_SelectedIndexChanged(object sender, EventArgs e) 
        {
            CarregaSubGrupo();
        }
    }
}
