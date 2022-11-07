//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO OPERACIONAL DE COLABORADORES
// OBJETIVO: REGISTRO DE OCORRÊNCIAS DE SAÚDE (ATESTADOS MÉDICOS) DO COLABORADOR
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
namespace C2BR.GestorEducacao.UI.GSAUD._7000_ControleOperRH._7100_CtrlPontoFuncional._7130_ManuPontoColabor
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
            CarregaUnidades();
            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
            CarregaColaboradores();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "HR_FREQ", "DT_FREQ", "CO_COL", "CO_EMP", "CO_SEQ_FREQ" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_COL",
                HeaderText = "Colaborador"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "PRESENCA",
                HeaderText = "Tipo Presença"
            });

            BoundField bfRealizado = new BoundField();
            bfRealizado.DataField = "DT_FREQ";
            bfRealizado.HeaderText = "Data";
            bfRealizado.ItemStyle.CssClass = "codCol";
            bfRealizado.DataFormatString = "{0:d}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRealizado);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "HR_FREQ",
                HeaderText = "Hora",
                DataFormatString = "{0:00:00}"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            if (ddlColaborador.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Selecione co Colaborador");
                return;
            }

            int coCol = int.Parse(ddlColaborador.SelectedValue);

            DateTime dataInicio;
            DateTime dataFim;
            if (!DateTime.TryParse(txtDataPeriodoIni.Text, out dataInicio))
            {
                AuxiliPagina.EnvioMensagemErro(this, "A data de início informada é inválida.");
                return;
            }
            if (!DateTime.TryParse(txtDataPeriodoFim.Text, out dataFim))
            {
                AuxiliPagina.EnvioMensagemErro(this, "A data final informada é inválida.");
                return;
            }

            var resultado = (from tb199 in TB199_FREQ_FUNC.RetornaTodosRegistros()
                             where tb199.CO_COL == coCol && tb199.STATUS == "A"
                             && tb199.DT_FREQ >= dataInicio && tb199.DT_FREQ <= dataFim
                             orderby tb199.DT_FREQ, tb199.CO_SEQ_FREQ
                             select new
                             {
                                 tb199.CO_COL,
                                 tb199.CO_EMP,
                                 tb199.CO_SEQ_FREQ,
                                 tb199.TB03_COLABOR.NO_COL,
                                 tb199.DT_FREQ,
                                 tb199.HR_FREQ,
                                 PRESENCA = tb199.TP_FREQ == "E" ? "Entrada" : tb199.TP_FREQ == "S" ? "Saída" : "Falta"
                             });

            /*
            var resultado2 = from res in resultado
                             select new
                             {
                                 HR_FREQ = res.HR_FREQ.ToString().PadLeft(4, '0').Insert(2, ":"),
                                 res.CO_COL, res.CO_EMP, res.CO_SEQ_FREQ, res.NO_COL, res.DT_FREQ, res.PRESENCA
                             };*/

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>("data", "DT_FREQ"));
            queryStringKeys.Add(new KeyValuePair<string, string>("col", "CO_COL"));
            queryStringKeys.Add(new KeyValuePair<string, string>("emp", "CO_EMP"));
            queryStringKeys.Add(new KeyValuePair<string, string>("seq", "CO_SEQ_FREQ"));
            queryStringKeys.Add(new KeyValuePair<string, string>("hora", "HR_FREQ"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

        //====> Método que carrega o DropDown de Unidades Escolares
        private void CarregaUnidades()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();
        }

        //====> Método que carrega o DropDown de Colaboradores
        private void CarregaColaboradores()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlColaborador.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(coEmp)
                                         select new { tb03.NO_COL, tb03.CO_COL }).OrderBy(c => c.NO_COL);

            ddlColaborador.DataTextField = "NO_COL";
            ddlColaborador.DataValueField = "CO_COL";
            ddlColaborador.DataBind();

            ddlColaborador.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaColaboradores();
        }
    }
}