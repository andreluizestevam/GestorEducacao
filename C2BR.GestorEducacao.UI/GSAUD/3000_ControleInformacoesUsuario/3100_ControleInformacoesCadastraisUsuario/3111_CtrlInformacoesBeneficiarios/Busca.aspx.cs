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
//             |                            |  3700_CtrlInformacoesResponsaveis\3710_CtrlInformacoesCadastraisResponsaveis
// ------------+----------------------------+-------------------------------------
// 01/12/2021  |  Artur Benevenuto Coelho   | Copia da Tela \GSAUD\3000_ControleInformacoesUsuario\
//             |                            |  3100_ControleInformacoesCadastraisUsuario\3105_CtrlInformacoesResponsaveis
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

namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3111_CtrlInformacoesBeneficiarios
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
                DataField = "NO_ALU",
                HeaderText = "Nome Beneficiário"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NU_CPF_ALU",
                HeaderText = "CPF"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SEXO_ALU",
                HeaderText = "Sexo"
            });

            /*CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_FLAG_ALU_FUNC",
                HeaderText = "FUNC"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TP_DEF_ALU",
                HeaderText = "Deficiência"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "RENDA_FAMILIAR_ALU",
                HeaderText = "Renda"
            });*/

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SITU_ALU",
                HeaderText = "Situação"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            var resultado = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                             where (txtNome.Text != "" ? tb07.NO_ALU.Contains(txtNome.Text) : txtNome.Text == "")
                             && (txtCpf.Text != "" ? tb07.NU_CPF_ALU.Equals(txtCpf.Text.Replace(".", "").Replace("-", "")) : txtCpf.Text == "")
                             //--&& (ddlDeficiencia.SelectedValue != "T" ? tb07.TP_DEF_ALU.Equals(ddlDeficiencia.SelectedValue) : ddlDeficiencia.SelectedValue == "T")
                             && (ddlSituacao.SelectedValue != "T" ? tb07.CO_SITU_ALU.Equals(ddlSituacao.SelectedValue) : ddlSituacao.SelectedValue == "T")
                             /*&& (ddlCategoriaFuncional.SelectedValue != "T" ? tb07.CO_FLAG_ALU_FUNC.Equals(ddlCategoriaFuncional.SelectedValue) : ddlCategoriaFuncional.SelectedValue == "T")
                             && (ddlRenda.SelectedValue != "T" ? tb07.RENDA_FAMILIAR_ALU.Equals(ddlRenda.SelectedValue) : ddlRenda.SelectedValue == "T")
                             && tb07.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO*/
                             && tb07.CO_EMP == LoginAuxili.CO_EMP
                             select new
                             {
                                 tb07.NO_ALU,
                                 //--tb07.NU_NIS_ALU,
                                 tb07.DT_NASC_ALU,
                                 tb07.CO_ALU,
                                 CO_SITU_ALU = tb07.CO_SITU_ALU == "I" ? "Inativo" : "Ativo",
                                 NU_TELE_RESI_ALU = tb07.NU_TELE_RESI_ALU.Length > 8 ? tb07.NU_TELE_RESI_ALU.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                                 NU_CPF_ALU = tb07.NU_CPF_ALU.Length == 11 ? tb07.NU_CPF_ALU.Insert(3, ".").Insert(7, ".").Insert(11, "-") : "",
                                 //--CO_FLAG_ALU_FUNC = tb07.CO_FLAG_ALU_FUNC == "S" ? "Sim" : "Não",
                                 CO_SEXO_ALU = tb07.CO_SEXO_ALU == "M" ? "Masculino" : "Feminino",
                                 /*TP_DEF_ALU = tb07.TP_DEF_ALU == "A" ? "Auditivo" : tb07.TP_DEF_ALU == "V" ? "Visual" : tb07.TP_DEF_ALU == "F" ? "Física" :
                                 tb07.TP_DEF_ALU == "M" ? "Mental" : tb07.TP_DEF_ALU == "I" ? "Múltiplas" : tb07.TP_DEF_ALU == "O" ? "Outros" : "Nenhuma",
                                 RENDA_FAMILIAR_ALU = tb07.RENDA_FAMILIAR_ALU == "1" ? "1 a 3 SM" : tb07.RENDA_FAMILIAR_ALU == "2" ? "3 a 3 SM" :
                                 tb07.RENDA_FAMILIAR_ALU == "3" ? "+5 SM" : "Sem Renda"*/
                             }).OrderBy(r => r.NO_ALU);

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