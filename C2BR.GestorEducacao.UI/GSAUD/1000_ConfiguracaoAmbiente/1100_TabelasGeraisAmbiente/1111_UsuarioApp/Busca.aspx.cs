//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: 
// SUBMÓDULO:
// OBJETIVO: Cadastro de usuário de acesso ao aplicativo 
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
// 03/03/14 | Débora Lohane              | Criação da funcionalidade para busca Regiões

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
namespace C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1111_UsuarioApp
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
            if (!IsPostBack)
            {
                ddlUsuario.Items.Insert(0, new ListItem("Todos", "0"));
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_USUAR_APP" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "no_tipo",
                HeaderText = "TIPO"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NOME",
                HeaderText = "NOME"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "usuario",
                HeaderText = "USUÁRIO"
            });

             CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_SITU",
                HeaderText = "SITUAÇÃO"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coUser = (!string.IsNullOrEmpty(ddlUsuario.SelectedValue) ? int.Parse(ddlUsuario.SelectedValue) : 0);
            string tipo = ddlTipo.SelectedValue;
            string situa = ddlSitu.SelectedValue;
            var resultado = (from tbs384 in TBS384_USUAR_APP.RetornaTodosRegistros()
                             where (!string.IsNullOrEmpty(txtLogin.Text) ? tbs384.DE_LOGIN.Contains(txtLogin.Text) : 0 == 0)
                             && (tipo != "0" ? tbs384.CO_TIPO == tipo : 0 == 0)
                                 //Se for gestor ou colaborador, filtra na coluna de colaboradores, 
                             //mas apenas se for diferente de TODOS em tipo e em usuário
                             && (tipo != "0" ? (coUser != 0 ? (tbs384.CO_TIPO == "G" || tbs384.CO_TIPO == "C" ? tbs384.CO_COL == coUser :
                                 //Se for responsável, filtra na coluna de responsáveis
                             (tbs384.CO_TIPO == "R" ? tbs384.CO_RESP == coUser :
                             tbs384.CO_ALU == coUser)) : 0 == 0) : 0 == 0)
                             && (situa != "0" ? tbs384.CO_SITUA == situa : 0 == 0)
                             select new saida
                             {
                                 ID_USUAR_APP = tbs384.ID_USUAR_APP,
                                 co_tipo = tbs384.CO_TIPO,
                                 usuario = tbs384.DE_LOGIN,
                                 NOME = tbs384.NM_USUAR,
                                 CO_SITU = tbs384.CO_SITUA,
                             }).ToList();

            resultado = resultado.OrderBy(W => W.no_tipo).ThenBy(w => w.NOME).ToList();

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        public class saida
        {
            public int ID_USUAR_APP { get; set; }
            public string co_tipo { get; set; }
            public string no_tipo 
            {
                get
                {
                    switch (this.co_tipo)
                    {
                        case "G":
                            return "Gestor";
                        case "C":
                            return "Colaborador";
                        case "R":
                            return "Responsável";
                        case "P":
                            return "Paciente";
                        default:
                            return " - ";
                    }
                }
            }
            public string usuario { get; set; }
            public string CO_SITU { get; set; }
            public string NO_SITU
            {
                get
                {
                    switch (this.CO_SITU)
                    {
                        case "A":
                            return "Ativo";
                        case "I":
                            return "Inativo";
                        case "S":
                            return "Suspenso";
                        default:
                            return " - ";
                    }
                }
            }
            public string NOME { get; set; }
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_USUAR_APP"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        protected void ddlTipo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.ddlTipo.SelectedValue)
            {
                case "G":
                case "C":
                    AuxiliCarregamentos.CarregaProfissionaisSaude(ddlUsuario, 0, true, "0", true, 0, false); 
                    break;
                case "R":
                    AuxiliCarregamentos.CarregaResponsaveis(ddlUsuario, LoginAuxili.ORG_CODIGO_ORGAO, true);
                    break;
                case "P":
                    AuxiliCarregamentos.CarregaPacientes(ddlUsuario, 0, true, true);
                    break;
                case "0":
                    ddlUsuario.Items.Clear();
                    ddlUsuario.Items.Insert(0, new ListItem("Todos", "0"));
                    break;
            }
        }

        #endregion
    }
}