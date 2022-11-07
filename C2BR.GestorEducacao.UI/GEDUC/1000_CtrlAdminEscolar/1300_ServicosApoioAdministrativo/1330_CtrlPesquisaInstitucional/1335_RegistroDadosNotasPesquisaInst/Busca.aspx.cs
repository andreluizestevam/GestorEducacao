//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PESQUISAS/ENQUETES DE SATISFAÇÃO
// OBJETIVO: REGISTRO DE DADOS/INFORMAÇÕE E NOTAS A PESQUISAS INSTITUCIONAIS
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1300_ServicosApoioAdministrativo.F1330_CtrlPesquisaInstitucional.F1335_RegistroDadosNotasPesquisaInst
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
            if (IsPostBack) return;

            CarregaTiposAvaliacao();
            CarregaPublicosAlvo();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_PESQ_AVAL" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_TIPO_AVAL",
                HeaderText = "Tipo Avalição"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DT_AVAL",
                HeaderText = "Data",
                DataFormatString = "{0:dd/MM/yyyy}"
            });
         
            BoundField bfRealizado = new BoundField();
            bfRealizado.DataField = "CO_PESQ_AVAL";
            bfRealizado.HeaderText = "N° Form";
            bfRealizado.ItemStyle.CssClass = "codCol";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRealizado);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
               DataField = "NOME",
               HeaderText = "Pessoa"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            string strPublicoAlvo = ddlPublicoAlvo.SelectedValue;

            int coTipoAval = ddlTipoAvaliacao.SelectedValue != "" ? int.Parse(ddlTipoAvaliacao.SelectedValue) : 0;

            if (strPublicoAlvo == PublicoAlvoAvaliacao.A.ToString())
            {
                var resultado = (from vw78 in VW78_07_PESQ_AVAL.RetornaTodosRegistros()
                                 where (coTipoAval != 0 ? vw78.CO_TIPO_AVAL == coTipoAval : coTipoAval == 0) 
                                 && vw78.CO_FLAG_PUBLICO == strPublicoAlvo && (txtNome.Text != "" ? vw78.NO_ALU.Contains(txtNome.Text) : txtNome.Text == "")
                                select new
                                {
                                    vw78.CO_PESQ_AVAL, vw78.DT_AVAL, vw78.NO_TIPO_AVAL, vw78.CO_TIPO_AVAL, NOME = vw78.NO_ALU
                                }).OrderBy( p => p.NO_TIPO_AVAL );

                CurrentPadraoBuscas.GridBusca.DataSource = resultado;
            }
            else if (strPublicoAlvo == PublicoAlvoAvaliacao.F.ToString() || strPublicoAlvo == PublicoAlvoAvaliacao.P.ToString())
            {
                var resultado = (from vw78 in VW78_03_PESQ_AVAL.RetornaTodosRegistros()
                                 where (coTipoAval != 0 ? vw78.CO_TIPO_AVAL == coTipoAval : coTipoAval == 0) 
                                 && vw78.CO_FLAG_PUBLICO == strPublicoAlvo && (txtNome.Text != "" ? vw78.NO_COL.Contains(txtNome.Text) : txtNome.Text == "")
                                 select new
                                 {
                                    vw78.CO_PESQ_AVAL, vw78.DT_AVAL, vw78.NO_TIPO_AVAL, vw78.CO_TIPO_AVAL, NOME = vw78.NO_COL
                                 }).OrderBy( p => p.NO_TIPO_AVAL );

                CurrentPadraoBuscas.GridBusca.DataSource = resultado;
            }
            else if (strPublicoAlvo == PublicoAlvoAvaliacao.R.ToString())
            {
                var resultado = (from vw78 in VW78_108_PESQ_AVAL.RetornaTodosRegistros()
                                 where (coTipoAval != 0 ? vw78.CO_TIPO_AVAL == coTipoAval : coTipoAval == 0)
                                 && vw78.CO_FLAG_PUBLICO == strPublicoAlvo && (txtNome.Text != "" ? vw78.NO_RESP.Contains(txtNome.Text) : txtNome.Text == "")
                                 select new
                                 {
                                    vw78.CO_PESQ_AVAL, vw78.DT_AVAL, vw78.NO_TIPO_AVAL, vw78.CO_TIPO_AVAL, NOME = vw78.NO_RESP
                                 }).OrderBy( p => p.NO_TIPO_AVAL );

                CurrentPadraoBuscas.GridBusca.DataSource = resultado;
            }
            else if (strPublicoAlvo == PublicoAlvoAvaliacao.O.ToString())
            {
                var resultado = (from vw78 in TB78_PESQ_AVAL.RetornaTodosRegistros()
                                 where (coTipoAval != 0 ? vw78.TB73_TIPO_AVAL.CO_TIPO_AVAL == coTipoAval : coTipoAval == 0) 
                                 && vw78.CO_FLAG_PUBLICO == strPublicoAlvo
                                 select new
                                 {
                                    vw78.CO_PESQ_AVAL, vw78.DT_AVAL, vw78.TB73_TIPO_AVAL.NO_TIPO_AVAL, vw78.TB73_TIPO_AVAL.CO_TIPO_AVAL, NOME = ""
                                 }).OrderBy( p => p.NO_TIPO_AVAL );

                CurrentPadraoBuscas.GridBusca.DataSource = resultado;
            }
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_PESQ_AVAL"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Público Alvo
        private void CarregaPublicosAlvo()
        {
            ddlPublicoAlvo.Load<PublicoAlvoAvaliacao>();
        }

//====> Método que carrega o DropDown de Tipos de Avaliação
        private void CarregaTiposAvaliacao()
        {
            ddlTipoAvaliacao.DataSource = (from tb73 in TB73_TIPO_AVAL.RetornaTodosRegistros()
                                           select new { tb73.NO_TIPO_AVAL, tb73.CO_TIPO_AVAL });

            ddlTipoAvaliacao.DataTextField = "NO_TIPO_AVAL";
            ddlTipoAvaliacao.DataValueField = "CO_TIPO_AVAL";
            ddlTipoAvaliacao.DataBind();

            ddlTipoAvaliacao.Items.Insert(0, new ListItem("Todos", ""));
        }
        #endregion
    }
}
