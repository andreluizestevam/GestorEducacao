//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: TABELAS DE APOIO OPERACIONAL PEDAGÓGICO
// OBJETIVO: CADASTRAMENTO DE TIPOS DE ATIVIDADE
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3090_TabelasGeraisCtrlPedagogico.F3091_TipoAtividade
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

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_TIPO_ATIV" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_TIPO_ATIV",
                HeaderText = "Nome"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_TIPO_ATIV",
                HeaderText = "Descrição"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            string classificacao = ddlClass.SelectedValue;
            string situacao = ddlSituacao.SelectedValue;
            string lanNota = ddlLancaNota.SelectedValue;
            string tipoEnsino = drpTipoEnsino.SelectedValue;

            var resultado = (from tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros()
                             where (txtDescricao.Text != "" ? tb273.NO_TIPO_ATIV.Contains(txtDescricao.Text) : txtDescricao.Text == "")
                             && (txtSigla.Text != "" ? tb273.CO_SIGLA_ATIV.Contains(txtSigla.Text) : txtSigla.Text == "")
                             && (classificacao != "0" ? tb273.CO_CLASS_ATIV == classificacao : 0 == 0 )
                             && (situacao != "0" ? tb273.CO_SITUA_ATIV == situacao : 0 == 0)
                             && (lanNota != "0" ? tb273.FL_LANCA_NOTA_ATIV == lanNota : 0 == 0)
                             && (tipoEnsino != "0" ? tb273.CO_TIPO_ENSINO == tipoEnsino : 0 == 0)
                             select tb273).OrderBy(m => m.NO_TIPO_ATIV);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_TIPO_ATIV"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion
    }
}
