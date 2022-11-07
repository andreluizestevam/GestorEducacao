//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: SOLICITAÇÃO DE ITENS DE SECRETARIA ESCOLAR
// OBJETIVO: REPASSE DE ITENS DE SOLICITAÇÕES.
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
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2101_SolicitacaoItensSecretaria.RepasseItensSolicitacao
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
            if (!Page.IsPostBack)
                CarregaUnidades();
        }

        protected void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_EMP", "CO_ALU", "CO_CUR", "CO_SOLI_ATEN", "TIPO" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DT_SOLI_ATEN",
                HeaderText = "Data",
                DataFormatString = "{0:dd/MM/yyyy}"

            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NU_DCTO_SOLIC",
                HeaderText = "Nº Solicitação"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NU_NIRE",
                HeaderText = "NIRE"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_ALU",
                HeaderText = "Aluno"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_TELE_CONT",
                HeaderText = "Telefone"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DT_PREV_ENTR",
                HeaderText = "Previsão",
                DataFormatString = "{0:dd/MM/yyyy}"
            });
        }

        protected void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            string strSituFinalizada = SituacaoItemSolicitacao.F.ToString();
            string strSituEmTransito = SituacaoItemSolicitacao.T.ToString();
            string strSituDisponivel = SituacaoItemSolicitacao.D.ToString();

            if (ddlTipo.SelectedValue == "E")
            {
                var resultado = (from vw65 in VW65_HIST_SOLIC.RetornaTodosRegistros()
                                where (vw65.CO_SITU_SOLI == strSituFinalizada && vw65.CO_EMP_ALU == LoginAuxili.CO_EMP) ||
                                (vw65.CO_SITU_SOLI == strSituDisponivel && vw65.UNID_ENTREGA == LoginAuxili.CO_EMP) ||
                                (!vw65.PENDENTE.Value && vw65.CO_SITU_SOLI == strSituEmTransito && vw65.UNID_ENTREGA == LoginAuxili.CO_EMP)
                                select new { vw65.CO_SOLI_ATEN }).Distinct();

                var resultado2 = (from tb64 in TB64_SOLIC_ATEND.RetornaTodosRegistros()
                                  where (coEmp != 0 ? tb64.TB25_EMPRESA1.CO_EMP == coEmp : coEmp == 0)
                                 join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb64.CO_ALU equals tb07.CO_ALU
                                 join result in resultado on tb64.CO_SOLI_ATEN equals result.CO_SOLI_ATEN
                                 select new
                                 {
                                    tb07.NU_NIRE, tb64.CO_CUR, tb64.CO_ALU, tb07.NO_ALU, tb64.CO_EMP, tb64.TB108_RESPONSAVEL.NO_RESP,
                                    tb64.CO_TELE_CONT, tb64.DT_SOLI_ATEN, tb64.DT_PREV_ENTR, tb64.CO_SOLI_ATEN, tb64.NU_DCTO_SOLIC, TIPO = "E"
                                 }).OrderByDescending( s => s.DT_SOLI_ATEN ).ThenBy( s => s.NU_DCTO_SOLIC );

                CurrentPadraoBuscas.GridBusca.DataSource = (resultado2.Count() > 0) ? resultado2 : null;
            }
            else
            {
//------------> Solicitações que possuem itens em trânsito e com unidade de entrega nessa unidade
                var resultado = (from tb64 in TB64_SOLIC_ATEND.RetornaTodosRegistros()
                                join tb65 in TB65_HIST_SOLICIT.RetornaTodosRegistros() on tb64.CO_SOLI_ATEN equals tb65.CO_SOLI_ATEN
                                where tb65.CO_SITU_SOLI == strSituEmTransito && tb65.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                select new { tb65.CO_SOLI_ATEN }).Distinct();

                var resultado2 = (from tb64 in TB64_SOLIC_ATEND.RetornaTodosRegistros()
                                 join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb64.CO_ALU equals tb07.CO_ALU
                                 where (coEmp != 0 ? tb64.CO_EMP_ALU == coEmp : coEmp == 0)
                                 from result in resultado
                                 where tb64.CO_SOLI_ATEN == result.CO_SOLI_ATEN                                 
                                 select new
                                 {
                                    tb64.CO_CUR, tb64.CO_ALU, tb07.NO_ALU, tb07.NU_NIRE, tb64.CO_EMP, tb64.TB108_RESPONSAVEL.NO_RESP,
                                    tb64.CO_TELE_CONT, tb64.DT_SOLI_ATEN, tb64.DT_PREV_ENTR, tb64.CO_SOLI_ATEN, tb64.NU_DCTO_SOLIC, TIPO = "R"
                                 }).OrderByDescending( s => s.DT_SOLI_ATEN ).ThenBy( s => s.NU_DCTO_SOLIC );

                CurrentPadraoBuscas.GridBusca.DataSource = (resultado2.Count() > 0) ? resultado2 : null;
            }
        }

        protected void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {             
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoEmp, "CO_EMP"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoAlu, "CO_ALU"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoCur, "CO_CUR"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_SOLI_ATEN"));
            queryStringKeys.Add(new KeyValuePair<string, string>("tipo", "TIPO"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Unidades Escolares
        private void CarregaUnidades()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP });

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.Items.Insert(0, new ListItem("Todas", ""));
        }
        #endregion

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipo.SelectedValue == "E")
                lblUnidade.InnerText = "Unidade de Destino";
            else
                lblUnidade.InnerText = "Unidade de Origem";

            CarregaUnidades();
        }
    }
}