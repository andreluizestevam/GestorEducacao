//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: AGENDA DE ATIVIDADES PROFISSIONAIS
// OBJETIVO: REATIVAÇÃO DE ATIVIDADES FUNCIONAIS AGENDADAS ENCERRADAS
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1300_ServicosApoioAdministrativo.F1310_CtrlAgendaAtividadesFuncional.F1314_ReativaAgendaAtividadeFinaCanc
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
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_CHAVE_UNICA_TAREF", "CO_IDENT_TAREF" };            

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DT_LIMIT_TAREF_AGEND",
                HeaderText = "Limite",
                DataFormatString = "{0:dd/MM/yy}"
            });
            
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_RESUM_TAREF_AGEND",
                HeaderText = "Título"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_COL",
                HeaderText = "Responsável"
            });            

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_SITU_TAREF_AGEND",
                HeaderText = "Status"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int chaveUnicaTaref = txtChaveUnica.Text != "" ? int.Parse(txtChaveUnica.Text.Trim()) : 0;

            DateTime dataTarefa;

            if (txtDtInicial.Text == "")
                dataTarefa = new DateTime(1900, 01, 01);
            else
                DateTime.TryParse(txtDtInicial.Text, out dataTarefa);

            var resultado = (from tb137 in TB137_TAREFAS_AGENDA.RetornaTodosRegistros()
                             where ((tb137.TB140_PRIOR_TAREF_AGEND.CO_PRIOR_TAREF_AGEND.Equals(ddlPrioridade.SelectedValue)) || ddlPrioridade.SelectedValue == "")
                             && (chaveUnicaTaref != 0 ? tb137.CO_CHAVE_UNICA_TAREF == chaveUnicaTaref : chaveUnicaTaref == 0)
                             && (txtCpf.Text != "" ? tb137.TB03_COLABOR.NU_CPF_COL.Equals(txtCpf.Text.Replace(".", "").Replace("-", "")) : txtCpf.Text == "")
                             && (txtDtInicial.Text != "" ? tb137.DT_COMPR_TAREF_AGEND >= dataTarefa : txtDtInicial.Text == "")
                             && ((tb137.TB139_SITU_TAREF_AGEND.CO_SITU_TAREF_AGEND.Equals(TB139_SITU_TAREF_AGEND.CO.CO_SITU_TAREF_AGEND)) ||
                                   (tb137.TB139_SITU_TAREF_AGEND.CO_SITU_TAREF_AGEND.Equals(TB139_SITU_TAREF_AGEND.CR.CO_SITU_TAREF_AGEND)) ||
                                   (tb137.TB139_SITU_TAREF_AGEND.CO_SITU_TAREF_AGEND.Equals(TB139_SITU_TAREF_AGEND.TF.CO_SITU_TAREF_AGEND)))
                             && tb137.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                             select new
                             {
                                 tb137.DT_COMPR_TAREF_AGEND, tb137.DT_LIMIT_TAREF_AGEND, tb137.NM_RESUM_TAREF_AGEND, tb137.TB03_COLABOR.NO_COL,
                                 tb137.TB139_SITU_TAREF_AGEND.DE_SITU_TAREF_AGEND, tb137.CO_CHAVE_UNICA_TAREF,
                                 tb137.CO_IDENT_TAREF, tb137.CO_COL, tb137.CO_EMP, tb137.ORG_CODIGO_ORGAO
                             });

            if (LoginAuxili.FLA_REABER_SOLICI_GLOBAL.Equals("N"))
            {
                resultado = resultado.Where(tarefas => tarefas.CO_COL == LoginAuxili.CO_COL)
                                           .Where(tarefas => tarefas.CO_EMP == LoginAuxili.CO_UNID_FUNC)
                                           .Where(tarefas => tarefas.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);
            }

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderBy(t => t.DT_COMPR_TAREF_AGEND) : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_CHAVE_UNICA_TAREF"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoIdentTarefa, "CO_IDENT_TAREF"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion
    }
}
