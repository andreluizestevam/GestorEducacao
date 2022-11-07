//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PS - Portal 
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CADASTRO E INFORMAÇÕES DE ALUNOS
// OBJETIVO: CADASTRAMENTO DE ALUNOS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 06/03/2013|    Julio Gleisson          | Copia da tela 3611_CadastramentoAlunos
//           |                            | 
//           |                            | 

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

namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3109_OcorrenciasPaciente
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
                CarregarUnidades();
                CarregarProfissionais();
                CarregarTiposOcorrencia();
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
                DataField = "DATA",
                HeaderText = "Data"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_ALU",
                HeaderText = "Paciente"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TIPO",
                HeaderText = "Tipo"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TITULO",
                HeaderText = "Título"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_COL",
                HeaderText = "Responsável"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "ACAO",
                HeaderText = "Ação"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            DateTime dataInicio = new DateTime();
            DateTime dataFim = new DateTime();

            DateTime.TryParse(IniPeri.Text, out dataInicio);
            DateTime.TryParse(FimPeri.Text, out dataFim);
            dataFim = dataFim.AddDays(1);
            int unidade = int.Parse(ddlUnidade.SelectedValue);
            int profissional = int.Parse(ddlProfissional.SelectedValue);
            string tipo = ddlTipo.SelectedValue;

            var resultado = (from tbs408 in TBS408_OCORR_PACIE.RetornaTodosRegistros().Where(o => o.DT_OCORR >= dataInicio && o.DT_OCORR <= dataFim)
                             join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs408.CO_ALU equals tb07.CO_ALU
                             join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs408.CO_COL_CADAS equals tb03.CO_COL
                             where tb07.NO_ALU.Contains(txtPaciente.Text)
                             && (unidade != 0 ? tbs408.CO_EMP_OCORR == unidade : 0 == 0)
                             && (profissional != 0 ? tbs408.CO_COL_CADAS == profissional : 0 == 0)
                             && (tipo != "0" ? tbs408.TP_OCORR == tipo : 0 == 0)
                             select new listaOcorrencias
                             {
                                 ID_OCORR = tbs408.ID_OCORR,
                                 DATA = tbs408.DT_OCORR,
                                 NO_ALU = !String.IsNullOrEmpty(tb07.NO_APE_ALU) ? tb07.NO_APE_ALU : tb07.NO_ALU,
                                 TIPO = tbs408.TP_OCORR,
                                 TITULO = tbs408.NO_OCORR,
                                 NO_COL = tb03.NO_APEL_COL,
                                 ACAO = tbs408.DE_ACAO_OCORR
                             }).OrderBy(a => a.NO_ALU);

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

        private void CarregarUnidades()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        private void CarregarProfissionais()
        {
            AuxiliCarregamentos.CarregaProfissionaisSaude(ddlProfissional, LoginAuxili.CO_EMP, true, "0");
        }

        private void CarregarTiposOcorrencia()
        {
            AuxiliCarregamentos.CarregaTiposOcorrencia(ddlTipo, true);
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
            public string NO_ALU { get; set; }
            public string NO_COL { get; set; }
            public string ACAO { get; set; }
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