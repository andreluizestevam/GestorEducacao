//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
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
// 14/03/2013| André Nobre Vinagre        | Corrigida a máscara do NIRE de busca.
//           |                            | 
//           |                            | 

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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3610_CtrlInformacoesCadastraisAlunos.F3611_CadastramentoAlunos
{
    public partial class Busca : System.Web.UI.Page
    {
        public PadraoBuscas CurrentPadraoBuscas { get { return ((PadraoBuscas)this.Master); } }
        private static Dictionary<string, string> tipoDef = AuxiliBaseApoio.chave(tipoDeficienciaAluno.ResourceManager, true);
        
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaDeficiencias();
                CarregaAnoOrigem();
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
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_ALU" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NU_NIRE",
                HeaderText = "NIRE"
            });

            BoundField bfRealizado = new BoundField();
            bfRealizado.DataField = "NU_NIS";
            bfRealizado.HeaderText = "NIS";
            bfRealizado.ItemStyle.CssClass = "codCol";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRealizado);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_ALU",
                HeaderText = "Nome"
            });

            BoundField bfRealizado1 = new BoundField();
            bfRealizado1.DataField = "DT_NASC_ALU";
            bfRealizado1.HeaderText = "Dt Nasc";
            bfRealizado1.ItemStyle.CssClass = "codCol";
            bfRealizado1.DataFormatString = "{0:d}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRealizado1);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TP_DEF",
                HeaderText = "Deficiência"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            if (txtNire.Text == "_________")
            {
                txtNire.Text = "";
            }
            
            int nuNire = 0;
            if(txtNire.Text != "" && !int.TryParse(txtNire.Text, out nuNire))
                return;
            DateTime dataInicio = DateTime.MinValue;
            DateTime dataFim = DateTime.MinValue;
            DateTime? dataVerifIni = null;
            DateTime? dataVerifFim = null;
            string deficiencia = ddlDeficiencia.SelectedValue;
            string anoOri = ddlAnoOri.SelectedValue;

            DateTime.TryParse(txtPeriodoDe.Text, out dataInicio);
            DateTime.TryParse(txtPeriodoAte.Text, out dataFim);

            dataVerifIni = dataInicio == DateTime.MinValue ? null : (DateTime?)dataInicio;
            dataVerifFim = dataFim == DateTime.MinValue ? null : (DateTime?)dataFim;

            var resultado = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                             where tb07.TB25_EMPRESA1.CO_EMP == LoginAuxili.CO_EMP
                             && (nuNire != 0 ? tb07.NU_NIRE == nuNire : nuNire == 0)
                             && (txtNome.Text != "" ? tb07.NO_ALU.Contains(txtNome.Text) :0==0)
                             && (txtCpf.Text != "" ? tb07.NU_CPF_ALU.Equals(txtCpf.Text.Replace(".", "").Replace("-", "")) : txtCpf.Text == "")
                             && (deficiencia == "" ? 0 == 0 : (deficiencia == "-1" ? (tb07.TP_DEF != "" && tb07.TP_DEF != "N") : tb07.TP_DEF == deficiencia))
                             && (dataVerifIni != null ? tb07.DT_NASC_ALU.Value >= dataVerifIni : dataVerifIni == null)
                             && (dataVerifFim != null ? tb07.DT_NASC_ALU.Value <= dataVerifFim : dataVerifFim == null)
                             && (anoOri == "" || anoOri == "-1" ? 0 == 0 : tb07.CO_ANO_ORI == anoOri)
                             select new listaAlunos
                             {
                                 CO_ALU = tb07.CO_ALU,
                                 NO_ALU = tb07.NO_ALU,
                                 NU_NIRE = tb07.NU_NIRE,
                                 NU_NIS = tb07.NU_NIS,
                                 DT_NASC_ALU = tb07.DT_NASC_ALU,
                                 siglaDef = tb07.TP_DEF
                             }).OrderBy( a => a.NO_ALU );

            if(resultado != null && resultado.Count() > 0)
                CurrentPadraoBuscas.GridBusca.DataSource = resultado;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_ALU"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion

        #region Carregadores
        /// <summary>
        /// Carrega todos os tipos de deficiências dos alunos
        /// </summary>
        private void CarregaDeficiencias()
        {
            ddlDeficiencia.Items.Clear();
            ddlDeficiencia.Items.AddRange(AuxiliBaseApoio.BaseApoioDDL(tipoDeficienciaAluno.ResourceManager, true, true));
        }
        /// <summary>
        /// Carrega os anos de origem da tabela de alunos do sistema
        /// </summary>
        private void CarregaAnoOrigem()
        {
            ddlAnoOri.Items.Clear();
            ddlAnoOri.Items.AddRange(AuxiliBaseApoio.AnosOrigemDDL(true, true));
        }
        #endregion

        #region Classes
        /// <summary>
        /// Classe com os campos necessários para carregar a grid de buscas
        /// </summary>
        private class listaAlunos
        {
            public int CO_ALU { get; set; } 
            public string NO_ALU { get; set; } 
            public int NU_NIRE { get; set;}
            public decimal? NU_NIS { get; set; }
            public DateTime? DT_NASC_ALU { get; set; }
            public string siglaDef {
                set
                {
                    if (value.Trim() != "")
                        this.TP_DEF = tipoDef[value];
                    else
                        this.TP_DEF = "";
                }
            }
            public string TP_DEF { get; set; }
        }

        #endregion
    }
}
