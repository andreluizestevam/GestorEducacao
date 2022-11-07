//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CADASTRO E INFORMAÇÕES DE PAIS OU RESPONSÁVEIS 
// OBJETIVO: CADASTRAMENTO DE PAIS OU RESPONSÁVEIS DE ALUNOS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA       |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ------------+----------------------------+-------------------------------------
// 09/03/2012  |  Julio Gleisson Rodrigues  | Copia da Tela \GEDUC\3000_CtrlOperacionalPedagogico\
//                                          |  3700_CtrlInformacoesResponsaveis\3710_CtrlInformacoesCadastraisResponsaveis
//
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

namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3105_CtrlInformacoesResponsaveis
{
    public partial class CtrlInformacoesResponsaveis : System.Web.UI.Page
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
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_RESP" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_RESP",
                HeaderText = "Nome Responsável"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NU_CPF_RESP",
                HeaderText = "CPF"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SEXO_RESP",
                HeaderText = "Sexo"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_FLAG_RESP_FUNC",
                HeaderText = "FUNC"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TP_DEF_RESP",
                HeaderText = "Deficiência"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "RENDA_FAMILIAR_RESP",
                HeaderText = "Renda"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SITU_RESP",
                HeaderText = "Situação"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            var resultado = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                             where (txtNome.Text != "" ? tb108.NO_RESP.Contains(txtNome.Text) : txtNome.Text == "")
                             && (txtCpf.Text != "" ? tb108.NU_CPF_RESP.Equals(txtCpf.Text.Replace(".", "").Replace("-", "")) : txtCpf.Text == "")
                             && (ddlDeficiencia.SelectedValue != "T" ? tb108.TP_DEF_RESP.Equals(ddlDeficiencia.SelectedValue) : ddlDeficiencia.SelectedValue == "T")
                             && (ddlSituacao.SelectedValue != "T" ? tb108.CO_SITU_RESP.Equals(ddlSituacao.SelectedValue) : ddlSituacao.SelectedValue == "T")
                             && (ddlCategoriaFuncional.SelectedValue != "T" ? tb108.CO_FLAG_RESP_FUNC.Equals(ddlCategoriaFuncional.SelectedValue) : ddlCategoriaFuncional.SelectedValue == "T")
                             && (ddlRenda.SelectedValue != "T" ? tb108.RENDA_FAMILIAR_RESP.Equals(ddlRenda.SelectedValue) : ddlRenda.SelectedValue == "T")
                             && tb108.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                             select new
                             {
                                 tb108.NO_RESP,
                                 tb108.NU_NIS_RESP,
                                 tb108.DT_NASC_RESP,
                                 tb108.CO_RESP,
                                 CO_SITU_RESP = tb108.CO_SITU_RESP == "I" ? "Inativo" : "Ativo",
                                 NU_TELE_RESI_RESP = tb108.NU_TELE_RESI_RESP.Length > 8 ? tb108.NU_TELE_RESI_RESP.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                                 NU_CPF_RESP = tb108.NU_CPF_RESP.Length == 11 ? tb108.NU_CPF_RESP.Insert(3, ".").Insert(7, ".").Insert(11, "-") : "",
                                 CO_FLAG_RESP_FUNC = tb108.CO_FLAG_RESP_FUNC == "S" ? "Sim" : "Não",
                                 CO_SEXO_RESP = tb108.CO_SEXO_RESP == "M" ? "Masculino" : "Feminino",
                                 TP_DEF_RESP = tb108.TP_DEF_RESP == "A" ? "Auditivo" : tb108.TP_DEF_RESP == "V" ? "Visual" : tb108.TP_DEF_RESP == "F" ? "Física" :
                                 tb108.TP_DEF_RESP == "M" ? "Mental" : tb108.TP_DEF_RESP == "I" ? "Múltiplas" : tb108.TP_DEF_RESP == "O" ? "Outros" : "Nenhuma",
                                 RENDA_FAMILIAR_RESP = tb108.RENDA_FAMILIAR_RESP == "1" ? "1 a 3 SM" : tb108.RENDA_FAMILIAR_RESP == "2" ? "3 a 3 SM" :
                                 tb108.RENDA_FAMILIAR_RESP == "3" ? "+5 SM" : "Sem Renda"
                             }).OrderBy(r => r.NO_RESP);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }
       
        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_RESP"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion
    }
}