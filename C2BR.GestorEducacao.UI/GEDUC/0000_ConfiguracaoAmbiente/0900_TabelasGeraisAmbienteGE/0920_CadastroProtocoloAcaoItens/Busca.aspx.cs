//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: CADASTRO INTERNACIONAL DE DOENÇAS.
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
namespace C2BR.GestorEducacao.UI.GEDUC._0000_ConfiguracaoAmbiente._0900_TabelasGeraisAmbienteGE._0920_CadastroProtocoloAcaoItens
{
    public partial class Busca : System.Web.UI.Page
    {
        public PadraoBuscas CurrentPadraoBuscas { get { return ((PadraoBuscas)this.Master); } }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }


        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            CurrentPadraoBuscas.OnAcaoBuscaDefineGridView += new PadraoBuscas.OnAcaoBuscaDefineGridViewHandler(CurrentPadraoBuscas_OnAcaoBuscaDefineGridView);
            CurrentPadraoBuscas.OnDefineColunasGridView += new PadraoBuscas.OnDefineColunasGridViewHandler(CurrentPadraoBuscas_OnDefineColunasGridView);
            CurrentPadraoBuscas.OnDefineQueryStringIds += new PadraoBuscas.OnDefineQueryStringIdsHandler(CurrentPadraoBuscas_OnDefineQueryStringIds);
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            try
            {
                CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_PROTO_ACAO" };

                CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
                {
                    DataField = "SISTEMA",
                    HeaderText = "SISTEMA"
                });

                CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
                {
                    DataField = "TIPO",
                    HeaderText = "TIPO"
                });

                CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
                {
                    DataField = "NO_PROTO_ACAO",
                    HeaderText = "PROTOCOLO"
                });

                CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
                {
                    DataField = "SITUACAO",
                    HeaderText = "SITUACAO"
                });

            }
            catch (Exception e)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro de execução, entre em contato com o suporte para solucionar o problema.");
            }
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            try
            {

                var resultado = (from tb426 in TB426_PROTO_ACAO.RetornaTodosRegistros()
                                 where (txtNome.Text != "" ? tb426.NO_PROTO_ACAO.Contains(txtNome.Text) : 0 == 0)
                                 && (!String.IsNullOrEmpty(ddlTipo.SelectedValue) ? tb426.TP_PROTO_ACAO == ddlTipo.SelectedValue : 0 == 0)
                                 && (ddlSituacao.SelectedValue != "T" ? tb426.FL_SITUA == ddlSituacao.SelectedValue : 0 == 0)
                                 && (tb426.TP_SISTEMA == LoginAuxili.CO_TIPO_UNID)
                                 select new
                                 {
                                     tb426.ID_PROTO_ACAO,
                                     tb426.NO_PROTO_ACAO,
                                     TIPO = tb426.TP_PROTO_ACAO.Equals("CLR") ? "Classificação de Risco" : tb426.TP_PROTO_ACAO.Equals("EST") ? "Esterilização" : tb426.TP_PROTO_ACAO.Equals("HIG") ? "Higienização" : tb426.TP_PROTO_ACAO.Equals("LAV") ? "Lavanderia" : tb426.TP_PROTO_ACAO.Equals("SCA") ? "Serviço Camareira" :
                                     tb426.TP_PROTO_ACAO.Equals("ACO") ? "Acomodação" : tb426.TP_PROTO_ACAO.Equals("PRO") ? "Procedimento" : tb426.TP_PROTO_ACAO.Equals("CTI") ? "Controles Internos" : tb426.TP_PROTO_ACAO.Equals("MAN") ? "Manutenção" : "Segurança",
                                     SITUACAO = tb426.FL_SITUA == "A" ? "Ativo" : "Inativo",
                                     SISTEMA = tb426.TP_SISTEMA.Equals("PGS") ? "Portal Gestor Saúde" : "Gestor Educação",
                                 }).OrderBy(c => c.NO_PROTO_ACAO);

                int count = resultado.Count();

                if (count > 0)
                {
                    CurrentPadraoBuscas.GridBusca.DataSource = resultado;
                }
            }
            catch (Exception e)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro de execução, entre em contato com o suporte para solucionar o problema");
            }
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_PROTO_ACAO"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion
    }
}
