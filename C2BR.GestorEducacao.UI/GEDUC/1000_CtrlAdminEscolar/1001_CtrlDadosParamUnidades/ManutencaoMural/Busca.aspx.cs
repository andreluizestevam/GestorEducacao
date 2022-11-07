﻿//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO OPERACIONAL DE COLABORADORES
// OBJETIVO: CADASTRAMENTO DE TIPOS DE BENEFÍCIOS INSTITUCIONAIS A COLABORADORES
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1001_CtrlDadosParamUnidades.ManutencaoMural
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
                txtInstituicao.Text = LoginAuxili.ORG_NOME_ORGAO;
                CarregaUnidades();
            }
        } 

        private void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_MURAL" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "ORG_NOME_ORGAO",
                HeaderText = "Instituição"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TP_MURAL",
                HeaderText = "Tipo"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_TITUL_MURAL",
                HeaderText = "Título"
            });
        }        

        private void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView() 
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            var resultado = (from tb301 in TB301_MURAL.RetornaTodosRegistros()
                             where (coEmp != 0 ? tb301.TB25_EMPRESA.CO_EMP.Equals(coEmp) : coEmp == 0)
                             && tb301.TB000_INSTITUICAO.ORG_CODIGO_ORGAO.Equals(LoginAuxili.ORG_CODIGO_ORGAO)
                             select new 
                             {
                                tb301.TB000_INSTITUICAO.ORG_NOME_ORGAO, tb301.NO_TITUL_MURAL, tb301.ID_MURAL,
                                TP_MURAL = tb301.TP_MURAL == "I" ? "Instituição de Ensino" : tb301.TP_MURAL == "U" ? "Unidade Escolar" : "Turma"
                             }).Distinct().OrderBy(c => c.ORG_NOME_ORGAO).ThenBy(c => c.TP_MURAL);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        private void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_MURAL"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

//====> Método que carrega o dropdown de Unidades
        private void CarregaUnidades() 
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                     select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP }).OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.Items.Insert(0, new ListItem("", ""));
        }
        #endregion
    }
}
