﻿//=============================================================================
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

namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3101_CadastramentoMultiEndUsuario
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

            CarregaUnidades();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_ALUNO_ENDERECO" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_ALU",
                HeaderText = "Nome do Aluno"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_TIPO_ENDERECO",
                HeaderText = "Tipo de Endereço"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "FL_PRINCIPAL",
                HeaderText = "Principal?"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SITUACAO",
                HeaderText = "Situação"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            var resultado = from tb241 in TB241_ALUNO_ENDERECO.RetornaTodosRegistros()
                            where tb241.TB07_ALUNO.TB25_EMPRESA1.CO_EMP == coEmp
                            && (txtNome.Text != "" ? tb241.TB07_ALUNO.NO_ALU.Contains(txtNome.Text) : txtNome.Text == "")
                            select new
                            {
                                tb241.TB07_ALUNO.NO_ALU,
                                tb241.TB238_TIPO_ENDERECO.NM_TIPO_ENDERECO,
                                tb241.ID_ALUNO_ENDERECO,
                                FL_PRINCIPAL = tb241.FL_PRINCIPAL == true ? "Sim" : "Não",
                                CO_SITUACAO = tb241.CO_SITUACAO.ToUpper().Equals("A") ? "Ativo" : "Inativo"
                            };

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_ALUNO_ENDERECO"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

        //====> Método que carrega o DropDown de Unidades Escolares
        private void CarregaUnidades()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP });

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }
        #endregion
      
    }
}