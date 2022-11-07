//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CADASTRO E INFORMAÇÕES DE ALUNOS
// OBJETIVO: CADASTRAMENTO DE MÚLTIPLOS ENDEREÇOS DO ALUNO
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3610_CtrlInformacoesCadastraisAlunos.F3616_CadastramentoCuidaEspec
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

        protected void Page_Load()
        {
            if (IsPostBack) return;
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView() 
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_MEDICACAO" };

            CurrentPadraoBuscas.GridBusca.Columns.Add( new BoundField 
            { 
                DataField = "NO_ALU", 
                HeaderText = "Nome do Aluno" 
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add( new BoundField 
            {
                DataField = "NM_REMEDIO_CUIDADO", 
                HeaderText = "Descrição" 
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add( new BoundField 
            {
                DataField = "DT_RECEITA_CUIDADO", 
                DataFormatString = "{0:dd/MM/yyyy}",
                HeaderText = "Data Receita" 
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add( new BoundField 
            {
                DataField = "CO_STATUS_MEDICAC", 
                HeaderText = "Situação" 
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView() 
        {
            var resultado = (from tb293 in TB293_CUIDAD_SAUDE.RetornaTodosRegistros()
                            where tb293.TB07_ALUNO.TB25_EMPRESA1.CO_EMP == LoginAuxili.CO_EMP
                            && (txtNome.Text != "" ? tb293.TB07_ALUNO.NO_ALU.Contains(txtNome.Text) : txtNome.Text == "")
                            select new
                            {
                                tb293.TB07_ALUNO.NO_ALU, tb293.DT_RECEITA_CUIDADO, tb293.NM_REMEDIO_CUIDADO,
                                tb293.ID_MEDICACAO, CO_STATUS_MEDICAC = tb293.CO_STATUS_MEDICAC.ToUpper().Equals("A") ? "Ativo" : "Inativo"
                            }).OrderBy( r => r.DT_RECEITA_CUIDADO );

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds() 
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_MEDICACAO"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion
    }
}