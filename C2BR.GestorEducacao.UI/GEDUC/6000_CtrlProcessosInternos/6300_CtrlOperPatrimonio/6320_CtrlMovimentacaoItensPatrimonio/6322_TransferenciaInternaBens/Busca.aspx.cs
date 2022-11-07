﻿//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE PATRIMÔNIO
// SUBMÓDULO: MOVIMENTAÇÃO DE ITENS DE PATRIMÔNIO
// OBJETIVO: TRANSFERÊNCIA INTERNA DE ITENS DE PATRIMÔNIO
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
namespace C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6300_CtrlOperPatrimonio.F6320_CtrlMovimentacaoItensPatrimonio.F6322_TransferenciaInternaBens
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
            if (IsPostBack) return;

            CarregaUnidade();
            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
            CarregaDepto();
            CarregaPatrimonio();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "IDE_HISTO_MOVIM_PATRI" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_PATR",
                HeaderText = "Patrimônio"
            });

             CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DeptoDestino",
                HeaderText = "Depto Destino"
            });

             CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
             {
                 DataField = "DeptoOrigem",
                 HeaderText = "Depto Origem"
             });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DT_MOVIM_PATRI",
                HeaderText = "Dt Transf",
                DataFormatString = "{0:d}"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP;
            int coDepto = ddlDepartamento.SelectedValue != "" ? int.Parse(ddlDepartamento.SelectedValue) : 0;
            decimal codPatr = ddlPatrimonio.SelectedValue != "" ? decimal.Parse(ddlPatrimonio.SelectedValue) : 0;

            var resultado = (from tb228 in TB228_PATRI_HISTO_MOVIM.RetornaTodosRegistros()
                            join tb14Dest in TB14_DEPTO.RetornaTodosRegistros() on tb228.CO_DEPTO_DESTI equals tb14Dest.CO_DEPTO
                            join tb14Orig in TB14_DEPTO.RetornaTodosRegistros() on tb228.CO_DEPTO_ORIGEM equals tb14Orig.CO_DEPTO
                            where tb228.TB212_ITENS_PATRIMONIO.CO_EMP == coEmp && coDepto != 0 ? tb228.CO_DEPTO_DESTI == coDepto : coDepto == 0
                            && codPatr != 0 ? tb228.TB212_ITENS_PATRIMONIO.COD_PATR == codPatr : codPatr == 0
                            select new
                            {
                                tb228.TB212_ITENS_PATRIMONIO.DE_PATR, DeptoOrigem = tb14Orig.NO_DEPTO, DeptoDestino = tb14Dest.NO_DEPTO,
                                tb228.DT_MOVIM_PATRI, tb228.IDE_HISTO_MOVIM_PATRI   
                            });

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "IDE_HISTO_MOVIM_PATRI"));
            
            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion
        
        #region Carregamento DropDown

//====> Método que carrega o DropDown de Patrimônios
        private void CarregaPatrimonio()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP;

            ddlPatrimonio.DataSource = (from tb212 in TB212_ITENS_PATRIMONIO.RetornaTodosRegistros()
                                        where tb212.CO_EMP == coEmp && tb212.CO_STATUS != "T"
                                        select new { tb212.DE_PATR, tb212.COD_PATR }).OrderBy( p => p.DE_PATR );

            ddlPatrimonio.DataTextField = "DE_PATR";
            ddlPatrimonio.DataValueField = "COD_PATR";
            ddlPatrimonio.DataBind();

            ddlPatrimonio.Items.Insert(0, new ListItem("Selecione", ""));
        }

//====> Método que carrega o DropDown de Departamentos
        private void CarregaDepto()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP;

            ddlDepartamento.DataSource = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                                          where tb14.TB25_EMPRESA.CO_EMP == coEmp
                                          select new { tb14.CO_DEPTO, tb14.NO_DEPTO }).OrderBy( d => d.NO_DEPTO );

            ddlDepartamento.DataTextField = "NO_DEPTO";
            ddlDepartamento.DataValueField = "CO_DEPTO";
            ddlDepartamento.DataBind();

            ddlDepartamento.Items.Insert(0, new ListItem("Selecione", ""));
        }

//====> Método que carrega o DropDown de Unidades Escolares
        private void CarregaUnidade()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP }).OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }
        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaPatrimonio();
            CarregaDepto();
        }
    }
}
