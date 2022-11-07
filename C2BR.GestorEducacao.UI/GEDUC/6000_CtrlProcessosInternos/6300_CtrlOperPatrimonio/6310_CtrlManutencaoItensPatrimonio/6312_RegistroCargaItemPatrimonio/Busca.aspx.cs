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

namespace C2BR.GestorEducacao.UI.GEDUC._6000_CtrlProcessosInternos._6300_CtrlOperPatrimonio._6310_CtrlManutencaoItensPatrimonio._6312_RegistroCargaItemPatrimonio
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

        void Page_Load()
        {
            if (IsPostBack) return;

            CarregaUnidades();
            CarregaColaboradores();
            CarregaGrupo();
            CarregaSubGrupo();
            CarregaTipoPatrimonio();
            CarregaItemPatrimonio();
            CarregaSituacaoCarga();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_CARGA_PATR" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "sigla",
                HeaderText = "Unidade Patrimônio"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_APEL_COL",
                HeaderText = "Colaborador"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "COD_PATR",
                HeaderText = "Códido Patrimônio"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NOM_PATR",
                HeaderText = "Nome do Ítem de Patrimônio"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SITUA_STATU",
                HeaderText = "Situação"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int idUnidadePatrimonio = ddlUnidadePatrimonio.SelectedValue != "" ? int.Parse(ddlUnidadePatrimonio.SelectedValue) : 0;
            int idUnidadeColaborador = ddlUnidadeColaborador.SelectedValue != "" ? int.Parse(ddlUnidadeColaborador.SelectedValue) : 0;
            int? idColaborador = ddlColaborador.SelectedValue != "" ? int.Parse(ddlColaborador.SelectedValue) : 0;
            int? idGrupo = ddlColaborador.SelectedValue != "" ? int.Parse(ddlColaborador.SelectedValue) : 0;
            int? idSubGrupo = ddlColaborador.SelectedValue != "" ? int.Parse(ddlColaborador.SelectedValue) : 0;
            int? idTipoPatrimonio = ddlColaborador.SelectedValue != "" ? int.Parse(ddlColaborador.SelectedValue) : 0;
            int? idItemPatrimonio = ddlColaborador.SelectedValue != "" ? int.Parse(ddlColaborador.SelectedValue) : 0;
            string situacaoCarga = ddlSituacaoCarga.SelectedValue != "" ? ddlSituacaoCarga.SelectedValue : "";

            var resultado = (from tb232 in TB232_PATRI_CARGA_ITEM.RetornaTodosRegistros()
                             where tb232.CO_EMP_CARGA == idUnidadePatrimonio
                             && tb232.CO_EMP_COL_CARGA == idUnidadeColaborador
                             && (idColaborador != 0 ? tb232.CO_COL_CARGA == idColaborador : idColaborador == 0)
                             && (idGrupo != 0 ? tb232.TB212_ITENS_PATRIMONIO.TB261_SUBGRUPO.TB260_GRUPO.ID_GRUPO == idGrupo : idGrupo == 0)
                             && (idSubGrupo != 0 ? tb232.TB212_ITENS_PATRIMONIO.TB261_SUBGRUPO.ID_SUBGRUPO == idSubGrupo : idSubGrupo == 0)
                             && (idTipoPatrimonio != 0 ? tb232.TB213_CLASSIF_PATR.CO_CLASSIF_PATR == idTipoPatrimonio : idTipoPatrimonio == 0)
                             && (idItemPatrimonio != 0 ? tb232.TB212_ITENS_PATRIMONIO.COD_PATR == idItemPatrimonio : idItemPatrimonio == 0)
                             && (situacaoCarga != "" ? tb232.CO_SITUA_STATU == situacaoCarga : situacaoCarga == "")
                             join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tb232.CO_EMP_COL_CARGA equals tb03.CO_EMP
                             where tb03.CO_COL == tb232.CO_COL_CARGA
                             join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb232.CO_EMP_CARGA equals tb25.CO_EMP
                             join tb212 in TB212_ITENS_PATRIMONIO.RetornaTodosRegistros() on tb232.TB212_ITENS_PATRIMONIO.COD_PATR equals tb212.COD_PATR
                             select new { tb232.ID_CARGA_PATR, tb25.sigla, tb03.NO_APEL_COL, tb212.COD_PATR, tb212.NOM_PATR, CO_SITUA_STATU = tb232.CO_SITUA_STATU == "A" ? "Ativa" : "Inativa" });
            //select new { tb232.ID_CARGA_PATR, tb25.sigla, tb03.NO_APEL_COL, tb212.COD_PATR, tb212.NOM_PATR, CO_SITUA_STATU = RetornaSituacaoStatus(tb232.CO_SITUA_STATU) });

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_CARGA_PATR"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

        //====> Método que carrega o DropDown de Unidades de Patrimonio
        private void CarregaUnidades()
        {
            var ds = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                      select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP });
            ddlUnidadePatrimonio.DataSource = ds;

            ddlUnidadePatrimonio.DataValueField = "CO_EMP";
            ddlUnidadePatrimonio.DataTextField = "NO_FANTAS_EMP";
            ddlUnidadePatrimonio.DataBind();

            ddlUnidadeColaborador.DataSource = ds;

            ddlUnidadeColaborador.DataValueField = "CO_EMP";
            ddlUnidadeColaborador.DataTextField = "NO_FANTAS_EMP";
            ddlUnidadeColaborador.DataBind();
        }

        //====> Método que carrega o DropDown de Colaboradores
        private void CarregaColaboradores()
        {
            int unidadeColaborador = ddlUnidadeColaborador.SelectedValue != "" ? int.Parse(ddlUnidadePatrimonio.SelectedValue) : 0;
            ddlColaborador.DataSource = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                         where tb03.CO_EMP.Equals(unidadeColaborador)
                                         select new { tb03.CO_COL, tb03.NO_COL });

            ddlColaborador.DataTextField = "NO_COL";
            ddlColaborador.DataValueField = "CO_COL";
            ddlColaborador.DataBind();

            ddlColaborador.Items.Insert(0, new ListItem("Todos", ""));
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
        private void CarregaTipoPatrimonio()
        {
            ddlTipoPatrimonio.DataSource = (from tb213 in TB213_CLASSIF_PATR.RetornaTodosRegistros()
                                            select new { tb213.CO_CLASSIF_PATR, tb213.NO_CLASSIF_PATR });

            ddlTipoPatrimonio.DataValueField = "CO_CLASSIF_PATR";
            ddlTipoPatrimonio.DataTextField = "NO_CLASSIF_PATR";
            ddlTipoPatrimonio.DataBind();

            ddlTipoPatrimonio.Items.Insert(0, new ListItem("Todos", ""));
        }
        private void CarregaItemPatrimonio()
        {
            string strTipoPatrimonio = ddlTipoPatrimonio.SelectedValue != "" ? ddlTipoPatrimonio.SelectedValue : "";
            ddlItemPatrimonio.DataSource = (from tb212 in TB212_ITENS_PATRIMONIO.RetornaTodosRegistros()
                                            where tb212.TP_PATR == strTipoPatrimonio
                                            select new { tb212.COD_PATR, tb212.NOM_PATR });

            ddlItemPatrimonio.DataValueField = "COD_PATR";
            ddlItemPatrimonio.DataTextField = "NOM_PATR";
            ddlItemPatrimonio.DataBind();

            ddlItemPatrimonio.Items.Insert(0, new ListItem("Todos", ""));
        }
        private void CarregaSituacaoCarga()
        {
            ddlSituacaoCarga.Items.Insert(0, new ListItem("Todos", ""));
            ddlSituacaoCarga.Items.Insert(1, new ListItem("Ativa", "A"));
            ddlSituacaoCarga.Items.Insert(2, new ListItem("Inativa", "I"));
            ddlSituacaoCarga.Items.Insert(3, new ListItem("Cancelada", "C"));
            ddlSituacaoCarga.Items.Insert(4, new ListItem("Repassada", "R"));
        }
        #endregion
        protected void ddlUnidadeColaborador_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaColaboradores();
        }
        protected void ddlGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupo();
        }
        protected void ddlTipoPatrimonio_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaItemPatrimonio();
        }
        private string RetornaSituacaoStatus(string statusSigla)
        {
            string status = "";
            switch (statusSigla)
            {
                case "A":
                    status = "Ativa";
                    break;
                case "I":
                    status = "Inativa";
                    break;
                case "C":
                    status = "Cancelada";
                    break;
                case "R":
                    status = "Repassada";
                    break;
            }
            return status;
        }
    }
}