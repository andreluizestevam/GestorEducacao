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
namespace C2BR.GestorEducacao.UI.GEDUC._0000_ConfiguracaoAmbiente._0900_TabelasGeraisAmbienteGE._0918_CadastroProtocoloCID
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
                CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_PROTO_CID" };

                CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
                {
                    DataField = "NO_PROTO_CID",
                    HeaderText = "NOME"
                });

                //CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
                //{
                //    DataField = "TIPO",
                //    HeaderText = "TIPO"
                //});

                CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
                {
                    DataField = "SITUACAO",
                    HeaderText = "SITUACAO"
                });

                CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
                {
                    DataField = "NO_CID",
                    HeaderText = "CID"
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
                //int tipo = int.Parse(ddlTipo.SelectedValue);

                var resultado = (from tbs434 in TBS434_PROTO_CID.RetornaTodosRegistros()
                                 where (txtNome.Text != "" ? tbs434.NO_PROTO_CID.Contains(txtNome.Text) : 0 == 0)
                                 && (txtCID.Text != "" ? tbs434.TB117_CODIGO_INTERNACIONAL_DOENCA.NO_CID.Contains(txtCID.Text) : 0 == 0)
                                 //&& (tipo != 0 ? tbs434.CO_TIPO == tipo : 0 == 0)
                                 && (ddlSituacao.SelectedValue != "T" ? tbs434.FL_STATUS == ddlSituacao.SelectedValue : 0 == 0)
                                 select new
                                 {
                                     tbs434.ID_PROTO_CID,
                                     tbs434.NO_PROTO_CID,
                                     //TIPO = tbs434.CO_TIPO == 1 ? "Consulta" : (tbs434.CO_TIPO == 2 ? "Exame" : (tbs434.CO_TIPO == 3 ? "Procedimento" : "Vacina")) ,
                                     tbs434.TB117_CODIGO_INTERNACIONAL_DOENCA.NO_CID,
                                     SITUACAO = tbs434.FL_STATUS == "A" ? "Ativo" : "Inativo",
                                 }).OrderBy(c => c.NO_PROTO_CID);

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
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_PROTO_CID"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion
    }
}
