//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: ************************
// SUBMÓDULO: *********************
// OBJETIVO: **********************
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3610_CtrlInformacoesCadastraisAlunos.F3615_ExclusaoAlunoSemMovtoDados
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
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_ALU" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NU_NIRE",
                HeaderText = "NIRE"
            });

            BoundField bfRealizado = new BoundField();
            bfRealizado.DataField = "NU_NIS";
            bfRealizado.HeaderText = "NIS";
            bfRealizado.ItemStyle.CssClass = "codCol";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRealizado);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_ALU",
                HeaderText = "Nome"
            });

            BoundField bfRealizado1 = new BoundField();
            bfRealizado1.DataField = "DT_NASC_ALU";
            bfRealizado1.HeaderText = "Dt Nasc";
            bfRealizado1.ItemStyle.CssClass = "codCol";
            bfRealizado1.DataFormatString = "{0:d}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRealizado1);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TP_DEF",
                HeaderText = "Deficiência"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_RESP",
                HeaderText = "Responsável"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int nuNire = txtNire.Text != "" ? int.Parse(txtNire.Text) : 0;

            var resultado = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                             where tb07.CO_EMP == LoginAuxili.CO_EMP && (nuNire != 0 ? tb07.NU_NIRE == nuNire : nuNire == 0)
                             && (txtNome.Text != "" ? tb07.NO_ALU.Contains(txtNome.Text) : txtNome.Text == "")
                             && (txtCpf.Text != "" ? tb07.NU_CPF_ALU.Equals(txtCpf.Text.Replace(".", "").Replace("-", "")) : txtCpf.Text == "")
                             && (ddlDeficiencia.SelectedValue == "" || tb07.TP_DEF == ddlDeficiencia.SelectedValue ||
                                (ddlDeficiencia.SelectedValue == "T" && (tb07.TP_DEF != "" && tb07.TP_DEF != "N")))
                             select new
                             {
                                 tb07.CO_ALU, tb07.NO_ALU, tb07.NU_NIRE, tb07.NU_NIS, tb07.DT_NASC_ALU, tb07.TB108_RESPONSAVEL.NO_RESP,
                                 TP_DEF = tb07.TP_DEF == "A" ? "Auditiva" : (tb07.TP_DEF == "V" ? "Visual" : 
                                          (tb07.TP_DEF == "F" ? "Física" : (tb07.TP_DEF == "M" ? "Mental" : (tb07.TP_DEF == "P" ? "Múltiplas" : 
                                          (tb07.TP_DEF == "O" ? "Outras" : (tb07.TP_DEF == "N" ? "Nenhuma" : ""))))))
                             }).OrderBy( a => a.NO_ALU );

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_ALU"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion
    }
}
