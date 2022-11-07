//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: BAIRRO
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3100_CtrlOperProfessores.F3110_CtrlPlanejamentoAulas.F3314_CadastroReferenciaConteudo
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
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_REFER_CONTE" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_TIPO_REFER_CONTE",
                HeaderText = "Tipo"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_TITUL_REFER_CONTE",
                HeaderText = "Título"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_NIVEL_APREN",
                HeaderText = "Nível"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_STATUS",
                HeaderText = "Status"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            var resultado = (from tb310 in TB310_REFER_CONTEUDO.RetornaTodosRegistros()
                             where ddlTipoConte.SelectedValue != "T" ? tb310.CO_TIPO_REFER_CONTE == ddlTipoConte.SelectedValue : ddlTipoConte.SelectedValue == "T"
                               && ddlStatus.SelectedValue != "T" ? tb310.CO_STATUS == ddlStatus.SelectedValue : ddlStatus.SelectedValue == "T"
                               && ddlNivelConte.SelectedValue != "T" ? tb310.CO_NIVEL_APREN == ddlNivelConte.SelectedValue : ddlNivelConte.SelectedValue == "T"
                               && txtTitulConte.Text != "" ? tb310.NO_TITUL_REFER_CONTE.Contains(txtTitulConte.Text) : txtTitulConte.Text == ""
                               && tb310.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                               select new 
                               {
                                   CO_TIPO_REFER_CONTE = tb310.CO_TIPO_REFER_CONTE == "C" ? "Conteúdo Escolar" : "Bibliográfico",
                                   CO_NIVEL_APREN = tb310.CO_NIVEL_APREN == "F" ? "Fácil" : tb310.CO_NIVEL_APREN == "M" ? "Médio" :
                                   tb310.CO_NIVEL_APREN == "D" ? "Difícil" : tb310.CO_NIVEL_APREN == "A" ? "Avançado" : "Sem Registro",
                                   tb310.NO_TITUL_REFER_CONTE, tb310.ID_REFER_CONTE,
                                   CO_STATUS = tb310.CO_STATUS == "A" ? "Ativa" : "Inativa"
                               }).OrderBy(b => b.CO_TIPO_REFER_CONTE).ThenBy(b => b.NO_TITUL_REFER_CONTE);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_REFER_CONTE"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion
    }
}