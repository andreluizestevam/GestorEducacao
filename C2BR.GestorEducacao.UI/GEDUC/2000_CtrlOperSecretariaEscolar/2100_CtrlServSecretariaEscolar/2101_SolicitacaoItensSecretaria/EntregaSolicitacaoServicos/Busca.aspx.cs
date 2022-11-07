//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: SOLICITAÇÃO DE ITENS DE SECRETARIA ESCOLAR
// OBJETIVO: ENTREGA DE SOLICITAÇÃO DE SERVIÇOS.
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
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2101_SolicitacaoItensSecretaria.EntregaSolicitacaoServicos
{
    public partial class Busca : System.Web.UI.Page
    {
        public PadraoBuscas CurrentPadraoBuscas { get { return ((PadraoBuscas)this.Master); } }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            base.OnPreInit(e);

            CurrentPadraoBuscas.OnAcaoBuscaDefineGridView += new PadraoBuscas.OnAcaoBuscaDefineGridViewHandler(CurrentPadraoBuscas_OnAcaoBuscaDefineGridView);
            CurrentPadraoBuscas.OnDefineColunasGridView += new PadraoBuscas.OnDefineColunasGridViewHandler(CurrentPadraoBuscas_OnDefineColunasGridView);
            CurrentPadraoBuscas.OnDefineQueryStringIds += new PadraoBuscas.OnDefineQueryStringIdsHandler(CurrentPadraoBuscas_OnDefineQueryStringIds);

            if (!IsPostBack)
            {
                CarregaSolicitacoes();
            }
        }

        protected void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_SOLI_ATEN" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DT_SOLI_ATEN",
                HeaderText = "Data",
                DataFormatString = "{0:dd/MM/yyyy}"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NU_DCTO_SOLIC",
                HeaderText = "N° Solicitação"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NU_NIRE",
                HeaderText = "NIRE"
            });
               
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_ALU",
                HeaderText = "Beneficiário"
            });
                  
            BoundField bfRealizado1 = new BoundField();
            bfRealizado1.DataField = "DT_PREV_ENTR";
            bfRealizado1.HeaderText = "Previsão";
            bfRealizado1.ItemStyle.CssClass = "codCol";
            bfRealizado1.DataFormatString = "{0:d}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRealizado1);
        }

        protected void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coTipoSoli = ddlSolicitacoes.SelectedValue != "" ? int.Parse(ddlSolicitacoes.SelectedValue) : 0;   

            var resultado = (from tb65 in TB65_HIST_SOLICIT.RetornaTodosRegistros()
                             where tb65.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                             && (tb65.CO_SITU_SOLI == "F" || tb65.CO_SITU_SOLI == "D")
                             && (coTipoSoli != 0 ? tb65.CO_TIPO_SOLI == coTipoSoli : coTipoSoli == 0)
                             select new { tb65.CO_SOLI_ATEN }).Distinct();

            var resultado2 = from tb64 in TB64_SOLIC_ATEND.RetornaTodosRegistros()
                             where (tb64.NU_DCTO_SOLIC == txtNumeroSolicitacao.Text || txtNumeroSolicitacao.Text == "") && tb64.CO_SIT == "A"
                             join tb65 in resultado on tb64.CO_SOLI_ATEN equals tb65.CO_SOLI_ATEN
                             join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb64.CO_ALU equals tb07.CO_ALU
                             where (txtAluno.Text != "" ? tb07.NO_ALU.Contains(txtAluno.Text) : txtAluno.Text == "")
                             && (txtResponsavel.Text != "" ? tb07.TB108_RESPONSAVEL.NO_RESP.Contains(txtResponsavel.Text) : txtResponsavel.Text == "")
                             select new
                             {
                                tb07.NU_NIRE, tb07.NO_ALU, CO_SIT = tb64.CO_SIT == "A" ? "Aberta" : (tb64.CO_SIT == "C" ? "Cancelada" : "Finalizada"),
                                tb64.NU_DCTO_SOLIC, tb64.DT_SOLI_ATEN, tb64.DT_PREV_ENTR, tb64.localizacao, tb64.CO_SOLI_ATEN
                             };

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado2.Count() > 0) ? resultado2 : null;
        }

        protected void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_SOLI_ATEN"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        //====> Método que carrega o DropDown de Solicitações
        private void CarregaSolicitacoes()
        {
            ddlSolicitacoes.DataSource = TB66_TIPO_SOLIC.RetornaTodosRegistros().OrderBy(t => t.NO_TIPO_SOLI);
            ddlSolicitacoes.DataTextField = "NO_TIPO_SOLI";
            ddlSolicitacoes.DataValueField = "CO_TIPO_SOLI";
            ddlSolicitacoes.DataBind();

            ddlSolicitacoes.Items.Insert(0, new ListItem("Todas", ""));
        }
    }
}