////=============================================================================
//// EMPRESA: C2BR Soluções em Tecnologia
//// SISTEMA: PS - Portal 
//// PROGRAMADOR: Equipe Desenvolvimento
//// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
//// SUBMÓDULO: CADASTRO E INFORMAÇÕES DE ALUNOS
//// OBJETIVO: CADASTRAMENTO DE ALUNOS
//// DATA DE CRIAÇÃO: 
////-----------------------------------------------------------------------------
////                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
////-----------------------------------------------------------------------------
////  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
//// ----------+----------------------------+-------------------------------------
//// 06/03/2013|    Julio Gleisson          | Copia da tela 3611_CadastramentoAlunos
////           |                            | 
////           |                            | 

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

namespace C2BR.GestorEducacao.UI.GSAUD._7000_ControleOperRH._7950_CtrlCadastralParceiros._7952_OcorrenciasParceiros
{
    public partial class Busca : System.Web.UI.Page
    {
        public PadraoBuscas CurrentPadraoBuscas { get { return ((PadraoBuscas)this.Master); } }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IniPeri.Text = DateTime.Now.AddMonths(-1).ToShortDateString();
                FimPeri.Text = DateTime.Now.ToShortDateString();
                CarregarTiposOcorrenciaParceiros();
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
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_OCORR" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_PARCE",
                HeaderText = "Parceiro"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DATA",
                HeaderText = "Data"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TITULO",
                HeaderText = "Titulo"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TIPO",
                HeaderText = "Tipo"
            });

        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            DateTime dataInicio = new DateTime();
            DateTime dataFim = new DateTime();

            DateTime.TryParse(IniPeri.Text, out dataInicio);
            DateTime.TryParse(FimPeri.Text, out dataFim);
            dataFim = dataFim.AddDays(1);
            string tipo = ddlTipo.SelectedValue;

            var resultado = (from tb422 in TB422_REGIS_OCORR_PARCE.RetornaTodosRegistros().Where(o => o.DT_OCORR >= dataInicio && o.DT_OCORR <= dataFim)
                             join tb421 in TB421_PARCEIROS.RetornaTodosRegistros() on tb422.TB421_PARCEIROS.CO_PARCE equals tb421.CO_PARCE
                             join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tb422.TB03_COLABOR.CO_COL equals tb03.CO_COL
                             where tb422.TB421_PARCEIROS.DE_RAZSOC_PARCE.Contains(txtParceiro.Text)
                             && (tipo != "0" ? tb422.TP_OCORR == tipo : 0 == 0)
                             select new listaOcorrencias
                             {
                                 ID_OCORR = tb422.CO_OCORR,
                                 DATA = tb422.DT_OCORR,
                                 NO_PARCE = tb422.TB421_PARCEIROS.DE_RAZSOC_PARCE,
                                 TIPO = tb422.TP_OCORR,
                                 TITULO = tb422.NO_OCORR,
                                 OCORRENCIA = tb422.TX_OCORR,
                                 RESPONSAVEL = tb03.NO_APEL_COL,
                                 ACAO = tb422.TX_ACAO_OCORR,
                                 IP = tb422.IP_CADAS_OCORR
                             }).OrderBy(a => a.NO_PARCE);

            if (resultado != null && resultado.Count() > 0)
                CurrentPadraoBuscas.GridBusca.DataSource = resultado;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_OCORR"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion

        #region Carregadores


        private void CarregarTiposOcorrenciaParceiros()
        {
            AuxiliCarregamentos.CarregaTiposOcorrenciaParceiros(ddlTipo, true);
        }

        #endregion

        #region Classes
        /// <summary>
        /// Classe com os campos necessários para carregar a grid de buscas
        /// </summary>
        private class listaOcorrencias
        {
            public int ID_OCORR { get; set; }
            public DateTime DATA { get; set; }
            public string NO_PARCE { get; set; }
            public string RESPONSAVEL { get; set; }
            public string ACAO { get; set; }
            public string OCORRENCIA { get; set; }
            public string IP { get; set; }
            public string TITULO { get; set; }
            private string tipo;
            public string TIPO
            {
                get
                {
                    return AuxiliFormatoExibicao.RetornarTipoOcorrencia(tipo);
                }
                set
                {
                    tipo = value;
                }
            }
        }

        #endregion

    }
}